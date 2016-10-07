namespace Orc.Metadata.Model.Tests.Catel.Models.Properties
{
    using global::Catel.Data;
    using global::Catel.Reflection;

    using Orc.Metadata.Model.Models.Properties;

    public class TestModelPropertyDescriptor : ModelPropertyDescriptor
    {
        #region Fields

        public const string TestKey = "Test";

        #endregion



        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="TestModelPropertyDescriptor" /> class.</summary>
        /// <param name="modelInstance">The model instance.</param>
        /// <param name="propertyName">The property name.</param>
        public TestModelPropertyDescriptor(
            object modelInstance, string propertyName)
            : base(modelInstance, propertyName)
        {
            PropertyDescriptors[TestKey] = TestKey;
        }

        #endregion
    }
}