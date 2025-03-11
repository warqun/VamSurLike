using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBase : MonoBehaviour
{
    public static GameBase gameBase;
    public Player player = null;
    DamageManager damageManager = null;
    ObjectManager objectManager = null;
    MapManager mapManager = null;
    MobManager mobManager = null;

    private void Awake()
    {
        if(gameBase == null)
            gameBase = this;

        // �÷��̾� ��Ͼȵǰų� ���� ������ ������� ���
        if (player == null)
        {
            /// ���߿� ��Ƽ �÷��̾��� ��쿡 ���� �����ʿ�.
            List<Player> playerList = FindComponentsInChildren(transform.parent);
            if (playerList.Count == 0)
            {
                Debug.LogError("[Manager][Start] Error None Player");
                Time.timeScale = 0f;
            }
            else if (playerList.Count > 1)
            {
                Debug.LogError("[Manager][Start] Error Multi Player");
                Time.timeScale = 0f;
            }
            // playerList.Count == 1
            else
            {
                player = playerList[0];
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (damageManager == null)
            damageManager = DamageManager.instance;

        if (objectManager == null)
            objectManager = ObjectManager.instance;

        if (mapManager == null)
            mapManager = MapManager.instance;

        if (mobManager == null)
            mobManager = MobManager.instance;

        StartCoroutine(GameProcess());
    }
    List<Player> FindComponentsInChildren(Transform parent)
    {
        List<Player> components = new List<Player>();
        foreach (Transform child in parent)
        {
            Player comp = child.GetComponent<Player>();
            if (comp != null)
            {
                components.Add(comp);
            }
            components.AddRange(FindComponentsInChildren(child)); // ��� Ž��
        }
        return components;
    }

    protected virtual IEnumerator GameProcess()
    {
        while (true)
        {
            try
            {
                // �� �̵� 
                if (mobManager != null)
                    mobManager.FrameUpdate();

                // �ʰ� ���õ� ��Ʈ�� �� �Ʒ��� ��������.
                if (mapManager != null)
                    mapManager.FrameUpdate();

                if (objectManager != null)
                    objectManager.FrameUpdate();

                // ���� ���õ� ��Ʈ�� �� �Ʒ��� ��������.
                if(damageManager != null)
                    damageManager.FrameUpdate();

            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

            yield return new WaitForSeconds(0.02f);
        }
    }

    public void AddDamageEvent(ObjectDamage objectDamage)
    {
        if(damageManager != null)
            damageManager.damageEventList.Add(objectDamage);
    }
    public void AddMobEvent(MobRoot mob)
    {
        if (mobManager != null)
            mobManager.mobList.Add(mob);
    }

    public List<MobRoot> GetMobList { get { return mobManager.mobList;  } }
}
