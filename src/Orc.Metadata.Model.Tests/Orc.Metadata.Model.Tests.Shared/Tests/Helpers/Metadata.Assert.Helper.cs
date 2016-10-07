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

namespace Orc.Metadata.Model.Tests.Tests.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using global::Catel;

    using Orc.Metadata.Model.Models.Interfaces;
    using Orc.Metadata.Model.Models.Model;
    using Orc.Metadata.Model.Models.Properties;

    public static class MetadataAssertHelper
    {
        #region Methods

        public static void AssertPropertiesContain<TModel>(
            this IModelObjectWithMetadata<TModel> modelObjectWithMetadata,
            IEnumerable<string> propertiesName)
            where TModel : class, IModelMetadataCollection
        {
            Argument.IsNotNull(() => modelObjectWithMetadata);

            var propertiesDescriptor =
                (IEnumerable<IModelPropertyDescriptor>)
                modelObjectWithMetadata.GetMetadataValue(ModelMetadataTypes.PropertyDescriptors);

            propertiesDescriptor.Should()
                                .Contain(pd => propertiesName.Contains(pd.PropertyName));
        }

        public static void AssertValueEqualTo<TValue>(
            this IModelPropertyObjectWithMetadata modelPropertyObjectWithMetadata,
            TValue value)
        {
            Argument.IsNotNull(() => modelPropertyObjectWithMetadata);

            TValue propValue =
                (TValue)
                modelPropertyObjectWithMetadata.GetMetadataValue(
                    ModelPropertyMetadataTypes.Value);

            propValue.Should().Be(value);
        }

        #endregion
    }
}