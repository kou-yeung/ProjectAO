using System;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Random = UnityEngine.Random;

public static class PlayFabcustomidLogin
{

    public static Action FarstLoginTure;

    public static Action LoginTure;
    
    public static Action LoginFalse;
    
    private static string _id;
    static bool _isAccountCreate;
    
    public static async UniTask Set()
    {
        if (TestForNullOrEmpty(SaveDataManager.Instance.userData.id))
        {
            _isAccountCreate = true;
            _id = CreateID().ToString();
            await CheckAccountExistence(_id);
        }
        else
        {
            _isAccountCreate = false;
            _id = SaveDataManager.Instance.userData.id;
            await CustomIDLoginAsync();
        }
    }

    static async UniTask CustomIDLoginAsync()
    {
        PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest {
                CustomId = _id, 
                CreateAccount = _isAccountCreate, 
            }, result => {
                ResultCallback(result).Forget();
            },
            error => {
                // 失敗時の処理
                LoginFalse?.Invoke();
            }
        );
    }

    private static async UniTaskVoid ResultCallback(LoginResult result)
    {
        Debug.Log("ログイン成功");
        SaveDataManager.Instance.userData.playFabId = result.PlayFabId;
        await SaveDataManager.Instance.SaveDataAsync();
        // if (_isAccountCreate)
        // {
        //     FarstLoginTure?.Invoke();
        //     return;
        // }

        LoginTure?.Invoke();
    }

    public static async UniTask CheckAccountExistence(string customId)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = customId,
            CreateAccount = false,
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnAccountExistenceCheckSuccess, OnAccountExistenceCheckFailure);
    }

    
    private static async void OnAccountExistenceCheckSuccess(LoginResult result)
    {
        //アカウントが存在する場合の処理を書く
        _id = CreateID().ToString();
        await CheckAccountExistence(_id);

    }
    private static async void OnAccountExistenceCheckFailure(PlayFabError error)
    {
        SaveDataManager.Instance.userData.id = _id;
        await CustomIDLoginAsync();
        Debug.Log("Check完了");
    }

    static int CreateID()
    {
        int min = 100000000;
        int max = 999999999;
        int randomNumber = Random.Range(min, max + 1);

        return randomNumber;
    }

     static bool TestForNullOrEmpty(string s)
     {
         bool result;
         result = s == null || s == string.Empty;
         return result;
     }

   
     
     /// <summary>
     /// メアドとパスワードでログインした場合のcustomIDをセーブデータmanagerに入れる関数
     /// </summary>
     private static void GetCustomID()
     {
         var request = new GetAccountInfoRequest();

         PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnGetAccountInfoFailure);
     }

     // 取得成功時のコールバックメソッド
     private static void OnGetAccountInfoSuccess(GetAccountInfoResult result)
     {
         SaveDataManager.Instance.userData.id = result.AccountInfo.CustomIdInfo.CustomId;
         SaveDataManager.Instance.userData.accountName = result.AccountInfo.TitleInfo.DisplayName;
     }

     // 取得失敗時のコールバックメソッド
     private static void OnGetAccountInfoFailure(PlayFabError error)
     {
         Debug.LogError("Failed to get custom ID: " + error.ErrorMessage);
     }
}
