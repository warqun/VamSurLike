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
            float reqDamage = damageEvent.reqObj();
            damageEvent.resObj(reqDamage);
            foreach(ObjectDamage.EventCallBack callBack in damageEvent.eventCallBackList)
            {
                callBack();
            }
            Debug.LogFormat("[Manager][Damage] State EventCount:{0}", damageEvent.eventCallBackList.Count);
        }

    }
}
public class ObjectDamage
{
    // �������� �ִ� ���
    public ReqEventCallBack reqObj;
    public delegate float ReqEventCallBack();
    // �������� �޴� ���
    public delegate void ResEventCallBack(float damage);
    public ResEventCallBack resObj;
    // ������ ������ �Ͼ�� �̺�Ʈ ���
    public delegate void EventCallBack();
    public List<EventCallBack> eventCallBackList = new List<EventCallBack>();
    // ������
    public ObjectDamage(ReqEventCallBack reqObj, ResEventCallBack resObj)
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
