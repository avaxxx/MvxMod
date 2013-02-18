// MvxActivityView.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using Android.Support.V4.App;
using Cirrious.MvvmCross.Interfaces.ViewModels;

namespace Cirrious.MvvmCross.Droid.Views
{
    public abstract partial class MvxActivityView<TViewModel> : FragmentActivity
        where TViewModel : class, IMvxViewModel
    {
        protected MvxActivityView()
        {
            IsVisible = true;
        }
    }
}