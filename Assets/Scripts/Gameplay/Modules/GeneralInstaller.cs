using Gameplay.Modules.Gui.Signals;
using Zenject;

namespace Gameplay.Modules
{
    public class GeneralInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<ShowMessagePopupSignal>();
        }
    }
}