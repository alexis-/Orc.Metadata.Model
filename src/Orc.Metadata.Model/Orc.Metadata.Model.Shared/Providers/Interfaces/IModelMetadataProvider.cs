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

namespace Orc.Metadata.Model.Providers.Interfaces
{
    using System.Threading.Tasks;

    using Orc.Metadata.Model.Models.Interfaces;

    /// <summary>
    ///     Factory-pattern interface which generates a
    ///     <see cref="IModelObjectWithMetadata{TModel, TProperty}" /> with the provided model
    ///     instance. See <see cref="IModelMetadataProvider"/> for non-generic version.
    /// </summary>
    /// <typeparam name="TModel">
    ///     <see cref="IModelObjectWithMetadata{TModel, TProperty}" /> model type constraint for
    ///     (optional) providers contract.
    /// </typeparam>
    /// <typeparam name="TProperty">
    ///     <see cref="IModelPropertyObjectWithMetadata{TProperty}" /> property type constraint for
    ///     (optional) providers contract.
    /// </typeparam>
    public interface IModelMetadataProvider<TModel, TProperty> : IMetadataProvider
        where TProperty : class, IModelPropertyMetadataCollection
        where TModel : class, IModelMetadataCollection<TProperty>
    {
        #region Methods

        /// <summary>
        ///     Asynchronously generates a
        ///     <see cref="IModelObjectWithMetadata{TModel, TProperty}" /> from given model
        ///     instance.
        /// </summary>
        /// <param name="obj">Model instance.</param>
        /// <returns></returns>
        Task<IModelObjectWithMetadata<TModel, TProperty>> GetModelMetadataAsync(object obj);

        #endregion
    }

    /// <summary>
    ///     Factory-pattern interface which generates a
    ///     <see cref="IModelObjectWithMetadata{TModel, TProperty}" /> with the provided model
    ///     instance.
    /// </summary>
    public interface IModelMetadataProvider
        : IModelMetadataProvider<
            IModelMetadataCollection<IModelPropertyMetadataCollection>,
            IModelPropertyMetadataCollection>
    {
    }
}