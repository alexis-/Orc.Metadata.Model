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
    using global::Catel;
    using global::Catel.Data;
    using global::Catel.Logging;

    using Orc.Metadata.Model.Models.Model;
    using Orc.Metadata.Model.Tests.Models.Properties;

    /// <inheritdoc />
    public class CatelModelObjectWithMetadata
        : ModelObjectWithMetadata
            <CatelModelMetadataCollection, CatelModelPropertyMetadataCollection>
    {
        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="CatelModelObjectWithMetadata" /> class.</summary>
        /// <param name="modelInstance">The model instance.</param>
        /// <param name="modelMetadataCollection">The model metadata collection.</param>
        public CatelModelObjectWithMetadata(
            object modelInstance, CatelModelMetadataCollection modelMetadataCollection)
            : base(modelInstance, modelMetadataCollection)
        {
            Argument.IsNotNull(() => modelInstance);

            if (modelInstance is ModelBase == false)
            {
                LogManager.GetCurrentClassLogger()
                          .Warning(
                              $"modelInstance should be of type ModelBase, type is : {modelInstance.GetType()}");
            }
        }

        #endregion
    }
}