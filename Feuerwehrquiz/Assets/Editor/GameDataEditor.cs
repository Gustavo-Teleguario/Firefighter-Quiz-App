using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.IO;

public class GameDataEditor : EditorWindow
{
    public GameData gameData;

    private string gameDataProjectFilePath = "/StreamingAssets/data.json";
    [MenuItem("Window/Game Data Editor")]

    static void Init()
    {
        EditorWindow.GetWindow(typeof(GameDataEditor)).Show();
    }

    void OnGUI()
    {
        if (gameData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("gameData");
            EditorGUILayout.PropertyField(serializedProperty, true);

            serializedObject.ApplyModifiedProperties();
            if (GUILayout.Button("Save data"))
            {
                SaveGameData();
            }
        }
        if (GUILayout.Button("Load data"))
        {
            LoadGameData();
        }
    }

    public void LoadGameData()
    {
       string filePath = Application.dataPath + gameDataProjectFilePath;
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);

            if (gameData == null)
            {
                gameData = new GameData();
            }
        }
        else
        {
            gameData = new GameData();
        }
    }

    public void SaveGameData()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;
        string dataAsJson = JsonUtility.ToJson(gameData);
        File.WriteAllText(filePath,dataAsJson);
        Debug.Log("Save data ok. " + filePath);
    }
}

