using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Model;

public class OrderDateSequence
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CurrentValue { get; set; }
}