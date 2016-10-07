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

namespace Orc.Metadata.Model.Providers
{
    using System;
    using System.Threading.Tasks;

    using Catel.Threading;

    using Orc.Metadata.Model.Models.Interfaces;
    using Orc.Metadata.Model.Providers.Interfaces;

    /// <summary>
    ///     Factory-pattern interface which generates a
    ///     <see cref="IModelObjectWithMetadata{TModel}" /> with the provided model
    ///     instance. See <see cref="ModelMetadataBaseProvider"/> for non-generic version.
    /// </summary>
    /// <typeparam name="TModel">
    ///     <see cref="IModelObjectWithMetadata{TModel}" /> model type constraint for
    ///     (optional) providers contract.
    /// </typeparam>
    public abstract class ModelMetadataBaseProvider<TModel> : IModelMetadataProvider<TModel>
        where TModel : class, IModelMetadataCollection
    {
        #region Fields

        private const string GetMetadataObsoleteMsg = "This method is not implemented.";
        private const string GetMetadataAsyncObsoleteMsg =
            "There is an overhead due to Task casting.";
        private const string GetMetadataObsoleteReplacementMethod = "GetModelMetadataAsync";

        #endregion



        #region Methods

        [ObsoleteEx(Message = GetMetadataObsoleteMsg,
             ReplacementTypeOrMember = GetMetadataObsoleteReplacementMethod,
             TreatAsErrorFromVersion = "1.1")]
        public IObjectWithMetadata GetMetadata(object obj)
        {
            throw new NotImplementedException();
        }

        [ObsoleteEx(Message = GetMetadataAsyncObsoleteMsg,
             ReplacementTypeOrMember = GetMetadataObsoleteReplacementMethod)]
        public Task<IObjectWithMetadata> GetMetadataAsync(object obj)
        {
            var task = GetModelMetadataAsync(obj);

            // Fast path
            if (task.IsCompleted)
            {
                return TaskHelper<IObjectWithMetadata>.FromResult(task.Result);
            }

            return GetMetadataInternalAsync(task);
        }

        /// <summary>
        ///     Asynchronously generates a
        ///     <see cref="IModelObjectWithMetadata{TModel}" /> from given model
        ///     instance.
        /// </summary>
        /// <param name="obj">Model instance.</param>
        /// <returns></returns>
        public abstract Task<IModelObjectWithMetadata<TModel>> GetModelMetadataAsync(object obj);

        /// <summary>Configures the <see cref="TModel"/>.</summary>
        /// <param name="modelConfigurationAction">The model configuration action.</param>
        public abstract void ConfigureWith(Action<TModel> modelConfigurationAction);

        private async Task<IObjectWithMetadata> GetMetadataInternalAsync(
            Task<IModelObjectWithMetadata<TModel>> task)
        {
            return await task.ConfigureAwait(false);
        }

        #endregion
    }

    /// <summary>
    ///     Factory-pattern interface which generates a
    ///     <see cref="IModelObjectWithMetadata{TModel}" /> with the provided model
    ///     instance. See <see cref="IModelMetadataProvider"/> for non-generic version.
    /// </summary>
    public abstract class ModelMetadataBaseProvider :
        ModelMetadataBaseProvider<IModelMetadataCollection>
    {
    }
}