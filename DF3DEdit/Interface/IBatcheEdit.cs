using System;
using DF3DEdit.Class;

namespace DF3DEdit.Interface
{
    public interface IBatcheEdit
    {
        bool BeginEdit(bool bNeedTooltip);
        bool BeginEdit();
        void DoWork(EditType editType, EditParameters parameters);
        void EndEdit();
    }
}
