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
    public enum SpawnRotationType { Front, Rotated, Random, Directional, CameraDirectional , RandomRoation, DuoRandomRoation }
    public SpawnRotationType rotationType;

    // 램덤에서 최대 회전값.
    float limitRandomRotationRange; // (-x < a < x)각 안에서.
    // 입력 값.
    float inputValue;
    // 스킬 개수
    public int spawnCount;

    // 타켓 위치에따른 생성.
    Vector3 targetVector = Vector3.zero;

    // 기준 정면 
    Vector3 originalVector = new Vector3(0, 0, 1);
    // 회전 축.
    Vector3 rotationAxis = new Vector3(0, 1, 0);

    public void Set()
    {
        // rotationType
        // limitRandomRotationRange
        // inputValue
        // spawnCount
        // targetVector
    }
    public List<Vector3> SkillSpawnRotationTypeReturn()
    {
        List<Vector3> result = new List<Vector3>();
        switch (rotationType)
        {
            case SpawnRotationType.Front:
                {
                    result.Add(new Vector3(0, 0, 1));
                    return result;
                }
            case SpawnRotationType.Rotated:
                {
                    if (spawnCount == 0)
                        return result;

                    // 나누어 안떨어지느 값에 대해서
                    int remainValue = (int)(2 * inputValue) % spawnCount;

                    // 단위 회전 값.
                    float eachRotationRange = (2*inputValue- remainValue) / spawnCount;

                    // 회전 적용
                    float sumRotation = (inputValue) - remainValue/2;
                    // -inputValue < x < inputValue;
                    for (int i = 0; i < spawnCount; i++)
                    {
                        Vector3 rotatedVector = Quaternion.AngleAxis(sumRotation, rotationAxis) * originalVector;
                        result.Add(rotatedVector);
                        sumRotation -= eachRotationRange;
                    }
                    return result;
                }
            case SpawnRotationType.Directional:
                {
                    if(inputValue == 0)
                        return result;

                    // 나누어 안떨어지느 값에 대해서
                    int remainValue = 360 % (int)inputValue;

                    float eachRotationRange = (360-remainValue) / inputValue;
                    float sumRotation = remainValue;
                    for (int i=0;i< inputValue; i++)
                    {
                        // 단위 회전 값.
                        Vector3 rotatedVector = Quaternion.AngleAxis(sumRotation, rotationAxis) * originalVector;
                        result.Add(rotatedVector);
                        sumRotation += eachRotationRange;
                    }
                    return result;
                }
            case SpawnRotationType.CameraDirectional:
                {
                    if (inputValue == 0)
                        return result;
                    // 나누어 안떨어지느 값에 대해서
                    int remainValue = 360 % (int)inputValue;

                    float eachRotationRange = (360-remainValue) / inputValue;
                    float sumRotation = remainValue;
                    for (int i = 0; i < inputValue; i++)
                    {
                        // targetVector: 카메라 방향.
                        // 단위 회전 값.
                        Vector3 rotatedVector = Quaternion.AngleAxis(sumRotation, rotationAxis) * targetVector;
                        result.Add(rotatedVector);
                        sumRotation += eachRotationRange;
                    }
                    return result;
                }
            case SpawnRotationType.RandomRoation:
                {
                    if (spawnCount == 0)
                        return result;
                    // 해당 랜덤에서 겹치는 스킬이 생기면 안되므로 어느정도 간격을 만들고 진행한다.
                    float eachRotationRange = limitRandomRotationRange * 2 / spawnCount;
                    float parameter0 = limitRandomRotationRange;
                    float parameter1 = limitRandomRotationRange - eachRotationRange;
                    for (int i = 0; i < spawnCount; i++)
                    {
                        float randomRotation = Random.Range(parameter0, parameter1);
                        parameter0 -= eachRotationRange;
                        parameter1 -= eachRotationRange;

                        Vector3 rotatedVector = Quaternion.AngleAxis(randomRotation, rotationAxis) * originalVector;
                    }
                    return result;
                }
            case SpawnRotationType.DuoRandomRoation:
                {
                    if (inputValue == 0)
                        return result;

                    Vector3 beforeVector = originalVector;
                    for (int i = 0; i < inputValue; i++)
                    {
                        float randomRotation = Random.Range(-limitRandomRotationRange, limitRandomRotationRange);
                        // targetVector: 카메라 방향.
                        // 단위 회전 값.
                        Vector3 rotatedVector = Quaternion.AngleAxis(randomRotation, rotationAxis) * beforeVector;
                        beforeVector = rotatedVector;
                        result.Add(rotatedVector);
                    }
                    return result;
                }
            default:
                result.Add(new Vector3(0, 0, 1));
                return result;
        }
    }


}
