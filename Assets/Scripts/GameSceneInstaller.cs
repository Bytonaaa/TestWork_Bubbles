using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
{
    public Camera m_mainCamera;
    public GameMaster m_gameMaster;
    public GameObject m_bubblePrefab;
    
    public override void InstallBindings()
    {
        Container.BindInstance(m_mainCamera);
        Container.BindInstance(m_gameMaster);
        var bubblesPoolMaster = new BubblesPoolMaster(m_bubblePrefab);
        Container.BindInstance(bubblesPoolMaster);
        Container.QueueForInject(bubblesPoolMaster);
    }
}
