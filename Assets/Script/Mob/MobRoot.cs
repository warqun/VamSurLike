using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ObjectDataType;

public class MobRoot: AliveObject
{
    protected Dictionary<WeaponBase, float> weaponCycle = new Dictionary<WeaponBase, float>();
    private void Awake()
    {
        type = ObjectDataType.AliveObjectType.Mob;
        weaponCycle = new Dictionary<WeaponBase, float>();
    }
    protected override void Start()
    {
        base.Start();
    }
    public override float DamageReqEvnet()
    {
        Debug.LogFormat("[MOB][Damage][REQ] {0} - Damage:{1}", gameObject.name, attackPoint);
        return DamageMobReqEvent();
    }
    public virtual float DamageMobReqEvent()
    {
        return attackPoint;
    }
    public override void DamageResEvnet(float damage)
    {
        Debug.LogFormat("[MOB][Damage][RES] {0} - Damage:{1}, RemainHP:{2}", gameObject.name, damage, healPoint);
        DamageMobResEvent(damage);
    }
    public virtual void DamageMobResEvent(float damage)
    {
        healPoint = damage - depancePoint;
    }
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

                    // 무기 주인에 따라 데미지 
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

                    // 접촉 중에 데미지 계산.
                    float weaponTimer = triggerWeapon.WeaponDamageTypeTick();
                    if(weaponTimer > 0)
                    {
                        weaponCycle[triggerWeapon] = triggerWeapon.WeaponDamageTypeTick();
                        Debug.Log("[MOB][WEAPON][STAY][ENTER] CHEKCIN ENTER WEAPON");
                    }
                }
            }

        }
    }
    void OnTriggerStay(Collider other)
    {
        // 베이스에서 무기에 따른 무적타이머 설정됨.
        {
            /// 무기에 의한 데미지
            /// 특정 경우에 도트 뎀으로 들어와야하는 경우.
            WeaponBase triggerWeapon = other.GetComponent<WeaponBase>();
            if (triggerWeapon != null)
            {
                if (triggerWeapon.master == null)
                    return;
                float weaponTime = 0;
                if (weaponCycle.TryGetValue(triggerWeapon, out weaponTime))
                {
                    // 0 보다 작은 데미지 타이머는 작동하지 않는다.
                    if(weaponTime > 0)
                    {
                        weaponTime -= Time.fixedDeltaTime;
                        // 타이머 소모시 원상복귀
                        bool useTickDamage = false;
                        if(weaponTime < 0)
                        {
                            weaponCycle[triggerWeapon]= triggerWeapon.WeaponDamageTypeTick();
                            useTickDamage = true;
                        }
                        else
                        {
                            weaponCycle[triggerWeapon]= weaponTime;
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
                Debug.Log("[MOB][WEAPON][STAY][EXIT] CHEKCOUT EXIT WEAPON");
            }
        }
    }
}
