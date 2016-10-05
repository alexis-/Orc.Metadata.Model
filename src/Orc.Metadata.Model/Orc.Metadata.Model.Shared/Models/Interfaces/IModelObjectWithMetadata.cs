// 
// The MIT License (MIT)
// 
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the 
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

namespace Orc.Metadata.Model.Models.Interfaces
{
    /// <summary>
    ///     <see cref="IModelMetadataCollection{TProperty}" /> instance with associated object instance.
    ///     <see cref="IModelObjectWithMetadata" /> for a non-generic version.
    /// </summary>
    /// <typeparam name="TModel">
    ///     <see cref="IModelObjectWithMetadata{TModel, TProperty}" /> model type constraint for
    ///     (optional) providers contract.
    /// </typeparam>
    /// <typeparam name="TProperty">
    ///     <see cref="IModelPropertyObjectWithMetadata{TProperty}" /> property type constraint for
    ///     (optional) providers contract.
    /// </typeparam>
    public interface IModelObjectWithMetadata<out TModel, out TProperty> : IObjectWithMetadata
        where TProperty : class, IModelPropertyMetadataCollection
        where TModel : class, IModelMetadataCollection<TProperty>
    {
        #region Properties

        /// <summary>
        ///     The <see cref="IModelMetadataCollection{TProperty}" /> associated with provided
        ///     model instance.
        /// </summary>
        TModel ModelMetadataCollection { get; }

        /// <summary>Generates a <see cref="IModelPropertyObjectWithMetadata" /> with associated
        ///     <see cref="IModelPropertyMetadataCollection" /> and property instance.</summary>
        /// <param name="propertyName">Target property propertyName.</param>
        /// <returns></returns>
        IModelPropertyObjectWithMetadata<TProperty> GetPropertyObjectWithMetadataByName(
            string propertyName);

        #endregion
    }

    /// <summary>
    ///     <see cref="IModelMetadataCollection{IModelPropertyMetadataCollection}" /> instance
    ///     with associated object instance.
    /// </summary>
    public interface IModelObjectWithMetadata :
        IModelObjectWithMetadata<
            IModelMetadataCollection<IModelPropertyMetadataCollection>,
            IModelPropertyMetadataCollection>
    {
    }
}