using Unity.VisualScripting;
using UnityEngine;
namespace Game.Core
{
    public static class VectorExt
    {
        public static Vector3 FlattenY(this Vector3 v)
        {
            return new Vector3(v.x, 0f, v.z);
        }

        public static Vector3 WithY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }
    }
}
