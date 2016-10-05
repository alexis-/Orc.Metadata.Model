﻿// 
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
    using System.Collections.Generic;

    using Catel.Collections;

    /// <summary>Aggregate Metadatas.</summary>
    public abstract class MetadataAggregator : IMetadata
    {
        /// <summary>The aggregated metadatas.</summary>
        protected readonly List<IMetadata> AggregatedMetadatas = new List<IMetadata>();

        #region Properties

        public abstract string Name { get; }
        public abstract string DisplayName { get; set; }
        public abstract Type Type { get; }

        #endregion



        #region Methods

        public object GetValue(object instance)
        {
            return AggregateValue(instance, AggregatedMetadatas);
        }

        /// <summary>Adds the metadata to be aggregated.</summary>
        /// <param name="metadata">The metadata.</param>
        /// <exception cref="System.InvalidOperationException">
        ///   Thrown when types do not match.
        /// </exception>
        public void AddMetadata(IMetadata metadata)
        {
            if (metadata.Type != Type)
            {
                throw new InvalidOperationException($"Invalid type, should be : {Type}");
            }

            AggregatedMetadatas.Add(metadata);
        }

        /// <summary>Adds the metadatas to be aggregated.</summary>
        /// <param name="metadatas">The metadatas.</param>
        /// <exception cref="System.InvalidOperationException">
        ///   Thrown when types do not match.
        /// </exception>
        public void AddMetadataRange(IEnumerable<IMetadata> metadatas)
        {
            metadatas.ForEach(AddMetadata);
        }

        public abstract void SetValue(object instance, object value);

        protected abstract object AggregateValue(
            object instance, IEnumerable<IMetadata> metadatas);

        #endregion
    }
}