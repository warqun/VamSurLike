using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class AliveObject : MonoBehaviour
{
    public ObjectDataType.AliveObjectType type = ObjectDataType.AliveObjectType.None;
    protected float healPoint = 0;
    public float HealPoint { get { return healPoint; } set { healPoint = value; } }
    protected float speedPoint =0;
    public float SpeedPoint { get { return speedPoint; } set { speedPoint = value; } }
    protected float attackPoint=0;
    public float AttackPoint { get { return attackPoint; } set { attackPoint = value; } }
    protected float depancePoint=0;
    public float DepancePoint { get { return depancePoint; } set { depancePoint = value; } }
    protected float healRegenPoint=0;
    public float HealRegenPoint { get { return healRegenPoint; } set { healRegenPoint = value; } }
    protected float damageTick=0.5f;
    protected bool allowDamage = true;
    public bool AllowDamage { get { return allowDamage; } set { allowDamage = value; } }

    protected virtual void Start()
    {
        Debug.LogFormat("[Alive][State] {0} State - HP:{1}, Speed:{2}, AP:{3}, DP:{4}, RP:{5}, Type:{6}",gameObject.name, healPoint, speedPoint, attackPoint,depancePoint, healRegenPoint, type);
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
        Debug.LogFormat("[Alive][Damage][REQ] {0} - Damage:{1}",gameObject.name, attackPoint);
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
        Debug.LogFormat("[Alive][Damage][RES] {0} - Damage:{1}, RemainHP:{2}", gameObject.name, damage, healPoint);
    }
    // 데미지 이벤트가 발생될대 일어나는 일.
    // 피격 무적, 피격 색 변화, 피격 애니메이션, 아이템 효과 등.
    public virtual void DamageEvnet()
    {
        Debug.LogFormat("[Alive][Damage][Event] Name:{0}, Type:{1}", gameObject.name, type);
    }

    float damageTimer = 0;
    protected virtual void Update()
    {
        {
            // 이미 데미지 받고 무적 상태 돌입상태.
            if (allowDamage == false)
            {
                damageTimer += Time.deltaTime;
                if (damageTimer > damageTick)
                {
                    damageTimer = 0;
                    allowDamage = true;
                }
            }
        }
    }
}