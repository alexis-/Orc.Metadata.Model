namespace Orc.Metadata.Model.Models.Interfaces
{
    /// <summary>
    ///     <see cref="IModelPropertyMetadataCollection" /> instance with associated model property instance.
    ///     See <see cref="IModelPropertyObjectWithMetadata"/> for non-generic version.
    /// </summary>
    public interface IModelPropertyObjectWithMetadata : IObjectWithMetadata
    {
        #region Properties

        /// <summary>
        ///     The <see cref="IModelPropertyMetadataCollection" /> associated with provided model
        ///     property instance.
        /// </summary>
        IModelPropertyMetadataCollection ModelPropertyMetadataCollection { get; }

        #endregion
    }
}