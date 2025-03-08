using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public static class ObjectDataType
{
    public enum AliveObjectStatus
    {
        HP,// ü��
        HPRegen, // ü�� ���
        DP, // ����
        BasicDamage, // �Ϲ� ���ݷ�.
        Speed, // �̵��ӵ�.


        MAX // �������ͽ� �ִ�ġ 
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
        Time, // �ð��� ����
        Use,// Ư�� ���� ����
        UnLimit,// ���� ���� ��������.
        MAX
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