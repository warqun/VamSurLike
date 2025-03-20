using UnityEngine;

public class SkillTransformMoveType : MonoBehaviour
{
    public enum MoveType
    {
        Fixed, 
        UniformMove, 
        TargetGuidedMove
    }
    public MoveType moveType;
    public enum RotationType
    {
        Fixed,
        UniformRotation,
        TargetGuidedRotation
    }
    public RotationType rotationType;
    public enum ExpansionType  
    { 
        Fixed, 
        UniformExpansion, 
        TargetDistanceExpansion, 
        CasterDistanceExpansion 
    }
    public ExpansionType expansionType;
    // �̵� �ӵ�
    public float moveSpeed;
    // Ȯ�� �ӵ�
    float expansionSpeed;
}
