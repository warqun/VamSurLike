using System.IO;
using UnityEngine;

public class FileChangeJsonToCSV : MonoBehaviour
{

    public void SavePlayerDataToCSV(PlayerWeaponStatusList playerWeaponStatusList)
    {
        try
        {
            PlayerWeaponStatusList list = new JsonDataTableTool().ReadPlayerDataToJson();
            if (list == null)
            {
                Debug.LogError("[Error][JSON] emptry JSON");
                return;
            }
            string saveCSV = "";
            saveCSV= new PlayerWeaponStatus().KeyToString() + "\n";
            for (int i = 0; i < list.playerWeaponStatusList.Count; i++)
            {
                string lineCSV = "";
                lineCSV = list.playerWeaponStatusList[i].ToString();
                saveCSV += lineCSV +"\n";
            }

            // 데이터를 저장할 경로 지정
            string path = Path.Combine(Application.streamingAssetsPath, "PlayerWeaponStatus.json");
            // 파일 생성 및 저장
            File.WriteAllText(path, saveCSV);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }
}
