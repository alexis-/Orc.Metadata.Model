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

namespace Orc.Metadata.Model.Models.Properties
{
    using System;

    using Anotar.Catel;

    using Catel;

    using Orc.Metadata.Model.Models.Interfaces;

    /// <summary>
    ///     <see cref="TProperty" /> instance with associated object instance.
    ///     <see cref="ModelPropertyObjectWithMetadata" /> for a non-generic version.
    /// </summary>
    /// <typeparam name="TProperty">
    ///     <see cref="IModelPropertyObjectWithMetadata{TProperty}" /> property type constraint for
    ///     (optional) providers contract.
    /// </typeparam>
    public class ModelPropertyObjectWithMetadata<TProperty> :
        IModelPropertyObjectWithMetadata<TProperty>
        where TProperty : class, IModelPropertyMetadataCollection
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ModelPropertyObjectWithMetadata{TProperty}" /> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="modelPropertyMetadataCollection">The model metadata.</param>
        public ModelPropertyObjectWithMetadata(
            IModelPropertyDescriptor instance, TProperty modelPropertyMetadataCollection)
        {
            Argument.IsNotNull(() => instance);
            Argument.IsNotNull(() => modelPropertyMetadataCollection);

            Instance = instance;
            PropertyDescriptor = instance;
            ModelPropertyMetadataCollection = modelPropertyMetadataCollection;
        }

        #endregion



        #region Properties

        public object Instance { get; }
        public IModelPropertyDescriptor PropertyDescriptor { get; }
        public IMetadataCollection MetadataCollection => ModelPropertyMetadataCollection;
        public TProperty ModelPropertyMetadataCollection { get; }

        #endregion



        #region Methods

        public object GetMetadataValue(string key)
        {
            var metadata = ModelPropertyMetadataCollection.GetMetadata(key);

            return metadata?.GetValue(Instance);
        }

        public bool SetMetadataValue(string key, object value)
        {
            var metadata = ModelPropertyMetadataCollection.GetMetadata(key);

            if (metadata == null)
            {
                return false;
            }

            try
            {
                metadata.SetValue(Instance, value);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex, "Failed to set value on Metadata");

                return false;
            }

            return true;
        }

        #endregion
    }

    /// <summary>
    ///     <see cref="IModelPropertyMetadataCollection" /> instance with associated object instance.
    /// </summary>
    public class ModelPropertyObjectWithMetadata :
        ModelPropertyObjectWithMetadata<IModelPropertyMetadataCollection>
    {
        #region Constructors


        /// <summary>
        ///     Initializes a new instance of the <see cref="ModelPropertyObjectWithMetadata" />
        ///     class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="modelPropertyMetadataCollection">The model metadata.</param>
        public ModelPropertyObjectWithMetadata(
            IModelPropertyDescriptor instance,
            IModelPropertyMetadataCollection modelPropertyMetadataCollection)
            : base(instance, modelPropertyMetadataCollection)
        {
        }

        #endregion
    }
}