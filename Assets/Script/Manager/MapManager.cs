using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �ϳ��� ū ���� ������ �ش� �ʿ��� �ִ� ��ġ�� ���� Ÿ�� ��ġ.
/// </summary>
public class MapManager : ManagerBase
{
    public static MapManager instance;

    public int chunkSize = 20; // ûũ ũ�� (16x16 ���)
    public int renderDistance = 3; // �ε��� ûũ �Ÿ�
    public GameObject tilePrefab; // Ÿ�� ������

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
    // ������ �������� ���� ûũ Ȱ��ȭ
    void LoadChunk(Vector2Int coord)
    {
        GameObject chunk = new GameObject($"Chunk_{coord.x}_{coord.y}");
        chunk.transform.position = new Vector3(coord.x * chunkSize, 0, coord.y * chunkSize);
        chunk.transform.parent = transform;

        // tilePrefab ����
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

    // ������ �������� ��� ûũ�� ��Ȱ��ȭ
    void UnloadChunk(Vector2Int coord)
    {
        if (loadedChunks.ContainsKey(coord))
        {
            Destroy(loadedChunks[coord]);
            loadedChunks.Remove(coord);
        }
    }
}
