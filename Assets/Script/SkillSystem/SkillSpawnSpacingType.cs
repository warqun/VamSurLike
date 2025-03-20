using System.Collections.Generic;
using UnityEngine;

public class SkillSpawnSpacingType
{
    public enum SpacingType { None, SkillBased, Random, Directional }
    public SpacingType spacingType;
    float spawnInterval;

    public List<Vector3> SkillSpawnSpacingTypeReturn(List<Vector3> rotationList)
    {
        switch (spacingType)
        {
            case SpacingType.Random:
                for (int i = 0; i < rotationList.Count; i++)
                {
                    float randomInterval = Random.Range(0, spawnInterval);
                    rotationList[i] = rotationList[i] * randomInterval;
                }
                return rotationList;
            case SpacingType.SkillBased:
            case SpacingType.Directional:
                for(int i=0;i< rotationList.Count; i++)
                {
                    rotationList[i] = rotationList[i] * spawnInterval;
                }
                return rotationList;

            case SpacingType.None:
            default:
                return rotationList;
        }
    }
}
