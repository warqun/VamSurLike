using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����̾� �����̹��� Ư���� ���� ������Ʈ�� �����ǰ� �������. 
/// �̶� ��� ������ �����ϰ� �״�� ������� ���� ��� �����ؾ��ϴ� �޸𸮰� �þ�� �����ϱ� ���������.
/// ���� �ش� ������ ����, ������ ���ؼ� �����ؾ��ϴ� �Ŵ����� �������Ѵ�.
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
