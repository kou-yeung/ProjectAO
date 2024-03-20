using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class PlaFabFastLogin : MonoBehaviour
{
    [SerializeField] 
    //private TMP_Text _text;

    private bool isLogin = false;

    private void Awake()
    {
        PlayFabcustomidLogin.LoginTure += AccountSetUP;
        PlayFabcustomidLogin.LoginFalse += LoginFalse;
        
        Login();
    }

    private void OnDisable()
    {
        PlayFabcustomidLogin.FarstLoginTure -= AccountSetUP;
        PlayFabcustomidLogin.LoginFalse -= LoginFalse;
    }

    public async void Login()
    {
        if (isLogin)return;
        isLogin = true;
        await PlayFabcustomidLogin.Set();
    }

    void AccountSetUP()
    {
        //_text.text = "ログインに成功しました";
        isLogin = false;
    }

    void LoginFalse()
    {
        //_text.text = "ログインに失敗しました";
        isLogin = false;
    }

  
}
