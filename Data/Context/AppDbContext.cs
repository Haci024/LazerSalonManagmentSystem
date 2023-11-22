using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete
{
    public class AppDbContext : IdentityDbContext<AppUser,IdentityRole ,string>
    {
        public DbSet<LazerAppointment> LazerAppointments { get; set; }
        public DbSet<LazerAppointmentReports> LazerAppointmentReports { get; set; } 
        public DbSet<Kassa> Budget { get; set; }
        public DbSet<LazerCategory> LazerCategories { get; set; }
        public DbSet<LazerMaster> LazerMasters { get; set; }
        public DbSet<OutMoney> OutMoney { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<KassaActionList> KassaActionLists { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Filial> Filials { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<BodyShapingMaster> BodyShapingMasters { get; set; }    

  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=Odissey;initial catalog=LazerFullProjectForManagment;integrated Security=true;TrustServerCertificate=true;");
            optionsBuilder.ConfigureWarnings(warnings =>
            {
                warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored);
            });
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OutMoney>()
           .HasOne(o => o.Filial)
           .WithMany(x=>x.OutMoney)
           .HasForeignKey(o => o.FilialId)
           .OnDelete(DeleteBehavior.Restrict);
          
            modelBuilder.Entity<Income>()
         .HasOne(o => o.Filial)
         .WithMany(x=>x.Income)
         .HasForeignKey(o => o.FilialId)
         .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Income>()
      .HasOne(o => o.Stock)
      .WithMany(x => x.Incomes)
      .HasForeignKey(o => o.StockId)
      .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Stock>()
         .HasOne(o => o.Filial)
         .WithMany(x => x.Stock)
         .HasForeignKey(o => o.FilialId)
         .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity < Kassa>()
                    .HasOne(o => o.Filial)
                    .WithMany(x => x.Kassa)
                    .HasForeignKey(o => o.FilialId)
                    .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<SolariumAppointment>()
               .HasOne(o => o.Filial)
               .WithMany(x => x.SolariumAppointments)
               .HasForeignKey(o => o.FilialId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KassaActionList>()
               .HasOne(o => o.Kassa)
               .WithMany(x => x.KassaActionLists)
               .HasForeignKey(o => o.KassaId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LazerAppointment>()
           .HasOne(o => o.LazerMaster)
           .WithMany(x => x.LazerAppointment)
           .HasForeignKey(o => o.LazerMasterId)
           .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LazerAppointment>()
        .HasOne(o => o.Customers)
        .WithMany(x => x.LazerAppointments)
        .HasForeignKey(o => o.CustomerId)
        .OnDelete(DeleteBehavior.Restrict);
       
     modelBuilder.Entity<BodyshapingAppointment>()
   .HasOne(o => o.Filial)
   .WithMany(x => x.BodyShapingAppointments)
   .HasForeignKey(o => o.FilialId)
   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Stock>()
   .HasOne(o => o.Filial)
   .WithMany(x => x.Stock)
   .HasForeignKey(o => o.FilialId)
   .OnDelete(DeleteBehavior.Restrict);




            modelBuilder.Entity<LazerAppointment>()
   .HasOne(o => o.Filial)
   .WithMany(x => x.LazerAppointments)
   .HasForeignKey(o => o.FilialId)
   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Kassa>().HasData(
             new Kassa { Id = 1, Budget = 0,FilialId=1 },
               new Kassa { Id = 2, Budget = 0,FilialId=2 },
                 new Kassa { Id = 3, Budget = 0,FilialId=3 }

           );
            modelBuilder.Entity<Filial>().HasData(
          new Filial { Id = 1, FilialName = "Arzum Mini Lazer Studio"},
          new Filial { Id = 2, FilialName = "Arzum Beauty Studio"},
          new Filial { Id = 3, FilialName = "Arzum Estetik Studio"},
          new Filial { Id = 4, FilialName = "İdarə Paneli"}


        );
            modelBuilder.Entity<LazerCategory>().HasData(
        
           new LazerCategory { Id = 1, MainCategoryId = null,Name="Lazer Mini Qadınlar üçün",Price=0},
           new LazerCategory { Id = 2, MainCategoryId = null, Name = "Lazer Mini Kişilər üçün", Price = 0 },
           new LazerCategory { Id = 3, MainCategoryId = null, Name = "Lazer Beauty Qadınlar üçün", Price = 0 },
           new LazerCategory { Id = 4, MainCategoryId = null, Name = "Lazer Beauty Kişilər  üçün", Price = 0 },
           new LazerCategory { Id = 5, MainCategoryId = null, Name = "Lazer Qadınlar üçün Estetik Salon", Price = 0 },
          
           new LazerCategory { Id = 6, MainCategoryId = 1, Name = "Bütün bədən",Price=40 },
           new LazerCategory { Id = 7, MainCategoryId = 1, Name = "Sadə bədən",Price=35 },
           new LazerCategory { Id = 8, MainCategoryId = 1, Name = "Sinə & Qarın", Price=20 },
           new LazerCategory { Id = 9, MainCategoryId = 1, Name = "Kürək & Bel", Price=20 },
           new LazerCategory { Id = 10, MainCategoryId = 1, Name = "Bütün qol", Price=15 },
           new LazerCategory { Id = 11, MainCategoryId = 1, Name = "Qolaltı", Price=5 },
           new LazerCategory { Id = 12, MainCategoryId = 1, Name = "Üz", Price=5},
           new LazerCategory { Id = 13, MainCategoryId = 1, Name = "Dodaqüstü", Price=2},
           new LazerCategory { Id = 14, MainCategoryId = 1, Name = "Çənə", Price=3 },
           new LazerCategory { Id = 15, MainCategoryId = 1, Name = "Arxayan", Price = 10 },
           new LazerCategory { Id = 16, MainCategoryId = 1, Name = "Bikini", Price = 10 },
           new LazerCategory { Id = 17, MainCategoryId = 1, Name = "Bütün ayaq", Price = 20 },    
           new LazerCategory { Id = 18, MainCategoryId = 2, Name = "Yarı bədən", Price = 50 },
           new LazerCategory { Id = 19, MainCategoryId = 2, Name = "Üz & Boyun & Boğaz", Price = 10 },
           new LazerCategory { Id = 20, MainCategoryId = 2, Name = "Üz", Price = 5 },
           new LazerCategory { Id = 21, MainCategoryId = 2, Name = "Boyun", Price = 5 },
           new LazerCategory { Id = 22, MainCategoryId = 2, Name = "Boğaz", Price = 5 },
           new LazerCategory { Id = 23, MainCategoryId = 2, Name = "Yanaq", Price = 5 },
           new LazerCategory { Id = 24, MainCategoryId = 2, Name = "Qulaq", Price = 3 },
           new LazerCategory { Id = 25, MainCategoryId = 2, Name = "Bütün qol", Price = 25 },
           new LazerCategory { Id = 26, MainCategoryId = 2, Name = "Çiyin", Price = 10 },
           new LazerCategory { Id = 27, MainCategoryId = 2, Name = "Qolaltı", Price = 5 },
           new LazerCategory { Id = 28, MainCategoryId = 2, Name = "Kürək & Bel", Price = 30},
           new LazerCategory { Id = 29, MainCategoryId = 2, Name = "Sinə & Qarın", Price = 30 },
           new LazerCategory { Id = 30, MainCategoryId = 3, Name = "Bütün bədən", Price = 40 },
           new LazerCategory { Id = 31, MainCategoryId = 3, Name = "Sadə bədən", Price = 35 },
           new LazerCategory { Id = 32, MainCategoryId = 3, Name = "Sinə & Qarın", Price = 20 },
           new LazerCategory { Id = 33, MainCategoryId = 3, Name = "Kürək & Bel", Price = 20 },
           new LazerCategory { Id = 34, MainCategoryId = 4, Name = "Bütün qol", Price = 15 },
           new LazerCategory { Id = 35, MainCategoryId = 4, Name = "Qolaltı", Price = 5 },
           new LazerCategory { Id = 36, MainCategoryId = 4, Name = "Üz", Price = 5 },
           new LazerCategory { Id = 37, MainCategoryId = 4, Name = "Dodaqüstü", Price = 2 },
           new LazerCategory { Id = 38, MainCategoryId = 4, Name = "Çənə", Price = 3 },
           new LazerCategory { Id = 39, MainCategoryId = 4, Name = "Arxayan", Price = 10 },
           new LazerCategory { Id = 40, MainCategoryId = 4, Name = "Bikini", Price = 10 },
           new LazerCategory { Id = 41, MainCategoryId = 4, Name = "Bütün ayaq", Price = 20 },
           new LazerCategory { Id = 42, MainCategoryId = 4, Name = "Yarı bədən", Price = 50 },
           new LazerCategory { Id = 43, MainCategoryId = 5, Name = "Üz & Boyun & Boğaz", Price = 10 },
           new LazerCategory { Id = 44, MainCategoryId = 5, Name = "Üz", Price = 5 },
           new LazerCategory { Id = 45, MainCategoryId = 5, Name = "Boyun", Price = 5 },
           new LazerCategory { Id = 46, MainCategoryId = 5, Name = "Boğaz", Price = 5 },
           new LazerCategory { Id = 47, MainCategoryId = 5, Name = "Yanaq", Price = 5 },
           new LazerCategory { Id = 48, MainCategoryId = 5, Name = "Qulaq", Price = 3 },
           new LazerCategory { Id = 49, MainCategoryId = 5, Name = "Bütün qol", Price = 25 },
           new LazerCategory { Id = 50, MainCategoryId = 5, Name = "Çiyin", Price = 10 },
           new LazerCategory { Id = 51, MainCategoryId = 5, Name = "Qolaltı", Price = 5 },
           new LazerCategory { Id = 52, MainCategoryId = 5, Name = "Kürək & Bel", Price = 30 },
           new LazerCategory { Id = 53, MainCategoryId = 5, Name = "Sinə & Qarın", Price = 30 }

           );
            modelBuilder.Entity<LazerMaster>().HasData(
                new LazerMaster { Id = 1, FullName="Ellada",FilialId=1},
                new LazerMaster { Id = 2, FullName = "Aidə",FilialId=1},
                new LazerMaster { Id = 3, FullName = "Ellada", FilialId =2},
                new LazerMaster { Id = 4, FullName = "Nuridə", FilialId = 2},
                new LazerMaster { Id = 5, FullName = "Gülnar", FilialId = 3},
                 new LazerMaster { Id =6, FullName = "Nəzrin", FilialId = 3}
                );

            modelBuilder.Entity<SolariumCategories>().HasData(
             new SolariumCategories { Id = 1, MainCategoryId = null,Name="Günlük" ,Price =0,Minute=0 },
             new SolariumCategories { Id = 2, MainCategoryId = null,Name="Aylıq" ,Price = 0, Minute = 0 },
             new SolariumCategories { Id = 3, MainCategoryId = 1,Name="MiniPacket" ,Price = 4, Minute = 5,UsingPeriod=1},
             new SolariumCategories { Id = 4, MainCategoryId = 1, Name="MediumPacket",Price = 8, Minute = 10,UsingPeriod=1},
             new SolariumCategories { Id = 5, MainCategoryId =1 ,Name="LargePacket" ,Price = 12, Minute = 15 ,UsingPeriod=1},
             new SolariumCategories { Id = 6, MainCategoryId =1 ,Name="ExtraLarge" ,Price = 16, Minute = 20 ,UsingPeriod=30},
             new SolariumCategories { Id = 7, MainCategoryId = 2,Name="MiniPacket", Price = 19, Minute = 30 ,UsingPeriod=30},
             new SolariumCategories { Id = 8, MainCategoryId = 2,Name="MediumPacket" ,Price = 29, Minute = 40,UsingPeriod=30 },
             new SolariumCategories { Id = 9, MainCategoryId = 2,Name="LargePacket", Price = 39, Minute = 50,UsingPeriod=60 }
          
             );
            modelBuilder.Entity<Cosmetologs>().HasData(
         new Cosmetologs { Id = 1, FilialId = 2, FullName = "Nuray" }
     

         );
            modelBuilder.Entity<BodyShapingMaster>().HasData(
      new BodyShapingMaster { Id = 1, FilialId = 2, FullName = "Arifə" }


      );

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "qwertyu1234", Name = "Qeydiyyatçı", NormalizedName = "QEYDİYYATÇI"},
                new IdentityRole { Id = "qwerty12345", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "qwertys123567", Name = "SuperSupporter", NormalizedName = "SUPERSUPPORTER" },
                 new IdentityRole { Id = "qwertys123", Name = "Supporter", NormalizedName = "SUPPORTER" }
            );
        }


    }
}

