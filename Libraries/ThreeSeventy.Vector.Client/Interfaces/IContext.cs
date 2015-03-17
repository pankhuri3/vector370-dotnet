namespace ThreeSeventy.Vector.Client
{
    /// <summary>
    /// Interface for a context for getting repositories on an entity.
    /// </summary>
    /// <remarks>
    /// This abstraction is provided as a hook for generating unit test mocks.
    /// </remarks>
    public interface IContext
    {
        /// <summary>
        /// Gets an IRepository for the specified entity.
        /// </summary>
        /// <remarks>
        /// If the entity has a set of required arguments, then they need to be supplied with this
        /// function here.
        /// 
        /// E.g.: For the content:
        /// var contentRepo = context.Repository&lt;Content&gt;(new { accountId = 5 });
        /// 
        /// Note that any required parameters which are mapped to a different HTTP name, will use
        /// the name in the model, and not the mapped HTTP name.
        /// 
        /// E.g. the following will result in an exception:
        /// var contentRepo = context.Repository&lt;Content&gt;(new { accountId = 5 });
        /// </remarks>
        /// <typeparam name="TEntity">The type of entity to get the repository of.</typeparam>
        /// <param name="args">A list of required arguments for this model.</param>
        IRepository<TEntity> Repository<TEntity>(object args = null)
            where TEntity : class, new();
    }
}