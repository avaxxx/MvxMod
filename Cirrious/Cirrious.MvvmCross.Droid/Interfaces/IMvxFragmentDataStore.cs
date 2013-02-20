using System;
using Cirrious.MvvmCross.Droid.Platform.Fragments;

namespace Cirrious.MvvmCross.Droid.Interfaces
{
    public interface IMvxFragmentDataStore
    {
        void SetValue<T> (int id, T value) where T : class;
        T GetValue<T> (int id) where T : class;
        void ClearValue (int id);
        void ClearAll();

        event EventHandler<MvxFragmentDataEventArgs> ValueChanged;
    }
}