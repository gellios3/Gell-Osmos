using System.Collections.Generic;
using AppControl_Context.Modules.ScriptableObjects.Api;
using Gameplay.Modules.Sphere.Impl;
using UnityEngine;
using Zenject;

namespace AppControl_Context.Modules.ScriptableObjects.Impl
{
    /// <summary>
    /// Game save obj
    /// </summary>
    [CreateAssetMenu(fileName = "GameplaySettingsObj", menuName = "", order = 0)]
    public class GameplaySettingsObj : ScriptableObjectInstaller<GameplaySettingsObj>, IGameplaySettingsObj
    {
        public List<SphereColorItem> colorItems;
        public List<SphereColorItem> ColorItems => colorItems;
        
        public List<SphereObj> allSpheres;
        public List<ISphereObj> AllSpheres { get; private set; }

        /// <summary>
        /// Install bindings
        /// </summary>
        public override void InstallBindings()
        {
            AllSpheres = new List<ISphereObj>(allSpheres);
            
            Container.BindInstance(AllSpheres).IfNotBound();
            Container.BindInstance(ColorItems).IfNotBound();
        }
    }
}