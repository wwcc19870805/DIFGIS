using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF3DEdit.Class
{
    public class UndoRedoStateEventArgs : System.EventArgs
    {
        private bool canUndo;
        private bool canRedo;
        public bool CanUndo
        {
            get
            {
                return this.canUndo;
            }
            set
            {
                this.canUndo = value;
            }
        }
        public bool CanRedo
        {
            get
            {
                return this.canRedo;
            }
            set
            {
                this.canRedo = value;
            }
        }
    }
}
