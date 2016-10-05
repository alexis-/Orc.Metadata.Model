namespace Orc.Metadata.Model.Models.Interfaces
{
    /// <summary>
    ///     <see cref="IModelPropertyMetadataCollection" /> instance with associated model property instance.
    ///     See <see cref="IModelPropertyObjectWithMetadata"/> for non-generic version.
    /// </summary>
    /// <typeparam name="TProperty">
    ///     <see cref="IModelPropertyObjectWithMetadata" /> type constraint for (optional) providers
    ///     contract.
    /// </typeparam>
    public interface IModelPropertyObjectWithMetadata<out TProperty> : IObjectWithMetadata
        where TProperty : class, IModelPropertyMetadataCollection
    {
        #region Properties

        /// <summary>
        ///     The <see cref="IModelPropertyMetadataCollection" /> associated with provided model
        ///     property instance.
        /// </summary>
        TProperty ModelPropertyMetadataCollection { get; }

        #endregion
    }

    /// <summary>
    ///     <see cref="IModelPropertyMetadataCollection" /> instance with associated object property instance.
    /// </summary>
    public interface IModelPropertyObjectWithMetadata
        : IModelPropertyObjectWithMetadata<IModelPropertyMetadataCollection> { }
}