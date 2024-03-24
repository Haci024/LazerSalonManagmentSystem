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
        public DbSet<LazerCategory> LazerCategories { get; set; }
        public DbSet<LazerMaster> LazerMasters { get; set; }
        public DbSet<OutMoney> OutMoney { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cosmetologs> Cosmetologs { get; set; } 
        public DbSet<CosmetologyAppointment>CosmetologyAppointments { get; set;}
        public DbSet<CosmetologyCategory> CosmetologyCategories { get; set; }
        public DbSet<KassaActionList> KassaActionLists { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Filial> Filials { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<BodyShapingMaster> BodyShapingMasters { get; set; }
        public DbSet<BodyShapingPacketCategory> BodyShapingPacketCategories { get; set; }
        public DbSet<BodyShapingPacketsReports> BodyShapingPacketsReports { get; set; }
        public DbSet<BodyshapingAppointment> BodyShapingAppointments { get; set; }
        public DbSet<BodyShapingSessionList> BodyShapingSessionList { get; set; }
        public DbSet<SolariumAppointment> SolariumAppointments { get; set; }
        public DbSet<LazerMasterFilial> LazerMasterFilial { get; set; }
        public DbSet<CosmetologFilial> CosmetologFilials { get; set; }
        public DbSet<SolariumCategories> SolariumCategories { get; set; }
        public DbSet<SolariumUsingList> SolariumUsingLists { get; set; }
        public DbSet<PirsinqAppointment> PirsinqAppointments { get; set; }
        public DbSet<LipuckaAppointment> LipuckaAppointments { get; set; }
        public DbSet<LipuckaCategories> LipuckaCategories { get; set; }
        public DbSet<PirsinqCategory> PirsinqCategories { get; set; }
        public DbSet<LipuckaReports> LipuckaReports { get; set; }
        public DbSet<PirsinqReports> PirsinqReports { get; set; }
        public DbSet<SpendCategory> SpendCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=ODISSEY\\SQLEXPRESS01;initial catalog=SakinaBeautySalon;integrated Security=true;TrustServerCertificate=true;");
            optionsBuilder.ConfigureWarnings(warnings =>
            {
                warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored);
            });
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

   

            modelBuilder.Entity<Customer>()
        .HasOne(o => o.Filial)
        .WithMany(x => x.Customer)
        .HasForeignKey(o => o.FilialId)
        .OnDelete(DeleteBehavior.Restrict);

       modelBuilder.Entity<CosmetologyAppointment>()
  .HasOne(o => o.Filial)
  .WithMany(x => x.CosmetologyAppointments)
  .HasForeignKey(o => o.FilialId)
  .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LazerCategory>()
.HasOne(o => o.Filial)
.WithMany(x => x.LazerCategories)
.HasForeignKey(o => o.FilialId)
.OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PirsinqAppointment>()
 .HasOne(o => o.Filial)
 .WithMany(x => x.PirsinqAppointments)
 .HasForeignKey(o => o.FilialId)
 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LipuckaAppointment>()
 .HasOne(o => o.Filial)
 .WithMany(x => x.LipuckaAppointments)
 .HasForeignKey(o => o.FilialId)
 .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BodyShapingSessionList>()
.HasOne(o => o.AppUser)
.WithMany(x => x.BodyshapingSessionList)
.HasForeignKey(o => o.AppUserId)
.OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SolariumUsingList>()
.HasOne(o => o.AppUser)
.WithMany(x => x.SolariumUsingList)
.HasForeignKey(o => o.AppUserId)
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
            modelBuilder.Entity<BodyshapingAppointment>()
           .HasOne(o => o.AppUser)
           .WithMany(x => x.BodyshapingAppointments)
           .HasForeignKey(o => o.AppUserId);
            modelBuilder.Entity<SolariumAppointment>()
         .HasOne(o => o.AppUser)
         .WithMany(x => x.SolariumAppointments)
         .HasForeignKey(o => o.AppUserId)
     .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CosmetologyAppointment>()
     .HasOne(o => o.AppUser)
     .WithMany(x => x.CosmetologyAppointments)
     .HasForeignKey(o => o.AppUserId)
 .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity <KassaActionList>()
                    .HasOne(o => o.Filial)
                    .WithMany(x => x.KassaActionList)
                    .HasForeignKey(o => o.FilialId)
                    .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<SolariumAppointment>()
               .HasOne(o => o.Filial)
               .WithMany(x => x.SolariumAppointments)
               .HasForeignKey(o => o.FilialId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SpendCategory>()
              .HasOne(o => o.Filial)
              .WithMany(x => x.SpendCategory)
              .HasForeignKey(o => o.FilialId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LazerAppointment>()
           .HasOne(o => o.LazerMaster)
           .WithMany(x => x.LazerAppointment)
           .HasForeignKey(o => o.LazerMasterId)
           .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BodyshapingAppointment>()
          .HasOne(o => o.BodyShapingMaster)
          .WithMany(x => x.BodyShapingAppointment)
          .HasForeignKey(o => o.BodyshapingMasterId)
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

         
            modelBuilder.Entity<Filial>().HasData(
          new Filial { Id = 1, FilialName = "Arzum Mini Laser Studio"},
          new Filial { Id = 2, FilialName = "Arzum Beauty Studio"},
          new Filial { Id = 3, FilialName = "Arzum Estetik Studio"},
          new Filial { Id = 4, FilialName = "İdarə Paneli"},
            new Filial { Id = 5, FilialName = "Texniki Dəstək" }


        );
  
            modelBuilder.Entity<LazerCategory>().HasData(
        
           new LazerCategory { Id = 1, MainCategoryId = null,Name="Qadın",Price=0,FilialId=1},
           new LazerCategory { Id = 2, MainCategoryId = null, Name = " Kişi", Price = 0, FilialId=1},
           new LazerCategory { Id = 3, MainCategoryId = null, Name = "Qadın", Price = 0,FilialId=2 },
           new LazerCategory { Id = 4, MainCategoryId = null, Name = "Kişi", Price = 0,FilialId=2 },
           new LazerCategory { Id = 5, MainCategoryId = null, Name = "Qadın", Price = 0,FilialId=3 }
          
           );

           
    
       

            modelBuilder.Entity<SolariumCategories>().HasData(
             new SolariumCategories { Id = 1, MainCategoryId = null,Name="Günlük" ,Price =0,Minute=0 },
             new SolariumCategories { Id = 2, MainCategoryId = null,Name="Aylıq" ,Price = 0, Minute = 0 },
             new SolariumCategories { Id = 3, MainCategoryId = 1,Name="MiniPacket" ,Price = 5, Minute = 5,UsingPeriod=1},
             new SolariumCategories { Id = 4, MainCategoryId = 1, Name="MediumPacket",Price = 8, Minute = 10,UsingPeriod=1},
             new SolariumCategories { Id = 5, MainCategoryId =1 ,Name="LargePacket" ,Price = 12, Minute = 15 ,UsingPeriod=1},
             new SolariumCategories { Id = 6, MainCategoryId =1 ,Name="ExtraLarge" ,Price = 15, Minute = 20 ,UsingPeriod=1},
             new SolariumCategories { Id = 7, MainCategoryId = 2,Name="MiniPacket", Price = 20, Minute = 50 ,UsingPeriod=30},
             new SolariumCategories { Id = 8, MainCategoryId = 2,Name="MediumPacket" ,Price =35, Minute = 100,UsingPeriod=60 },
             new SolariumCategories { Id = 9, MainCategoryId = 2,Name="LargePacket", Price = 45, Minute = 150,UsingPeriod=60 }
             );
            modelBuilder.Entity<BodyShapingPacketCategory>().HasData(
               new BodyShapingPacketCategory { Id = 1, MainCategoryId = null, Packet = "G8 Turbo", IsDeactive = false, SessionCount = 0,SessionDuration=0},
               new BodyShapingPacketCategory { Id=2, MainCategoryId = null, Packet = "Miostimuliyasiya", IsDeactive = false, SessionCount = 0, SessionDuration = 0 },
                new BodyShapingPacketCategory { Id = 3, MainCategoryId = null, Packet = "Termoyorğan", IsDeactive = false, SessionCount = 0, SessionDuration = 0 }
               );
            modelBuilder.Entity<LipuckaCategories>().HasData(

                new LipuckaCategories { Id = 1, Name = "Lipuçka Qadın", MainCategoryId = null, Price = 0, IsDeactive = false },
                new LipuckaCategories { Id = 2, Name = "Lipuçka Kişi", MainCategoryId = null, Price = 0, IsDeactive = false }

           );
            modelBuilder.Entity<CosmetologyCategory>().HasData(
                new CosmetologyCategory { Id = 1, MainCategoryId = null, CategoryName = "Plazmaliftinq", IsDeactive = false },
                new CosmetologyCategory { Id = 2, MainCategoryId = null, CategoryName = "Mezoterapiya", IsDeactive = false },
                new CosmetologyCategory { Id = 3, MainCategoryId = null, CategoryName = "Plazma + Mezo", IsDeactive = false },
                new CosmetologyCategory { Id = 4, MainCategoryId = null, CategoryName = "Lipalitik", IsDeactive = false },
                new CosmetologyCategory { Id = 5, MainCategoryId = null, CategoryName = "Birovetializasiya", IsDeactive = false },
                new CosmetologyCategory { Id = 6, MainCategoryId = null, CategoryName = "Dolğu", IsDeactive = false },
                new CosmetologyCategory { Id = 7, MainCategoryId = null, CategoryName = "Botoks", IsDeactive = false },
                new CosmetologyCategory { Id = 8, MainCategoryId = null, CategoryName = "Saplarla liftinq", IsDeactive = false },
                new CosmetologyCategory { Id = 9, MainCategoryId = null, CategoryName = "Pilinq", IsDeactive = false },
                new CosmetologyCategory { Id = 10, MainCategoryId = null, CategoryName = "Hicama ", IsDeactive = false },
                new CosmetologyCategory { Id = 11, MainCategoryId = null, CategoryName = "Pirsinq", IsDeactive = false },
                new CosmetologyCategory { Id = 12, MainCategoryId = null, CategoryName = "Üz təmizləməsi", IsDeactive = false }

                ); ;
            modelBuilder.Entity<PirsinqCategory>().HasData(

             new PirsinqCategory { Id = 1, CategoryName = "Pirsinq Qadın", MainCategoryId = null, Price = 0, IsDeactive = false },
             new PirsinqCategory { Id = 2, CategoryName = "Pirsinq Kişi", MainCategoryId = null, Price = 0, IsDeactive = false }

        );




            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "qwertyu1234", Name = "Reservator", NormalizedName = "RESERVATOR"},
                new IdentityRole { Id = "qwerty12345", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "qwertys123567", Name = "SuperSupporter", NormalizedName = "SUPERSUPPORTER" },
                 new IdentityRole { Id = "qwertys123", Name = "Supporter", NormalizedName = "SUPPORTER" }
            );
        }


    }
}

