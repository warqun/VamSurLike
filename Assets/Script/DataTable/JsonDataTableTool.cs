using System.IO;
using UnityEngine;

public class JsonDataTableTool
{
    [ContextMenu("To Json Data")] // ������Ʈ �޴��� �Ʒ� �Լ��� ȣ���ϴ� To Json Data ��� ��ɾ ������
    public void SavePlayerDataToJson(PlayerWeaponStatusList playerWeaponStatusList)
    {
        try
        {
            // ToJson�� ����ϸ� JSON���·� �����õ� ���ڿ��� �����ȴ�  
            string jsonData = JsonUtility.ToJson(playerWeaponStatusList);
            // �����͸� ������ ��� ����
            string path = Path.Combine(Application.streamingAssetsPath, "PlayerWeaponStatus.json");
            // ���� ���� �� ����
            File.WriteAllText(path, jsonData);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }

    }


    public PlayerWeaponStatusList ReadPlayerDataToJson()
    {
        try
        {
            // �����͸� ������ ��� ����
#if UNITY_EDITOR
            string path = Path.Combine(Application.streamingAssetsPath, "PlayerWeaponStatus.json");
#else
        string path = Path.Combine(Application.streamingAssetsPath, "PlayerWeaponStatus.json");
#endif
            // ������ �ؽ�Ʈ�� string���� ����
            string jsonData = File.ReadAllText(path);

            PlayerWeaponStatusList playerWeaponStatus = JsonUtility.FromJson<PlayerWeaponStatusList>(jsonData);
            return playerWeaponStatus;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            return null;
        }

    }
}
