using UnityEngine;

/// 생성개수 SkillSpawnType
/// 사출 및 반복 방식(회전 방식) SkillSpawnRotationType
/// 생성 간격(간격 방식) SkillSpawnSpacingType
/// 해당 클래스을 가지고 해당 스킬 스폰 방식을 컨트롤 한다.
/// <summary>
/// 현재 게임에 대한 스킬 생성은 크게 정면 생성과 지점 생성으로 나눠진다.
/// n차 스킬 생성에 대한 클래스
/// </summary>
public class SkillSpawn : MonoBehaviour
{
    // 스킬 ID
    long skillId;
    // 단일, 복수
    // int spawnCount;
    SkillSpawnType skillSpawnType;
    // 스킬정면, 스킬정면회전, 랜덤방향, 이항랜덤방향, 스킬정면방위, 화면기준방위
    // float limitRandomRotationRange; // (-x < a < x)각 안에서.
    SkillSpawnRotationType skillSpawnRotationType;
    // 간격없음, 스킬기준간겨그, 랜덤간격, 방위기준 간격.
    // float spawnInterval;
    SkillSpawnSpacingType skillSpawnSpacingType;

    public void ExcuteSpawn()
    {

    }
}
