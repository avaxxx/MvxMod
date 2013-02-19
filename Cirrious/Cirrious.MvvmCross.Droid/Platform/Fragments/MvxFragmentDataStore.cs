using System;
using System.Collections.Generic;

namespace Cirrious.MvvmCross.Droid.Platform.Fragments
{
    /// <summary>
    /// Fragment data vault is used to store data for Fragments, which they then retrieve when they want
    /// and also monitor when the data changes.
    /// </summary>
    public sealed class MvxFragmentDataStore
    {
        private readonly Dictionary<Key, object> _values = new Dictionary<Key, object>();

        public MvxFragmentDataStore ()
        {
        }

        public void SetValue<T> (int id, T value)
            where T : class
        {
            SetValue (id, null, value);
        }

        public void SetValue<T> (string tag, T value)
            where T : class
        {
            SetValue (0, tag, value);
        }

        public void SetValue<T> (int id, string tag, T value)
            where T : class
        {
            _values [new Key (id, tag)] = value;
            OnValueChanged (id, tag, value);
        }

        public T GetValue<T> (int id)
            where T : class
        {
            return GetValue<T> (id, null);
        }

        public T GetValue<T> (string tag)
            where T : class
        {
            return GetValue<T> (0, tag);
        }

        public T GetValue<T> (int id, string tag)
            where T : class
        {
            object val;
            if (_values.TryGetValue (new Key (id, tag), out val)) {
                return val as T;
            }

            return default(T);
        }

        public void ClearValue (int id)
        {
            ClearValue (id, null);
        }

        public void ClearValue (string tag)
        {
            ClearValue (0, tag);
        }

        public void ClearValue (int id, string tag)
        {
            _values.Remove (new Key (id, tag));
            OnValueChanged (id, tag, null);
        }

        public void ClearAll() {
            _values.Clear ();
        }

        protected void OnValueChanged (int id, string tag, object value)
        {
            if (ValueChanged != null)
                ValueChanged (this, new MvxFragmentDataEventArgs (id, tag, value));
        }

        public event EventHandler<MvxFragmentDataEventArgs> ValueChanged;

        private struct Key : IEquatable<Key>
        {
            private readonly int _id;
            private readonly string _tag;
            
            public Key(int id, string tag) {
                _id = id;
                _tag = tag;
            }
            
            public int Id {
                get { return _id; }
            }
            
            public string Tag {
                get { return _tag; }
            }
            
            public override int GetHashCode ()
            {
                int hash = 17;
                hash = hash * 23 + _id;
                hash = hash * 23 + (_tag == null ? 0 : _tag.GetHashCode ());
                return hash;
            }
            
            public override bool Equals (object obj)
            {
                return obj is Key ? Equals ((Key)obj) : false;
            }
            
            public bool Equals (Key other)
            {
                return _id == other._id
                    && (_tag == other._tag || (_tag != null && _tag.Equals (other._tag)));
            }
        }
    }
}
