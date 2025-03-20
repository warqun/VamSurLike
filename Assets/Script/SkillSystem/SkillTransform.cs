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
    // 추적 타입
    // bool targetTracking;
    SkillTransformTrackingType skillTransformTrackingType;
    // 이동 타입
    // float moveSpeed;
    // float expansionSpeed;
    SkillTransformMoveType skillTransformMoveType;
    // 종료 타입
    SkillTransformEndType skillTransformEndType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
