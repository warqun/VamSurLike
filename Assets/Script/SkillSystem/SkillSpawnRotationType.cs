using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

/// <summary>
/// �Է� ���ǿ� �´� Vector�� ����. �̶� x-z��ǥ�� z�� ������ �������� �ȴ�.
/// </summary>
public class SkillSpawnRotationType
{
    // x - z ��ǥ: z: �� x ��
    // �Ÿ��� 1�� ��ǥ�踦 ���ؼ� ���� ���� ������ �����͸� ���.
    public enum SpawnRotationType { Front, Rotated, Random, Directional, CameraDirectional , RandomRoation, DuoRandomRoation }
    public SpawnRotationType rotationType;

    // �������� �ִ� ȸ����.
    float limitRandomRotationRange; // (-x < a < x)�� �ȿ���.
    // �Է� ��.
    float inputValue;
    // ��ų ����
    public int spawnCount;

    // Ÿ�� ��ġ������ ����.
    Vector3 targetVector = Vector3.zero;

    // ���� ���� 
    Vector3 originalVector = new Vector3(0, 0, 1);
    // ȸ�� ��.
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

                    // ������ �ȶ������� ���� ���ؼ�
                    int remainValue = (int)(2 * inputValue) % spawnCount;

                    // ���� ȸ�� ��.
                    float eachRotationRange = (2*inputValue- remainValue) / spawnCount;

                    // ȸ�� ����
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

                    // ������ �ȶ������� ���� ���ؼ�
                    int remainValue = 360 % (int)inputValue;

                    float eachRotationRange = (360-remainValue) / inputValue;
                    float sumRotation = remainValue;
                    for (int i=0;i< inputValue; i++)
                    {
                        // ���� ȸ�� ��.
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
                    // ������ �ȶ������� ���� ���ؼ�
                    int remainValue = 360 % (int)inputValue;

                    float eachRotationRange = (360-remainValue) / inputValue;
                    float sumRotation = remainValue;
                    for (int i = 0; i < inputValue; i++)
                    {
                        // targetVector: ī�޶� ����.
                        // ���� ȸ�� ��.
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
                    // �ش� �������� ��ġ�� ��ų�� ����� �ȵǹǷ� ������� ������ ����� �����Ѵ�.
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
                        // targetVector: ī�޶� ����.
                        // ���� ȸ�� ��.
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
