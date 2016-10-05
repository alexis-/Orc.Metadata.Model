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

namespace Orc.Metadata.Model.Tests.Models.Model.Metadatas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::Catel.Data;

    using Orc.Metadata.Model.Models.Interfaces;
    using Orc.Metadata.Model.Models.Metadatas;
    using Orc.Metadata.Model.Models.Model;
    using Orc.Metadata.Model.Models.Properties;
    using Orc.Metadata.Model.Tests.Catel.Models.Properties;

    public class CatelModelPropertyDescriptorCollectionAccessor
        : MetadataAggregator<IEnumerable<IModelPropertyDescriptor>>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="CatelModelPropertyDescriptorCollectionAccessor" /> class.
        /// </summary>
        /// <param name="includeCatelProperties">if set to <c>true</c> [include catel properties].</param>
        /// <param name="includeNonCatelProperties">if set to <c>true</c> [include catel non properties].</param>
        public CatelModelPropertyDescriptorCollectionAccessor(
            bool includeCatelProperties, bool includeNonCatelProperties)
        {
            IncludeCatelProperties = includeCatelProperties;
            IncludeNonCatelProperties = includeNonCatelProperties;
        }

        #endregion



        #region Properties

        public bool IncludeCatelProperties { get; set; }
        public bool IncludeNonCatelProperties { get; set; }

        public override string Name => ModelMetadataTypes.PropertyDescriptors;
        public override string DisplayName
        {
            get { return Name; }
            set { }
        }

        #endregion



        #region Methods

        protected override IEnumerable<IModelPropertyDescriptor> AggregateValue(
            object instance,
            IEnumerable<GenericMetadata<IEnumerable<IModelPropertyDescriptor>>> metadatas)
        {
            // Get main descriptors
            var descriptors = ComputeDescriptors(instance);
            var descriptorDictionary = descriptors.ToDictionary(k => k.PropertyName);

            // Iterate aggregated descriptor metadatas
            foreach (var metadata in metadatas)
            {
                // Get aggregated descriptors value & evaluate each item
                var otherDescriptors = metadata.GetTypedValue(instance);
                
                foreach (var otherDescriptor in otherDescriptors)
                {
                    // Check whether there is a conflict (merge) or if this is a new property name
                    // (add)
                    if (descriptorDictionary.ContainsKey(otherDescriptor.PropertyName))
                    {
                        var descriptor = descriptorDictionary[otherDescriptor.PropertyName];

                        descriptor.MergePropertyDescriptor(otherDescriptor);
                    }
                    else
                    {
                        descriptors.Add(otherDescriptor);
                    }
                }
            }

            return descriptors;
        }

        public override void SetTypedValue(
            object instance, IEnumerable<IModelPropertyDescriptor> value)
        {
            throw new InvalidOperationException("Value cannot be set.");
        }

        private List<IModelPropertyDescriptor> ComputeDescriptors(object instance)
        {
            List<IModelPropertyDescriptor> propertyNameCollection =
                new List<IModelPropertyDescriptor>();

            if (instance is ModelBase == false)
            {
                return propertyNameCollection;
            }

            var propertyDataManager = PropertyDataManager.Default;
            var catelTypeInfo = propertyDataManager.GetCatelTypeInfo(instance.GetType());
            
            if (IncludeCatelProperties)
            {
                var catelPropertyNameCollection =
                    catelTypeInfo.GetCatelProperties()
                                 .Select(
                                     catelProp =>
                                         new CatelModelPropertyDescriptor(
                                             instance, catelProp.Key, catelProp.Value));

                propertyNameCollection.AddRange(catelPropertyNameCollection);
            }

            if (IncludeNonCatelProperties)
            {
                var nonCatelpropertyNameCollection =
                    catelTypeInfo.GetNonCatelProperties()
                                 .Select(
                                     catelProp =>
                                         new CatelModelPropertyDescriptor(
                                             instance, catelProp.Key, catelProp.Value));

                propertyNameCollection.AddRange(nonCatelpropertyNameCollection);
            }

            return propertyNameCollection.Distinct().ToList();
        }

        #endregion
    }
}