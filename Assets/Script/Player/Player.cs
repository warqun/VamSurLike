using UnityEngine;

public class Player : AliveObject
{
    [HideInInspector]
    public Rigidbody rigidbody = null;
    private void Awake()
    {
        // 나중에 DB를 쓰든 파일 시스템을 쓰든 엑셀쓰든 다른 곳에서 등록하게 설정.
        SetStatusValue(ObjectDataType.AliveObjectStatus.HP,100);
        SetStatusValue(ObjectDataType.AliveObjectStatus.DP,1);
        SetStatusValue(ObjectDataType.AliveObjectStatus.Speed,3);
        SetStatusValue(ObjectDataType.AliveObjectStatus.BasicDamage,3);

        resDamageTick = 5;
        type = ObjectDataType.AliveObjectType.Player;
        rigidbody = GetComponent<Rigidbody>();
    }
    protected override void Start()
    {
        base.Start();
    }

    public override float DamageReqEvnet()
    {
        float attackPoint = 0;
        attackPoint += GetStatusValue(ObjectDataType.AliveObjectStatus.BasicDamage);
        Debug.LogFormat("[Player][Damage][REQ] {0} - Damage:{1}", gameObject.name, attackPoint);
        float fullDamage = attackPoint * GetBuffValue(ObjectDataType.AliveObjectStatus.BasicDamage);
        return fullDamage;
    }
    public override void DamageResEvnet(float damage)
    {
        float hp = GetStatusValue(ObjectDataType.AliveObjectStatus.HP);
        float dp = GetStatusValue(ObjectDataType.AliveObjectStatus.DP);

        hp = damage - dp * GetBuffValue(ObjectDataType.AliveObjectStatus.DP);
        SetStatusValue(ObjectDataType.AliveObjectStatus.HP, hp);
        if (hp <= 0)
            isAlive = false;
        Debug.LogFormat("[Player][Damage][RES] {0} - Damage:{1}, RemainHP:{2}", gameObject.name, damage, hp);
    }
    /// 트리거에 충돌했을때

    private void OnTriggerEnter(Collider other)
    {
        // 베이스에서 무기에 따른 무적타이머 설정됨.
        {
            /// 무기에 의한 데미지
            /// 특정 경우에 도트 뎀으로 들어와야하는 경우.
            WeaponBase triggerWeapon = other.GetComponent<WeaponBase>();

            if (triggerWeapon != null)
            {
                if (!weaponCycle.ContainsKey(triggerWeapon))
                {
                    if (triggerWeapon.master == null)
                        return;
                    // 죽어있는 상태일때 데미지 판정 종료.
                    if (triggerWeapon.master.isAlive == false)
                        return;
                    // 무기 주인에 따라 데미지 
                    switch (triggerWeapon.master.type)
                    {
                        case ObjectDataType.AliveObjectType.Mob:
                            // 무기에 따른 차별점이 생길대 추가.
                            ObjectDamage damageReport = new ObjectDamage(triggerWeapon.DamageReqEvnet, DamageResEvnet);
                            damageReport.AddEvnet(DamageEvnet);
                            damageReport.AddEvnet(triggerWeapon.DamageEvnet);
                            GameBase.gameBase.AddDamageEvent(damageReport);
                            break;
                    }

                    // 접촉 중에 데미지 계산.
                    float weaponTimer = triggerWeapon.WeaponDamageTypeTick();
                    if (weaponTimer > 0)
                    {
                        weaponCycle[triggerWeapon] = triggerWeapon.WeaponDamageTypeTick();
                        Debug.Log("[Player][WEAPON][STAY][ENTER] CHEKCIN ENTER WEAPON");
                    }
                }
            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (allowDamage == true)
        {
            {
                // 다이렉트 데미지 사용안함.
                // 다이렉트 데미지.
                //AliveObject triggerObject = other.GetComponent<AliveObject>();
                //if (triggerObject != null)
                //{
                //    switch (triggerObject.type)
                //    {
                //        case ObjectDataType.AliveObjectType.Mob:
                //            ObjectDamage damageReport = new ObjectDamage(triggerObject.DamageReqEvnet, DamageResEvnet);
                //            damageReport.AddEvnet(DamageEvnet);
                //            damageReport.AddEvnet(triggerObject.DamageEvnet);

                //            // 아이템 또는 추가되는 방식은 여기로.

                //            GameBase.gameBase.AddDamageEvent(damageReport);
                //            break;
                //    }
                //}
            }
            // 무적 활성화.
            allowDamage = false;
        }
        // 베이스에서 무기에 따른 무적타이머 설정됨.
        {
            /// 무기에 의한 데미지
            /// 특정 경우에 도트 뎀으로 들어와야하는 경우.
            WeaponBase triggerWeapon = other.GetComponent<WeaponBase>();
            if (triggerWeapon != null)
            {
                if (triggerWeapon.master == null)
                    return;
                // 죽어있는 상태일때 데미지 판정 종료.
                if (triggerWeapon.master.isAlive == false)
                    return;
                float weaponTime = 0;
                if (weaponCycle.TryGetValue(triggerWeapon, out weaponTime))
                {
                    // 0 보다 작은 데미지 타이머는 작동하지 않는다.
                    if (weaponTime > 0)
                    {
                        weaponTime -= Time.fixedDeltaTime;
                        // 타이머 소모시 원상복귀
                        bool useTickDamage = false;
                        if (weaponTime < 0)
                        {
                            weaponCycle[triggerWeapon] = triggerWeapon.WeaponDamageTypeTick();
                            useTickDamage = true;
                        }
                        else
                        {
                            weaponCycle[triggerWeapon] = weaponTime;
                            useTickDamage = false;
                        }
                        // 타이머가 되었을때 데미지 계산.
                        if (useTickDamage)
                        {
                            switch (triggerWeapon.master.type)
                            {
                                case ObjectDataType.AliveObjectType.Player:
                                    // 무기에 따른 차별점이 생길대 추가.
                                    ObjectDamage damageReport = new ObjectDamage(triggerWeapon.DamageReqEvnet, DamageResEvnet);
                                    damageReport.AddEvnet(DamageEvnet);
                                    damageReport.AddEvnet(triggerWeapon.DamageEvnet);
                                    GameBase.gameBase.AddDamageEvent(damageReport);
                                    break;
                            }
                        }
                    }
                }
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {

        WeaponBase triggerWeapon = other.GetComponent<WeaponBase>();
        if (triggerWeapon != null)
        {
            if (weaponCycle.ContainsKey(triggerWeapon))
            {
                weaponCycle.Remove(triggerWeapon);
                Debug.Log("[Player][WEAPON][STAY][EXIT] CHEKCOUT EXIT WEAPON");
            }
        }
    }
    public override void DamageEvnet()
    {
        Debug.Log("[Player][Damage] event ResDamage");
    }
}
