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

namespace Orc.Metadata.Model.Tests.Models.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Orc.Metadata.Model.Models.Interfaces;
    using Orc.Metadata.Model.Models.Metadatas;
    using Orc.Metadata.Model.Models.Model;
    using Orc.Metadata.Model.Models.Model.Metadatas;
    using Orc.Metadata.Model.Models.Properties;
    using Orc.Metadata.Model.Tests.Catel.Models.Properties;
    using Orc.Metadata.Model.Tests.Models.Model.Metadatas;
    using Orc.Metadata.Model.Tests.Models.Properties;

    /// <inheritdoc />
    public class CatelModelMetadataCollection
        : ModelMetadataCollection
            <CatelModelMetadataCollection, CatelModelPropertyMetadataCollection>
    {
        #region Constructors

        public CatelModelMetadataCollection()
        {
            PropertyMetadatasAccessor =
                new ModelPropertyMetadataCollectionAccessor
                    <CatelModelPropertyMetadataCollection>(
                        new CatelModelPropertyMetadataCollection());
            PropertyDescriptorsAccessor = new CatelModelPropertyDescriptorCollectionAccessor(true, true);
        }

        #endregion



        #region Properties

        public override MetadataAggregator<CatelModelPropertyMetadataCollection>
            PropertyMetadatasAccessor { get; }

        public override MetadataAggregator<IEnumerable<IModelPropertyDescriptor>>
            PropertyDescriptorsAccessor { get; }

        #endregion



        #region Methods

        public override CatelModelMetadataCollection ConfigurePropertiesWith(
            Action<CatelModelPropertyMetadataCollection> propertyConfigurationAction = null)
        {
            var propertyMetadataCollection = new CatelModelPropertyMetadataCollection();
            propertyConfigurationAction?.Invoke(propertyMetadataCollection);

            PropertyMetadatasAccessor.SetTypedValue(null, propertyMetadataCollection);

            return this;
        }

        public CatelModelMetadataCollection IncludeCatelProperties(bool include)
        {
            ((CatelModelPropertyDescriptorCollectionAccessor)PropertyDescriptorsAccessor)
                .IncludeCatelProperties = include;

            return this;
        }

        public CatelModelMetadataCollection IncludeNonCatelProperties(bool include)
        {
            ((CatelModelPropertyDescriptorCollectionAccessor)PropertyDescriptorsAccessor)
                .IncludeNonCatelProperties = include;

            return this;
        }

        public override IModelPropertyObjectWithMetadata<CatelModelPropertyMetadataCollection>
            GetPropertyObjectWithMetadataByName(object instance, string name)
        {
            var modelPropertyDescriptors = PropertyDescriptorsAccessor.GetTypedValue(instance);
            var modelPropertyDescriptor =
                modelPropertyDescriptors.FirstOrDefault(pd => pd.PropertyName == name);

            if (modelPropertyDescriptor == null)
            {
                return null;
            }

            var propertyMetadatas = PropertyMetadatasAccessor.GetTypedValue(instance);

            return new CatelModelPropertyObjectWithMetadata(
                modelPropertyDescriptor, propertyMetadatas);
        }

        #endregion
    }
}