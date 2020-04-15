using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BubblesSpawner : MonoBehaviour
{
    public GameObject m_bubblePrefab;
    public float m_minSecondsDelay;
    public float m_maxSecondsDelay;
    [Range(2, 100)]
    public float m_maxLevel;
    public float m_minSize;
    public float m_maxSize;

    public Vector2 m_spawnPoint;
    public float m_spawnLineHalfWidth = 1f;
    
    public void Awake()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private void OnEnable()
    {
        GameMaster.instance.GameEndEventHandler += OnGameEnd;
    }

    private void OnDisable()
    {
        GameMaster.instance.GameEndEventHandler -= OnGameEnd;
    }

    private void OnGameEnd(int score)
    {
        enabled = false;
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            var instantiatePoint =
                m_spawnPoint + Vector2.right * Random.Range(-m_spawnLineHalfWidth, m_spawnLineHalfWidth);
            var bubble = Instantiate(m_bubblePrefab, instantiatePoint, Quaternion.identity);
            var level = Random.Range(1, m_maxLevel);
            bubble.GetComponent<Bubble>().SetBubbleLevel(level);

            var size = Mathf.Lerp(m_maxSize, m_minSize, level / m_maxLevel);
            bubble.transform.localScale = new Vector3(size, size, size);
            
            var delay = Random.Range(m_minSecondsDelay, m_maxSecondsDelay);
            yield return new WaitForSeconds(delay);
        }
    }
}
