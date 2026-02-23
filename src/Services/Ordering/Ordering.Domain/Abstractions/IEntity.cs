namespace Ordering.Domain.Abstractions;

public interface IEntity<T> : IEntity
{
    public T Id { get; set; } // phụ thuộc vào kiểu khóa chính Gui, int hay string
}
public interface IEntity
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set;}
}
