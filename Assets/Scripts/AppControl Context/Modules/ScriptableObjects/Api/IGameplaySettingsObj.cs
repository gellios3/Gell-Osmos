using System.Collections.Generic;
using Gameplay.Modules.Sphere.Impl;

namespace AppControl_Context.Modules.ScriptableObjects.Api
{
    public interface IGameplaySettingsObj
    {
        List<ISphereObj> AllSpheres { get; }
        List<SphereColorItem> ColorItems { get; }
        
    }
}