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
    using System.Collections.Generic;

    using Orc.Metadata.Model.Models.Model;

    /// <summary>
    ///     Provides a collection of <see cref="IMetadata" /> for models object. Avalaible
    ///     metadatas are dependant on implementation. See <see cref="ModelMetadataTypes" /> for a
    ///     list of common model <see cref="IMetadata" /> types.
    /// </summary>
    /// <typeparam name="TProperty">
    ///     <see cref="IModelPropertyObjectWithMetadata" /> type constraint for (optional) providers
    ///     contract.
    /// </typeparam>
    public interface IModelMetadataCollection<out TProperty> : IMetadataCollection
        where TProperty : class, IModelPropertyMetadataCollection
    {
        #region Methods

        /// <summary>
        ///     Generates a <see cref="IModelPropertyObjectWithMetadata" /> with associated
        ///     <see cref="IModelPropertyMetadataCollection" /> and
        ///     <see cref="IModelPropertyDescriptor" />.
        /// </summary>
        /// <param name="instance">Model instance.</param>
        /// <param name="propertyName">Target property propertyName.</param>
        /// <returns></returns>
        IModelPropertyObjectWithMetadata<TProperty> GetPropertyObjectWithMetadataByName(
            object instance, string propertyName);

        /// <summary>
        ///     Gets the <see cref="ModelMetadataTypes.PropertyDescriptors" /> metadata value if
        ///     available.
        /// </summary>
        /// <param name="instance">Model instance.</param>
        /// <returns>Properties descriptor.</returns>
        IEnumerable<IModelPropertyDescriptor> GetPropertyDescriptors(object instance);

        #endregion
    }
}