// MvxMapActivityView.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using Android.GoogleMaps;
using Cirrious.MvvmCross.Interfaces.ViewModels;

namespace Cirrious.MvvmCross.Droid.Maps
{
    public abstract partial class MvxMapActivityView<TViewModel> : MapActivity
        where TViewModel : class, IMvxViewModel
    {
        protected MvxMapActivityView()
        {
            IsVisible = true;
        }
    }
}