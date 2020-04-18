using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public class ObjectPooler<T> where T : Pooleable
    {
        private List<T> _objects;
        private int _initialQuantity;
        private GameObject _parent;
        private T _objectToPool;
        private Action<List<T>> _onGrow;

        public List<T> Objects => _objects;

        /// <summary>
        /// Instantiates game objects with the given <paramref name="pooleable"/> and <paramref name="quantity"/>.
        /// The new gameObjects will have their parent called <paramref name="parentName"/>
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="pooleable"></param>
        /// <param name="parentName"></param>
        /// <param name="grow">Method that will be executed when the pool grows.</param>
        public void InstantiateObjects(int quantity, T pooleable, string parentName,
            Action<List<T>> grow = null)
        {
            _initialQuantity = quantity;
            _parent = new GameObject
            {
                name = parentName
            };
            _objectToPool = pooleable;
            _objects = new List<T>();
            _onGrow = grow;
            Grow();
        }

        /// <summary>
        /// <para>Returns the next available Pooleable. If there is no Pooleable deactivated then it will
        /// instantiate more</para>
        /// </summary>
        /// <returns></returns>
        public T GetNextObject()
        {
            while (true)
            {
                foreach (var pooleable in _objects)
                {
                    if (pooleable.IsActive) continue;
                
                    pooleable.Activate();
                    return pooleable;
                }
                Grow(); 
            }
        }

        private void Grow()
        {
            var quantity = _objects.Count;
            for (var i = 0; i < _initialQuantity; i++)
            {
                var newObject = Object.Instantiate(_objectToPool, _parent.transform, true);
                newObject.gameObject.SetActive(false);
                _objects.Add(newObject);
            }
            _onGrow?.Invoke(_objects.GetRange(quantity, _initialQuantity));
        }

        /// <summary>
        /// <para>Deactivates all the Pooleables</para>
        /// </summary>
        public void DeactivatePooleables()
        {
            foreach (var pooleable in _objects)
            {
                pooleable.Deactivate();
            }
        }
    }
}