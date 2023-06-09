﻿namespace ChangeMe.Shared.Domain;

// TODO: To record
public abstract class TypedIdValueBase : IEquatable<TypedIdValueBase>
{
    public Guid Value { get; }

    protected TypedIdValueBase(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidOperationException("Id value cannot be empty!");

        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        return obj is TypedIdValueBase other && Equals(other);
    }

    public bool NotEquals(object? obj) => !Equals(obj);
    public bool Equals(TypedIdValueBase? other) => Value == other?.Value;

    public bool NotEquals(TypedIdValueBase? other) => !Equals(other);

    public static bool operator ==(TypedIdValueBase obj1, TypedIdValueBase obj2)
    {
        if (Equals(obj1, null))
        {
            if (Equals(obj2, null))
                return true;

            return false;
        }

        return obj1.Equals(obj2);
    }

    public static bool operator !=(TypedIdValueBase x, TypedIdValueBase y) => !(x == y);

    public override int GetHashCode() => Value.GetHashCode();
}