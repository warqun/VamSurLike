using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// TODO
/// tilePrefab의 개수 증가, 무작위 생성 또는 특정 맵의 무한 반복 기능 추가.
/// => 타일 맵 제작.
/// <summary>
/// 하나의 큰 맵이 있을때 해당 맵에서 있는 위치에 따라 타일 배치.
/// </summary>
public class MapManager : ManagerBase
{
    public static MapManager instance;

    public int chunkSize = 20; // 청크 크기 (16x16 블록)
    public int renderDistance = 3; // 로드할 청크 거리
    public GameObject tilePrefab; // 타일 프리팹

    private Dictionary<Vector2Int, GameObject> loadedChunks = new Dictionary<Vector2Int, GameObject>();
    private void Awake()
    {
        instance = this;
    }
    public override void FrameUpdate()
    {
        if (GameBase.gameBase == null)
            return;
        if (GameBase.gameBase.player == null)
            return;
        if (tilePrefab == null)
            return;
        Transform player = GameBase.gameBase.player.transform;
        Vector2Int playerChunk = new Vector2Int(
            Mathf.FloorToInt(player.position.x / chunkSize),
            Mathf.FloorToInt(player.position.z / chunkSize)
        );

        HashSet<Vector2Int> activeChunks = new HashSet<Vector2Int>();

        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int z = -renderDistance; z <= renderDistance; z++)
            {
                Vector2Int chunkCoord = new Vector2Int(playerChunk.x + x, playerChunk.y + z);
                activeChunks.Add(chunkCoord);
                if (!loadedChunks.ContainsKey(chunkCoord))
                {
                    LoadChunk(chunkCoord);
                }
            }
        }

        List<Vector2Int> chunksToRemove = new List<Vector2Int>();
        foreach (var chunk in loadedChunks.Keys)
        {
            if (!activeChunks.Contains(chunk))
            {
                chunksToRemove.Add(chunk);
            }
        }

        foreach (var chunk in chunksToRemove)
        {
            UnloadChunk(chunk);
        }
    }
    // 유저가 범위에서 들어온 청크 활성화
    void LoadChunk(Vector2Int coord)
    {
        GameObject chunk = new GameObject($"Chunk_{coord.x}_{coord.y}");
        chunk.transform.position = new Vector3(coord.x * chunkSize, 0, coord.y * chunkSize);
        chunk.transform.parent = transform;

        // tilePrefab 길이
        float width = 10;
        for (int x = 0; x < chunkSize/ 10; x++)
        {
            for (int z = 0; z < chunkSize/ 10; z++)
            {
                Vector3 tilePosition = new Vector3(coord.x * chunkSize + x * width, 0, coord.y * chunkSize + z* width);
                Instantiate(tilePrefab, tilePosition, Quaternion.identity, chunk.transform);
            }
        }

        loadedChunks[coord] = chunk;
    }

    // 유저가 범위에서 벗어난 청크는 비활성화
    void UnloadChunk(Vector2Int coord)
    {
        if (loadedChunks.ContainsKey(coord))
        {
            Destroy(loadedChunks[coord]);
            loadedChunks.Remove(coord);
        }
    }
}
