using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class GetRanking : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _rankingText = default;

    //=================================================================================
    //ランキング取得
    //=================================================================================

    private void OnEnable()
    {
        GetLeaderboard();
    }


    /// <summary>
    /// ランキング(リーダーボード)を取得
    /// </summary>
    private void GetLeaderboard() { 
        //GetLeaderboardRequestのインスタンスを生成
        var request = new GetLeaderboardRequest{
            StatisticName   = "ScoreRanking", //ランキング名(統計情報名)
            StartPosition   = 0,                 //何位以降のランキングを取得するか
            MaxResultsCount = 10                  //ランキングデータを何件取得するか(最大100)
        };

        //ランキング(リーダーボード)を取得
        Debug.Log($"ランキング(リーダーボード)の取得開始");
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnGetLeaderboardFailure);
    }
  
    //ランキング(リーダーボード)の取得成功
    private void OnGetLeaderboardSuccess(GetLeaderboardResult result){
        Debug.Log($"ランキング(リーダーボード)の取得に成功しました");

        //result.Leaderboardに各順位の情報(PlayerLeaderboardEntry)が入っている
        _rankingText.text = "";
        foreach (var entry in result.Leaderboard) {
            _rankingText.text += $"\n順位 : {entry.Position + 1}, スコア : {entry.StatValue}";
        }
    }

    //ランキング(リーダーボード)の取得失敗
    private void OnGetLeaderboardFailure(PlayFabError error){
        Debug.LogError($"ランキング(リーダーボード)の取得に失敗しました\n{error.GenerateErrorReport()}");
    }
}
