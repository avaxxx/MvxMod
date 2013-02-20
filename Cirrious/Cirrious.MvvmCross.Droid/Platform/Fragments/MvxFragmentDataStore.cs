using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Droid.Interfaces;

namespace Cirrious.MvvmCross.Droid.Platform.Fragments
{
    /// <summary>
    /// Fragment data vault is used to store data for Fragments, which they then retrieve when they want
    /// and also monitor when the data changes.
    /// </summary>
    public sealed class MvxFragmentDataStore : IMvxFragmentDataStore
    {
        private readonly Dictionary<int, object> _values = new Dictionary<int, object>();

        public MvxFragmentDataStore ()
        {
        }

        public void SetValue<T> (int id, T value)
            where T : class
        {
            _values [id] = value;
            OnValueChanged (id, value);
        }

        public T GetValue<T> (int id)
            where T : class
        {
            object val;
            if (_values.TryGetValue (id, out val)) {
                return val as T;
            }

            return default(T);
        }

        public void ClearValue (int id)
        {
            _values.Remove (id);
            OnValueChanged (id, null);
        }

        public void ClearAll() {
            _values.Clear ();
        }

        protected void OnValueChanged (int id, object value)
        {
            if (ValueChanged != null)
                ValueChanged (this, new MvxFragmentDataEventArgs (id, value));
        }

        public event EventHandler<MvxFragmentDataEventArgs> ValueChanged;
    }
}
