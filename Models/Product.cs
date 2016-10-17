using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class Product
  {
    [Key]
    public int ProductId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}
// set a max character of 255 characters 
    [Required]
    [StringLength(255)]
    public string Description { get; set; }

    [Required]
    public double Price { get; set; }
    public ICollection<LineItem> LineItems;
    // compiler determines what type of collection to create. List, queue, dictionary, etc.
  }
}