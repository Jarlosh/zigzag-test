using Zenject;

namespace _0_Game.Scripts.Management.Score
{
    public class ScoreInstaller : Installer<ScoreInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ScoreSystem>().AsSingle();
        }
    }
}