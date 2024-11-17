using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Scarcity
{
    public class ShadowCasterVisibilityHandler : MonoBehaviour
    {
        public Transform viewer;
        public ShadowCaster2D[] shadowCasters;

        public void Start()
        {
            shadowCasters = GetComponentsInChildren<ShadowCaster2D>();
        }

        public void Update()
        {
            Vector2 viewPoint = viewer.position;

            foreach (var shadowCaster in shadowCasters)
            {
                Vector2 casterPoint = (Vector2)shadowCaster.transform.position;

                Collider2D hit = Physics2D.Raycast(casterPoint, viewPoint - casterPoint).collider;

                bool hidden = !hit || hit.transform != viewer;

                if (shadowCaster.selfShadows == hidden) continue;
                shadowCaster.selfShadows = hidden;
            }
        }
    }
}
