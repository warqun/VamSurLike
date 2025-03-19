using System.Collections.Generic;
using UnityEngine;

public class SkillMaker : MonoBehaviour
{
    // 생성 및 반복에 대한 클래스
    SkillSpawn skillSpawn;
    // 추적, 이동, 종료
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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
