using UnityEngine;

public class Player : AliveObject
{
    public Rigidbody rigidbody = null;
    private void Awake()
    {
        // 나중에 DB를 쓰든 파일 시스템을 쓰든 엑셀쓰든 다른 곳에서 등록하게 설정.
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
    /// 트리거에 충돌했을때
    private void OnTriggerStay(Collider other)
    {
        if (allowDamage == true)
        {
            {
                // 다이렉트 데미지.
                AliveObject triggerObject = other.GetComponent<AliveObject>();
                if (triggerObject != null)
                {
                    switch (triggerObject.type)
                    {
                        case ObjectDataType.AliveObjectType.Mob:
                            ObjectDamage damageReport = new ObjectDamage(triggerObject.DamageReqEvnet, DamageResEvnet);
                            damageReport.AddEvnet(DamageEvnet);
                            damageReport.AddEvnet(triggerObject.DamageEvnet);

                            // 아이템 또는 추가되는 방식은 여기로.

                            GameBase.gameBase.AddDamageEvent(damageReport);
                            break;
                    }
                }
            }
            // 무적 활성화.
            allowDamage = false;
        }
        /// 무기에 의한 데미지
        /// 특정 경우에 도트 뎀으로 들어와야하는 경우.
        WeaponBase triggerWeapon = other.GetComponent<WeaponBase>();
        if (triggerWeapon != null)
        {
            // 무기에 따른 차별점이 생길대 추가.
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
