using UnityEngine;

public class MobTest : MobRoot
{
    protected override void Start()
    {
        healPoint = 5;
        depancePoint = 0;
        speedPoint = 3.0f;
        attackPoint = 3;

        base.Start();
    }

    public override float DamageReqEvnet()
    {
        return attackPoint;
    }
    public override void DamageResEvnet(float damage)
    {
        healPoint = damage - depancePoint;
    }
}
