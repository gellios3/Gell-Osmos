using Gameplay.Modules.Border.Api;
using Gameplay.Modules.Border.Impl;
using Gameplay.Modules.Border.Views;
using UnityEngine;
using Zenject;

namespace Gameplay.Modules.Border
{
    public class BordersInstaller : MonoInstaller
    {
        [SerializeField] private BorderView[] borders = new BorderView[4];

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InstallBordersController>().AsSingle();
            Container.BindInstance(borders).AsTransient().WhenInjectedInto<InstallBordersController>();
        }
    }
}