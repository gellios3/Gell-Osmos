using System;
using System.Collections.Generic;
using Gameplay.Modules.Sphere.Api;
using Gameplay.Modules.Sphere.Impl;
using UnityEngine;
using Zenject;

namespace Gameplay.Modules.Sphere.Views
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(MeshRenderer))]
    public abstract class BaseSphereView : MonoBehaviour, ISphereView
    {
        [SerializeField] private float radius;
        public float Radius => radius;
        public Rigidbody Rigidbody { get; private set; }
        public Transform Transform => transform;

        [Inject] private List<SphereColorItem> ColorItems { get; }
        private Material _material;

        public void Init(float startRadius)
        {
            Rigidbody = GetComponent<Rigidbody>();
            _material = GetComponent<MeshRenderer>().material;
            
            SetRadius(startRadius);
        }

        public void ChangeVelocity(Vector3 force)
        {
            Rigidbody.AddForce(force, ForceMode.VelocityChange);
        }

        public void SetRadius(float newRadius)
        {
            if (newRadius < 0.1f)
            {
                Destroy(gameObject);

                return;
            }

            radius = newRadius;
            transform.localScale = new Vector3(newRadius, newRadius, newRadius);
            UpdateColor();
        }

        public virtual void UpdateView()
        {
            
        }

        private void OnCollisionStay(Collision other)
        {
            if (other?.gameObject is null)
                return;

            var sphereView = other.gameObject.GetComponent<BaseSphereView>();

            if (!(sphereView is IEnemySphere enemySphere))
                return;

            CalcNewRadius(enemySphere);
        }

        private void UpdateColor()
        {
            var findIndex = ColorItems.FindLastIndex(item => item.radius <= Radius);

            if (findIndex != -1)
            {
                _material.color = ColorItems[findIndex].color;
            }
        }

        private void CalcNewRadius(ISphereView enemySphere)
        {
            var absorbingSpeed = Time.deltaTime * 0.9f;

            if (radius >= enemySphere.Radius)
            {
                enemySphere.SetRadius(enemySphere.Radius - absorbingSpeed);
                SetRadius(Radius + absorbingSpeed);
            }
            else
            {
                SetRadius(Radius - absorbingSpeed);
                enemySphere.SetRadius(enemySphere.Radius + absorbingSpeed);
            }
        }
    }
}