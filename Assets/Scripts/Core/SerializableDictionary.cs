using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
    {
        [SerializeField] private List<KeyValuePair<TKey, TValue>> _keyValuePairs = new();

        private Dictionary<TKey, TValue> _dictionary = new();

        public Dictionary<TKey, TValue> Dictionary => _dictionary;

        public void OnBeforeSerialize()
        {
            _keyValuePairs.Clear();
            foreach (var kvp in _dictionary)
            {
                _keyValuePairs.Add(new KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value));
            }
        }

        public void OnAfterDeserialize()
        {
            _dictionary.Clear();
            foreach (var kvp in _keyValuePairs)
            {
                if (!_dictionary.ContainsKey(kvp.Key))
                {
                    _dictionary.Add(kvp.Key, kvp.Value);
                }
            }
        }
    }
    
    [System.Serializable]
    public class KeyValuePair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public KeyValuePair() { }

        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}