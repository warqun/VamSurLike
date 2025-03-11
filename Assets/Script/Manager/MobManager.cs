using System.Collections.Generic;
using UnityEngine;

public class MobManager: ManagerBase
{
    public static MobManager instance;
    public List<MobRoot> mobList = new List<MobRoot>();
    private void Awake()
    {
        if(instance == null)
            instance = this;
        mobList = new List<MobRoot>();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();

        foreach (MobRoot mob in mobList)
        {
            if(mob.isAlive)
                mob.FrameUpdate();
        }
        mobList.RemoveAll(e =>
        {
            return e.isAlive == false;
        });
    }
}
