using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bubble : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _level;
    private float _speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetBubbleLevel(float level)
    {
        _level = level;
        var gameMaster = GameMaster.instance;
        _speed = _level * (1 + gameMaster.m_secondsLeft / gameMaster.m_playSeconds);
        _rigidbody.velocity = new Vector2(0, _speed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag.Equals("BORDER"))
        {
            Destroy(gameObject);
        }
    }

    public void Damage()
    {
        GameMaster.instance.m_score += Mathf.FloorToInt(_level);
        Destroy(gameObject);
    }
}
