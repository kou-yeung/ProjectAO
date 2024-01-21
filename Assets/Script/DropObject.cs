using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    [SerializeField] private float _dragspeed = 10.0f;
    [SerializeField] private float _stillnessThreshold = 0.01f; // 静止閾値
    [SerializeField] private float _stillnessTime = 1.0f; // 静止と判定する時間

    private Rigidbody2D _rb;
    private bool _isDrop = false;
    private bool _isStill = false;
    private float lastMouseX;
    private float _stillnessTimer = 0;

    public bool IsDrop => _isDrop;
    public bool IsStill => _isStill;
    void OnEnable()
    {
        if (TryGetComponent(out Rigidbody2D rb))
        {
            Debug.Log("動いたで");
            _rb = rb;
            _rb.gravityScale = 0;
        }
    }

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !_isDrop)
        {
            float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float deltaX = mouseX - lastMouseX;
            _rb.velocity = new Vector2(deltaX * _dragspeed, _rb.velocity.y); 
            lastMouseX = mouseX;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y); 
            _rb.gravityScale = 1;
            _isDrop = true;
        }
        if (_isDrop && !_isStill)
        {
            if (_rb.velocity.magnitude < _stillnessThreshold)
            {
                _stillnessTimer += Time.deltaTime;
                if (_stillnessTimer >= _stillnessTime)
                {
                    _isStill = true;// 静止と判定
                    Debug.Log("静止したで");
                }
            }
            else
            {
                _stillnessTimer = 0; // 動きがあればタイマーをリセット
            }
        }
    }
}
