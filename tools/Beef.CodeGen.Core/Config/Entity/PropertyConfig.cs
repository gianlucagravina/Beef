﻿// Copyright (c) Avanade. Licensed under the MIT License. See https://github.com/Avanade/Beef

using Newtonsoft.Json;
using System;
using System.Linq;

namespace Beef.CodeGen.Config.Entity
{
    /// <summary>
    /// Represents the <b>Property</b> code-generation configuration.
    /// </summary>
    [ClassSchema("Property", Title = "The **Property** is used to define a property and its charateristics.", Description = "", Markdown = "")]
    [CategorySchema("Key", Title = "Provides the **key** configuration.")]
    [CategorySchema("Property", Title = "Provides the **Property** configuration.")]
    [CategorySchema("RefData", Title = "Provides the **Reference Data** configuration.")]
    [CategorySchema("Serialization", Title = "Provides the **Serialization** configuration.")]
    [CategorySchema("Data", Title = "Provides the generic **Data-layer** configuration.")]
    [CategorySchema("Database", Title = "Provides the specific **Database (ADO.NET)** configuration where `AutoImplement` is `Database`.")]
    [CategorySchema("EntityFramework", Title = "Provides the specific **Entity Framework** configuration where `AutoImplement` is `EntityFramework`.")]
    [CategorySchema("Cosmos", Title = "Provides the specific **Cosmos** configuration where `AutoImplement` is `Cosmos`.")]
    [CategorySchema("OData", Title = "Provides the specific **OData** configuration where `AutoImplement` is `OData`.")]
    [CategorySchema("Annotation", Title = "Provides additional property **Annotation** configuration.")]
    [CategorySchema("WebApi", Title = "Provides the data **Web API** configuration.")]
    [CategorySchema("Grpc", Title = "Provides the **gRPC** configuration.")]
    public class PropertyConfig : ConfigBase<CodeGenConfig, EntityConfig>
    {
        #region Key

        /// <summary>
        /// Gets or sets the unique property name.
        /// </summary>
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Key", Title = "The unique property name.", IsMandatory = true, IsImportant = true)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the overridding text for use in comments.
        /// </summary>
        [JsonProperty("text", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Key", Title = "The overriding text for use in comments.",
            Description = "By default the `Text` will be the `Name` reformatted as sentence casing. Depending on whether the `Type` is `bool`, will appear in one of the two generated sentences. Where not `bool` it will be: Gets or sets a value indicating whether {text}.'. " +
            "Otherwise, it will be: Gets or sets the {text}.'. To create a `<see cref=\"XXX\"/>` within use moustache shorthand (e.g. {{Xxx}}).")]
        public string? Text { get; set; }

        /// <summary>
        /// Indicates whether the property is inherited and therefore should not be output within the generated Entity class.
        /// </summary>
        [JsonProperty("inherited", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Key", Title = "Indicates whether the property is inherited and therefore should not be output within the generated Entity class.")]
        public bool? Inherited { get; set; }

        /// <summary>
        /// Gets or sets the overriding private name.
        /// </summary>
        [JsonProperty("privateName", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Key", Title = "The overriding private name.",
            Description = "Overrides the `Name` to be used for private fields. By default reformatted from `Name`; e.g. `FirstName` as `_firstName`.")]
        public string? PrivateName { get; set; }

        /// <summary>
        /// Gets or sets the overriding argument name.
        /// </summary>
        [JsonProperty("argumentName", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Key", Title = "The overriding argument name.",
            Description = "Overrides the `Name` to be used for argument parameters. By default reformatted from `Name`; e.g. `FirstName` as `firstName`.")]
        public string? ArgumentName { get; set; }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the .NET <see cref="Type"/>.
        /// </summary>
        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Property", Title = "The .NET `Type`.", IsImportant = true,
            Description = "Defaults to `string`. To reference a Reference Data `Type` always prefix with `RefDataNamespace` (e.g. `RefDataNamespace.Gender`). This will ensure that the appropriate Reference Data " +
            "using statement is used. Shortcut: Where the `Type` starts with (prefix) `RefDataNamespace.` and the correspondong `RefDataType` attribute is not specified it will automatically default the `RefDataType` to `string.`")]
        public string? Type { get; set; }

