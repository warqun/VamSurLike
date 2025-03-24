using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킬이 생성되었을때 생성 방향으로 정면
// 방위 계통 추적에 대해서는 카메라 기준 정면(forward)가 된다.
public class SkillMaker : MonoBehaviour
{
    AliveObject master;
    // 생성 및 반복에 대한 클래스
    // 스킬 방향 결정.
    SkillSpawn skillSpawn;
    // 추적, 이동, 종료
    // 
    SkillTransform skillTransform;
    //스킬 연계
    SkillConnection skillConnection;

    long skillID;
    string skillName;
    int SkillCategory;
    int skillType;
    long skillBaseID;
    int maxLevel;
    float cooldown;
    List<long> effectGrounps;
    string skillDescription;

    List<SkillBase> skillObject=new List<SkillBase>();
    public void Set()
    {
        // 주의점 해당 기능 중에 카메라가 있어 카메라 기준일때는 카메라 데이터를 넘겨야함.
        skillSpawn.SetSpawnData();
    }
    public List<SkillBase> SpawnExcute()
    {
        List<(Vector3, Vector3)> spawnVector = skillSpawn.ExcuteSpawnPositioin();

        // 스킬 생성.
        // skillSpawn을 통해서 초기 생성 위치와 스킬 방향이 결정된다.
        skillObject.Clear();
        for (int i = 0; i < spawnVector.Count; i++)
        {
            SkillBase createObject = Instantiate<SkillBase>(new SkillBase(), master.transform.position + spawnVector[i].Item1, Quaternion.Euler(spawnVector[i].Item2), master.transform);
            skillObject.Add(createObject);
        }
        return skillObject;
    }
    public bool IsSkillEnd()
    {
        return skillTransform.IsSkillTransformEnd();
    }
    /// <summary>
    /// 1: 현재 타켓, 2: 타켓  위치. 3: 다음 이동 위치, 4: 다음 회전각(이전 값에 더하는 방식), 5: 다음 커지는 스케일.(이전 값에서)
    /// </summary>
    /// <returns></returns>
    public List<(AliveObject target, Vector3 targetPos, Vector3 nextMove, float nextRotation, Vector3 nextScale)> Excute()
    {
        List<(AliveObject target, Vector3 targetPos, Vector3 nextMove, float nextRotation, Vector3 nextScale)> skillTrans 
            = new List<(AliveObject target, Vector3 targetPos, Vector3 nextMove, float nextRotation, Vector3 nextScale)>();
        // 게임 종료 이전 단계
        if (skillTransform.IsSkillTransformEnd() == false)
        {
            // 스킬 활성화 
            // skillTransform을 통해서 이동 및 컨트롤 작동.
            skillTrans = skillTransform.ExcuteTransform(skillObject);
        }
        return skillTrans;
    }
    public bool IsNextSkillConnection()
    {
        return false;
    }
    /// 해당 변수에서 해당 타입이 어디서 넘어오는지를 받으며
    /// 받은 데이터를 통해서 해당 스킬이 다음이 언제 작동하느지 체크하고 통과되면 연계시킨다.
    public void NextSkillConnection()
    {

        
    }
    private void Start()
    {
        StartCoroutine(SkillProccess());
    }
    IEnumerator SkillProccess()
    {
        SpawnExcute();

        while(IsSkillEnd() == false)
        {
            // 
            List<(AliveObject target, Vector3 targetPos, Vector3 nextMove, float nextRotation, Vector3 nextScale)>  nextTrans = Excute();
            // nextTrans 을 통해서 이동 및 행동 구현.

            yield return new WaitForSeconds(0.02f);
        }
        // 종료 이벤트 구현.

    }
}
