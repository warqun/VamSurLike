using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

/// ���ݿ� ���� Buff������ �����ȴ�. 
/// ������ ���� ������ ���� Buff�� �����Ǿ���� �Ź� ������ �ʿ��ϰ� �ȴ�.
/// �̷��� ������ �ذ��ϱ� ���ؼ� 
/// <summary>
/// ���� ���� Ŭ���� ���̽� ����ڴ� AliveObject�� �������� �Ѵ�.
/// </summary>
public class BuffBase : MonoBehaviour
{
    public AliveObject master;
    public string buffName="";
    public float[] buffStatus = new float[(int)ObjectDataType.AliveObjectStatus.MAX];

    public ObjectDataType.BuffType buffType = ObjectDataType.BuffType.None;
    public float endTrigger = 0;

    // ������.
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
    // �������� ���� �� ����.
    public void EditBuff(ObjectDataType.AliveObjectStatus buffStatusType, float value)
    {
        buffStatus[(int)buffStatusType] = value;
    }
    public float GetBuffValue(ObjectDataType.AliveObjectStatus buffStatusType)
    {
        return buffStatus[(int)buffStatusType];
    }
    // ����ϴ� ������ ������ ����������� �̺�Ʈ�� ���ɼ��� �����Ƿ�.
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
    // ���� ���̽� �ð��� ����Ǿ��־����.
    // Ÿ�̸� �Ŵ��� ���� �ʿ�

    protected virtual void FixedUpdate()
    {
        if(buffType == ObjectDataType.BuffType.Time)
        {
            endTrigger -= Time.fixedDeltaTime;
        }
    }
}
