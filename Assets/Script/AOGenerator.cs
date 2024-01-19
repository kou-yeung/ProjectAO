using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _AoPrefad;
    private Vector3 position;
    private Transform _generatePosition;
    
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        //まうすを押したらマウスの位置にAOを生成する
        if (Input.GetMouseButtonDown(0))
        {
            position = Input.mousePosition;
            position.z = 10f;
            position = Camera.main.ScreenToWorldPoint(position);
            GenerateAO(position);
        }
    }
    
    void GenerateAO(Vector3 transform)
    {
        Instantiate(_AoPrefad, position, Quaternion.identity);
    }

    
}
