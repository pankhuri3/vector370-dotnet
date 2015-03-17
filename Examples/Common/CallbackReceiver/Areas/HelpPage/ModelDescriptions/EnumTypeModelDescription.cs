using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CallbackReceiver.Areas.HelpPage.ModelDescriptions
{
    /// <summary />
    public class EnumTypeModelDescription : ModelDescription
    {
        /// <summary />
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        /// <summary />
        public Collection<EnumValueDescription> Values { get; private set; }
    }
}