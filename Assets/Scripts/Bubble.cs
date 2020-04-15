using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Bubble : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _level;
    private float _speed;

    [Inject]
    private readonly GameMaster _gameMaster = default;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetBubbleLevel(float level)
    {
        _level = level;
        _speed = _level * (1 + _gameMaster.m_secondsLeft / _gameMaster.m_playSeconds);
        _rigidbody.velocity = new Vector2(0, _speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Border"))
        {
            Destroy(gameObject);
        }
    }

    public void Damage()
    {
        _gameMaster.m_score += Mathf.FloorToInt(_level);
        Destroy(gameObject);
    }
}
