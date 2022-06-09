namespace ComputerShopDB.Entities.BaseEntity;

public abstract class Entity<TKey>
{
    public TKey Id { get; set; }
}