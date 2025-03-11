using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using static ObjectDataType;

public class TraceBase : MonoBehaviour
{
    public AliveObject master;
    public ObjectDataType.TraceType traceType = ObjectDataType.TraceType.None;
    public List<ObjectDataType.AliveObjectType> traceObjectTypeList = new List<ObjectDataType.AliveObjectType>();

    public void Set(AliveObject master, ObjectDataType.TraceType traceType, List<ObjectDataType.AliveObjectType> traceObjectTypeList)
    {
        this.master = master;
        this.traceType = traceType;
        this.traceObjectTypeList = traceObjectTypeList;
    }

    public AliveObject FrameTrace()
    {
        List<AliveObject> traceList = new List<AliveObject>();
        for (int i = 0; i < this.traceObjectTypeList.Count; i++)
        {
            ObjectDataType.AliveObjectType aliveType = this.traceObjectTypeList[i];
            switch (aliveType)
            {
                case ObjectDataType.AliveObjectType.Player:
                    // Player 캐릭터 늘어날경우 대비하기는해야됨.
                    if(GameBase.gameBase.player != null)
                        traceList.Add(GameBase.gameBase.player);
                    break;
                case ObjectDataType.AliveObjectType.Mob:
                    for(int mobIndex = 0; mobIndex < GameBase.gameBase.GetMobList.Count; mobIndex++)
                    {
                        traceList.Add(GameBase.gameBase.GetMobList[mobIndex]);
                    }
                    break;
                default:
                    break;
            }
        }
        AliveObject result = null;
        switch (traceType)
        {
            case TraceType.None:
            case TraceType.Near:
                {
                    float minDistance = float.MaxValue;
                    result = null;
                    for (int i =0; i < traceList.Count; i++)
                    {
                        float distance = Vector3.Distance(master.transform.position, traceList[i].transform.position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            result = traceList[i];
                        }
                    }
                }
                break;
            case TraceType.Far:
                {
                    float maxDistance = -1;
                    result = null;
                    for (int i = 0; i < traceList.Count; i++)
                    {
                        float distance = Vector3.Distance(master.transform.position, traceList[i].transform.position);
                        if (distance > maxDistance)
                        {
                            maxDistance = distance;
                            result = traceList[i];
                        }
                    }
                }
                break;
            case TraceType.Strong:
                {
                    // TODO 
                }
                break;
            case TraceType.Week:
                {
                    // TODO 
                }
                break;
        }

        return result;
    }

}
