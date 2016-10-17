using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
    // resource's whole existence is to establish a relationship between two things. 
    // this is a join table in a many to many relationship. 
  public class LineItem
  {
    [Key]
    public int LineItemId {get;set;}

    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
  }
}