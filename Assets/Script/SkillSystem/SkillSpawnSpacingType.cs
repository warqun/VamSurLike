using System.Collections.Generic;
using UnityEngine;

public class SkillSpawnSpacingType
{
    public enum SpacingType { None, SkillBased, Random, Directional }
    public SpacingType spacingType;
    float spawnInterval;
    public void Set()
    {
        //spacingType
        //spawnInterval
    }
    public List<(Vector3, Vector3)> SkillSpawnSpacingTypeReturn(List<(Vector3, Vector3)> rotationList)
    {
        switch (spacingType)
        {
            case SpacingType.Random:
                for (int i = 0; i < rotationList.Count; i++)
                {
                    float randomInterval = Random.Range(0, spawnInterval);

                    Vector3 rotationPos= rotationList[i].Item1 * randomInterval;
                    rotationList[i] = (rotationPos, rotationList[i].Item2);

                }
                return rotationList;
            case SpacingType.SkillBased:
            case SpacingType.Directional:
                for(int i=0;i< rotationList.Count; i++)
                {
                    Vector3 rotationPos = rotationList[i].Item1 * spawnInterval;
                    rotationList[i] = (rotationPos, rotationList[i].Item2);
                }
                return rotationList;

            case SpacingType.None:
            default:
                return rotationList;
        }
    }
}
