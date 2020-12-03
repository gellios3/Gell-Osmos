using Gameplay.Modules.Border.Impl;
using UnityEngine;

namespace Gameplay.Modules.Border.Api
{
    public interface IBorder
    {
        BoxCollider Collider { get; }
        Transform Transform { get; }
        BorderSide Side { get; }
    }
}