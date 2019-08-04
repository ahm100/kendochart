using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ken4read.Models
{
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID
        {
            get;
            set;
        }
        [Required]

        public string Month
        {
            get;
            set;
        }
        [Required]

        public int MonthlyExpense
        {
            get;
            set;
        }
    }
}