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

namespace Orc.Metadata.Model.Tests.Tests
{
    using System.Threading.Tasks;

    using NUnit.Framework;

    using Orc.Metadata.Model.Tests.Providers;
    using Orc.Metadata.Model.Tests.Tests.Helpers;
    using Orc.Metadata.Model.Tests.Tests.Models;

    [TestFixture]
    public class CatelTests
    {
        [Test]
        public async Task CatelMetadataIncludesAllProperties()
        {
            CatelTestModel model = CatelTestModelHelper.BuildRandom();
            CatelModelMetadataProvider provider = new CatelModelMetadataProvider();

            provider.ConfigureWith(
                mdc => mdc.IncludeCatelProperties(true).IncludeNonCatelProperties(true));

            var objectWithMetadata =
                await provider.GetModelMetadataAsync(model).ConfigureAwait(true);

            objectWithMetadata.AssertPropertiesContain(
                new[]
                {
                    "IntProperty", "IntCatelProperty", "StringProperty", "StringCatelProperty"
                });
        }

        [Test]
        public async Task CatelMetadataIncludesCatelProperties()
        {
            CatelTestModel model = CatelTestModelHelper.BuildRandom();
            CatelModelMetadataProvider provider = new CatelModelMetadataProvider();

            provider.ConfigureWith(
                mdc => mdc.IncludeCatelProperties(true).IncludeNonCatelProperties(false));

            var objectWithMetadata =
                await provider.GetModelMetadataAsync(model).ConfigureAwait(true);

            objectWithMetadata.AssertPropertiesContain(
                new[]
                {
                    "IntCatelProperty", "StringCatelProperty"
                });
        }

        [Test]
        public async Task CatelMetadataPropertiesValueMatchInstance()
        {
            CatelTestModel model = CatelTestModelHelper.BuildRandom();
            CatelModelMetadataProvider provider = new CatelModelMetadataProvider();

            provider.ConfigureWith(
                mdc => mdc.IncludeCatelProperties(true).IncludeNonCatelProperties(true));

            var objectWithMetadata =
                await provider.GetModelMetadataAsync(model).ConfigureAwait(true);

            var intPropertyObjWithMetadatas =
                objectWithMetadata.GetPropertyObjectWithMetadataByName("IntProperty");
            var intCatelPropertyObjWithMetadatas =
                objectWithMetadata.GetPropertyObjectWithMetadataByName("IntCatelProperty");
            var stringPropertyObjWithMetadatas =
                objectWithMetadata.GetPropertyObjectWithMetadataByName("StringProperty");
            var stringCatelPropertyObjWithMetadatas =
                objectWithMetadata.GetPropertyObjectWithMetadataByName("StringCatelProperty");

            intPropertyObjWithMetadatas.AssertValueEqualTo(model.IntProperty);
            intCatelPropertyObjWithMetadatas.AssertValueEqualTo(model.IntCatelProperty);
            stringPropertyObjWithMetadatas.AssertValueEqualTo(model.StringProperty);
            stringCatelPropertyObjWithMetadatas.AssertValueEqualTo(model.StringCatelProperty);
        }
    }
}
