using System.Collections.Generic;
using UnityEngine;

public class SkillMaker : MonoBehaviour
{
    // ���� �� �ݺ��� ���� Ŭ����
    SkillSpawn skillSpawn;
    // ����, �̵�, ����
    SkillTransform skillTransform;
    //��ų ����
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
