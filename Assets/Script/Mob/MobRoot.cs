using UnityEngine;

public class MobRoot: AliveObject
{
    private void Awake()
    {
        type = ObjectDataType.AliveObjectType.Mob;
    }
    protected override void Start()
    {
        base.Start();
    }
}
