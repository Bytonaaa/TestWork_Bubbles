using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
{
    public Camera m_mainCamera;
    public GameMaster m_gameMaster;
    
    public override void InstallBindings()
    {
        Container.BindInstance(m_mainCamera);
        Container.BindInstance(m_gameMaster);
    }
}
