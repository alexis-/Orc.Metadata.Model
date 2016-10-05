# Orc.Metadata.Model
**[Orc.Metadata](https://github.com/WildGums/Orc.Metadata)** abstraction layer for models meta-description.<br />
This project only defines the *interfaces* and convenience *base (abstract) classes*, and giving account to its purpose is thus left to their implementation.

## Usage examples

For purpose of illustration, we will assume the existence of a *fictive* **Orc.Metadata.Model.Reflection** implementation (feel free to write the actual implementation).<br/><br/>
This implementation uses reflection to provide metadata on model objects, and offers several parameters to customize the result.

### Using an implementation

The reflection implementation offers the following instantiation pathway and configurations :<br/>
```C#
var modelMetadataProvider = new ReflectionModelMetadataProvider();

// Tailor resulting metadatas to our need
modelMetadataProvider.ConfigureWith(modelConfig => modelConfig
    .IncludePublicProperties(true)
    .IncludePrivateProperties(true)
    .IncludePublicFields(false)
    .IncludePrivateFields(false)
    .AddMetadataCollection(myCustomModelMetadatas) // #1
    .ConfigurePropertiesWith(propertiesConfig => propertiesConfig
        .AddMetadataCollection(myCustomModelPropertiesMetadatas)) // #2
);

// Get a bundle of both our model instance, and metadatas which can be applied to it.
var modelWithMetadata = await provider.GetModelMetadataAsync(modelInstance);
```

\#1, #2: Optionally, you can aggregate other metadatas, which will be merged depending on the main provider implementation.<br/><br/>
Now, metadatas may be used as such :
```C#
// Get a list of available properties (public & private properties here)
ICollection<IModelPropertyDescriptor> propDescriptors =
    modelWithMetadata.GetMetadataValue(ModelMetadataTypes.PropertyDescriptors);

// Or get a property with its metadatas, directly by name
var propWithMetadatas =
    modelWithMetadata.GetPropertyObjectWithMetadataByName("MyPublicProperty");

// Get its value
var propValue = propWithMetadatas.GetMetadataValue(ModelPropertyMetadataTypes.Value);

// Assuming 'myCustomModelPropertiesMetadatas' contains a metadata named 'Operators'
var propAvailableOperators = propWithMetadatas.GetMetadataValue("Operators");
```

[ModelMetadataTypes](/blob/master/src/Orc.Metadata.Model/Orc.Metadata.Model.Shared/Models/Model/ModelMetadataTypes.cs) and [ModelPropertyMetadataTypes](/blob/master/src/Orc.Metadata.Model/Orc.Metadata.Model.Shared/Models/Properties/ModelPropertyMetadataTypes.cs) provide common metadatas name.<br />
Implementation of these metadatas is dependant on the provider.

### Building an implementation

A simple sample may be found in **[Orc.Metadata.Model.Catel.Shared](/blob/master/src/Orc.Metadata.Model.Tests/Orc.Metadata.Model.Catel.Shared/)**.
Steps are as follow:

* Implement *[ModelMetadataCollection](/blob/master/src/Orc.Metadata.Model/Orc.Metadata.Model.Shared/Models/Model/ModelMetadataCollection.cs)* or its interface *[IModelMetadataCollection](/blob/master/src/Orc.Metadata.Model/Orc.Metadata.Model.Shared/Models/Interfaces/IModelMetadataCollection.cs)*
* Implement *[ModelPropertyMetadataCollection](/blob/master/src/Orc.Metadata.Model/Orc.Metadata.Model.Shared/Models/Properties/ModelPropertyMetadataCollection.cs)* or its interface *[IModelPropertyMetadataCollection](/blob/master/src/Orc.Metadata.Model/Orc.Metadata.Model.Shared/Models/Interfaces/IModelPropertyMetadataCollection.cs)*
* Implement *[ModelMetadataBaseProvider](/blob/master/src/Orc.Metadata.Model/Orc.Metadata.Model.Shared/Providers/ModelMetadataBaseProvider.cs)* or its interface *[IModelMetadataBaseProvider](/blob/master/src/Orc.Metadata.Model/Orc.Metadata.Model.Shared/Providers/Interfaces/IModelMetadataBaseProvider.cs)*
* Implement properties descriptors and properties metadatas accessors.
* Implement your own metadatas and reference them from your model or property implementations.
<br/><br/> 
* (Optional) Implement *[ModelObjectWithData](/blob/master/src/Orc.Metadata.Model/Orc.Metadata.Model.Shared/Models/Model/ModelObjectWithData.cs)*
* (Optional) Implement *[ModelPropertyObjectWithMetadata](/blob/master/src/Orc.Metadata.Model/Orc.Metadata.Model.Shared/Models/Properties/ModelPropertyObjectWithMetadata.cs)*

### Using an unknown implementation (e.g. in a library)

#### Example using Catel ServiceLocator

#### Defining constraints


## Tech talk - interfaces & types explained

### IModelMetadataCollection

### IModelPropertyMetadataCollection

## Solution diagram

![Solution diagram](https://i.imgur.com/i9xgZ9l.png)
