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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Catel.Collections;

    /// <summary>Aggregates <see cref="IMetadata" /> descriptors from provided
    ///     <see cref="IMetadataCollection" /> collection.</summary>
    /// <typeparam name="TChild">Child type for (optional) fluent building.</typeparam>
    public abstract class MetadataCollectionAggregator<TChild> : IMetadataCollection
        where TChild : MetadataCollectionAggregator<TChild>
    {
        #region Fields

        /// <summary>The <see cref="IMetadata" /> aggregated collection</summary>
        protected readonly HashSet<IMetadataCollection> MetadataCollections =
            new HashSet<IMetadataCollection>();

        /// <summary>Maps aggreated <see cref="IMetadata" /> with their <see cref="IMetadata.Name" />
        /// </summary>
        protected readonly Dictionary<string, IMetadata> MetadataKeyToMetadataDictionary =
            new Dictionary<string, IMetadata>();

        #endregion



        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MetadataCollectionAggregator{TChild}" />
        ///     class.
        /// </summary>
        /// <param name="metadataCollections">The metadata collections to aggregate.</param>
        protected MetadataCollectionAggregator(params IMetadataCollection[] metadataCollections)
        {
            if (metadataCollections != null)
            {
                AddMetadataCollectionRange(metadataCollections);
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MetadataCollectionAggregator{TChild}" />
        ///     class.
        /// </summary>
        protected MetadataCollectionAggregator()
        {
        }

        #endregion



        #region Enums

        /// <summary>Available actions when merging is enabled, and a conflict occurs.</summary>
        public enum MergeConflictActions
        {
            /// <summary>New value is ignored (default).</summary>
            Ignore = 0,

            /// <summary>Old value is replaced with new one.</summary>
            Replace = 1
        }

        #endregion



        #region Properties

        /// <summary>Gets all the aggregated <see cref="IMetadata" />.</summary>
        public virtual IEnumerable<IMetadata> All => MetadataKeyToMetadataDictionary.Values;

        /// <summary>
        ///     Action taken when a merge conflict occurs between two <see cref="IMetadata" /> of
        ///     same <see cref="IMetadata.Name" />.
        /// </summary>
        protected MergeConflictActions MergeConflictAction { get; set; } =
            MergeConflictActions.Ignore;

        #endregion



        #region Methods

        /// <summary>Gets the metadata matching the given name.</summary>
        /// <param name="metadataName">Name of the metadata.</param>
        /// <returns>Matching <see cref="IMetadata" /> or null if it cannot be found.</returns>
        public virtual IMetadata GetMetadata(string metadataName)
        {
            return MetadataKeyToMetadataDictionary.ContainsKey(metadataName)
                       ? MetadataKeyToMetadataDictionary[metadataName]
                       : All.FirstOrDefault(m => m.Name == metadataName);
        }

        /// <summary>Gets the enumerator.</summary>
        /// <returns></returns>
        public virtual IEnumerator<IMetadata> GetEnumerator()
        {
            return All.GetEnumerator();
        }

        /// <summary>Gets the enumerator.</summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>Gets the aggregated metadata collections.</summary>
        /// <returns></returns>
        public virtual IEnumerable<IMetadataCollection> GetAggregatedMetadataCollections()
        {
            return MetadataCollections;
        }

        /// <summary>Adds the provided metadata collection to aggregated collections.</summary>
        /// <param name="metadataCollection">The metadata collection.</param>
        /// <returns></returns>
        public virtual TChild AddMetadataCollection(IMetadataCollection metadataCollection)
        {
            if (!MetadataCollections.Contains(metadataCollection))
            {
                MetadataCollections.Add(metadataCollection);
                MergeMetadatas(metadataCollection.All);
            }

            return (TChild)this;
        }

        /// <summary>Adds the provided metadata collections to aggregated collections.</summary>
        /// <param name="metadataCollections">The metadata collection.</param>
        /// <returns></returns>
        public virtual TChild AddMetadataCollectionRange(
            params IMetadataCollection[] metadataCollections)
        {
            metadataCollections.ForEach(mc => AddMetadataCollection(mc));

            return (TChild)this;
        }

        /// <summary>
        ///     Actually merges the metadatas, deals with conflict according to
        ///     <see cref="MergeConflictAction" />.
        /// </summary>
        /// <param name="metadatas">The metadatas.</param>
        protected virtual void MergeMetadatas(IEnumerable<IMetadata> metadatas)
        {
            foreach (var metadata in metadatas)
            {
                bool addMetadata = true;

                if (MetadataKeyToMetadataDictionary.ContainsKey(metadata.Name))
                {
                    switch (MergeConflictAction)
                    {
                        case MergeConflictActions.Ignore:
                            addMetadata = false;
                            break;

                        case MergeConflictActions.Replace:
                            addMetadata = true;
                            break;
                    }
                }

                if (addMetadata)
                {
                    MetadataKeyToMetadataDictionary[metadata.Name] = metadata;
                }
            }
        }

        #endregion
    }
}