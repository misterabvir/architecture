namespace Domain.Abstractions;

public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; protected set; }
    public bool Equals(Entity? other)=> other is not null && Id.Equals(other.Id);
    public override bool Equals(object? obj) => obj is Entity other && Equals(other);
    public override int GetHashCode() => Id.GetHashCode();
    public static bool operator ==(Entity? left, Entity? right) => Equals(left, right);
    public static bool operator !=(Entity? left, Entity? right) => !Equals(left, right);
}