using System;
using Gameplay.Modules.Border.Api;
using Gameplay.Modules.Border.Views;
using UnityEngine;
using Zenject;

namespace Gameplay.Modules.Border.Impl
{
    public class InstallBordersController : IInitializable
    {
        [Inject] private BorderView[] Borders { get; }
        
        public void Initialize()
        {
            if (Camera.main is null)
                return;

            var mainCamera = Camera.main;
            var bottomLeftScreenPoint = mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
            var topRightScreenPoint = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            foreach (var border in Borders)
            {
                CalcBorderPosition(border, bottomLeftScreenPoint, topRightScreenPoint);
            }
        }

        private void CalcBorderPosition(IBorder borderView, Vector3 bottomLeftScreenPoint, Vector3 topRightScreenPoint)
        {
            var xDelta = bottomLeftScreenPoint.x - topRightScreenPoint.x;
            var zDelta = topRightScreenPoint.z - bottomLeftScreenPoint.z;

            Vector3 size;

            switch (borderView.Side)
            {
                case BorderSide.Top:

                    size = new Vector3(Mathf.Abs(xDelta), 0.5f, 2f);
                    borderView.Collider.size = size;
                    borderView.Collider.center = new Vector2(size.x / 2f, size.z / 2f);
                    borderView.Transform.position = new Vector3(xDelta / 2f, 0, topRightScreenPoint.z);

                    break;
                case BorderSide.Bottom:
                    size = new Vector3(Mathf.Abs(xDelta), 0.5f, 2f);
                    borderView.Collider.size = size;
                    borderView.Collider.center = new Vector2(size.x / 2f, size.z / 2f);
                    borderView.Transform.position = new Vector3(xDelta / 2f, 0, bottomLeftScreenPoint.z - size.z);

                    break;
                case BorderSide.Right:
                    size = new Vector3(0.5f, 2f, Mathf.Abs(zDelta));
                    borderView.Collider.size = size;
                    borderView.Collider.center = new Vector3(size.x / 2f, 0, size.z / 2f);
                    borderView.Transform.position = new Vector3(xDelta / 2f - size.x, 0, bottomLeftScreenPoint.z);

                    break;
                case BorderSide.Left:
                    size = new Vector3(0.5f, 2f, Mathf.Abs(zDelta));
                    borderView.Collider.size = size;
                    borderView.Collider.center = new Vector3(size.x / 2f, 0, size.z / 2f);
                    borderView.Transform.position = new Vector3(topRightScreenPoint.x, 0, bottomLeftScreenPoint.z);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}