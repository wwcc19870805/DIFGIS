using System;
namespace ICSharpCode.Core
{
    public interface IStringTagProvider
    {
        string[] Tags
        {
            get;
        }
        string Convert(string tag);
    }
}
