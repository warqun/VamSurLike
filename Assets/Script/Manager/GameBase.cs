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

        // 플레이어 등록안되거나 무슨 문제로 사라지는 경우
        if (player == null)
        {
            /// 나중에 멀티 플레이어일 경우에 대한 조정필요.
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
            components.AddRange(FindComponentsInChildren(child)); // 재귀 탐색
        }
        return components;
    }

    protected virtual IEnumerator GameProcess()
    {
        while (true)
        {
            try
            {
                // 몹 이동 
                if (mobManager != null)
                    mobManager.FrameUpdate();

                // 맵과 관련된 파트는 이 아래로 내려간다.
                if (mapManager != null)
                    mapManager.FrameUpdate();

                if (objectManager != null)
                    objectManager.FrameUpdate();

                // 몹과 관련된 파트는 이 아래로 내려간다.
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
