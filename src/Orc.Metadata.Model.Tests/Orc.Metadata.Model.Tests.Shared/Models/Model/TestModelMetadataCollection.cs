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
    using Orc.Metadata.Model.Tests.Models.Model.Metadatas;
    using Orc.Metadata.Model.Tests.Models.Properties;

    /// <inheritdoc />
    public class TestModelMetadataCollection :
        ModelMetadataCollection<TestModelMetadataCollection>
    {
        #region Constructors

        public TestModelMetadataCollection()
        {
            PropertyMetadatasAccessor =
                new ModelPropertyMetadataCollectionAccessor(
                    new TestModelPropertyMetadataCollection());
            PropertyDescriptorsAccessor = new TestModelPropertyDescriptorCollectionAccessor();
        }

        #endregion



        #region Properties

        public override MetadataAggregator<IModelPropertyMetadataCollection>
            PropertyMetadatasAccessor { get; }

        public override MetadataAggregator<IEnumerable<IModelPropertyDescriptor>>
            PropertyDescriptorsAccessor { get; }

        #endregion



        #region Methods

        public override TestModelMetadataCollection ConfigurePropertiesWith(
            Action<IModelPropertyMetadataCollection> propertyConfigurationAction)
        {
            var propertyMetadataCollection = new TestModelPropertyMetadataCollection();
            propertyConfigurationAction?.Invoke(propertyMetadataCollection);

            PropertyMetadatasAccessor.SetTypedValue(null, propertyMetadataCollection);

            return this;
        }

        public override IModelPropertyObjectWithMetadata GetPropertyObjectWithMetadataByName(
            object instance, string name)
        {
            var modelPropertyDescriptors = PropertyDescriptorsAccessor.GetTypedValue(instance);
            var modelPropertyDescriptor =
                modelPropertyDescriptors.FirstOrDefault(pd => pd.PropertyName == name);

            if (modelPropertyDescriptor == null)
            {
                return null;
            }

            var propertyMetadatas = PropertyMetadatasAccessor.GetTypedValue(instance);

            return null;
        }

        #endregion
    }
}