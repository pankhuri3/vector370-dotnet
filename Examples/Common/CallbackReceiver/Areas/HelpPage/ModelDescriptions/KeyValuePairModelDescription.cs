namespace CallbackReceiver.Areas.HelpPage.ModelDescriptions
{
    /// <summary />
    public class KeyValuePairModelDescription : ModelDescription
    {
        /// <summary />
        public ModelDescription KeyModelDescription { get; set; }

        /// <summary />
        public ModelDescription ValueModelDescription { get; set; }
    }
}