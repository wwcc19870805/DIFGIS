using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF3DEdit.Class
{
    public abstract class BaseCommand
    {
        private string cmdName;
        private CameraParamter undoCamera;
        private CameraParamter redoCamera;
        private HashMap undoSelectionMap;
        private HashMap redoSelectionMap;
        public string CommandName
        {
            get
            {
                return this.cmdName;
            }
            set
            {
                this.cmdName = value;
            }
        }
        public CameraParamter UndoCamera
        {
            get
            {
                return this.undoCamera;
            }
            set
            {
                this.undoCamera = value;
            }
        }
        public CameraParamter RedoCamera
        {
            get
            {
                return this.redoCamera;
            }
            set
            {
                this.redoCamera = value;
            }
        }
        public HashMap UndoSelectionMap
        {
            get
            {
                return this.undoSelectionMap;
            }
            set
            {
                this.undoSelectionMap = value;
            }
        }
        public HashMap RedoSelectionMap
        {
            get
            {
                return this.redoSelectionMap;
            }
            set
            {
                this.redoSelectionMap = value;
            }
        }
        public abstract bool Undo();
        public abstract bool Redo();
    }
}
