using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInput : MonoBehaviour
{
    public float m_raycastDistance = 10f;
    public LayerMask m_targetLayers;
    
    private Camera _playerCamera;
    private readonly RaycastHit2D[] _rayCastHits = new RaycastHit2D[3];
    private bool _isTouchDown;
    private Ray _touchRay;
    
    private void OnEnable()
    {
        GameMaster.instance.GameEndEventHandler += OnGameEnd;
    }

    private void OnDisable()
    {
        GameMaster.instance.GameEndEventHandler -= OnGameEnd;
    }

    private void Awake()
    {
        _playerCamera = Camera.main;
    }

    private void Update()
    {
        if (_playerCamera == null)
        {
            _playerCamera = Camera.main;
            if (_playerCamera == null)
            {
                Debug.LogError("No camera with tag \"MainCamera\" in scene");
                _isTouchDown = false;
                return;
            }
        }

        _isTouchDown = Input.GetMouseButton(0);
        if (_isTouchDown)
        {
            _touchRay = _playerCamera.ScreenPointToRay(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        if (!_isTouchDown)
        {
            return;
        }
        
        var hitsCount = Physics2D.RaycastNonAlloc(_touchRay.origin, _touchRay.direction, _rayCastHits, m_raycastDistance, m_targetLayers);

        for (int i = 0; i < hitsCount; ++i)
        {
            var hit = _rayCastHits[i];
            var bubble = hit.transform.GetComponent<Bubble>();
            if (bubble == null)
            {
                continue;
            }
            bubble.Damage();
        }
    }
    
    private void OnGameEnd(int score)
    {
        enabled = false;
    }
}
