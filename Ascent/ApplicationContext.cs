using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ascent
{
    public class Mountain
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_mointain { get; set; }
        public string Name_mount { get; set; }
        public string Height_mount { get; set; }
        public string Location_mount { get; set; }
    }
    public class The_route
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cod_route { get; set; }
        public string Name_route { get; set; }
        public string Leanght { get; set; }
        public string Route_danger { get; set; }

        [ForeignKey("Mountain")]
        public int Code_mount { get; set; }
        public virtual Mountain Mountain { get; set; }
        
    }
    public class Ascent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cod_ascent { get; set; }
        [ForeignKey("Membership")]
        public int Id_membership { get; set; }
        public virtual Membership Membership { get; set; }

        [ForeignKey("The_route")]
        public int Id_route { get; set; }
        public virtual The_route The_Route{ get; set; }
        public DateTime data_start { get; set; }
        public DateTime data_finish { get; set; }
    }
    public class Club
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cod_club { get; set; }
        public string Name_club { get; set; }
        public string Name_Sotr { get; set; }        
        public string Address { get; set; }
        public string Phone { get; set; }
    }
    public class Climber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int code_cl { get; set; }
        public string FIO { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
    }
    public class Membership
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cod_member { get; set; }

        [ForeignKey("Climber")]
        public int cod_Climber1 { get; set; }
        public virtual Climber Climber { get; set; }
      
        [ForeignKey("Club")]
        public int id_club { get; set; }
        public virtual Club Club { get; set; }

       
        
    }
    public class Users
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int users_id { get; set; }
        public string logins { get; set; }
        public string Passwords { get; set; }
    
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Club> Club { get; set; }
        public DbSet<Membership> Membership { get; set; }
        public DbSet<Climber> Climber { get; set; }
        public DbSet<Ascent> Ascent { get; set; }
        public DbSet<The_route> The_route { get; set; }
        public DbSet<Mountain> Mountain { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=Bar00606a.;database=VoshozhdenieFin;", new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
