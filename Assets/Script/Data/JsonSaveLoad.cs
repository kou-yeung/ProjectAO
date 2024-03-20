using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Save
{
    public static class JsonSaveLoad
    {
        private static readonly string saveKey = "saveData";

        public static void Save(UserData userData)
        {
            string json = JsonUtility.ToJson(userData);
            PlayerPrefs.SetString(saveKey, json);
            PlayerPrefs.Save();
        }

        public static UserData LoadData()
        {
            if (!PlayerPrefs.HasKey(saveKey))
            {
                return null;
            }

            string json = PlayerPrefs.GetString(saveKey);
            return JsonUtility.FromJson<UserData>(json);
        }
    }
}