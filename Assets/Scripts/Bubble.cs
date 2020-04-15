using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bubble : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private float _level;
    private float _speed;

    [Inject]
    private readonly GameMaster _gameMaster = default;

    [Inject] 
    private readonly BubblesPoolMaster _bubblesPoolMaster = default;

    private const float SPEED_SCALE = 0.5f; //Общий скейл скорости
    private const float END_GAME_BUBBLES_SPEED_SCALE = 5f; //Во сколько раз скорость шариков одного уровня в конце игры будет больше, чем в начале
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetBubbleLevel(float level)
    {
        _level = level;
        
        _speed = _level * (1 + (END_GAME_BUBBLES_SPEED_SCALE - 1) * _gameMaster.m_secondsLeft / _gameMaster.m_playSeconds); 
        
        _rigidbody.velocity = new Vector2(0, _speed * SPEED_SCALE); 
        _spriteRenderer.color = new Color(Random.value * 0.5f + 0.5f, Random.value * 0.5f + 0.5f, Random.value * 0.5f + 0.5f); //Чтобы не было темных цветов. На фоне черного не очень смотрятся :)
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Border"))
        {
            _bubblesPoolMaster.InsertBubble(gameObject);
        }
    }

    public void Damage()
    {
        _gameMaster.m_score += Mathf.CeilToInt(_level);
        _bubblesPoolMaster.InsertBubble(gameObject);
    }
}
