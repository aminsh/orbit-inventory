using orbit_inventory_core.Domain;

namespace orbit_inventory_domain.entity;

public class StockTransfer: IEntity, IHaveCreator, IHaveTimestamps
{
    public int Id { get; set; }
    public User CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime Date { get; set; }
    public TransferType Type { get; set; }
    public TransferStatus Status { get; set; }
    public ICollection<StockTransferLine> Lines { get; set; }
}

public enum TransferType
{
    Input,
    Output
}

public enum TransferStatus
{
    Draft,
    Fixed
}