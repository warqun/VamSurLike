using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ObjectDataType;

public class MobRoot: AliveObject
{
    private void Awake()
    {
        type = ObjectDataType.AliveObjectType.Mob;
        weaponCycle = new Dictionary<WeaponBase, float>();
    }
    protected override void Start()
    {
        base.Start();
    }
    // 몬스터 프레임 별로 작동되어야하는 기능
    public virtual void FrameUpdate()
    {
        FrameMobMove();
    }

    // 몬스터 이동 기능.
    protected virtual void FrameMobMove()
    {

    }


    // 주는 데미지 출력부 - 고정 메소드
    public override float DamageReqEvnet()
    {
        float attackPoint = DamageMobReqEvent();
        Debug.LogFormat("[MOB][Damage][REQ] {0} - Damage:{1}", gameObject.name, attackPoint);
        return attackPoint;
    }
    // 주는 데미지 입력부 - 자식개체에 대한 설정.
    public virtual float DamageMobReqEvent()
    {
        float attackPoint = 0;
        attackPoint += GetStatusValue(AliveObjectStatus.BasicDamage);
        return attackPoint;
    }
    // 받는 데미지 출력부 - 고정 메소드
    public override void DamageResEvnet(float damage)
    {
        DamageMobResEvent(damage);
        float hp = GetStatusValue(AliveObjectStatus.HP);
        Debug.LogFormat("[MOB][Damage][RES] {0} - Damage:{1}, RemainHP:{2}", gameObject.name, damage, hp);
    }
    // 받는 데미지 입력부 - 자식 개체에 대한 설정.
    public virtual void DamageMobResEvent(float damage)
    {
        float hp = GetStatusValue(AliveObjectStatus.HP);
        float dp = GetStatusValue(AliveObjectStatus.DP);
        hp = damage - dp;
        SetStatusValue(AliveObjectStatus.HP,hp);
        if (hp <= 0)
            isAlive = false;
    }
    // 처음 무기와 접촉시 반응.
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
                    if(triggerWeapon.master.isAlive == false)
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
    // 특정 무기 데미지 타입에 대해서 접촉하고 있는 데미지 판정.
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
                // 죽어있는 상태일때 데미지 판정 종료.
                if (triggerWeapon.master.isAlive == false)
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
    // 무기와 떨어진 이후 데미지 판정.
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
