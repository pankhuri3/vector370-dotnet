using System;

namespace CallbackReceiver.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Describes a type model.
    /// </summary>
    public abstract class ModelDescription
    {
        /// <summary />
        public string Documentation { get; set; }

        /// <summary />
        public Type ModelType { get; set; }

        /// <summary />
        public string Name { get; set; }
    }
}