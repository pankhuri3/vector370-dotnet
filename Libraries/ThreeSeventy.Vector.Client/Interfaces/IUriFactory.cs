namespace ThreeSeventy.Vector.Client
{
    /// <summary>
    /// Interface for URI generation factory.
    /// </summary>
    public interface IUriFactory
    {
        /// <summary>
        /// Generate a URI for calling the 3Seventy APIs.
        /// </summary>
        /// <returns>A string representing the base URI to the 3Seventy APIs.</returns>
        string Create();
    }
}