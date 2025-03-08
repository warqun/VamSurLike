using System.IO; // 저장 등 파일 관리를 위해 추가
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class FileChangeCSVToJson
{
    public PlayerWeaponStatusList ConvertCSVToClass() 
    {
        PlayerWeaponStatusList list = new PlayerWeaponStatusList();
        var data = Resources.Load(Application.streamingAssetsPath + "PlayerWeaponStatus.csv") as TextAsset;

        string[] lines = Regex.Split(data.text, "\n");

        for (int i = 1; i < lines.Length - 1; i++)
        {
            string[] values = lines[i].Split(',');
            for (int j = 0; j < values.Length; j++)
            {
                string value = values[j];

                value = Regex.Replace(value, "`", ",");
                values[j] = value;
            }

            PlayerWeaponStatus t = new PlayerWeaponStatus();
            t.csvToClass(values); // ,로 구분된 csv 데이터를 클래스의 형태에 맞게 변환
            if (t == null)
            {
                Debug.LogErrorFormat("[Convert][Error] Line: {0}, Count Exception",i,values.Length);
                return null;
            }
            list.playerWeaponStatusList.Add(t); // 리스트에 추가해준다
        }

        return list;
    }
    public void SaveCSVToJson()
    {
        PlayerWeaponStatusList list = ConvertCSVToClass();
        if (list != null)
        {
            new JsonDataTableTool().SavePlayerDataToJson(list);
            Debug.LogError("[Success][Convert] Save JSON");
        }
        else
            Debug.LogError("[Error][Convert] empty JSON");
    }
}
