using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class AliveObject : MonoBehaviour
{
    public ObjectDataType.AliveObjectType type = ObjectDataType.AliveObjectType.None;
    public bool isAlive = true;

    // ObjectDataType.AliveObjectStatus 기반으로 구성된 스테이터스 배열
    float[] statusValue = new float[(int)ObjectDataType.AliveObjectStatus.MAX];
    public float GetStatusValue(ObjectDataType.AliveObjectStatus statusType)
    {
        return statusValue[(int)statusType];
    }
    public void SetStatusValue(ObjectDataType.AliveObjectStatus statusType, float value)
    {
        statusValue[(int)statusType] = value;
    }

    protected float resDamageTick=0.5f;
    protected bool allowDamage = true;
    public bool AllowDamage { get { return allowDamage; } set { allowDamage = value; } }
    /// <summary>
    /// 여러 무기에 대한 판정 정리.
    /// </summary>
    protected Dictionary<WeaponBase, float> weaponCycle = new Dictionary<WeaponBase, float>();

    /// <summary>
    /// 현재 AliveObject가 들고 있는 버프들
    /// </summary>
    protected List<BuffBase> buffList = new List<BuffBase>();

    protected virtual void Start()
    {
        isAlive = true;
        Debug.LogFormat("[Alive][State] {0} State - HP:{1}, Speed:{2}, AP:{3}, DP:{4}, RP:{5}, Type:{6}",
            gameObject.name, 
            GetStatusValue(ObjectDataType.AliveObjectStatus.HP), 
            GetStatusValue(ObjectDataType.AliveObjectStatus.Speed), 
            GetStatusValue(ObjectDataType.AliveObjectStatus.BasicDamage), 
            GetStatusValue(ObjectDataType.AliveObjectStatus.DP), 
            GetStatusValue(ObjectDataType.AliveObjectStatus.HPRegen), 
            type);
        // 모든 자식 오브젝트에서 WeaponBase 찾기
        WeaponBase[] weapons = GetComponentsInChildren<WeaponBase>();

        if (weapons.Length > 0)
        {
            foreach (var weapon in weapons)
            {
                weapon.master = this;
            }
        }
        else
        {
            Debug.LogWarning("[Alive][Weapon][Find] WeaponBase를 가진 자식 오브젝트를 찾을 수 없습니다.");
        }
    }

    // 주는 데미지 계산.
    public virtual float DamageReqEvnet()
    {
        float attackPoint = 0;
        attackPoint = GetStatusValue(ObjectDataType.AliveObjectStatus.BasicDamage);
        Debug.LogFormat("[Alive][Damage][REQ] {0} - Damage:{1}",gameObject.name, GetStatusValue(ObjectDataType.AliveObjectStatus.BasicDamage));
        /// TODO
        /// 아이템 강화 효과 또는 약화 효과 추가
        /// 스테이지 버프에 의한 효과 
        
        return attackPoint;
    }
    // 데미지를 받을 때 계산.
    public virtual void DamageResEvnet(float damage)
    {
        /// 아이템 강화 효과 또는 약화 효과 추가
        /// 스테이지 버프에 의한 효과 
        Debug.LogFormat("[Alive][Damage][RES] {0} - Damage:{1}, RemainHP:{2}", gameObject.name, damage, GetStatusValue(ObjectDataType.AliveObjectStatus.HP));
    }
    // 데미지 이벤트가 발생될대 일어나는 일.
    // 피격 무적, 피격 색 변화, 피격 애니메이션, 아이템 효과 등.
    public virtual void DamageEvnet()
    {
        Debug.LogFormat("[Alive][Damage][Event] Name:{0}, Type:{1}", gameObject.name, type);
    }

    public float GetBuffValue(ObjectDataType.AliveObjectStatus status)
    {
        float value = 1;
        for(int i = 0; i<buffList.Count; i++)
        {
            value += buffList[i].GetBuffValue(status);
        }
        return value+1;
    }

    float damageTimer = 0;
    protected virtual void Update()
    {
        {
            // 이미 데미지 받고 무적 상태 돌입상태.
            if (allowDamage == false)
            {
                damageTimer += Time.deltaTime;
                if (damageTimer > resDamageTick)
                {
                    damageTimer = 0;
                    allowDamage = true;
                }
            }
        }
    }
}