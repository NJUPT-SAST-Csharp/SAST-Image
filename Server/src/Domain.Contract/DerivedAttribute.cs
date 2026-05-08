using System.Text.Json.Serialization;

namespace Domain;

public abstract class DerivedAttribute<TType, TBaseType>()
    : JsonDerivedTypeAttribute(typeof(TType), typeof(TType).Name)
    where TType : TBaseType;
