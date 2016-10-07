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

    using Orc.Metadata.Model.Tests.Catel.Models.Properties;
    using Orc.Metadata.Model.Tests.Models.Model;
    using Orc.Metadata.Model.Tests.Models.Model.Metadatas;
    using Orc.Metadata.Model.Tests.Providers;
    using Orc.Metadata.Model.Tests.Tests.Helpers;
    using Orc.Metadata.Model.Tests.Tests.Models;

    [TestFixture]
    public class CatelWithTestMetadataCollectionTests
    {
        [Test]
        public async Task CatelAndTestMetadataIncludesAllProperties()
        {
            CatelTestModel model = CatelTestModelHelper.BuildRandom();
            CatelModelMetadataProvider provider = new CatelModelMetadataProvider();
            TestModelMetadataCollection testMetadataCollection = new TestModelMetadataCollection();

            provider.ConfigureWith(
                mdc =>
                    mdc.IncludeCatelProperties(true)
                       .IncludeNonCatelProperties(true)
                       .AddMetadataCollection(testMetadataCollection));

            var objectWithMetadata =
                await provider.GetModelMetadataAsync(model).ConfigureAwait(true);

            objectWithMetadata.AssertPropertiesContain(
                new[]
                {
                    "IntProperty", "IntCatelProperty", "StringProperty", "StringCatelProperty",
                    TestModelPropertyDescriptor.TestKey
                });
        }
    }
}
