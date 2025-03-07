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
        // ���̽����� ���⿡ ���� ����Ÿ�̸� ������.
        {
            /// ���⿡ ���� ������
            /// Ư�� ��쿡 ��Ʈ ������ ���;��ϴ� ���.
            WeaponBase triggerWeapon = other.GetComponent<WeaponBase>();

            if (triggerWeapon != null)
            {
                if (!weaponCycle.ContainsKey(triggerWeapon))
                {
                    if (triggerWeapon.master == null)
                        return;

                    // ���� ���ο� ���� ������ 
                    switch (triggerWeapon.master.type)
                    {
                        case ObjectDataType.AliveObjectType.Player:
                            // ���⿡ ���� �������� ����� �߰�.
                            ObjectDamage damageReport = new ObjectDamage(triggerWeapon.DamageReqEvnet, DamageResEvnet);
                            damageReport.AddEvnet(DamageEvnet);
                            damageReport.AddEvnet(triggerWeapon.DamageEvnet);
                            GameBase.gameBase.AddDamageEvent(damageReport);
                            break;
                    }

                    // ���� �߿� ������ ���.
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
        // ���̽����� ���⿡ ���� ����Ÿ�̸� ������.
        {
            /// ���⿡ ���� ������
            /// Ư�� ��쿡 ��Ʈ ������ ���;��ϴ� ���.
            WeaponBase triggerWeapon = other.GetComponent<WeaponBase>();
            if (triggerWeapon != null)
            {
                if (triggerWeapon.master == null)
                    return;
                float weaponTime = 0;
                if (weaponCycle.TryGetValue(triggerWeapon, out weaponTime))
                {
                    // 0 ���� ���� ������ Ÿ�̸Ӵ� �۵����� �ʴ´�.
                    if(weaponTime > 0)
                    {
                        weaponTime -= Time.fixedDeltaTime;
                        // Ÿ�̸� �Ҹ�� ���󺹱�
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
                        // Ÿ�̸Ӱ� �Ǿ����� ������ ���.
                        if (useTickDamage)
                        {
                            switch (triggerWeapon.master.type)
                            {
                                case ObjectDataType.AliveObjectType.Player:
                                    // ���⿡ ���� �������� ����� �߰�.
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
