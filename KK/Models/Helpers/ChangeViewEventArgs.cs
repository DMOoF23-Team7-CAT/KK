using System;

namespace KK.Models.Helpers
{
    public class ChangeViewEventArgs : EventArgs
    {
        public object NewView { get; }

        public ChangeViewEventArgs(object newView)
        {
            NewView = newView;
        }
    }
}
