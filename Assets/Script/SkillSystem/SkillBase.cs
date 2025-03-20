using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// 현재 기획된 스킬 기능에서 스킬이 스킬로 이어지는 릴레이되는 스킬 기능을 구현하기 위한 기초.
/// <summary>
/// </summary>
public class SkillBase : MonoBehaviour
{
    // 스킬 사용자
    public AliveObject master;
    public GameObject mainSkill;
    public List<GameObject> subSkillList=new List<GameObject>();

    public void Set(AliveObject master, GameObject mainSkill, List<GameObject> subSkillList)
    {

    }

    public virtual void NextSkillEvent()
    {

    }
    protected virtual void EndSkillEvent()
    {

    }

}
