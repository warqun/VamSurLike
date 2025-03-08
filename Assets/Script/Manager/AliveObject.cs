using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class AliveObject : MonoBehaviour
{
    public ObjectDataType.AliveObjectType type = ObjectDataType.AliveObjectType.None;
    public bool isAlive = true;

    // ObjectDataType.AliveObjectStatus ������� ������ �������ͽ� �迭
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
    /// ���� ���⿡ ���� ���� ����.
    /// </summary>
    protected Dictionary<WeaponBase, float> weaponCycle = new Dictionary<WeaponBase, float>();

    /// <summary>
    /// ���� AliveObject�� ��� �ִ� ������
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
        // ��� �ڽ� ������Ʈ���� WeaponBase ã��
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
            Debug.LogWarning("[Alive][Weapon][Find] WeaponBase�� ���� �ڽ� ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    // �ִ� ������ ���.
    public virtual float DamageReqEvnet()
    {
        float attackPoint = 0;
        attackPoint = GetStatusValue(ObjectDataType.AliveObjectStatus.BasicDamage);
        Debug.LogFormat("[Alive][Damage][REQ] {0} - Damage:{1}",gameObject.name, GetStatusValue(ObjectDataType.AliveObjectStatus.BasicDamage));
        /// TODO
        /// ������ ��ȭ ȿ�� �Ǵ� ��ȭ ȿ�� �߰�
        /// �������� ������ ���� ȿ�� 
        
        return attackPoint;
    }
    // �������� ���� �� ���.
    public virtual void DamageResEvnet(float damage)
    {
        /// ������ ��ȭ ȿ�� �Ǵ� ��ȭ ȿ�� �߰�
        /// �������� ������ ���� ȿ�� 
        Debug.LogFormat("[Alive][Damage][RES] {0} - Damage:{1}, RemainHP:{2}", gameObject.name, damage, GetStatusValue(ObjectDataType.AliveObjectStatus.HP));
    }
    // ������ �̺�Ʈ�� �߻��ɴ� �Ͼ�� ��.
    // �ǰ� ����, �ǰ� �� ��ȭ, �ǰ� �ִϸ��̼�, ������ ȿ�� ��.
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
            // �̹� ������ �ް� ���� ���� ���Ի���.
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