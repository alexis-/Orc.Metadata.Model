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
    using Orc.Metadata.Model.Tests.Tests.Models;

    public class TestModelPropertyDescriptorCollectionAccessor
        : MetadataAggregator<IEnumerable<IModelPropertyDescriptor>>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="TestModelPropertyDescriptorCollectionAccessor" /> class.
        /// </summary>
        public TestModelPropertyDescriptorCollectionAccessor()
        {
        }

        #endregion



        #region Properties

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
            return new[]
            {
                new TestModelPropertyDescriptor(instance, TestModelPropertyDescriptor.TestKey),
                new TestModelPropertyDescriptor(instance, nameof(CatelTestModel.IntProperty)),
                new TestModelPropertyDescriptor(instance, nameof(CatelTestModel.StringProperty)),
                new TestModelPropertyDescriptor(instance, nameof(CatelTestModel.IntCatelProperty)),
                new TestModelPropertyDescriptor(instance, nameof(CatelTestModel.StringCatelProperty))
            };
        }

        public override void SetTypedValue(
            object instance, IEnumerable<IModelPropertyDescriptor> value)
        {
            throw new InvalidOperationException("Value cannot be set.");
        }

        #endregion
    }
}