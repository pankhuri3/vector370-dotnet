using System.Collections.Generic;
using System.Linq;

namespace ThreeSeventy.Vector.Client
{
    /// <summary>
    /// Interface for persistent data repositories.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity being stored/retrieved.</typeparam>
    /// /// <remarks>
    /// This abstraction is provided as a hook for generating unit test mocks.
    /// </remarks>
    public interface IRepository<TEntity>
        where TEntity : class, new()
    {
        /// <summary>
        /// Gets a specific entity from the repository.
        /// </summary>
        /// <remarks>
        /// For objects with a compound key 'id' can be a complex object who's properties are 
        /// each part of the compound key.
        /// 
        /// For example:
        /// var sub = contactSubRepo.Get(new { ContactId = 5, SubscriptionId = 10 });
        /// 
        /// If there's a single key, there is no need for boxing:
        /// var contact = contactRepo.Get(5);
        /// </remarks>
        /// <typeparam name="TKey">The type of object that specifies the key data.</typeparam>
        /// <param name="id">The ID of the object being pulled</param>
        /// <param name="throwIfNotFound">Set to true if an error should be thrown if an object is not found.
        /// If this is false, then NULL is returned when an object is not found.</param>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <returns>The object if found, or NULL if not.
        /// If throwIfNotFound is set to true then an exception will be thrown when the object cannot be located.
        /// </returns>
        TEntity Get<TKey>(TKey id, bool throwIfNotFound = false);

        /// <summary>
        /// Returns a list of all objects available in the repository.
        /// </summary>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <returns>An IQueryable for the given entity type.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Adds an object to the repository.
        /// </summary>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <param name="entity">The entity to add</param>
        void Add(TEntity entity);

        /// <summary>
        /// Adds a list of objects to the repository.
        /// </summary>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <param name="entities">The list of objects to add</param>
        void AddRange(ICollection<TEntity> entities);

        /// <summary>
        /// Updates an object in the repository.
        /// </summary>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <param name="entity">The object to update</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates a list of objects in the repository.
        /// </summary>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <param name="entities">The list of objects to add</param>
        void UpdateRange(ICollection<TEntity> entities);

        /// <summary>
        /// Removes an object from the repository.
        /// </summary>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <param name="entity">The object ot delete</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Removes a list of objects from the repository.
        /// </summary>
        /// <throws>
        /// TODO: Fill this in
        /// </throws>
        /// <param name="entries">The list of objects to remove.</param>
        void DeleteAll(ICollection<TEntity> entries);
    }
}