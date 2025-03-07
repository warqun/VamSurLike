using UnityEngine;

public static class ObjectDataType
{
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
}
