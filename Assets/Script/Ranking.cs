using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [SerializeField]
    private InputField _nameText = default;
    
    [SerializeField]
    private TMP_Text _eroorText = default;

    //=================================================================================
    //ユーザ名
    //=================================================================================

    /// <summary>
    /// ユーザ名を登録
    /// </summary>
    public void UpdateUserName() {
        if (2 >= _nameText.text.Length || _nameText.text.Length >= 10)
        {
            _eroorText.text = "3文字以上10文字以下で入力してください";
            return;
        }
        //ユーザ名を指定して、UpdateUserTitleDisplayNameRequestのインスタンスを生成
        var request = new UpdateUserTitleDisplayNameRequest{
            DisplayName = _nameText.text
        };

        //ユーザ名の更新
        Debug.Log($"ユーザ名の更新開始");
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateUserNameSuccess, OnUpdateUserNameFailure);
    }
  
    //ユーザ名の更新成功
    private void OnUpdateUserNameSuccess(UpdateUserTitleDisplayNameResult result){
        //result.DisplayNameに更新した後のユーザ名が入ってる
        Debug.Log($"ユーザ名の更新が成功しました : {result.DisplayName}");
        UpdatePlayerStatistics();
    }

    //ユーザ名の更新失敗
    private void OnUpdateUserNameFailure(PlayFabError error){
        Debug.LogError($"ユーザ名の更新に失敗しました\n{error.GenerateErrorReport()}");
    }

    private void UpdatePlayerStatistics() {
        //UpdatePlayerStatisticsRequestのインスタンスを生成
        var request = new UpdatePlayerStatisticsRequest{
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate{
                    StatisticName = "ScoreRanking",   //ランキング名(統計情報名)
                    Value = GameManager.Instance.Score, //スコア(int)
                }
            }
        };

        //ユーザ名の更新
        Debug.Log($"スコア(統計情報)の更新開始");
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdatePlayerStatisticsSuccess, OnUpdatePlayerStatisticsFailure);
    }
  
    //スコア(統計情報)の更新成功
    private void OnUpdatePlayerStatisticsSuccess(UpdatePlayerStatisticsResult result){
        Debug.Log($"スコア(統計情報)の更新が成功しました");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //スコア(統計情報)の更新失敗
    private void OnUpdatePlayerStatisticsFailure(PlayFabError error){
        Debug.LogError($"スコア(統計情報)更新に失敗しました\n{error.GenerateErrorReport()}");
    }
}
