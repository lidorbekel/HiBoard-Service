namespace HiBoard.Domain.Models;

public abstract class ModelBase<TEntity, TId>
    where TEntity : ModelBase<TEntity, TId>
{
    private int? _mCachedHashCode;

    public abstract TId Id { get; protected set; }

    public abstract DateTime CreatedAt { get; protected set; } 
    public abstract DateTime UpdatedAt { get; set; }


    public override bool Equals(object? obj)
    {
        var other = obj as TEntity;

        if (other == null) return false;

        if (IsTransient && other.IsTransient) {
            // Same object (CLR)
            return ReferenceEquals(this, other);
        }

        return Id is not null && Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        // Transient
        if (_mCachedHashCode.HasValue) return _mCachedHashCode.Value;

        if (IsTransient) {
            _mCachedHashCode = base.GetHashCode();

            return _mCachedHashCode.Value;
        }

        // Persisted
        if (Id is not null) return Id.GetHashCode();

        return -1;
    }

    public virtual bool IsTransient => Id is null || Id.Equals(default(TId));

    public virtual Type? GetEntityType()
    {
        var type = GetType();

        return type.Name.EndsWith("Proxy", StringComparison.Ordinal) ? type.BaseType : type;
    }

    public virtual bool IsOfType<T>() => typeof(T).IsAssignableFrom(GetEntityType());

    public static bool operator ==(ModelBase<TEntity, TId>? lh, ModelBase<TEntity, TId>? rh) => Equals(lh, rh);

    public static bool operator !=(ModelBase<TEntity, TId>? lh, ModelBase<TEntity, TId> rh) => !Equals(lh, rh);
}
