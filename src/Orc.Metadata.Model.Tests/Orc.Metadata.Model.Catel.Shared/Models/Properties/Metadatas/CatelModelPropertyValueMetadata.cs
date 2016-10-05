namespace Orc.Metadata.Model.Tests.Catel.Models.Properties.Metadatas
{
    using System;
    using System.Reflection;

    using global::Catel.Data;
    using global::Catel.Reflection;

    using Orc.Metadata.Model.Models.Properties;

    public class CatelModelPropertyValueMetadata : IMetadata
    {
        #region Properties

        public string Name => ModelPropertyMetadataTypes.Value;

        public string DisplayName
        {
            get { return Name; }
            set { }
        }

        public Type Type => typeof(object);

        #endregion



        #region Methods

        public object GetValue(object instance)
        {
            var catelModelPropDesc = (CatelModelPropertyDescriptor)instance;

            return GetPropertyInfo(instance)?.GetValue(catelModelPropDesc.ModelInstance, null);
        }

        public void SetValue(object instance, object value)
        {
            var catelModelPropDesc = (CatelModelPropertyDescriptor)instance;

            GetPropertyInfo(instance)?.SetValue(catelModelPropDesc.ModelInstance, value, null);
        }

        private PropertyInfo GetPropertyInfo(object instance)
        {
            var catelModelPropDesc = (CatelModelPropertyDescriptor)instance;

            if (catelModelPropDesc == null)
            {
                return null;
            }

            CachedPropertyInfo propInfo;
            var propData =
                (PropertyData)catelModelPropDesc[CatelModelPropertyDescriptor.PropertyDataKey];

            if (propData != null)
            {
                propInfo = propData.GetPropertyInfo(catelModelPropDesc.ModelInstance.GetType());
            }
            else
            {
                propInfo =
                    (CachedPropertyInfo)
                    catelModelPropDesc[CatelModelPropertyDescriptor.CachedPropertyInfoKey];
            }

            return propInfo?.PropertyInfo;
        }

        #endregion
    }
}