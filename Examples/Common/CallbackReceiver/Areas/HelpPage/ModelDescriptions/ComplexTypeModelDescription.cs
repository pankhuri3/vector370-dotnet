using System.Collections.ObjectModel;

namespace CallbackReceiver.Areas.HelpPage.ModelDescriptions
{
    /// <summary />
    public class ComplexTypeModelDescription : ModelDescription
    {
        /// <summary />
        public ComplexTypeModelDescription()
        {
            Properties = new Collection<ParameterDescription>();
        }

        /// <summary />
        public Collection<ParameterDescription> Properties { get; private set; }
    }
}