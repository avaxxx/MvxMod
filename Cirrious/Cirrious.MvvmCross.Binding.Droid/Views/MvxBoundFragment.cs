using System;
using Android.App;
using Android.OS;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.ExtensionMethods;
using Cirrious.MvvmCross.Binding.Droid.Interfaces.Binders;
using Cirrious.MvvmCross.Binding.Droid.Interfaces.Views;
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
        /// Constructor for the Android framework to restore the fragment.
        /// </summary>
        public MvxBoundFragment () : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MvxBoundFragment"/> class with specified
        /// layout resource.
        /// </summary>
        /// <param name="layoutId">Layout resource id to use for this fragment.</param>
        public MvxBoundFragment(int layoutId) : this() {
            var args = new Bundle ();
            args.PutInt (LayoutIdArgument, layoutId);
            Arguments = args;
        }

        protected IMvxViewBindingManager BindingManager {
            get { return _bindingManager; }
        }

        protected int LayoutId {
            get {
                if (Arguments != null && Arguments.ContainsKey(LayoutIdArgument))
                    return Arguments.GetInt(LayoutIdArgument);
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