# VamSurLike

# GameBase
전반적인 게임에 대한 처리가 이곳에서 이루어지며 유니티 특유의 호출 순서 문제의 경우

코르틴을 사용하여 각 매니저에 대한 처리를 정리함으로써 해결한다.
## DamageManager
데미지 판정과 후속 개발에 대한 정리를 하기 위해서 GameBase에 등록된 코르틴에서 돌아가는 게임 이벤트으로 만든다.

 List< ObjectDamage > damageEventList를 통해서 GameBase를 통해서 등록된 damageEventList을 반복문을 통해 처리한다.
 
 이때 처리하는 방식은 ObjectDamage.reqObj.DamageReqEvnet()으로 데미지를 받아 ObjectDamage.resObj.DamageResEvnet(Damage)를 적용함으로써 해당 데미지의 결과를 만든다. 이후 ObjectDamage.eventCallBackList을 통해서 등록되어 있는 이벤트를 처리함으로써 해당 데미지 이벤트를 완성시킨다.

 ```
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
 ```

### Class ObjectDamage
    1. AliveObject reqObj

        데미지 주는 대상. 

    2. AliveObject resObj

        데미지를 받는 대상.
    
    3. delegate void EventCallBack(void 함수 등록)

        데미지 이벤트가 발생했을때 후속으로 따라오는 이벤트
    



## ObjectManager
--------
# AliveObject
HP가 있는 오브젝트에 대한 부모 클래스
- healPoint: 살아있을 수 있는 체력
- attakcPoint: 상대를 공격하는 기본 공격

## SubClass
- Damage

    뱀서 게임류의 특징으로 기본적으로 적들의 공격은 Player.OnTriggerStay으로 컨트롤된다.

    이때 AliveObject.allowDamage에 의해서 맞는 것이 조절되며 데미지가 받는 순간이 조절된다.

    데미지 계산과 이벤트는 관리하기 위하여 DamageManager에 등록하여 작동하게 한다.
    

    #### AliveObject.DamageReqEvnet()
    해당 메소드는 데미지 이벤트 발생시 주는 데미지 계산을 하기 위해서 만들어져 있다.
    - ex ) 버프와 무기에 의해서 데미지가 버프 되는 상황에서 이러한 계산을 하는 공간.\
    
    #### AliveObject.DamageResEvnet(float damage)
    해당 메소드는 데미지 이벤트 발생시 받는 데미지 계산을 하기 위해서 만들어져 있다.
    - ex ) 방어력과 버프에 의해서 받는 데미지가 감소되는 상황을 계산하는 공간.

### Mob
### Player
----------------
# GameBase
## DamageManager
## ObjectManager


