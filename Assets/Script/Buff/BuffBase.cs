using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

/// 스텟에 따른 Buff종류가 결정된다. 
/// 문제는 스텟 개수에 따른 Buff가 고정되어버려 매번 수정이 필요하게 된다.
/// 이러한 문제를 해결하기 위해서 
/// <summary>
/// 버프 관련 클래스 베이스 대상자는 AliveObject을 기준으로 한다.
/// </summary>
public class BuffBase : MonoBehaviour
{
    public AliveObject master;
    public string buffName="";
    public float[] buffStatus = new float[(int)ObjectDataType.AliveObjectStatus.MAX];

    public ObjectDataType.BuffType buffType = ObjectDataType.BuffType.None;
    public float endTrigger = 0;

    // 생성자.
    public BuffBase(AliveObject master, string buffName="", ObjectDataType.BuffType buffType= ObjectDataType.BuffType.None, float endTrigger = 0)
    {
        this.master = master;
        this.buffName = buffName;
        this.buffStatus = new float[(int)ObjectDataType.AliveObjectStatus.MAX];
        for (int i = 0; i < this.buffStatus.Length; i++)
        {
            this.buffStatus[i] = 0f;
        }
        this.buffType = buffType;
        this.endTrigger = endTrigger;
    }
    // 생성이후 버프 값 수정.
    public void EditBuff(ObjectDataType.AliveObjectStatus buffStatusType, float value)
    {
        buffStatus[(int)buffStatusType] = value;
    }
    public float GetBuffValue(ObjectDataType.AliveObjectStatus buffStatusType)
    {
        return buffStatus[(int)buffStatusType];
    }
    // 상속하는 이유는 스택이 사라질때마다 이벤트의 가능성이 있으므로.
    public virtual void UseTrigger()
    {
        switch (buffType)
        {
            case ObjectDataType.BuffType.Use:
                endTrigger -= 1;
                break;
        }
    }
    public bool isEndBuff
    {
        get
        {
            switch (buffType)
            {
                case ObjectDataType.BuffType.None:
                    return true;
                case ObjectDataType.BuffType.Time:
                    return endTrigger <= 0;
                case ObjectDataType.BuffType.Use:
                    return endTrigger == 0;
                case ObjectDataType.BuffType.UnLimit:
                    return false;
                default:
                    return true;
            }
        }
    }
    // TODO
    // 게임 베이스 시간에 연결되어있어야함.
    // 타이머 매니저 제작 필요

    protected virtual void FixedUpdate()
    {
        if(buffType == ObjectDataType.BuffType.Time)
        {
            endTrigger -= Time.fixedDeltaTime;
        }
    }
}
