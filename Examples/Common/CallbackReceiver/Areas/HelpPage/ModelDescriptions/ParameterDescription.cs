using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CallbackReceiver.Areas.HelpPage.ModelDescriptions
{
    /// <summary />
    public class ParameterDescription
    {
        /// <summary />
        public ParameterDescription()
        {
            Annotations = new Collection<ParameterAnnotation>();
        }

        /// <summary />
        public Collection<ParameterAnnotation> Annotations { get; private set; }

        /// <summary />
        public string Documentation { get; set; }

        /// <summary />
        public string Name { get; set; }

        /// <summary />
        public ModelDescription TypeDescription { get; set; }
    }
}