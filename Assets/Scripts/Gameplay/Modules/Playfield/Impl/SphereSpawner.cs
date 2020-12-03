using System.Collections.Generic;
using AppControl_Context.Modules.ScriptableObjects.Api;
using Gameplay.Modules.Gui.Signals;
using Gameplay.Modules.Sphere.Views;
using UnityEngine;
using Zenject;

namespace Gameplay.Modules.Playfield.Impl
{
    public class SphereSpawner : IInitializable, ITickable
    {
        [Inject] private Transform Parent { get; }
        [Inject] private List<ISphereObj> AllSpheres { get; }
        [Inject] private SignalBus SignalBus { get; }

        private List<BaseSphereView> SpawnedSpheres { get; } = new List<BaseSphereView>();

        private EnemySphereView.Factory EnemySphereViewFactory { get; }
        private PlayableSphereView.Factory PlayableSphereViewFactory { get; }

        public SphereSpawner(EnemySphereView.Factory enemySphereViewFactory,
            PlayableSphereView.Factory playableSphereViewFactory)
        {
            EnemySphereViewFactory = enemySphereViewFactory;
            PlayableSphereViewFactory = playableSphereViewFactory;
        }

        public void Initialize()
        {
            foreach (var sphere in AllSpheres)
            {
                SpawnSphere(sphere);
            }
        }

        private void SpawnSphere(ISphereObj sphereObj)
        {
            var sphereView = CreateSphereView(sphereObj);

            var transform = sphereView.Transform;

            transform.parent = Parent;
            transform.localPosition = new Vector3(sphereObj.StartPosition.x, 0, sphereObj.StartPosition.y);
            transform.rotation = new Quaternion(0, 0, 0, 0);

            sphereView.Init(sphereObj.StartRadius);

            SpawnedSpheres.Add(sphereView);
        }

        private BaseSphereView CreateSphereView(ISphereObj sphereObj)
        {
            if (sphereObj.IsPlayable)
            {
                return PlayableSphereViewFactory.Create();
            }

            return EnemySphereViewFactory.Create();
        }

        public void Tick()
        {
            var removeIndex = -1;

            for (var i = 0; i < SpawnedSpheres.Count; i++)
            {
                if (SpawnedSpheres[i] != null)
                {
                    SpawnedSpheres[i].UpdateView();
                }
                else
                {
                    removeIndex = i;
                }
            }

            if (removeIndex > -1)
            {
                SpawnedSpheres.RemoveAt(removeIndex);
            }

            if (SpawnedSpheres.Count == 1)
            {
                SignalBus.Fire(SpawnedSpheres[0] is PlayableSphereView
                    ? new ShowMessagePopupSignal("You Win !!!")
                    : new ShowMessagePopupSignal("You Lose !!!"));
            }
        }
    }
}