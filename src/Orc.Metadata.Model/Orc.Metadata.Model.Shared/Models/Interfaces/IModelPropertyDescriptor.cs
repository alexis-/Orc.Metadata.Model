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

namespace Orc.Metadata.Model.Models.Interfaces
{
    using System.Collections.Generic;

    /// <summary>Model property descriptor.</summary>
    public interface IModelPropertyDescriptor
    {
        #region Properties

        /// <summary>Target model property name.</summary>
        string PropertyName { get; }

        /// <summary>Target model instance.</summary>
        object ModelInstance { get; }

        /// <summary>Gets available descriptor keys.</summary>
        IEnumerable<string> Keys { get; }

        /// <summary>Gets property descriptor for provided key.</summary>
        object this[string key] { get; }

        #endregion



        #region Methods

        /// <summary>Merge another <see cref="IModelPropertyDescriptor" /> descriptors with this instance.</summary>
        void MergePropertyDescriptor(IModelPropertyDescriptor other);

        #endregion
    }
}