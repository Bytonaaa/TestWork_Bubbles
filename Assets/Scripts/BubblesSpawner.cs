using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class BubblesSpawner : MonoBehaviour
{
    public GameObject m_bubblePrefab;
    public float m_minSecondsDelay = 0.2f;
    public float m_maxSecondsDelay = 1f;
    [Range(1f, 10f)]
    public float m_maxLevel = 2.5f;
    public float m_minSize = 0.5f;
    public float m_maxSize = 1.5f;

    public float m_spawnYPos;
    
    private float _spawnHalfWidth = -10f;

    [Inject] 
    private readonly GameMaster _gameMaster = default;

    [Inject] 
    private readonly Camera _mainCamera = default;

    [Inject] 
    private readonly DiContainer _container = default;
    
    private void OnEnable()
    {
        _gameMaster.GameEndEventHandler += OnGameEnd;
    }

    private void Start()
    {
        _spawnHalfWidth = _mainCamera.orthographicSize * _mainCamera.aspect;
        StartCoroutine(SpawnCoroutine());
    }

    private void OnDisable()
    {
        _gameMaster.GameEndEventHandler -= OnGameEnd;
    }

    private void OnGameEnd(int score)
    {
        enabled = false;
        StopAllCoroutines();
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            var level = Random.Range(0.5f, m_maxLevel);
            var size = Mathf.Lerp(m_maxSize, m_minSize, level / m_maxLevel);

            var spawnHalfWidth =
                _spawnHalfWidth - size * 0.6f; //Используем 0.6, а не 0.5, чтобы шар даже не косался границы экрана 

            var instantiatePoint = new Vector2(Random.Range(-spawnHalfWidth, spawnHalfWidth), m_spawnYPos);
            var bubble = _container.InstantiatePrefab(m_bubblePrefab, instantiatePoint, Quaternion.identity, null);
            bubble.GetComponent<Bubble>().SetBubbleLevel(level);

            bubble.transform.localScale = new Vector3(size, size, size);

            var delay = Random.Range(m_minSecondsDelay, m_maxSecondsDelay);
            yield return new WaitForSeconds(delay);
        }
        // ReSharper disable once IteratorNeverReturns
    }
}
