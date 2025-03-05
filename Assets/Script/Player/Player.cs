using UnityEngine;

public class Player : AliveObject
{
    public Rigidbody rigidbody = null;
    private void Awake()
    {
        // ���߿� DB�� ���� ���� �ý����� ���� �������� �ٸ� ������ ����ϰ� ����.
        healPoint = 100;
        depancePoint = 1;
        speedPoint = 3.0f;
        attackPoint = 3;
        damageTick = 5;

        type = ObjectDataType.AliveObjectType.Player;
        rigidbody = GetComponent<Rigidbody>();
    }
    protected override void Start()
    {
        base.Start();
    }
    private void FixedUpdate()
    {
    }

    public override float DamageReqEvnet()
    {
        return attackPoint;
    }
    public override void DamageResEvnet(float damage)
    {
        healPoint = damage - depancePoint; 
    }
    /// Ʈ���ſ� �浹������
    private void OnTriggerStay(Collider other)
    {
        if (allowDamage == true)
        {
            allowDamage = false;
            AliveObject triggerObject = other.GetComponent<AliveObject>();
            if (triggerObject != null)
            {
                switch (triggerObject.type)
                {
                    case ObjectDataType.AliveObjectType.Mob:
                        ObjectDamage damageReport = new ObjectDamage(triggerObject, this);
                        damageReport.AddEvnet(DamageEvnet);
                        GameBase.gameBase.AddDamageEvent(damageReport);
                        break;
                }
            }
        }
    }
    public void DamageEvnet()
    {
        Debug.Log("[Player][Damage] event ResDamage");
    }
}
