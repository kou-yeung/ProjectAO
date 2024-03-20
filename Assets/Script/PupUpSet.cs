using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupUpSet : MonoBehaviour
{
    [SerializeField] private GameObject _closepopup;
    [SerializeField]private GameObject _openPopUp;
    
    public void ClosePopUp()
    {
        _openPopUp.SetActive(true);
        _closepopup.SetActive(false);
    }
    
    
}
