using System;
using Android.OS;
using Android.Support.V4.App;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.Droid.Interfaces;

namespace Cirrious.MvvmCross.Droid.Platform.Fragments
{
    /// <summary>
    /// Base class for fragments which use <see cref="IMvxFragmentDataStore"/> to look up data to operate with.
    /// </summary>
    public abstract class MvxFragment<TData>
        : Fragment
        where TData : class
    {
        private const string DataIdArgument = "mvxDataId";

        private IMvxAndroidFragmentView _androidView;
        private TData _data;

        /// <summary>
        /// Initializes the class to use the fragment <see cref="Id"/> as the data Id when looking up
        /// data from <see cref="IMvxFragmentDataStore"/>.
        /// This constructor is also used by Android framework to restore the fragments.
        /// </summary>
        public MvxFragment() : base() {
        }

        /// <summary>
        /// Initializes the class with <paramref name="dataId"/> for looking up data for the fragment.
        /// </summary>
        /// <param name="dataId">
        /// Data Id for the fragment, used to look up data from <see cref="IMvxFragmentDataStore"/>.
        /// </param>
        public MvxFragment (int dataId) : this() {
            var args = new Bundle ();
            args.PutInt (DataIdArgument, dataId);
            Arguments = args;
        }

        /// <summary>
        /// Data Id for the fragment used to look up the data from <see cref="IMvxFragmentDataStore"/>.
        /// </summary>
        /// <value>The data Id for the fragment. If the data Id is not set it returns the fragment Id.</value>
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