namespace Orc.Metadata.Model.Tests.Catel.Models.Properties
{
    using global::Catel.Data;
    using global::Catel.Reflection;

    using Orc.Metadata.Model.Models.Properties;

    public class CatelModelPropertyDescriptor : ModelPropertyDescriptor
    {
        #region Fields

        public const string PropertyDataKey = "PropertyData";
        public const string CachedPropertyInfoKey = "CachedPropertyInfo";

        #endregion



        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="CatelModelPropertyDescriptor" /> class.</summary>
        /// <param name="modelInstance">The model instance.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyData">The property data.</param>
        public CatelModelPropertyDescriptor(
            object modelInstance, string propertyName, PropertyData propertyData)
            : base(modelInstance, propertyName)
        {
            PropertyDescriptors[PropertyDataKey] = propertyData;
        }

        /// <summary>Initializes a new instance of the <see cref="CatelModelPropertyDescriptor" /> class.</summary>
        /// <param name="modelInstance">The model instance.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyInfo">The property data.</param>
        public CatelModelPropertyDescriptor(
            object modelInstance, string propertyName, CachedPropertyInfo propertyInfo)
            : base(modelInstance, propertyName)
        {
            PropertyDescriptors[CachedPropertyInfoKey] = propertyInfo;
        }

        #endregion
    }
}