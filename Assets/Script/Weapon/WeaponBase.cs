using UnityEngine;
using static ObjectDataType;

/// <summary>
/// AliveObject �ڽ����� ���� ��� 
/// AliveObject�� ������ ��ó�ϴ� ����.
/// </summary>
public class WeaponBase : MonoBehaviour
{
    /// 1. ������ü �÷��̾� �����Ͱ� ������
    /// 2. �߻�ü�� ���� �ý��ۿ� ��ϵǾ� ��밡���ϰ� ����.
    /// 3. ������ ��Ʈ�� �÷��̾ ������ �����Ƿ� ������ ������ �ִ� �÷��̾�� ���ο�
    /// 4. ���� Ÿ�� ����.
    /// 5. �÷��̾ ��밡�� ���·� ����.

    // ���� ������ ������ �մ� �÷��̾�
    public AliveObject master= null;

    // ���� Ȱ��ȭ �������� �ƴ��� üũ.
    protected bool useWeapon = false;
    public bool UseWeapon { get { return useWeapon; } set { useWeapon = value; } }

    // ���� ������ ��.
    protected float wpDamage = 0;
    public float WPDamage { get { return wpDamage; } set { wpDamage = value; } }

    // ���� ������ �ִ� ���
    protected WeaponDamageType weaponDamageType =WeaponDamageType.None;
    public WeaponDamageType WeaponDamageType { get { return weaponDamageType; } set { weaponDamageType = value; } }
    // ���� Ÿ��
    protected WeaponType weaponType = WeaponType.None;
    public WeaponType WeaponType { get { return weaponType; } set { weaponType = value; } }

    public void Set(AliveObject master)
    {
        this.master = master; 
    }
    public virtual float GetWeaponDamage()
    {
        return wpDamage;
    }
    /// ������ ������ ����ϴ� �������� �ΰ� �־����.
    /// ������� ü���� �� �������� ��� ������ �ʰ� ��������.
    /// DamageManager ��꿡 ���ԵǴ� �ڵ� 
    public virtual float DamageReqEvnet()
    {
        if (master != null)
            Debug.LogFormat("[Weapon][Damage][REQ] Player: {0} - Damage:{1}", master.type, GetWeaponDamage());
        return wpDamage;
    }
    /// DamageManager ��꿡�� �Ͼ���ϴ� ��.
    public virtual void DamageEvnet()
    {
        if(gameObject != null)
            Debug.LogFormat("[Weapon][Damage][Event] {0}", gameObject.name);
    }

    /// TODO
    /// ������ ƽ ����.
    /// <summary>
    /// 0 �϶� �ݺ��Ǵ� �������� ���� �κ�.
    /// </summary>
    /// <returns></returns>
    public float WeaponDamageTypeTick()
    {
        // ���� �߿� ������ ���.
        switch (weaponDamageType)
        {
            case ObjectDataType.WeaponDamageType.Short:
                return 1f;
            case ObjectDataType.WeaponDamageType.Long:
                return 0.3f;
            case ObjectDataType.WeaponDamageType.Once:
                return 0f;
            default:
                return 0f;
        }
    }
}

