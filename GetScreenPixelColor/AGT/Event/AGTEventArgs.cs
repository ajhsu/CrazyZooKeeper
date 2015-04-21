using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.artgital.ajhsu
{
    public class AGTEventArgs : EventArgs
    {
        private object passObject;
        public object PassObject
        {
            get { return passObject; }
        }

        public AGTEventArgs(object passObject = null)
        {
            this.passObject = passObject;
        }
    }
}
