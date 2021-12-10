using Zenject;

namespace Scripts
{
    public class PlayerInstaller : MonoInstaller
    {
        public PlayerData playerData;
        public PlayerMovement.Settings moveSettings;
        public PlayerCollisions.LayerConfig collisionLayers;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerMovement>()
                .AsSingle()
                .WithArguments(moveSettings, playerData);
            
            Container
                .BindInterfacesAndSelfTo<PlayerCollisions>()
                .AsSingle()
                .WithArguments(collisionLayers);
            
            Container
                .BindInterfacesTo<PlayerController>()
                .AsSingle();
        }
    }
}