using System.ComponentModel.DataAnnotations;
using System.Reflection;

const string NullableAttributeFullTypeName = "System.Runtime.CompilerServices.NullableAttribute";
const string NullableFlagsFieldName = "NullableFlags";

const string NullableContextAttributeFullName = "System.Runtime.CompilerServices.NullableContextAttribute";
const string NullableContextFlagsFieldName = "Flag";

var type = typeof(NullableReferenceTypes);
var property = type.GetProperty(nameof(NullableReferenceTypes.NullableReferenceType))!;
Console.WriteLine($"NullableReferenceType: {IsNullableReferenceType(type, member: null, property.GetCustomAttributes(inherit: true))}");

var a = new NullableReferenceTypes();

property = type.GetProperty(nameof(NullableReferenceTypes.NonNullableReferenceType))!;
Console.WriteLine($"NonNullableReferenceType: {IsNullableReferenceType(type, member: null, property.GetCustomAttributes(inherit: true))}");


bool IsNullableReferenceType(Type? containingType, MemberInfo? member, IEnumerable<object> attributes)
{
    if (HasNullableAttribute(attributes, out var result))
    {
        return result;
    }

    return IsNullableBasedOnContext(containingType, member);
}

bool HasNullableAttribute(IEnumerable<object> attributes, out bool isNullable)
{
    // [Nullable] is compiler synthesized, comparing by name.
    var nullableAttribute = attributes
        .FirstOrDefault(a => string.Equals(a.GetType().FullName, NullableAttributeFullTypeName, StringComparison.Ordinal));
    if (nullableAttribute == null)
    {
        isNullable = false;
        return false; // [Nullable] not found
    }

    // We don't handle cases where generics and NNRT are used. This runs into a
    // fundamental limitation of ModelMetadata - we use a single Type and Property/Parameter
    // to look up the metadata. However when generics are involved and NNRT is in use
    // the distance between the [Nullable] and member we're looking at is potentially
    // unbounded.
    //
    // See: https://github.com/dotnet/roslyn/blob/master/docs/features/nullable-reference-types.md#annotations
    if (nullableAttribute.GetType().GetField(NullableFlagsFieldName) is FieldInfo field &&
        field.GetValue(nullableAttribute) is byte[] flags &&
        flags.Length > 0 &&
        flags[0] == 1) // First element is the property/parameter type.
    {
        isNullable = true;
        return true; // [Nullable] found and type is an NNRT
    }

    isNullable = false;
    return true; // [Nullable] found but type is not an NNRT
}

bool IsNullableBasedOnContext(Type? containingType, MemberInfo? member)
{
    if (containingType is null)
    {
        return false;
    }

    // For generic types, inspecting the nullability requirement additionally requires
    // inspecting the nullability constraint on generic type parameters. This is fairly non-triviial
    // so we'll just avoid calculating it. Users should still be able to apply an explicit [Required]
    // attribute on these members.
    if (containingType.IsGenericType)
    {
        return false;
    }

    // The [Nullable] and [NullableContext] attributes are not inherited.
    //
    // The [NullableContext] attribute can appear on a method or on the module.
    var attributes = member?.GetCustomAttributes(inherit: false) ?? Array.Empty<object>();
    var isNullable = AttributesHasNullableContext(attributes);
    if (isNullable != null)
    {
        return isNullable.Value;
    }

    // Check on the containing type
    var type = containingType;
    do
    {
        attributes = type.GetCustomAttributes(inherit: false);
        isNullable = AttributesHasNullableContext(attributes);
        if (isNullable != null)
        {
            return isNullable.Value;
        }

        type = type.DeclaringType;
    }
    while (type != null);

    // If we don't find the attribute on the declaring type then repeat at the module level
    attributes = containingType.Module.GetCustomAttributes(inherit: false);
    isNullable = AttributesHasNullableContext(attributes);
    return isNullable ?? false;

    bool? AttributesHasNullableContext(object[] attributes)
    {
        var nullableContextAttribute = attributes
            .FirstOrDefault(a => string.Equals(a.GetType().FullName, NullableContextAttributeFullName, StringComparison.Ordinal));
        if (nullableContextAttribute != null)
        {
            if (nullableContextAttribute.GetType().GetField(NullableContextFlagsFieldName) is FieldInfo field &&
                field.GetValue(nullableContextAttribute) is byte @byte)
            {
                return @byte == 1; // [NullableContext] found
            }
        }

        return null;
    }
}

#nullable enable
public class NullableReferenceTypes
{
    public string NonNullableReferenceType { get; set; } = default!;
    [Required(ErrorMessage = "Test")]
    public string NonNullableReferenceTypeWithRequired { get; set; } = default!;
    public string? NullableReferenceType { get; set; } = default!;
    public void Method(string nonNullableParameter, string? nullableParameter)
    { }
}
#nullable restore
