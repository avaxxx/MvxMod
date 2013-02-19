using System;
using Cirrious.MvvmCross.Droid.Platform.Fragments;

namespace Cirrious.MvvmCross.Droid.Interfaces
{
    public interface IMvxFragmentDataStore
    {
        void SetValue<T> (int id, T value) where T : class;
        void SetValue<T> (string tag, T value) where T : class;
        void SetValue<T> (int id, string tag, T value) where T : class;

        T GetValue<T> (int id) where T : class;
        T GetValue<T> (string tag) where T : class;
        T GetValue<T> (int id, string tag) where T : class;

        void ClearValue (int id);
        void ClearValue (string tag);
        void ClearValue (int id, string tag);

        void ClearAll();

        event EventHandler<MvxFragmentDataEventArgs> ValueChanged;
    }
}