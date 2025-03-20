using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 뱀파이어 서바이벌의 특성상 많은 오브젝트가 생성되고 사라진다. 
/// 이때 모든 유닛을 생성하고 그대로 사라지게 만들 경우 감당해야하는 메모리가 늘어나며 관리하기 힘들어진다.
/// 따라서 해당 유닛이 생성, 삭제에 대해서 관리해야하는 매니저를 만들어야한다.
/// </summary>
public class ObjectManager: ManagerBase
{
    public static ObjectManager instance;
    public List<GameObject> createList = new List<GameObject>();
    public List<GameObject> destoryList = new List<GameObject>();
    private void Awake()
    {
        if(instance == null) instance = this;
        createList = new List<GameObject>();
        destoryList = new List<GameObject>();
    }

    public override void Init()
    {
        if (instance == null)
            instance = this;

        if(createList != null)
            createList.Clear();

        if (destoryList != null)
            destoryList.Clear();
    }
    public override void FrameUpdate()
    {
        foreach(GameObject createObject in createList)
        {

        }
        foreach (GameObject destoryObject in createList)
        {

        }
    }
}
