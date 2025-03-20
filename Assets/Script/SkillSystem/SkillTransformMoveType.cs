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
    // 이동 속도
    public float moveSpeed;
    // 확장 속도
    float expansionSpeed;
}
