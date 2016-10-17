using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
    // name of the class below is the name of the table 
  public class Customer
  {
    //   this is the primary key below 
    [Key]
    public int CustomerId {get;set;}
// this is required. It needs to be a date. And the database needs to create it. 
    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
// every customer has a collection of payment types 
// a customer has many payment types 
    public ICollection<PaymentType> PaymentTypes;
  }
}