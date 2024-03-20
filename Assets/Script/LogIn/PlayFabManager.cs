using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabManager : MonoBehaviour
{
    private static PlayFabManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        ConnectToPlayFab();
    }
    // private void Start()
    // {
    //     // 初回接続
    // }

    private void ConnectToPlayFab()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SaveDataManager.Instance.userData.id,
            CreateAccount = false
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private async void OnLoginSuccess(LoginResult result)
    {
        //await CharacterManager.Instance.UpdateUserDate();
        Debug.Log("Successfully connected to PlayFab!");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Failed to connect to PlayFab: " + error.ErrorMessage);
    }
}
