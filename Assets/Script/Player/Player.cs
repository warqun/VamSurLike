using UnityEngine;

public class Player : AliveObject
{
    public Rigidbody rigidbody = null;
    private void Awake()
    {
        // 나중에 DB를 쓰든 파일 시스템을 쓰든 엑셀쓰든 다른 곳에서 등록하게 설정.
        healPoint = 100;
        depancePoint = 1;
        speedPoint = 3.0f;
        attackPoint = 3;
        damageTick = 5;

        type = ObjectDataType.AliveObjectType.Player;
        rigidbody = GetComponent<Rigidbody>();
    }
    protected override void Start()
    {
        base.Start();
    }
    private void FixedUpdate()
    {
    }

    public override float DamageReqEvnet()
    {
        return attackPoint;
    }
    public override void DamageResEvnet(float damage)
    {
        healPoint = damage - depancePoint; 
    }
    /// 트리거에 충돌했을때
    private void OnTriggerStay(Collider other)
    {
        if (allowDamage == true)
        {
            allowDamage = false;
            AliveObject triggerObject = other.GetComponent<AliveObject>();
            if (triggerObject != null)
            {
                switch (triggerObject.type)
                {
                    case ObjectDataType.AliveObjectType.Mob:
                        ObjectDamage damageReport = new ObjectDamage(triggerObject, this);
                        damageReport.AddEvnet(DamageEvnet);
                        GameBase.gameBase.AddDamageEvent(damageReport);
                        break;
                }
            }
        }
    }
    public void DamageEvnet()
    {
        Debug.Log("[Player][Damage] event ResDamage");
    }
}
