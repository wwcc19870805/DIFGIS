using System;
using System.Collections;
namespace ICSharpCode.Core
{
    public interface IBuildItemsModifier
    {
        void Apply(IList items);
    }
}

