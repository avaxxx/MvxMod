using System;

namespace Cirrious.MvvmCross.Droid.Platform.Fragments
{
    public class MvxFragmentDataEventArgs : EventArgs
    {
        public readonly int Id;
        public readonly object Value;
        
        public MvxFragmentDataEventArgs (int id, object value)
        {
            Id = id;
            Value = value;
        }
    }}

