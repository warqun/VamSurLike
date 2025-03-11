using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public static class ObjectDataType
{
    public enum AliveObjectStatus
    {
        HP,// 체력
        HPRegen, // 체력 재생
        DP, // 방어력
        BasicDamage, // 일반 공격력.
        Speed, // 이동속도.


        MAX // 스테이터스 최대치 
    }
    public enum AliveObjectType
    {
        None = 0,
        Player,
        Mob,
        NPC,
        MAX
    }
    public enum MapTailType
    {
        None=0,
        MAX
    }
    public enum WeaponType
    {
        None = 0,
        MAX

    }
    public enum WeaponDamageType
    {
        None = 0,
        Short,
        Long,
        Once,
        MAX
    }
    public enum BuffType
    {
        None = 0,
        Time, // 시간초 제한
        Use,// 특정 조건 제한
        UnLimit,// 제한 없음 영구적용.
        MAX
    }
    public enum TraceType
    {
        None = 0,
        Near,
        Far,
        Strong,
        Week,
        Max
    }
}

[System.Serializable]
public class PlayerWeaponStatus
{
    public int weaponID;
    public float damage;

    public PlayerWeaponStatus csvToClass(string[] value)
    {
        if (value.Length > 2)
            return null;

        PlayerWeaponStatus status = new PlayerWeaponStatus();
        weaponID = int.Parse(value[0]);
        damage = float.Parse(value[1]);
        return status;
    }
    public string ToString()
    {
        string csvData = "";
        csvData += weaponID.ToString() + ",";
        csvData += damage.ToString();
        return csvData;
    }
    public string KeyToString()
    {
        string csvData = "";
        csvData += "weaponID" + ",";
        csvData += "damage";
        return csvData;
    }
    public int DataCount()
    {
        return 2;
    }
}
public class PlayerWeaponStatusList
{
    public List<PlayerWeaponStatus> playerWeaponStatusList;
}