using Gameplay.Modules.Sphere.Api;
using Zenject;

namespace Gameplay.Modules.Sphere.Views
{
    public class EnemySphereView : BaseSphereView, IEnemySphere
    {
        [Inject]
        public void Construct( )
        {
        }

        public class Factory : PlaceholderFactory<EnemySphereView>
        {
        }
    }
}