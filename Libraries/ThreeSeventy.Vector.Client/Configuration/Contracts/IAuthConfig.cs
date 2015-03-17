namespace ThreeSeventy.Vector.Client
{
    /// <summary>
    /// Details for user authentication
    /// </summary>
    public interface IAuthConfig
    {
        /// <summary>
        /// The user name to use.
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// The password to use.
        /// </summary>
        string Password { get; set; }
    }
}