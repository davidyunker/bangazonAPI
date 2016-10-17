using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class Order
  {
    [Key]
    public int OrderId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    
    [DataType(DataType.Date)]
    public DateTime? DateCompleted {get;set;}

// another foreign key 
    public int CustomerId {get;set;}

// another relationship 
    public Customer Customer {get;set;}

// It can have a null value because you can create an order without paying for it 

// this is the foreign key. What is it the foreign key to?
    public int? PaymentTypeId {get;set;}

    // establishing the relationship. Letting you know.  
    public PaymentType PaymentType {get;set;} 

    public ICollection<LineItem> LineItems;
  }
}