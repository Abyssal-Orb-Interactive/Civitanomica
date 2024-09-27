using System.Collections.Generic;
using UnityEngine;

namespace AtomicFramework.AtomicObject
{
    public class AtomicProxy : MonoBehaviour, IAtomicObject
    {
        [SerializeField]
        public AtomicObject _source = null;

        public T Get<T>(string key) where T : class
        {
            return _source.Get<T>(key);
        }

        public bool TryGet<T>(string key, out T result) where T : class
        {
            return _source.TryGet(key, out result);
        }

        public object Get(string key)
        {
            return _source.Get(key);
        }

        public bool TryGet(string key, out object result)
        {
            return _source.TryGet(key, out result);
        }

        public IEnumerable<string> Types()
        {
            return _source.Types();
        }

        public IEnumerable<KeyValuePair<string, object>> GetAll()
        {
            return _source.GetAll();
        }

        public bool Is(string type)
        {
            return _source.Is(type);
        }
    }
}