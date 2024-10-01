namespace Inbazar.Domain.Primitives;

public class Entity : IEquatable<Entity>
{
    protected Entity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private init; }
    public bool Equals(Entity? other)
    {
        if (other is null)
            return false;

        if (other.GetType() != this.GetType())
            return false;

        return other.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 43;
    }
}
