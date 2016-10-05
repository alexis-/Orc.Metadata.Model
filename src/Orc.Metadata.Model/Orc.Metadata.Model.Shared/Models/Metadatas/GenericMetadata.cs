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

namespace Orc.Metadata.Model.Models.Metadatas
{
    using System;

    using Anotar.Catel;

    /// <summary>Constraints Metadata to a given type.</summary>
    /// <typeparam name="T">Target type.</typeparam>
    public abstract class GenericMetadata<T> : IMetadata
    {
        #region Properties

        public Type Type => typeof(T);

        public abstract string Name { get; }
        public abstract string DisplayName { get; set; }

        #endregion



        #region Methods

        public object GetValue(object instance)
        {
            return GetTypedValue(instance);
        }

        public void SetValue(object instance, object value)
        {
            if (value == null || value is T == false)
            {
                LogTo.Warning("invalid value.");
                return;
            }

            SetTypedValue(instance, (T)value);
        }

        public abstract T GetTypedValue(object instance);

        public abstract void SetTypedValue(object instance, T value);

        #endregion
    }
}