namespace Orc.Metadata.Model.Models.Properties
{
    using System.Collections.Generic;
    using System.Linq;

    using Catel;

    using Orc.Metadata.Model.Extensions;
    using Orc.Metadata.Model.Models.Interfaces;

    /// <summary>
    ///     Simple implementation of <see cref="IModelPropertyDescriptor" />. Grants access to a
    ///     given <see cref="IModelPropertyMetadataCollection" />.
    /// </summary>
    public class ModelPropertyDescriptor : IModelPropertyDescriptor
    {
        #region Fields

        public const string PropertyNameKey = "PropertyName";
        public const string ModelInstanceKey = "ModelInstance";

        protected readonly Dictionary<string, object> PropertyDescriptors =
            new Dictionary<string, object>();

        #endregion



        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="ModelPropertyDescriptor" /> class.</summary>
        /// <param name="modelInstance">The model instance.</param>
        /// <param name="propertyName">Name of the property.</param>
        public ModelPropertyDescriptor(object modelInstance, string propertyName)
        {
            PropertyDescriptors[ModelInstanceKey] = modelInstance;
            PropertyDescriptors[PropertyNameKey] = propertyName;

            PreserveValuesOnMerge = true;
        }

        #endregion



        #region Properties

        /// <summary>Target model property name.</summary>
        public string PropertyName => PropertyDescriptors[PropertyNameKey] as string;

        /// <summary>Target model instance.</summary>
        public object ModelInstance => PropertyDescriptors[ModelInstanceKey];

        /// <summary>Gets available descriptor keys.</summary>
        public IEnumerable<string> Keys => PropertyDescriptors.Keys;

        public bool PreserveValuesOnMerge { get; set; }

        /// <summary>Gets property descriptor for provided key.</summary>
        public object this[string key]
        {
            get { return PropertyDescriptors.SafeGet(key); }
            set { PropertyDescriptors[key] = value; }
        }

        #endregion



        #region Methods

        /// <summary>Merge another <see cref="IModelPropertyDescriptor" /> descriptors with this instance.</summary>
        public void MergePropertyDescriptor(IModelPropertyDescriptor other)
        {
            Argument.IsNotNull(() => other);

            var otherKeys = other.Keys.Except(new[] { PropertyNameKey, ModelInstanceKey });

            foreach (var key in otherKeys)
            {
                if (!PreserveValuesOnMerge || !Keys.Contains(key))
                {
                    this[key] = other[key];
                }
            }
        }

        protected bool Equals(ModelPropertyDescriptor other)
        {
            return PropertyName == other.PropertyName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((ModelPropertyDescriptor)obj);
        }

        public override int GetHashCode()
        {
            return PropertyName.GetHashCode();
        }

        #endregion
    }
}