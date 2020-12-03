using UnityEngine;

namespace AppControl_Context.Modules.ScriptableObjects.Api
{
    public interface ISphereObj
    {
        Vector2 StartPosition { get; }
        bool IsPlayable { get; }
        float StartRadius { get; }
    }
}