        /// <summary>
        /// Indicates whether the .NET <see cref="Type"/> should be declared as nullable.
        /// </summary>
        [JsonProperty("nullable", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Property", Title = "Indicates whether the .NET `Type should be declared as nullable; e.g. `string?`. Will be inferred where the `Type` is denoted as nullable; i.e. suffixed by a `?`.", IsImportant = true)]
        public bool? Nullable { get; set; }

        /// <summary>
        /// Indicates whether the property is considered part of the unique (primary) key.
        /// </summary>
        [JsonProperty("uniqueKey", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Property", Title = "Indicates whether the property is considered part of the unique (primary) key.", IsImportant = true,
            Description = "This is also used to simplify the parameter specification for an Entity Operation by inferrence.")]
        public bool? UniqueKey { get; set; }

        /// <summary>
        /// Indicates that the property Type is another generated entity / collection and therefore specific capabilities can be assumed (e.g. CopyFrom and Clone).
        /// </summary>
        [JsonProperty("isEntity", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Property", Title = "Indicates that the property `Type` is another generated entity / collection and therefore specific capabilities can be assumed (e.g. `CopyFrom` and `Clone`).", IsImportant = true)]
        public bool? IsEntity { get; set; }

        /// <summary>
        /// Indicates that the value is immutable and therefore cannot be changed once set.
        /// </summary>
        [JsonProperty("immutable", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Property", Title = "Indicates that the value is immutable and therefore cannot be changed once set.")]
        public bool? Immutable { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> transformation to be performed on <c>Set</c> and <c>CleanUp</c>.
        /// </summary>
        [JsonProperty("dateTimeTransform", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Property", Title = "The `DateTime` transformation to be performed on `Set` and `CleanUp`.", Options = new string[] { "UseDefault", "None", "DateOnly", "DateTimeLocal", "DateTimeUtc", "DateTimeUnspecified" },
            Description = "Defaults to `UseDefault`. This is only applied where the `Type` is `DateTime`.")]
        public string? DateTimeTransform { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="string"/> trimming of white space characters to be performed on <c>Set</c> and <c>CleanUp</c>.
        /// </summary>
        [JsonProperty("stringTrim", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Property", Title = "The `string` trimming of white space characters to be performed on `Set` and `CleanUp`.", Options = new string[] { "UseDefault", "None", "Start", "End", "Both" },
            Description = "Defaults to `UseDefault`. This is only applied where the `Type` is `string`.")]
        public string? StringTrim { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="string"/> transformation to be performed on <c>Set</c> and <c>CleanUp</c>.
        /// </summary>
        [JsonProperty("stringTransform", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Property", Title = "The `string` transformation to be performed on `Set` and `CleanUp`.", Options = new string[] { "UseDefault", "None", "NullToEmpty", "EmptyToNull" },
            Description = "Defaults to `UseDefault`. This is only applied where the `Type` is `string`.")]
        public string? StringTransform { get; set; }

        /// <summary>
        /// Indicates whether an instance of the <see cref="Type"/> is to be automatically created/instantiated when the property is first accessed (i.e. lazy instantiation).
        /// </summary>
        [JsonProperty("autoCreate", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Property", Title = "Indicates whether an instance of the `Type` is to be automatically created/instantiated when the property is first accessed (i.e. lazy instantiation).")]
        public bool? AutoCreate { get; set; }

        /// <summary>
        /// Gets or sets the C# code to default the value.
        /// </summary>
        [JsonProperty("default", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Property", Title = "The C# code to default the value.",
            Description = "Where the `Type` is `string` then the specified default value will need to be delimited. Any valid value assignment C# code can be used.")]
        public string? Default { get; set; }

        /// <summary>
        /// Gets or sets the names of the secondary property(s), comma delimited, that are to be notified on a property change.
        /// </summary>
        [JsonProperty("secondaryPropertyChanged", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Property", Title = "The names of the secondary property(s), comma delimited, that are to be notified on a property change.")]
        public string? SecondaryPropertyChanged { get; set; }

        /// <summary>
        /// Indicates whether the value should bubble up property changes versus only recording within the sub-entity itself.
        /// </summary>
        [JsonProperty("bubblePropertyChanges", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Property", Title = "Indicates whether the value should bubble up property changes versus only recording within the sub-entity itself.",
            Description = "Note that the `IsEntity` property is also required to enable.")]
        public bool? BubblePropertyChanged { get; set; }

        /// <summary>
        /// Indicates that CleanUp is not to be performed for the property within the Entity.CleanUp method.
        /// </summary>
        [JsonProperty("excludeCleanup", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Property", Title = "Indicates that `CleanUp` is not to be performed for the property within the `Entity.CleanUp` method.")]
        public bool? ExcludeCleanup { get; set; }

        #endregion

        #region RefData

        /// <summary>
        /// Gets or sets the underlying Reference Data Type that is also used as the Reference Data serialization identifier (SID).
        /// </summary>
        [JsonProperty("refDataType", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("RefData", Title = "The underlying Reference Data Type that is also used as the Reference Data serialization identifier (SID).", Options = new string[] { "string", "int", "Guid" },
            Description = "Defaults to `string` where not specified and the corresponding `Type` starts with (prefix) `RefDataNamespace.`.")]
        public string? RefDataType { get; set; }

        /// <summary>
        /// Indicates that the Reference Data property is to be a serializable list (ReferenceDataSidList). 
        /// </summary>
        [JsonProperty("refDataList", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("RefData", Title = "Indicates that the Reference Data property is to be a serializable list (`ReferenceDataSidList`).",
            Description = "This is required to enable a list of Reference Data values (as per `RefDataType`) to be passed as an argument for example.")]
        public bool? RefDataList { get; set; }

        /// <summary>
        /// Indicates whether a corresponding <i>text</i> property is added when generating a Reference Data property overridding the <c>CodeGeneration.RefDataText</c> selection.
        /// </summary>
        [JsonProperty("refDataText", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("RefData", Title = "Indicates whether a corresponding `text` property is added when generating a Reference Data property overridding the `Entity.RefDataText` selection.",
            Description = "This is used where serializing within the `Controller` and the `ExecutionContext.IsRefDataTextSerializationEnabled` is set to true (automatically performed where url contains `$text=true`).")]
        public bool? RefDataText { get; set; }

        /// <summary>
        /// Indicates whether the property should use the underlying Reference Data mapping capabilities. 
        /// </summary>
        [JsonProperty("refDataMapping", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("RefData", Title = "Indicates whether the property should use the underlying Reference Data mapping capabilities.",
            Description = "Mapped properties are a special Reference Data property type that ensure value uniqueness; this allows the likes of additional to/from mappings to occur between systems where applicable.")]
        public bool? RefDataMapping { get; set; }

        #endregion

        #region Serialization

        /// <summary>
        /// Gets or sets the JSON property name.
        /// </summary>
        [JsonProperty("jsonName", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Serialization", Title = "The JSON property name.",
            Description = "Defaults to `ArgumentName` where not specified (i.e. camelCase).")]
        public string? JsonName { get; set; }

        /// <summary>
        /// Indicates whether the property is not to be serialized.
        /// </summary>
        [JsonProperty("serializationIgnore", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Serialization", Title = "Indicates whether the property is not to be serialized.",
            Description = "All properties are serialized by default.")]
        public bool? SerializationIgnore { get; set; }

        /// <summary>
        /// Indicates whether to emit the default value when serializing.
        /// </summary>
        [JsonProperty("serializationEmitDefault", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Serialization", Title = "Indicates whether to emit the default value when serializing.")]
        public bool? SerializationEmitDefault { get; set; }

        /// <summary>
        /// Gets or sets the override JSON property name where outputting as a data model.
        /// </summary>
        [JsonProperty("dataModelJsonName", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Serialization", Title = "The override JSON property name where outputting as a data model.",
            Description = "Defaults to `JsonName` where not specified.")]
        public string? DataModelJsonName { get; set; }

        #endregion

        #region Data

        /// <summary>
        /// Gets or sets the data name where `Entity.AutoImplement` is selected.
        /// </summary>
        [JsonProperty("dataName", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Data", Title = "The data name where Entity.AutoImplement is selected.", IsImportant = true,
            Description = "Defaults to the property `Name`. Represents the column name for a `Database`, or the correspinding property name for the other options.")]
        public string? DataName { get; set; }

        /// <summary>
        /// Gets or sets the data `Converter` class name where `Entity.AutoImplement` is selected.
        /// </summary>
        [JsonProperty("dataConverter", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Data", Title = "The data `Converter` class name where `Entity.AutoImplement` is selected.", IsImportant = true,
            Description = "A `Converter` is used to convert a data source value to/from a .NET `Type` where no standard data conversion can be applied.")]
        public string? DataConverter { get; set; }

        /// <summary>
        /// Indicates whether the data `Converter` is a generic class and will automatically use the corresponding property `Type` as the generic `T`.
        /// </summary>
        [JsonProperty("dataConverterIsGeneric", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Data", Title = "Indicates whether the data `Converter` is a generic class and will automatically use the corresponding property `Type` as the generic `T`.")]
        public bool? DataConverterIsGeneric { get; set; }

        /// <summary>
        /// Indicates whether the property should be ignored (excluded) from the Data / DataMapper generated output. 
        /// </summary>
        [JsonProperty("dataMapperIgnore", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Data", Title = "Indicates whether the property should be ignored (excluded) from the `Data`-layer / data `Mapper` generated output.",
            Description = "All properties are included by default.")]
        public bool? DataMapperIgnore { get; set; }

        /// <summary>
        /// Indicates whether the `UniqueKey` property value is automatically generated by the data source on `Create`.
        /// </summary>
        [JsonProperty("dataAutoGenerated", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Data", Title = "Indicates whether the `UniqueKey` property value is automatically generated by the data source on `Create`.")]
        public bool? DataAutoGenerated { get; set; }

        /// <summary>
        /// Gets or sets the operations types (`ExecutionContext.OperationType`) selection to enable inclusion and exclusion of property mapping.
        /// </summary>
        [JsonProperty("dataOperationTypes", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Data", Title = "The operations types (`ExecutionContext.OperationType`) selection to enable inclusion and exclusion of property mapping.",
            Options = new string[] { "Any", "AnyExceptCreate", "AnyExceptUpdate", "AnyExceptGet", "Get", "Create", "Update", "Delete" },
            Description = "Defaults to `Any`.")]
        public string? DataOperationTypes { get; set; }

        #endregion

        #region Database

        /// <summary>
        /// Gets or sets the database property `Mapper` class name where `Entity.AutoImplement` is selected.
        /// </summary>
        [JsonProperty("databaseMapper", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Database", Title = "The database property `Mapper` class name where `Entity.AutoImplement` is selected.",
            Description = "A `Mapper` is used to map a data source value to/from a .NET complex `Type` (i.e. class with one or more properties).")]
        public string? DatabaseMapper { get; set; }

        /// <summary>
        /// Indicates whether the property should be ignored (excluded) from the database `Mapper` generated output.
        /// </summary>
        [JsonProperty("databaseIgnore", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Database", Title = "Indicates whether the property should be ignored (excluded) from the database `Mapper` generated output.")]
        public bool? DatabaseIgnore { get; set; }

        #endregion

        #region EntityFramework

        /// <summary>
        /// Gets or sets the Entity Framework property `Mapper` class name where `Entity.AutoImplement` is selected.
        /// </summary>
        [JsonProperty("entityFrameworkMapper", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("EntityFramework", Title = "The Entity Framework property `Mapper` class name where `Entity.AutoImplement` is selected.",
            Description = "A `Mapper` is used to map a data source value to/from a .NET complex `Type` (i.e. class with one or more properties).")]
        public string? EntityFrameworkMapper { get; set; }

        /// <summary>
        /// Indicates whether the property should be ignored (excluded) from the Entity Framework `Mapper` generated output.
        /// </summary>
        [JsonProperty("entityFrameworkIgnore", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("EntityFramework", Title = "Indicates whether the property should be ignored (excluded) from the Entity Framework `Mapper` generated output.")]
        public bool? EntityFrameworkIgnore { get; set; }

        #endregion

        #region Cosmos

        /// <summary>
        /// Gets or sets the Cosmos property `Mapper` class name where `Entity.AutoImplement` is selected.
        /// </summary>
        [JsonProperty("cosmosMapper", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Cosmos", Title = "The Cosmos property `Mapper` class name where `Entity.AutoImplement` is selected.",
            Description = "A `Mapper` is used to map a data source value to/from a .NET complex `Type` (i.e. class with one or more properties).")]
        public string? CosmosMapper { get; set; }

        /// <summary>
        /// Indicates whether the property should be ignored (excluded) from the Cosmos `Mapper` generated output.
        /// </summary>
        [JsonProperty("cosmosIgnore", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Cosmos", Title = "Indicates whether the property should be ignored (excluded) from the Cosmos `Mapper` generated output.")]
        public bool? CosmosIgnore { get; set; }

        #endregion

        #region OData

        /// <summary>
        /// Gets or sets the OData property `Mapper` class name where `Entity.AutoImplement` is selected.
        /// </summary>
        [JsonProperty("odataMapper", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("OData", Title = "The OData property `Mapper` class name where `Entity.AutoImplement` is selected.",
            Description = "A `Mapper` is used to map a data source value to/from a .NET complex `Type` (i.e. class with one or more properties).")]
        public string? ODataMapper { get; set; }

        /// <summary>
        /// Indicates whether the property should be ignored (excluded) from the OData `Mapper` generated output.
        /// </summary>
        [JsonProperty("odataIgnore", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("OData", Title = "Indicates whether the property should be ignored (excluded) from the OData `Mapper` generated output.")]
        public bool? ODataIgnore { get; set; }

        #endregion

        #region Annotation

        /// <summary>
        /// Gets or sets the display name used in the likes of error messages for the property.
        /// </summary>
        [JsonProperty("displayName", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Annotation", Title = "The display name used in the likes of error messages for the property.",
            Description = "Defaults to the `Name` as sentence case.")]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the property annotation (e.g. attribute) declaration code.
        /// </summary>
        [JsonProperty("annotation1", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Annotation", Title = "The property annotation (e.g. attribute) declaration code.")]
        public string? Annotation1 { get; set; }

        /// <summary>
        /// Gets or sets the property annotation (e.g. attribute) declaration code.
        /// </summary>
        [JsonProperty("annotation2", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Annotation", Title = "The property annotation (e.g. attribute) declaration code.")]
        public string? Annotation2 { get; set; }

        /// <summary>
        /// Gets or sets the property annotation (e.g. attribute) declaration code.
        /// </summary>
        [JsonProperty("annotation3", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Annotation", Title = "The property annotation (e.g. attribute) declaration code.")]
        public string? Annotation3 { get; set; }

        #endregion

        #region WebApi

        /// <summary>
        /// Gets or sets the `IPropertyMapperConverter` to perform `Type` to `string` conversion for writing to and parsing from the query string.
        /// </summary>
        [JsonProperty("webApiQueryStringConverter", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("WebApi", Title = "The `IPropertyMapperConverter` to perform `Type` to `string` conversion for writing to and parsing from the query string.")]
        public string? WebApiQueryStringConverter { get; set; }

        #endregion

        #region Grpc

        /// <summary>
        /// Gets or sets the unique (immutable) field number required to enable gRPC support.
        /// </summary>
        [JsonProperty("grpcFieldNo", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Grpc", Title = "The unique (immutable) field number required to enable gRPC support.", IsImportant = true)]
        public int? GrpcFieldNo { get; set; }

        /// <summary>
        /// Gets or sets the underlying gRPC data type.
        /// </summary>
        [JsonProperty("grpcType", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [PropertySchema("Grpc", Title = "The underlying gRPC data type; will be inferred where not specified.")]
        public string? GrpcType { get; set; }

        #endregion

        /// <summary>
        /// Gets the formatted summary text.
        /// </summary>
        public string? SummaryText => CodeGenerator.ToComments($"{(Type == "bool" ? "Indicates whether" : "Gets or sets the")} {Text}.");

        /// <summary>
        /// Gets the formatted summary text for the Reference Data Serialization Identifier (SID) property.
        /// </summary>
        public string? SummaryRefDataSid => CompareValue(RefDataList, true)
            ? CodeGenerator.ToComments($"Gets or sets the {{{{{Name}}}}} list using the underlying Serialization Identifier (SID).")
            : CodeGenerator.ToComments($"Gets or sets the {{{{{Name}}}}} using the underlying Serialization Identifier (SID).");

        /// <summary>
        /// Gets the formatted summary text for the Reference Data Text property.
        /// </summary>
        public string? SummaryRefDataText => $"Gets the corresponding {{{{{Name}}}}} text (read-only where selected).";

        /// <summary>
        /// Gets the formatted summary text when used in a parameter context.
        /// </summary>
        public string? ParameterSummaryText => CodeGenerator.ToComments($"{(Type == "bool" ? "Indicates whether" : "The")} {Text}.");

        /// <summary>
        /// Gets the <see cref="Name"/> formatted as see comments.
        /// </summary>
        public string? PropertyNameSeeComments => CodeGenerator.ToSeeComments(Name);

        /// <summary>
        /// Gets the computed declared property type.
        /// </summary>
        public string PropertyType => string.IsNullOrEmpty(RefDataType) 
            ? PrivateType 
            : (CompareValue(RefDataList, true) ? $"ReferenceDataSidList<{Type}, {RefDataType}>?" : CompareValue(Nullable, true) ? Type + "?" : Type!);

        /// <summary>
        /// Gets the computed declared private type.
        /// </summary>
        public string PrivateType
        {
            get
            {
                if (string.IsNullOrEmpty(RefDataType))
                    return CompareValue(Nullable, true) ? Type + "?" : Type!;

                var rt = CompareValue(RefDataList, true) ? $"List<{RefDataType}>" : RefDataType!;
                return CompareValue(Nullable, true) ? rt + "?" : rt!;
            }
        }

        /// <summary>
        /// Gets or sets the declared type including nullability.
        /// </summary>
        public string? DeclaredType { get; set; } 

        /// <summary>
        /// Gets the computed property name.
        /// </summary>
        public string PropertyName => string.IsNullOrEmpty(RefDataType) ? Name! : Name! + (CompareValue(RefDataList, true) ? "Sids" : "Sid");

        /// <summary>
        /// Gets the computed argument name.
        /// </summary>
        public string PropertyArgumentName => string.IsNullOrEmpty(RefDataType) ? ArgumentName! : ArgumentName! + (CompareValue(RefDataList, true) ? "Sids" : "Sid");

        /// <summary>
        /// Gets the computed private name.
        /// </summary>
        public string PropertyPrivateName => string.IsNullOrEmpty(RefDataType) ? PrivateName! : PrivateName! + (CompareValue(RefDataList, true) ? "Sids" : "Sid");

        /// <summary>
        /// Gets the computed data mapper property name.
        /// </summary>
        public string DataMapperPropertyName => string.IsNullOrEmpty(RefDataType) ? Name! : CompareNullOrValue(DataConverter, "ReferenceDataCodeConverter") ? PropertyName : Name!;

        /// <summary>
        /// Gets the data converter C# code.
        /// </summary>
        public string? DataConverterCode => string.IsNullOrEmpty(DataConverter) ? null : $".SetConverter({DataConverter}{(CompareValue(DataConverterIsGeneric, true) ? $"<{Type}>" : "")}.Default!)";

        /// <summary>
        /// Gets the data converter C# code for reference data data access.
        /// </summary>
        public string? RefDataConverterCode => string.IsNullOrEmpty(DataConverter) ? null : $"{DataConverter}{(CompareValue(DataConverterIsGeneric, true) ? $"<{Type}>" : "")}.Default.ConvertToSrce(";

        /// <summary>
        /// Gets the WebAPI parameter type.
        /// </summary>
        public string WebApiParameterType => (string.IsNullOrEmpty(RefDataType) ? (string.IsNullOrEmpty(WebApiQueryStringConverter) ? Type! : "string") : (CompareValue(RefDataList, true) ? $"List<{RefDataType}>" : RefDataType!)) + (CompareValue(Nullable, true) ? "?" : "");

        /// <summary>
        /// Gets or sets the gRPC converter.
        /// </summary>
        public string? GrpcConverter { get; set; }

        /// <summary>
        /// Gets or sets the gRPC mapper.
        /// </summary>
        public string? GrpcMapper { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override void Prepare()
        {
            Type = DefaultWhereNull(Type, () => "string");
            if (Type!.StartsWith("RefDataNamespace.", StringComparison.InvariantCulture))
                RefDataType = DefaultWhereNull(RefDataType, () => "string");

            if (RefDataType != null && !Type!.StartsWith("RefDataNamespace.", StringComparison.InvariantCulture))
                Type = $"RefDataNamespace.{Type}";

            if (Type!.EndsWith("?", StringComparison.InvariantCulture))
            {
                Type = Type[0..^1];
                Nullable = true;
            }

            DeclaredType = $"{Type}{(CompareValue(Nullable, true) ? "?" : "")}";

            Text = CodeGenerator.ToComments(DefaultWhereNull(Text, () =>
            {
                if (Type!.StartsWith("RefDataNamespace.", StringComparison.InvariantCulture))
                    return $"{StringConversion.ToSentenceCase(Name)} (see {CodeGenerator.ToSeeComments(Type)})";

                if (Type == "ChangeLog")
                    return $"{StringConversion.ToSentenceCase(Name)} (see {CodeGenerator.ToSeeComments("Beef.Entities." + Type)})";

                var ent = Root!.Entities.FirstOrDefault(x => x.Name == Type);
                if (ent != null)
                {
                    if (ent.EntityScope == null || ent.EntityScope == "Common")
                        return $"{StringConversion.ToSentenceCase(Name)} (see {CodeGenerator.ToSeeComments("Common.Entities." + Type)})";
                    else
                        return $"{StringConversion.ToSentenceCase(Name)} (see {CodeGenerator.ToSeeComments("Business.Entities." + Type)})";
                }

                return StringConversion.ToSentenceCase(Name);
            }));

            PrivateName = DefaultWhereNull(PrivateName, () => StringConversion.ToPrivateCase(Name));
            ArgumentName = DefaultWhereNull(ArgumentName, () => StringConversion.ToCamelCase(Name));
            DateTimeTransform = DefaultWhereNull(DateTimeTransform, () => "UseDefault");
            StringTrim = DefaultWhereNull(StringTrim, () => "UseDefault");
            StringTransform = DefaultWhereNull(StringTransform, () => "UseDefault");
            RefDataText = DefaultWhereNull(RefDataText, () => Parent!.RefDataText);
            DisplayName = DefaultWhereNull(DisplayName, () => GenerateDisplayName());
            Nullable = DefaultWhereNull(Nullable, () => !Beef.CodeGen.CodeGenConfig.IgnoreNullableTypes.Contains(Type!));
            JsonName = DefaultWhereNull(JsonName, () => ArgumentName);
            SerializationEmitDefault = DefaultWhereNull(SerializationEmitDefault, () => CompareValue(UniqueKey, true));
            DataModelJsonName = DefaultWhereNull(DataModelJsonName, () => JsonName);
            DataOperationTypes = DefaultWhereNull(DataOperationTypes, () => "Any");
            IsEntity = DefaultWhereNull(IsEntity, () => Parent!.Parent!.Entities!.Any(x => x.Name == Type) && RefDataType == null);
            Immutable = DefaultWhereNull(Immutable, () => false);
            BubblePropertyChanged = DefaultWhereNull(BubblePropertyChanged, () => CompareValue(IsEntity, true));

            DataConverter = DefaultWhereNull(DataConverter, () => string.IsNullOrEmpty(RefDataType) ? null : Root!.RefDataDefaultMapperConverter);
            if (!string.IsNullOrEmpty(DataConverter) && (DataConverter.EndsWith("{T}", StringComparison.InvariantCulture) || DataConverter.EndsWith("<T>", StringComparison.InvariantCulture)))
            {
                DataConverterIsGeneric = true;
                DataConverter = DataConverter![0..^3];
            }

            if (CompareValue(RefDataType, "string") && CompareValue(DataConverter, "ReferenceDataCodeConverter"))
                DataConverter = null;

            GrpcType = DefaultWhereNull(GrpcType, () => InferGrpcType(string.IsNullOrEmpty(RefDataType) ? Type! : RefDataType!, RefDataType, RefDataList, DateTimeTransform));
            GrpcMapper = Beef.CodeGen.CodeGenConfig.SystemTypes.Contains(Type) || RefDataType != null ? null : Type;
            GrpcConverter = Type switch
            {
                "DateTime" => $"{(CompareValue(Nullable, true) ? "Nullable" : "")}{(DateTimeTransform == "DateOnly" ? "DateTimeToDateOnly" : "DateTimeToTimestamp")}",
                "Guid" => $"{(CompareValue(Nullable, true) ? "Nullable" : "")}GuidToStringConverter",
                "decimal" => $"{(CompareValue(Nullable, true) ? "Nullable" : "")}DecimalToDecimalConverter",
                _ => null
            };
        }

        /// <summary>
        /// Generates the display name (checks for Id and handles specifically).
        /// </summary>
        private string GenerateDisplayName()
        {
            var dn = StringConversion.ToSentenceCase(Name)!;
            var parts = dn.Split(' ');
            if (parts.Length == 1)
                return (parts[0] == "Id") ? "Identifier" : dn;

            if (parts.Last() != "Id")
                return dn;

            var parts2 = new string[parts.Length - 1];
            Array.Copy(parts, parts2, parts.Length - 1);
            return string.Join(" ", parts2);
        }

        /// <summary>
        /// Infers the gRPC data type.
        /// </summary>
        internal static string InferGrpcType(string type, string? refDataType = null, bool? refDataList = null, string? dateTimeTransform = null)
        {
            var gt = type switch
            {
                "string" => "google.protobuf.StringValue",
                "bool" => "google.protobuf.BoolValue",
                "double" => "google.protobuf.DoubleValue",
                "float" => "google.protobuf.FloatValue",
                "int" => "google.protobuf.Int32Value",
                "long" => "google.protobuf.Int64Value",
                "unit" => "google.protobuf.UInt32Value",
                "ulong" => "google.protobuf.UInt64Value",
                "short" => "google.protobuf.Int32Value",  // Not natively supported
                "ushort" => "google.protobuf.UInt32Value", // Not natively supported
                "Guid" => "google.protobuf.StringValue", // Not natively supported
                "byte[]" => "bytes", // Not natively supported
                "Decimal" => "Decimal", // Not natively supported
                "DateTime" => string.Compare(dateTimeTransform, "DateOnly", StringComparison.InvariantCulture) == 0 ? "DateOnly" : "google.protobuf.Timestamp", // DateOnly not natively supported
                "TimeSpan" => "google.protobuf.Duration",
                "void" => "google.protobuf.Empty",
                _ => type
            };

            return !string.IsNullOrEmpty(refDataType) && CompareValue(refDataList, true) ? "repeated " + gt : gt;
        }
    }
}