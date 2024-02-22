using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    static GameManager _instance;
    
    [SerializeField]private DropObject[] _dropObject;
    [SerializeField] private GameObject _gameOverPopUp;
    [SerializeField] private GameObject _targetcameraPos;

    private float highestPoint = 0;
    public float HighestPoint1 => highestPoint;
    //  最高点
    private DropObject _nowGameObject;
    private Transform _generatePosition;
    
    
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        
    } 
    
    public void GameStart()
    {
        Vector3 position = _targetcameraPos.transform.position;
        position.y = highestPoint + 3f;
        _targetcameraPos.transform.position = position;
        Spawn(highestPoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (_nowGameObject != null && _nowGameObject.IsStill)
        {
            Debug.Log("動いたよ");
            float animalHeight = _nowGameObject.transform.position.y;
            if (animalHeight >= highestPoint)
            {
                highestPoint = animalHeight; // 最高点を更新
                Vector3 position = _targetcameraPos.transform.position;
                position.y = highestPoint + 3f;
                _targetcameraPos.transform.position = position;
                Spawn(highestPoint); // 新しい動物をスポーン
            }
            else if (animalHeight <= highestPoint)
            {
                Spawn(highestPoint);
            }
        }
    }

    void Spawn(float height)
    {
        Debug.Log("スポーンしたよ");
        int i = Random.Range(0, _dropObject.Length);
        _nowGameObject = Instantiate(_dropObject[i], new Vector3(0, height + 1.5f, 0), _dropObject[i].transform.rotation);
    }
    
    public void GameOver()
    {
        _gameOverPopUp.SetActive(true);
    }
}
