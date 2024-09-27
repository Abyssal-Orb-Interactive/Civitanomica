using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AtomicFramework.AtomicObject
{
    [AddComponentMenu("Atomic/Atomic Object")]
    public class AtomicObjectBase : MonoBehaviour, IAtomicObject
    {
        [Title("Data"), PropertySpace, PropertyOrder(100)]
        [ShowInInspector, HideInEditorMode]
        protected internal ISet<string> _types = new HashSet<string>();
        
        [ShowInInspector, HideInEditorMode, PropertyOrder(100)]
        protected internal IDictionary<string, object> _references = new Dictionary<string, object>();

        
        public bool Is(string type)
        {
            return _types.Contains(type);
        }

        public T Get<T>(string key) where T : class
        {
            if (_references.TryGetValue(key, out var value))
            {
                return value as T;
            }

            return default;
        }
        
        public bool TryGet<T>(string key, out T result) where T : class
        {
            if (_references.TryGetValue(key, out var value))
            {
                result = value as T;
                return true;
            }

            result = default;
            return false;
        }

        public object Get(string key)
        {
            return _references.TryGetValue(key, out var value) ? value : default;
        }

        public bool TryGet(string key, out object result)
        {
            return _references.TryGetValue(key, out result);
        }

        public IEnumerable<string> Types()
        {
            return _types;
        }

        public IEnumerable<KeyValuePair<string, object>> GetAll()
        {
            return _references;
        }

        public bool AddData(string key, object value)
        {
            return _references.TryAdd(key, value);
        }

        public void SetData(string key, object value)
        {
            _references[key] = value;
        }

        public bool RemoveData(string key)
        {
            return _references.Remove(key);
        }

        public void OverrideData(string key, object value, out object prevValue)
        {
            _references.TryGetValue(key, out prevValue);
            _references[key] = value;
        }

        public bool AddType(string type)
        {
            return _types.Add(type);
        }

        public void AddTypes(IEnumerable<string> types)
        {
            _types.UnionWith(types);
        }

        public bool RemoveType(string type)
        {
            return _types.Remove(type);
        }

        public void RemoveTypes(IEnumerable<string> types)
        {
            foreach (var type in types)
            {
                _types.Remove(type);
            }
        }
    }
}