using System;

namespace Cirrious.MvvmCross.Droid.Platform.Fragments
{
    public class MvxFragmentDataEventArgs : EventArgs
    {
        public readonly int Id;
        public readonly string Tag;
        public readonly object Value;
        
        public MvxFragmentDataEventArgs (int id, string tag, object value)
        {
            Id = id;
            Tag = tag;
            Value = value;
        }
        
        public bool Check(int id, string tag) {
            if (id != Id)
                return false;
            
            if (tag == Tag)
                return true;
            if (tag != null && tag.Equals (Tag))
                return true;
            
            return false;
        }
    }}

