using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Scarcity
{
    [RequireComponent(typeof(CompositeCollider2D))]
    [RequireComponent(typeof(CompositeShadowCaster2D))]
    public class TilemapCompositeColliderShadowCaster2D : MonoBehaviour
    {
        public CompositeCollider2D compositeCollider;
        public ShadowCaster2D shadowCasterPrefab;

        private static readonly FieldInfo shapePathInfo = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo shapePathHashInfo = typeof(ShadowCaster2D).GetField("m_ShapePathHash", BindingFlags.Instance | BindingFlags.NonPublic);

        private void Reset()
        {
            compositeCollider = GetComponent<CompositeCollider2D>();
        }

        private void Start()
        {
            if (!shadowCasterPrefab || !compositeCollider) return;

            for (int i = 0; i < compositeCollider.pathCount; i++)
            {
                Vector2[] path = new Vector2[compositeCollider.GetPathPointCount(i)];
                compositeCollider.GetPath(i, path);

                ShadowCaster2D shadowCaster = Instantiate(shadowCasterPrefab, transform);

                shapePathInfo.SetValue(shadowCaster, Array.ConvertAll<Vector2, Vector3>(path, point => point));
                shapePathHashInfo.SetValue(shadowCaster, -1); // Invalidates the path hash, causing a recalculation.
            }
        }
    }
}
