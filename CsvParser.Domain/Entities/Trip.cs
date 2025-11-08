using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CsvParser.Domain.Entities;

public class Trip
{
    public int Id { get; set; }

    [Required]
    public DateTime PickupTime { get; set; }

    [Required]
    public DateTime DropoffTime { get; set; }

    [Required]
    public byte PassengerCount { get; set; }

    [Required]
    public float Distance { get; set; }

    [Required]
    [MaxLength(5)]
    public string StoreAndForwardFlag { get; set; }

    [Required]
    public short PickUpLocationId { get; set; }

    [Required]
    public short DropOffLocationId { get; set; }

    [Required]
    public decimal FareAmount { get; set; }

    [Required]
    public decimal TipAmount { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Trip {");
        sb.AppendLine($"  {nameof(Id)}: {Id}");
        sb.AppendLine($"  {nameof(PickupTime)}: {PickupTime}");
        sb.AppendLine($"  {nameof(DropoffTime)}: {DropoffTime}");
        sb.AppendLine($"  {nameof(PassengerCount)}: {PassengerCount}");
        sb.AppendLine($"  {nameof(Distance)}: {Distance}");
        sb.AppendLine($"  {nameof(StoreAndForwardFlag)}: {StoreAndForwardFlag}");
        sb.AppendLine($"  {nameof(PickUpLocationId)}: {PickUpLocationId}");
        sb.AppendLine($"  {nameof(DropOffLocationId)}: {DropOffLocationId}");
        sb.AppendLine($"  {nameof(FareAmount)}: {FareAmount}");
        sb.AppendLine($"  {nameof(TipAmount)}: {TipAmount}");
        sb.Append('}');

        return sb.ToString();
    }
}