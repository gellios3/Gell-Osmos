using System;
using Gameplay.Modules.Border.Api;
using Gameplay.Modules.Border.Impl;
using Gameplay.Modules.Sphere.Api;
using Gameplay.Modules.Sphere.Views;
using UnityEngine;

namespace Gameplay.Modules.Border.Views
{
    [RequireComponent(typeof(BoxCollider))]
    public class BorderView : MonoBehaviour, IBorder
    {
        [SerializeField] private BorderSide side;
        [SerializeField] private BoxCollider boxCollider;
        
        public BoxCollider Collider => boxCollider;
        public Transform Transform => transform;
        public BorderSide Side => side;
        
        private const float BounceForce = 20f;

        private void OnCollisionEnter(Collision other)
        {
            if (other?.gameObject is null)
                return;

            var sphereView = other.gameObject.GetComponent<BaseSphereView>();

            if (!(sphereView is ISphereView sphere))
                return;

            Vector3 force;

            // TODO: I can't understand why velocity in zero gravity and drag getting less per frame. I just set static bounce force. 
            switch (Side)
            {
                case BorderSide.Top:
                    force = new Vector3(0, 0, -BounceForce);

                    break;
                case BorderSide.Bottom:
                    force = new Vector3(0, 0, BounceForce);

                    break;
                case BorderSide.Right:
                    force = new Vector3(BounceForce, 0, 0);

                    break;
                case BorderSide.Left:
                    force = new Vector3(-BounceForce, 0, 0);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            sphere.ChangeVelocity(force);
        }
    }
}