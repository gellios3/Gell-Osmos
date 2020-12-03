using Gameplay.Modules.Gui.Signals;
using Gameplay.Modules.Sphere.Api;
using UnityEngine;
using Zenject;

namespace Gameplay.Modules.Sphere.Views
{
    public class PlayableSphereView : BaseSphereView, IPlayableSphere
    {
        [SerializeField] private float clickForce = 500f;
        [Inject] private SignalBus SignalBus { get; }
        private Plane _plane = new Plane(Vector3.up, Vector3.zero);

        public override void UpdateView()
        {
            if (!Input.GetMouseButtonDown(0) || Camera.main is null)
                return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!_plane.Raycast(ray, out var enter))
                return;

            var hitPoint = ray.GetPoint(enter);
            var mouseDir = hitPoint - gameObject.transform.position;
            mouseDir = mouseDir.normalized;

            ChangeVelocity(mouseDir * Time.deltaTime * -clickForce);
        }

        private void OnDestroy()
        {
            SignalBus.Fire(new ShowMessagePopupSignal("You Lose !!!"));
        }

        public class Factory : PlaceholderFactory<PlayableSphereView>
        {
        }
    }
}