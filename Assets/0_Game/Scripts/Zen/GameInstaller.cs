using _0_Game.Scripts.Management;
using _0_Game.Scripts.Management.Score;
using Zenject;

namespace _0_Game.Scripts.Zen
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallSignals();
            ScoreInstaller.Install(Container);
            GameManagerInstaller.Install(Container);
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            var bus = Container.Resolve<SignalBus>();
            bus.DeclareSignal<DeathSignal>();
            bus.DeclareSignal<GameOverSignal>();
            bus.DeclareSignal<GameStartSignal>();
            bus.DeclareSignal<ItemCollectedSignal>();
        }
    }
}