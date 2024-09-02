namespace RobotCloudService.Application.Common;

public abstract class Entity : IEquatable<Entity>
{
    public bool Equals(Entity? other) => other is not null && EqualityComponents().SequenceEqual(other.EqualityComponents());
    public override bool Equals(object? obj) => obj is Entity other && Equals(other);
    public override int GetHashCode() => EqualityComponents().Aggregate(0, HashCode.Combine);
    protected abstract IEnumerable<ValueObject> EqualityComponents();
}
