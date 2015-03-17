using RestSharp;

namespace ThreeSeventy.Vector.Client
{
    /// <summary>
    /// Interface for generating a new instance of IRestClient
    /// </summary>
    /// <remarks>
    /// This interface is a hook for generating mock objects for unit testing.
    /// </remarks>
    public interface IRestClientFactory
    {
        /// <summary>
        /// Generates a new object that implements the IRestClient interface.
        /// </summary>
        IRestClient Create(IConfiguration config);
    }
}