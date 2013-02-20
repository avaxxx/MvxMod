using System;
using Android.OS;
using Android.Support.V4.App;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.Droid.Interfaces;

namespace Cirrious.MvvmCross.Droid.Platform.Fragments
{
    public abstract class MvxFragment<TData>
        : Fragment
        where TData : class
    {
        private const string DataIdArgument = "mvxDataId";

        private IMvxAndroidFragmentView _androidView;
        private TData _data;

        public MvxFragment() : base() {
        }

        public MvxFragment (int dataId) : this() {
            var args = new Bundle ();
            args.PutInt (DataIdArgument, dataId);
            Arguments = args;
        }

        private int DataId {
            get {
                if (Arguments != null) {
                    return Arguments.GetInt(DataIdArgument, Id);
                }
                return Id;
            }
        }

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
                Data = ds.GetValue<TData> (DataId);
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
            if (_androidView == null || e.Id != DataId)
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