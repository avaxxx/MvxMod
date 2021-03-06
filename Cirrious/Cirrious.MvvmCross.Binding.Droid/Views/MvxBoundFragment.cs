using System;
using Android.App;
using Android.OS;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.ExtensionMethods;
using Cirrious.MvvmCross.Binding.Droid.Interfaces.Binders;
using Cirrious.MvvmCross.Binding.Droid.Interfaces.Views;
using Cirrious.MvvmCross.Droid.Platform.Fragments;
using Cirrious.MvvmCross.Droid.Views;

namespace Cirrious.MvvmCross.Binding.Droid.Views
{
    /// <summary>
    /// Bound fragment makes creating bound fragments trivial.
    /// </summary>
    public class MvxBoundFragment : MvxFragment<object>
    {
        private const string LayoutIdArgument = "layoutId";

        private IMvxViewBindingManager _bindingManager;

        /// <summary>
        /// Internal constructor for the Android framework to restore the fragment.
        /// </summary>
        public MvxBoundFragment () : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MvxBoundFragment"/> class with specified
        /// layout resource.
        /// </summary>
        /// <param name="layoutId">Layout resource id to use for this fragment.</param>
        public MvxBoundFragment(int dataId, int layoutId) : base(dataId) {
            Arguments.PutInt (LayoutIdArgument, layoutId);
        }

        protected IMvxViewBindingManager BindingManager {
            get { return _bindingManager; }
        }

        protected int LayoutId {
            get {
                if (Arguments != null) {
                    return Arguments.GetInt(LayoutIdArgument, 0);
                }
                return 0;
            }
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedState)
        {
            var root = inflater.Inflate (LayoutId, container, false);
            // Create the data source boundary:
            if (BindingManager != null && Data != null) {
                BindingManager.BindView (root, Data);
            } else {
                root.UpdateDataSource(Data);
            }
            return root;
        }

        public override void OnAttach (Activity activity)
        {
            base.OnAttach (activity);

            var bindingActivity = activity as IMvxBindingActivity;
            if (bindingActivity != null) {
                _bindingManager = bindingActivity.BindingManager;
            }
        }

        public override void OnDetach ()
        {
            if (_bindingManager != null) {
                _bindingManager.UnbindView(View);
                _bindingManager = null;
            }

            base.OnDetach ();
        }

        protected override void OnDataChanged ()
        {
            base.OnDataChanged ();

            if (View != null) {
                if (BindingManager != null) {
                    BindingManager.BindView(View, Data);
                } else {
                    View.UpdateDataSource(Data);
                }
            }
        }
    }
}