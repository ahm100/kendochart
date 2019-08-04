namespace ken4read.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class mycontext : DbContext
    {
        public mycontext()
            : base("name=mycontext")
        {
        }

        public virtual DbSet<EmpCountries> EmpCountries { get; set; }
        public virtual DbSet<EmployeeList> EmployeeList { get; set; }
        public DbSet<Expense> MonthlyExpense { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeList>()
                .HasMany(e => e.EmpCountries)
                .WithRequired(e => e.EmployeeList)
                .HasForeignKey(e => e.EmpId)
                .WillCascadeOnDelete(false);
        }
    }
}
