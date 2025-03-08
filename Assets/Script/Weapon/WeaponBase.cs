using UnityEngine;
using static ObjectDataType;

/// <summary>
/// AliveObject 자식으로 들어가는 모듈 
/// AliveObject의 공격을 대처하는 역할.
/// </summary>
public class WeaponBase : MonoBehaviour
{
    /// 1. 상위개체 플레이어 데이터가 져오기
    /// 2. 발사체도 웨폰 시스템에 등록되어 사용가능하게 하자.
    /// 3. 데미지 파트는 플레이어가 가지고 있으므로 권한을 가지고 있는 플레이어에게 쓰로우
    /// 4. 무기 타입 설정.
    /// 5. 플레이어가 사용가능 상태로 구성.

    // 현재 권한을 가지고 잇는 플레이어
    public AliveObject master= null;

    // 현재 활성화 상태인지 아닌지 체크.
    protected bool useWeapon = false;
    public bool UseWeapon { get { return useWeapon; } set { useWeapon = value; } }

    // 무기 데미지 값.
    protected float wpDamage = 0;
    public float WPDamage { get { return wpDamage; } set { wpDamage = value; } }

    // 무기 데미지 주는 방식
    protected WeaponDamageType weaponDamageType =WeaponDamageType.None;
    public WeaponDamageType WeaponDamageType { get { return weaponDamageType; } set { weaponDamageType = value; } }
    // 무기 타입
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
    /// 오로지 데미지 계산하는 영역으로 두고 있어야함.
    /// 사용자의 체력이 다 떨어지는 경우 계산되지 않게 막혀야함.
    /// DamageManager 계산에 포함되는 코드 
    public virtual float DamageReqEvnet()
    {
        if (master != null)
            Debug.LogFormat("[Weapon][Damage][REQ] Player: {0} - Damage:{1}", master.type, GetWeaponDamage());
        return wpDamage;
    }
    /// DamageManager 계산에서 일어나야하는 일.
    public virtual void DamageEvnet()
    {
        if(gameObject != null)
            Debug.LogFormat("[Weapon][Damage][Event] {0}", gameObject.name);
    }

    /// TODO
    /// 데미지 틱 수정.
    /// <summary>
    /// 0 일때 반복되는 데미지가 없는 부분.
    /// </summary>
    /// <returns></returns>
    public float WeaponDamageTypeTick()
    {
        // 접촉 중에 데미지 계산.
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

