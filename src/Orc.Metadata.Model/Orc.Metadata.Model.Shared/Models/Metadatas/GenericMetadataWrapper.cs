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

    using Catel;

    /// <summary>
    ///     Wrap a non-generic <see cref="IMetadata" /> in a <see cref="GenericMetadata{T}" />
    ///     container.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericMetadataWrapper<T> : GenericMetadata<T>
    {
        #region Fields

        private readonly IMetadata _wrappedMetadata;

        #endregion



        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="GenericMetadataWrapper{T}" /> class.</summary>
        /// <param name="metadataToWrap">The metadata to wrap.</param>
        public GenericMetadataWrapper(IMetadata metadataToWrap)
        {
            Argument.IsNotNull(() => metadataToWrap);

            var targetType = typeof(T);

            if (_wrappedMetadata.Type != targetType)
            {
                throw new InvalidOperationException($"Invalid Type, should be {targetType}");
            }

            _wrappedMetadata = metadataToWrap;
        }

        #endregion



        #region Properties

        public override string Name => _wrappedMetadata.Name;
        public override string DisplayName
        {
            get { return _wrappedMetadata.DisplayName; }
            set { _wrappedMetadata.DisplayName = value; }
        }

        #endregion



        #region Methods

        public override T GetTypedValue(object instance)
        {
            return _wrappedMetadata.GetValue<T>(instance);
        }

        public override void SetTypedValue(object instance, T value)
        {
            _wrappedMetadata.SetValue(instance, value);
        }

        #endregion
    }
}