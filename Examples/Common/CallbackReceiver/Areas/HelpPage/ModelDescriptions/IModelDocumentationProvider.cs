using System;
using System.Reflection;

namespace CallbackReceiver.Areas.HelpPage.ModelDescriptions
{
    /// <summary />
    public interface IModelDocumentationProvider
    {
        /// <summary />
        string GetDocumentation(MemberInfo member);

        /// <summary />
        string GetDocumentation(Type type);
    }
}