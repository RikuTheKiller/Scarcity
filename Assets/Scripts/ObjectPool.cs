using System.Collections.Generic;
using UnityEngine;

namespace Scarcity
{
    [CreateAssetMenu(fileName = "Object Pool", menuName = "Object Pool")]
    public class ObjectPool : ScriptableObject
    {
        public Object prefab;
        public Transform container;

        private readonly List<Object> pool = new();

        private void OnDisable()
        {
            pool.Clear();
            container = null;
        }

        public T Get<T>() where T : Component
        {
            if (container == null)
            {
                container = new GameObject($"Object Pool ({prefab.name})").transform;
                container.gameObject.SetActive(false);
            }

            if (pool.Count == 0)
            {
                pool.Add(Instantiate(prefab, container));
            }

            T instance = (T)pool[0];
            instance.transform.parent = null;
            pool.RemoveAt(0);
            return instance;
        }

        public void Release(Object instance)
        {
            pool.Add(instance);

            if (instance is GameObject obj)
            {
                obj.transform.parent = container;
            }
            else if (instance is Component component)
            {
                component.transform.parent = container;
            }
        }
    }
}
