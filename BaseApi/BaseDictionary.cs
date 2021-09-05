using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Api
{
    public class BaseDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        #region PROPS
        public ICollection<TKey> Keys => _dictionary.Keys;

        public ICollection<TValue> Values => _dictionary.Values;

        public int Count => _dictionary.Count;

        public bool IsReadOnly => false;

        #endregion

        #region FIELDS

        private Dictionary<TKey, TValue> _dictionary;

        #endregion

        #region CONSTRUCTORS

        public BaseDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        public BaseDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary);
        }

        #endregion

        #region INDEXERS

        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set
            {
                if (_dictionary.ContainsKey(key))
                    _dictionary[key] = value;
                else Add(key, value);
            }
        }

        #endregion

        #region PUBLIC METHODS

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
            ItemAdded?.Invoke( key);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            bool removed = _dictionary.Remove(key);
            ItemRemoved?.Invoke(key);
            return removed;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region EVENTS & DELEGS

        public delegate void ItemHandler(TKey key);

        public event ItemHandler ItemAdded;

        public event ItemHandler ItemRemoved;
                
        #endregion
    }
}
