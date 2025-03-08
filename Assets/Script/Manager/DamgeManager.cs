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
    // GameBase에서 코르틴에서 돌아가는 프레임에 적용.
    public override void FrameUpdate()
    {
        /// 데미지 계산.
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
    // 데미지를 주는 대상
    public ReqEventCallBack reqObj;
    public delegate float ReqEventCallBack();
    // 데미지를 받는 대상
    public delegate void ResEventCallBack(float damage);
    public ResEventCallBack resObj;
    // 데미지 받을때 일어나느 이벤트 등록
    public delegate void EventCallBack();
    public List<EventCallBack> eventCallBackList = new List<EventCallBack>();
    // 생성자
    public ObjectDamage(ReqEventCallBack reqObj, ResEventCallBack resObj)
    {
        this.reqObj = reqObj;
        this.resObj = resObj; 
        eventCallBackList = new List<EventCallBack>();
    }
    // 데미지 맞을때 일어나야하는 이벤트 등록
    // 해당 기능은 맵에서 일어나는 또는 플레이어가 일어나는 등 여러가지 데미지로 발생되는 경우의 수를 등록하기 위해서 구현.
    public void AddEvnet(EventCallBack eventCallBack)
    {
        eventCallBackList.Add(eventCallBack);
    }
}
