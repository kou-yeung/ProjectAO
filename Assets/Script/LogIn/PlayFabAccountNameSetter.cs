using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class PlayFabAccountNameSetter : MonoBehaviour
{
    [SerializeField] private TMP_InputField _accountnameSet;
    
    [SerializeField] private TMP_Text _accountNameErrorText;
    
    [SerializeField] GameObject _characterSelect;

    public void AccountNameSet()
    {
        if (_accountnameSet == null)
        {
            _accountNameErrorText.text = "アカウントの名前を入れてください";
        }
        string accountName = _accountnameSet.text;
        PlayFabClientAPI.UpdateUserTitleDisplayName(
            new UpdateUserTitleDisplayNameRequest {
                DisplayName = accountName
            }, async result =>
            {
                _accountNameErrorText.text = "アカウントの名前が正常に登録できました";
                _characterSelect.SetActive(true);
                this.gameObject.SetActive(false);
                
            },
            error => {
                _accountNameErrorText.text = "すでにこの名前は使わっれてります";
            }
        );
    }
}
