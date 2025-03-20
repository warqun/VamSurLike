using System.Collections.Generic;
using UnityEngine;

public class SkillSpawnType
{
    public enum SpawnType { Single, Multi }
    public SpawnType spawnType;
    
    public int spawnCount;

    public void Set()
    {
        // spwanPoint ¼³Á¤.
    }
    public int SpawnCout()
    {
        return spawnCount;
    }

}
