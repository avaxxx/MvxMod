using System;
using Android.Support.V4.App;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.Droid.Interfaces;

namespace Cirrious.MvvmCross.Droid.Platform.Fragments
{
    public abstract class MvxFragment<TData>
        : Fragment
        where TData : class
    {
        private IMvxAndroidFragmentView _androidView;
        private TData _data;

        public TData Data {
            get { return _data; }
            private set {
                if (_data != value) {
                    _data = value;
                    OnDataChanged ();
                }
            }
        }

        public override void OnAttach (Android.App.Activity activity)
        {
            base.OnAttach (activity);

            _androidView = activity as IMvxAndroidFragmentView;
            if (_androidView != null) {
                var ds = _androidView.FragmentDataStore;
                ds.ValueChanged += HandleValueChanged;
                Data = ds.GetValue<TData> (Id, Tag);
            }
        }

        public override void OnDetach ()
        {
            if (_androidView != null) {
                _androidView.FragmentDataStore.ValueChanged -= HandleValueChanged;
                Data = null;
            }

            base.OnDetach ();
        }

        private void HandleValueChanged (object sender, MvxFragmentDataEventArgs e)
        {
            if (_androidView == null || !e.Check (Id, Tag))
                return;

            Data = e.Value as TData;
        }

        /// <summary>
        /// Internal hook for reacting to data changes. This should be used to hide/show content
        /// in the fragment depending what data is available.
        /// </summary>
        protected virtual void OnDataChanged ()
        {
        }
    }
}