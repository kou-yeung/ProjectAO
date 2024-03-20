
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveDataManager))]
public class SaveDataManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var maneger = target as SaveDataManager;

        base.OnInspectorGUI();

        EditorGUILayout.Space(5f);

        if (GUILayout.Button("セーブ"))
        {
            maneger.Save();
        }
        
        if (GUILayout.Button("ロード"))
        {
            maneger.Lood();
        }
        
        if (GUILayout.Button("リセット"))
        {
            maneger.RestSaveData();
        }
    }
}
