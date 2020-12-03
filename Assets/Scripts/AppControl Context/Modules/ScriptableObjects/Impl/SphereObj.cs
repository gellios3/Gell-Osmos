using AppControl_Context.Modules.ScriptableObjects.Api;
using UnityEngine;

namespace AppControl_Context.Modules.ScriptableObjects.Impl
{
    [CreateAssetMenu(fileName = "SphereObj", menuName = "", order = 0)]
    public class SphereObj : ScriptableObject, ISphereObj
    {
        public Vector2 startPosition;
        public Vector2 StartPosition => startPosition;
        
        public bool isPlayable;
        public bool IsPlayable => isPlayable;

        public float startRadius;
        public float StartRadius => startRadius;
    }
}