using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 스킬 타켓 - SkillTransformTrackingType
///     실시간 타켓 변경
///     실시간 타켓 고정
///     대상 타켓 생성
///     조건부 방위 타켓 생성(특정 구역 감지하는 것으로 감지)
///     
/// 스킬 이동 - SkillTransformMoveType
///     회전
///     이동
///     확장
///     
/// 스킬 충돌 및 종료 - SkillTransformEndType
///     지속 시간 종료
///     충돌 카운트 
///     타켓 충돌
/// <summary>
/// 이동 및 충돌, 종료 시점을 결정하는 클래스
/// 생성된 스킬이 물리적 현상에 대한 명세
/// </summary>
public class SkillTransform : MonoBehaviour
{
    // 스킬 고유 번호
    long skillId;
    // 추적 대상 출력
    SkillTransformTrackingTargetType skillTransformTrackingTargetType;
    // 추적 타입
    // bool targetTracking;
    SkillTransformTrackingType skillTransformTrackingType;
    // 이동 타입
    // float moveSpeed;
    // float expansionSpeed;
    SkillTransformMoveType skillTransformMoveType;
    // 종료 타입
    SkillTransformEndType skillTransformEndType;

    public List<(AliveObject target, Vector3 targetPos, Vector3 nextMove, float nextRotation, Vector3 nextScale)> ExcuteTransform(List<SkillBase> skillList)
    {
        //(추적 대상 위치, 이동 위치.)
        List<(AliveObject target, Vector3 targetPos, Vector3 nextMove, float nextRotation, Vector3 nextScale)> moveReq 
            = new List<(AliveObject target, Vector3 targetPos, Vector3 nextMove, float nextRotation, Vector3 nextScale)>();
        Vector3 masterPosition = transform.position;
        //skillList.forward;

        for(int i = 0; i < skillList.Count; i++)
        {
            // 추적 대상에 대해서 리턴.
            AliveObject targetObject = skillTransformTrackingTargetType.SKillTransformTrackingTargetTypeReturn(skillList[i]);
            // 프레임 추적.
            Vector3 targetPosition = skillTransformTrackingType.SkillTransformTrackingTypeReturn(targetObject);

            // 프레임 별 이동할 위치. 
            (Vector3 nextMove, float nextRotation, Vector3 nextScale) nextTrans = skillTransformMoveType.SkillTransformMoveTypeReturn(targetPosition);
            (AliveObject target, Vector3 targetPos, Vector3 nextMove, float nextRotation, Vector3 nextScale) element 
                = (targetObject, targetPosition, nextTrans.nextMove, nextTrans.nextRotation, nextTrans.nextScale);
            moveReq.Add(element);
        }

        
        return moveReq;

    }

    /// <summary>
    /// True: 종료, false: 종료이전.
    /// </summary>
    /// <returns></returns>
    public bool IsSkillTransformEnd()
    {
        return false;
    }
}

