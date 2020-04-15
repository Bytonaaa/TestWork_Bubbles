using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BubblesPoolMaster
{
    private readonly GameObject _bubblePrefab;

    public BubblesPoolMaster(GameObject bubblePrefab)
    {
        _bubblePrefab = bubblePrefab;
    }

    private readonly Stack<GameObject> _bubbles = new Stack<GameObject>();

    [Inject] 
    private readonly DiContainer _container = default;
    
    public GameObject GetBubble()
    {
        if (_bubbles.Count <= 0)
        {
            return _container.InstantiatePrefab(_bubblePrefab);
        }

        var bubble = _bubbles.Pop();
        bubble.SetActive(true);
        return bubble;
    }

    public void InsertBubble(GameObject bubble)
    {
        bubble.SetActive(false);
        _bubbles.Push(bubble);
    }
}
