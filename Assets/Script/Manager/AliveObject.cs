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
    }

    public virtual float DamageReqEvnet()
    {
        Debug.LogFormat("[Alive][Damage][REQ] {0} - Damage:{1}",gameObject.name, attackPoint);
        return attackPoint;
    }
    public virtual void DamageResEvnet(float damage)
    {
        Debug.LogFormat("[Alive][Damage][RES] {0} - Damage:{1}, RemainHP:{2}", gameObject.name, damage, healPoint);
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