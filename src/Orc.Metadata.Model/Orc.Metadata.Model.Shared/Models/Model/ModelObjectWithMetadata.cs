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

namespace Orc.Metadata.Model.Models.Model
{
    using System;

    using Anotar.Catel;

    using Catel;

    using Orc.Metadata.Model.Models.Interfaces;

    /// <summary>
    ///     <see cref="IModelMetadataCollection{TProperty}" /> instance with associated object instance.
    ///     <see cref="ModelObjectWithMetadata" /> for a non-generic version.
    /// </summary>
    /// <typeparam name="TModel">
    ///     <see cref="IModelObjectWithMetadata{TModel, TProperty}" /> model type constraint for
    ///     (optional) providers contract.
    /// </typeparam>
    /// <typeparam name="TProperty">
    ///     <see cref="IModelPropertyObjectWithMetadata{TProperty}" /> property type constraint for
    ///     (optional) providers contract.
    /// </typeparam>
    public class ModelObjectWithMetadata<TModel, TProperty> :
        IModelObjectWithMetadata<TModel, TProperty>
        where TProperty : class, IModelPropertyMetadataCollection
        where TModel : class, IModelMetadataCollection<TProperty>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ModelObjectWithMetadata{TModel, TProperty}" /> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="modelMetadataCollection">The model metadata.</param>
        public ModelObjectWithMetadata(object instance, TModel modelMetadataCollection)
        {
            Argument.IsNotNull(() => instance);
            Argument.IsNotNull(() => modelMetadataCollection);

            Instance = instance;
            ModelMetadataCollection = modelMetadataCollection;
        }

        #endregion



        #region Properties

        public object Instance { get; }
        public IMetadataCollection MetadataCollection => ModelMetadataCollection;
        public TModel ModelMetadataCollection { get; }

        #endregion



        #region Methods

        public IModelPropertyObjectWithMetadata<TProperty> GetPropertyObjectWithMetadataByName(
            string propertyName)
        {
            return ModelMetadataCollection.GetPropertyObjectWithMetadataByName(
                Instance, propertyName);
        }

        public object GetMetadataValue(string key)
        {
            var metadata = ModelMetadataCollection.GetMetadata(key);

            return metadata?.GetValue(Instance);
        }

        public bool SetMetadataValue(string key, object value)
        {
            var metadata = ModelMetadataCollection.GetMetadata(key);

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
    ///     <see cref="IModelMetadataCollection{TProperty}" /> instance with associated object instance.
    /// </summary>
    public class ModelObjectWithMetadata :
        ModelObjectWithMetadata<
            IModelMetadataCollection<IModelPropertyMetadataCollection>,
            IModelPropertyMetadataCollection>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ModelObjectWithMetadata" /> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="modelMetadataCollection">The model metadata.</param>
        public ModelObjectWithMetadata(
            object instance,
            IModelMetadataCollection<IModelPropertyMetadataCollection> modelMetadataCollection)
            : base(instance, modelMetadataCollection)
        {
        }

        #endregion
    }
}