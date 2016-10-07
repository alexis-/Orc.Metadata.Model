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

namespace Orc.Metadata.Model.Tests.Providers
{
    using System;
    using System.Threading.Tasks;

    using global::Catel.Data;
    using global::Catel.Threading;

    using Orc.Metadata.Model.Providers;
    using Orc.Metadata.Model.Tests.Models.Model;
    using Orc.Metadata.Model.Tests.Models.Properties;

    using ModelObjectType =
        Orc.Metadata.Model.Models.Interfaces.IModelObjectWithMetadata
        <Orc.Metadata.Model.Tests.Models.Model.CatelModelMetadataCollection>;

    /// <inheritdoc />
    public class CatelModelMetadataProvider :
        ModelMetadataBaseProvider<CatelModelMetadataCollection>
    {
        #region Fields

        private Action<CatelModelMetadataCollection> _modelConfigurationAction;

        #endregion



        #region Methods

        public override Task<ModelObjectType> GetModelMetadataAsync(object modelInstance)
        {
            if (modelInstance is ModelBase == false)
            {
                return TaskHelper<ModelObjectType>.DefaultValue;
            }

            var modelMetadatas = new CatelModelMetadataCollection();
            var modelObjectWithMetadata =
                new CatelModelObjectWithMetadata(modelInstance, modelMetadatas);

            _modelConfigurationAction?.Invoke(modelMetadatas);

            return TaskHelper<ModelObjectType>.FromResult(modelObjectWithMetadata);
        }

        public override void ConfigureWith(
            Action<CatelModelMetadataCollection> modelConfigurationAction)
        {
            _modelConfigurationAction = modelConfigurationAction;
        }

        #endregion
    }
}