namespace NewYou.Domain.Entities.Common;
public class BaseEntity : BaseKeyEntity
{
    public DateTime CreateData { get; set; }
    public DateTime? UpdateData { get; set; }
    public DateTime RemoveData { get; set; }
    public bool IsDeleted { get; set; }

}
