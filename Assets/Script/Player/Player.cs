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
        Debug.LogFormat("[Player][Damage][REQ] {0} - Damage:{1}", gameObject.name, attackPoint);
        return attackPoint;
    }
    public override void DamageResEvnet(float damage)
    {
        healPoint = damage - depancePoint;
        Debug.LogFormat("[Player][Damage][RES] {0} - Damage:{1}, RemainHP:{2}", gameObject.name, damage, healPoint);
    }
    /// Ʈ���ſ� �浹������
    private void OnTriggerStay(Collider other)
    {
        if (allowDamage == true)
        {
            {
                // ���̷�Ʈ ������.
                AliveObject triggerObject = other.GetComponent<AliveObject>();
                if (triggerObject != null)
                {
                    switch (triggerObject.type)
                    {
                        case ObjectDataType.AliveObjectType.Mob:
                            ObjectDamage damageReport = new ObjectDamage(triggerObject.DamageReqEvnet, DamageResEvnet);
                            damageReport.AddEvnet(DamageEvnet);
                            damageReport.AddEvnet(triggerObject.DamageEvnet);

                            // ������ �Ǵ� �߰��Ǵ� ����� �����.

                            GameBase.gameBase.AddDamageEvent(damageReport);
                            break;
                    }
                }
            }
            // ���� Ȱ��ȭ.
            allowDamage = false;
        }
        /// ���⿡ ���� ������
        /// Ư�� ��쿡 ��Ʈ ������ ���;��ϴ� ���.
        WeaponBase triggerWeapon = other.GetComponent<WeaponBase>();
        if (triggerWeapon != null)
        {
            // ���⿡ ���� �������� ����� �߰�.
            switch (triggerWeapon.WeaponType)
            {
            }
            ObjectDamage damageReport = new ObjectDamage(triggerWeapon.DamageReqEvnet, DamageResEvnet);
            damageReport.AddEvnet(DamageEvnet);
            damageReport.AddEvnet(triggerWeapon.DamageEvnet);
            GameBase.gameBase.AddDamageEvent(damageReport);
        }
    }
    public override void DamageEvnet()
    {
        Debug.Log("[Player][Damage] event ResDamage");
    }
}
