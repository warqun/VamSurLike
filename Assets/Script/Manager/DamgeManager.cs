using System.Collections.Generic;
using UnityEngine;

public class DamageManager : ManagerBase
{
    public static DamageManager instance;
    public List<ObjectDamage> damageEventList;

    private void Awake()
    {
        if(instance == null) instance = this;
        damageEventList=new List<ObjectDamage> ();
    }
    public override void Init()
    {
        if(instance == null)
            instance = this;
        damageEventList = new List<ObjectDamage> ();
    }
    // GameBase���� �ڸ�ƾ���� ���ư��� �����ӿ� ����.
    public override void FrameUpdate()
    {
        /// ������ ���.
        List < ObjectDamage > frameList = new List < ObjectDamage > ();
        frameList.AddRange(damageEventList);
        damageEventList.Clear();
        foreach (ObjectDamage damageEvent in frameList)
        {
            float reqDamage = damageEvent.reqObj.DamageReqEvnet();
            damageEvent.resObj.DamageResEvnet(reqDamage);
            foreach(ObjectDamage.EventCallBack callBack in damageEvent.eventCallBackList)
            {
                callBack();
            }
            Debug.LogFormat("[Manager][Damage] State EventCount:{0}", damageEvent.eventCallBackList.Count);
            Debug.LogFormat("[Manager][Damage] State req TYPE:{0}, Damage:{1}", damageEvent.reqObj.type, reqDamage);
            Debug.LogFormat("[Manager][Damage] State req TYPE:{0}, HP:{1}", damageEvent.resObj.type, damageEvent.resObj.HealPoint);
        }

    }

}
public class ObjectDamage
{
    // �������� �ִ� ���
    public AliveObject reqObj;
    // �������� �޴� ���
    public AliveObject resObj;
    // ������ ������ �Ͼ�� �̺�Ʈ ���
    public delegate void EventCallBack();
    public List<EventCallBack> eventCallBackList = new List<EventCallBack>();
    // ������
    public ObjectDamage(AliveObject reqObj, AliveObject resObj)
    {
        this.reqObj = reqObj;
        this.resObj = resObj; 
        eventCallBackList = new List<EventCallBack>();
    }
    // ������ ������ �Ͼ���ϴ� �̺�Ʈ ���
    // �ش� ����� �ʿ��� �Ͼ�� �Ǵ� �÷��̾ �Ͼ�� �� �������� �������� �߻��Ǵ� ����� ���� ����ϱ� ���ؼ� ����.
    public void AddEvnet(EventCallBack eventCallBack)
    {
        eventCallBackList.Add(eventCallBack);
    }
}
