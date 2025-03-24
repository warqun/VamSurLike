using UnityEngine;

public class SkillTransformMoveType
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

    public (Vector3 nextMove, float nextRotation, Vector3 nextScale) SkillTransformMoveTypeReturn(Vector3 targetPos)
    {
        return (MoveTypeReturn(), RotationTypeReturn(), ExpansionTypeReturn());
    }
    Vector3 MoveTypeReturn()
    {
        switch (moveType)
        {
            case MoveType.Fixed:
                return (Vector3.zero);
        }
        return Vector3.zero;
    }
    float RotationTypeReturn()
    {
        switch (rotationType)
        {
            case RotationType.Fixed:
                return (0);
        }
        return 0;
    }
    Vector3 ExpansionTypeReturn()
    {
        switch (expansionType)
        {
            case ExpansionType.Fixed:
                return (Vector3.zero);
        }
        return Vector3.zero;
    }
}
