using Cysharp.Threading.Tasks;
using UnityEngine;
using Save; // JsonSaveLoadを含む名前空間を追加

public class SaveDataManager : SingletonMonoBehaviour<SaveDataManager>
{
    public UserData userData = new UserData();

    protected override async void Awake()
    {
        base.Awake();
        await LoadDataAsync();
    }
    
    public async UniTask SaveDataAsync()
    { 
        JsonSaveLoad.Save(userData);
    }

    public async UniTask LoadDataAsync()
    {
        UserData loadedData = JsonSaveLoad.LoadData();

        if (loadedData != null)
        {
            userData = loadedData;
            Debug.Log("ロード完了");
        }
        else
        {
            Debug.Log("新規セーブデータ作成");
            await SaveDataAsync();
        }
    }

    public async UniTask ResetDataAsync()
    {
        userData = new UserData();
        Debug.Log("セーブデータをリセットしました");
        await SaveDataAsync();
    }

    public void Save()
    {
         JsonSaveLoad.Save(userData);
    }

    public void RestSaveData()
    {
        userData = new UserData();
        Debug.Log("セーブデータをリセットしました");
        Save();
    }

    public void Lood()
    {
        UserData loadedData =  JsonSaveLoad.LoadData();

        if (loadedData != null)
        {
            userData = loadedData;
            Debug.Log("ロード完了");
        }
        else
        {
            Debug.Log("新規セーブデータ作成");
             Save();
        }
    }
}