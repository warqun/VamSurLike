using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

/// <summary>
/// 입력 조건에 맞는 Vector값 리턴. 이때 x-z좌표로 z가 정면을 기준으로 된다.
/// </summary>
public class SkillSpawnRotationType
{
    // x - z 좌표: z: 위 x 왼
    // 거리가 1인 좌표계를 통해서 현재 정면 기준의 데이터를 출력.
    public enum SpawnRotationType { Front, Rotated, Random, Directional, CameraDirectional, RandomRoation, DuoRandomRoation }
    public SpawnRotationType rotationType;

    // 램덤에서 최대 회전값.
    float limitRandomRotationRange; // (-x < a < x)각 안에서.
    // 입력 값.
    float inputValue;

    // 타켓 위치에따른 생성.
    Vector3 cameraVector = Vector3.forward;

    // 기준 정면 
    Vector3 originalVector = new Vector3(0, 0, 1);
    // 회전 축.
    Vector3 rotationAxis = new Vector3(0, 1, 0);


    public void Set()
    {
        // rotationType
        // limitRandomRotationRange
        // inputValue
        // targetVector
    }
    public List<(Vector3,Vector3)> SkillSpawnRotationTypeReturn(int spawnCount)
    {
        List<(Vector3, Vector3)> result = new List<(Vector3, Vector3)>();
        switch (rotationType)
        {
            case SpawnRotationType.Front:
            default:
                {
                    ( Vector3, Vector3) keyValuePair = (new Vector3(0, 0, 1), new Vector3(0, 0, 1));
                    result.Add(keyValuePair);
                    return result;
                }
            // 다수의 경우 범위각도가 inputValue만큼 진행.
            case SpawnRotationType.Rotated:
                {
                    if (spawnCount == 0)
                        return result;

                    // 나누어 안떨어지느 값에 대해서
                    int remainValue = (int)(2 * inputValue) % spawnCount;

                    // 단위 회전 값.
                    float eachRotationRange = (2 * inputValue - remainValue) / spawnCount;

                    // 회전 적용
                    float sumRotation = (inputValue) - remainValue / 2;
                    // -inputValue < x < inputValue;
                    for (int i = 0; i < spawnCount; i++)
                    {
                        Vector3 rotatedVector = Quaternion.AngleAxis(sumRotation, rotationAxis) * originalVector;
                        (Vector3, Vector3) keyValuePair = (rotatedVector, rotatedVector);
                        result.Add(keyValuePair);
                        sumRotation -= eachRotationRange;
                    }
                    return result;
                }
                // 무조건 정면은 하나
            case SpawnRotationType.Directional:
                {
                    if (inputValue == 0)
                        return result;

                    float eachRotationRange = (360) / inputValue;
                    float sumRotation = 0;
                    for (int i = 0; i < inputValue; i++)
                    {
                        Vector3 rotatedVector = Quaternion.AngleAxis(sumRotation, rotationAxis) * originalVector;
                        (Vector3, Vector3) keyValuePair = (rotatedVector, rotatedVector);
                        result.Add(keyValuePair);
                        sumRotation += eachRotationRange;
                    }
                    return result;
                }
                // 정면은 하나
            case SpawnRotationType.CameraDirectional:
                {
                    if (inputValue == 0)
                        return result;

                    float eachRotationRange = (360 ) / inputValue;
                    float sumRotation = 0;
                    for (int i = 0; i < inputValue; i++)
                    {
                        Vector3 rotatedVector = Quaternion.AngleAxis(sumRotation, rotationAxis) * cameraVector;
                        (Vector3, Vector3) keyValuePair = (rotatedVector, rotatedVector);
                        result.Add(keyValuePair);
                        sumRotation += eachRotationRange;
                    }
                    return result;
                }
                // 한계 수치 없음. 360
            case SpawnRotationType.RandomRoation:
                {
                    if (spawnCount == 0)
                        return result;
                    float eachRotationRange = 360 / spawnCount;
                    for (int i = 0; i < spawnCount; i++)
                    {
                        float randomRotation = Random.Range(-180, 180);

                        Vector3 rotatedVector = Quaternion.AngleAxis(randomRotation, rotationAxis) * originalVector;
                        (Vector3, Vector3) keyValuePair = (rotatedVector, rotatedVector);
                        result.Add(keyValuePair);
                    }
                    return result;
                }

                // 램덤 범위 360
                // vec a, vec b (a,b) a: 전진할 벡터, b: 방향 벡터.
            case SpawnRotationType.DuoRandomRoation:
                {
                    if (inputValue == 0)
                        return result;

                    for (int i = 0; i < inputValue; i++)
                    {
                        // 방향
                        float randomRotation = Random.Range(-360, 360);
                        Vector3 rotatedVector = Quaternion.AngleAxis(randomRotation, rotationAxis) * originalVector;

                        // 바로보고 있는 방향
                        float randomRotation2 = Random.Range(-360, 360);
                        Vector3 rotatedVector2 = Quaternion.AngleAxis(randomRotation, rotationAxis) * rotatedVector;

                        (Vector3, Vector3) keyValuePair = (rotatedVector, rotatedVector2);
                        result.Add(keyValuePair);
                    }
                    return result;
                }
        }
    }
}