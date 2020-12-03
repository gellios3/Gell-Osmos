using AppControl_Context.Modules.ScriptableObjects.Api;
using Gameplay.Modules.Playfield.Impl;
using Gameplay.Modules.Sphere.Views;
using UnityEngine;
using Zenject;

namespace Gameplay.Modules.Playfield
{
    public class PlayfieldInstaller : MonoInstaller
    {
        [SerializeField] private GameObject playableSphere;
        [SerializeField] private GameObject enemySphere;

        public override void InstallBindings()
        {
            Container.BindInstance(transform).AsTransient().WhenInjectedInto<SphereSpawner>();
            Container.BindInterfacesAndSelfTo<SphereSpawner>().AsSingle();

            Container.BindFactory<PlayableSphereView, PlayableSphereView.Factory>()
                .FromComponentInNewPrefab(playableSphere);

            Container.BindFactory<EnemySphereView, EnemySphereView.Factory>()
                .FromComponentInNewPrefab(enemySphere);
        }
    }
}