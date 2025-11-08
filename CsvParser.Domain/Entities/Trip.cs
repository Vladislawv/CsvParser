using System.ComponentModel.DataAnnotations;

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
}