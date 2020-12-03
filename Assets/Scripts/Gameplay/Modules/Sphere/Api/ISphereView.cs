using UnityEngine;

namespace Gameplay.Modules.Sphere.Api
{
    public interface ISphereView
    {
        float Radius { get; }
        Transform Transform { get; }
        Rigidbody Rigidbody { get; }
        void ChangeVelocity(Vector3 force);
        void SetRadius(float radius);
        
        void UpdateView();
        
    }
}