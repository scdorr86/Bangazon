using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string FBkey { get; set; }
    public bool isSeller { get; set; }
}