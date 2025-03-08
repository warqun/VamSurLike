using System.IO;
using UnityEngine;

public class JsonDataTableTool
{
    [ContextMenu("To Json Data")] // 컴포넌트 메뉴에 아래 함수를 호출하는 To Json Data 라는 명령어가 생성됨
    public void SavePlayerDataToJson(PlayerWeaponStatusList playerWeaponStatusList)
    {
        try
        {
            // ToJson을 사용하면 JSON형태로 포멧팅된 문자열이 생성된다  
            string jsonData = JsonUtility.ToJson(playerWeaponStatusList);
            // 데이터를 저장할 경로 지정
            string path = Path.Combine(Application.streamingAssetsPath, "PlayerWeaponStatus.json");
            // 파일 생성 및 저장
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
            // 데이터를 저장할 경로 지정
#if UNITY_EDITOR
            string path = Path.Combine(Application.streamingAssetsPath, "PlayerWeaponStatus.json");
#else
        string path = Path.Combine(Application.streamingAssetsPath, "PlayerWeaponStatus.json");
#endif
            // 파일의 텍스트를 string으로 저장
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
