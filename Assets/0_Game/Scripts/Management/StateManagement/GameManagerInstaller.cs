using Zenject;

namespace _0_Game.Scripts.Management
{
    public class GameManagerInstaller : Installer<GameManagerInstaller>
    {
        public override void InstallBindings()
        {
            var gameState = new GameState();
            Container
                .BindInterfacesAndSelfTo<GameStateInput>()
                .AsSingle()
                .WithArguments(gameState);
            
            Container
                .BindInterfacesAndSelfTo<GameManager>()
                .AsSingle()
                .WithArguments(gameState)
                .NonLazy();
        }
    }
}