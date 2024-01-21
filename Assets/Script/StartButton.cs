using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
   [SerializeField] private GameObject _closepopup;
   
   public void GameStart()
   {
      _closepopup.SetActive(false);
      GameManager.Instance.GameStart();
   } 
}
