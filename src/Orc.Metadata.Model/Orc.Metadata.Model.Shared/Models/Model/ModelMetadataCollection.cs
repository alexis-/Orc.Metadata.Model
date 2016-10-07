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
    using System.Collections.Generic;
    using System.Linq;

    using Orc.Metadata.Model.Models.Interfaces;
    using Orc.Metadata.Model.Models.Metadatas;

    /// <summary>
    ///     Provides a collection of <see cref="IMetadata" /> for models object. Avalaible
    ///     metadatas are dependant on implementation. See <see cref="ModelMetadataTypes" /> for a
    ///     list of common model <see cref="IMetadata" /> types.
    /// </summary>
    /// <typeparam name="TChild">
    ///     Child type for (optional) fluent building, see <see cref="MetadataCollectionAggregator{TChild}"/>.
    /// </typeparam>
    public abstract class ModelMetadataCollection<TChild>
        : MetadataCollectionAggregator<TChild>, IModelMetadataCollection
        where TChild : ModelMetadataCollection<TChild>
    {
        #region Fields

        private readonly bool _mergeProperties;

        #endregion



        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ModelMetadataCollection{TChild, TProperty}" /> class.
        /// </summary>
        /// <param name="mergeProperties">Try and merge {TProperty} metadatas</param>
        /// <param name="metadataCollections">The metadata collections.</param>
        protected ModelMetadataCollection(
            bool mergeProperties = true, params IMetadataCollection[] metadataCollections)
            : base(metadataCollections)
        {
            _mergeProperties = mergeProperties;
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ModelMetadataCollection{TChild, TProperty}" /> class.
        /// </summary>
        /// <param name="mergeProperties">Try and merge {TProperty} metadatas</param>
        protected ModelMetadataCollection(bool mergeProperties = true)
            : this(mergeProperties, null)
        {
        }

        #endregion



        #region Properties

        /// <summary>All available <see cref="IMetadata" /></summary>
        public override IEnumerable<IMetadata> All
            =>
            base.All.Union(
                    new IMetadata[]
                    {
                        PropertyMetadatasAccessor,
                        PropertyDescriptorsAccessor
                    });

        /// <summary>Gets the <see cref="IModelPropertyMetadataCollection" /> accessor.</summary>
        /// <returns></returns>
        public abstract MetadataAggregator<IModelPropertyMetadataCollection> PropertyMetadatasAccessor
        {
            get;
        }

        /// <summary>Gets the properties name accessor.</summary>
        /// <returns></returns>
        public abstract MetadataAggregator<IEnumerable<IModelPropertyDescriptor>>
            PropertyDescriptorsAccessor
        {
            get;
        }

        #endregion



        #region Methods

        /// <summary>Gets the metadata.</summary>
        /// <param name="metadataName">Name of the metadata.</param>
        /// <returns></returns>
        public override IMetadata GetMetadata(string metadataName)
        {
            switch (metadataName)
            {
                case ModelMetadataTypes.PropertyMetadatas:
                    return PropertyMetadatasAccessor;

                case ModelMetadataTypes.PropertyDescriptors:
                    return PropertyDescriptorsAccessor;

                default:
                    return base.GetMetadata(metadataName);
            }
        }

        /// <summary>Configures the <see cref="IModelPropertyMetadataCollection"/>.</summary>
        /// <param name="propertyConfigurationAction">The property configuration action.</param>
        public abstract TChild ConfigurePropertiesWith(
            Action<IModelPropertyMetadataCollection> propertyConfigurationAction);

        /// <summary>
        ///     Generates a <see cref="IModelPropertyObjectWithMetadata" /> with associated
        ///     <see cref="IModelPropertyMetadataCollection" /> and
        ///     <see cref="IModelPropertyDescriptor" />.
        /// </summary>
        /// <param name="instance">Model instance.</param>
        /// <param name="propertyName">Target property propertyName.</param>
        /// <returns></returns>
        public abstract IModelPropertyObjectWithMetadata GetPropertyObjectWithMetadataByName(
            object instance, string propertyName);

        /// <summary>
        ///     Gets the <see cref="ModelMetadataTypes.PropertyDescriptors" /> metadata value if
        ///     available.
        /// </summary>
        /// <param name="instance">Model instance.</param>
        /// <returns>Properties name or null.</returns>
        public IEnumerable<IModelPropertyDescriptor> GetPropertyDescriptors(object instance)
        {
            return PropertyDescriptorsAccessor?.GetTypedValue(instance);
        }

        public override TChild AddMetadataCollection(IMetadataCollection metadataCollection)
        {
            if (!_mergeProperties)
            {
                return base.AddMetadataCollection(metadataCollection);
            }

            if (!MetadataCollections.Contains(metadataCollection))
            {
                MetadataCollections.Add(metadataCollection);

                var propMetadataAccessor =
                    metadataCollection.FirstOrDefault(
                        md => md.Name == ModelMetadataTypes.PropertyMetadatas);
                var propNames =
                    metadataCollection.FirstOrDefault(
                        md => md.Name == ModelMetadataTypes.PropertyDescriptors);

                PropertyMetadatasAccessor.AddMetadata(propMetadataAccessor);
                PropertyDescriptorsAccessor.AddMetadata(propNames);

                MergeMetadatas(
                    metadataCollection.All.Except(new[] { propNames, propMetadataAccessor }));
            }

            return (TChild)this;
        }

        #endregion
    }
}