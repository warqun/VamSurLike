using UnityEngine;

/// ��ų Ÿ�� - SkillTransformTrackingType
///     �ǽð� Ÿ�� ����
///     �ǽð� Ÿ�� ����
///     ��� Ÿ�� ����
///     ���Ǻ� ���� Ÿ�� ����(Ư�� ���� �����ϴ� ������ ����)
///     
/// ��ų �̵� - SkillTransformMoveType
///     ȸ��
///     �̵�
///     Ȯ��
///     
/// ��ų �浹 �� ���� - SkillTransformEndType
///     ���� �ð� ����
///     �浹 ī��Ʈ 
///     Ÿ�� �浹
/// <summary>
/// �̵� �� �浹, ���� ������ �����ϴ� Ŭ����
/// ������ ��ų�� ������ ���� ���� ��
/// </summary>
public class SkillTransform : MonoBehaviour
{
    // ��ų ���� ��ȣ
    long skillId;
    // ���� Ÿ��
    // bool targetTracking;
    SkillTransformTrackingType skillTransformTrackingType;
    // �̵� Ÿ��
    // float moveSpeed;
    // float expansionSpeed;
    SkillTransformMoveType skillTransformMoveType;
    // ���� Ÿ��
    SkillTransformEndType skillTransformEndType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
