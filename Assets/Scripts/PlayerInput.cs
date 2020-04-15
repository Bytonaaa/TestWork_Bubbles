using UnityEngine;
using Zenject;

public class PlayerInput : MonoBehaviour
{
    public float m_raycastDistance = 10f;
    public LayerMask m_targetLayers;
    
    private readonly RaycastHit2D[] _rayCastHits = new RaycastHit2D[3];
    private bool _isTouchDown;
    private Ray _touchRay;

    [Inject] private readonly GameMaster _gameMaster = default;

    [Inject] private readonly Camera _camera = default;
    
    private void OnEnable()
    {
        _gameMaster.GameEndEventHandler += OnGameEnd;
    }

    private void OnDisable()
    {
        _gameMaster.GameEndEventHandler -= OnGameEnd;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        
        _touchRay = _camera.ScreenPointToRay(Input.mousePosition);
            
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
