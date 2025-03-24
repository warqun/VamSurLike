using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillTransformTrackingType 
{
    public enum SkillTargetType
    { 
        None,
	    TargetDynamicRealTimeTracking, //타겟 변경 실시간 추적
        TargetRealTimeTracking, //타겟 실시간 추적
        TargetLockedPosition, //타겟 위치 고정
        TargetLockedDirection //타겟 방향 고정
    }
    public SkillTargetType skillTargetType;

    AliveObject master;
    // 실시간 추적 여부
    public bool targetTracking;
    // 추적하느 대상 타입 
    ObjectDataType.AliveObjectType traceObjectType;
    // 변경되는 타입
    
    // 추적하는 방위각 수
    int lockedDirectionNum;
    Vector3 targetPosition = Vector3.zero;
    // 현재 스킬의 정면 방향.
    Vector3 forward = Vector3.zero;
    // 현재 스킬 정보
    SkillBase skillBase;
    public void Set()
    {
        //master
        //skillTargetType
        //lockedDirectionNum
        //traceObjectType
        // skillBase
        targetTracking = false;

    }
    public Vector3 SkillTransformTrackingTypeReturn(AliveObject targetObject)
    {
        Vector3 result = (Vector3.zero);
        switch (skillTargetType)
        {
            case SkillTargetType.None:
            default:
                result = Vector3.zero;
                return result;

            case SkillTargetType.TargetDynamicRealTimeTracking:
                {
                    return targetObject.transform.position;
                }
            case SkillTargetType.TargetRealTimeTracking:
                {
                    return targetObject.transform.position;
                }
            case SkillTargetType.TargetLockedPosition:
                {
                    if (targetPosition != Vector3.zero)
                        return targetPosition;
                    else
                        targetPosition = targetObject.transform.position;
                        return targetPosition;
                }
            case SkillTargetType.TargetLockedDirection:
                {
                    if (targetPosition != Vector3.zero)
                        return targetPosition;
                    else
                    {
                        // 방위각 입력을 말해야함.
                        int remainValue = 360 % (int)lockedDirectionNum;

                        float eachRotationRange = (360) / lockedDirectionNum;
                        float sumRotation = 0;
                        Vector3 rotationAxis = new Vector3(0, 1, 0);
                        float checkCos = -1;
                        Vector3 returnVector = Vector3.zero;
                        for (int i = 0; i < lockedDirectionNum; i++)
                        {
                            Vector3 rotatedVector = Quaternion.AngleAxis(sumRotation, rotationAxis) * forward;
                            sumRotation += eachRotationRange;

                            // 최소값 탐지.
                            // 사용자 정면 내적
                            float dotProduct = Vector3.Dot(targetObject.transform.position - skillBase.transform.position, rotatedVector.normalized);
                            if (checkCos <= dotProduct)
                            {
                                checkCos = dotProduct;
                                returnVector = rotatedVector;
                            }
                        }
                        return targetPosition = returnVector;
                    }
                }
        }
    }
}
