namespace Movies.DataModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;


    /// <summary>
    /// Админская точка к бд
    /// </summary>
    /// 

    public partial class MyDB : UserDB
    {
        public MyDB()
            :base("name=AdminDB")
        {

        }

        //public virtual DbSet<Actors> Actors { get; set; }
        //public virtual DbSet<ActorsFilm> ActorsFilm { get; set; }Films { get; set; }
        //public virtual DbSet<Genres> Genres { get; set; }
        //public virtual DbSet<Films> 
        //public virtual DbSet<Producers> Producers { get; set; }
        //public virtual DbSet<Ratings> Ratings { get; set; }
        //public virtual DbSet<Status> Status { get; set; }
        //public virtual DbSet<Users> Users { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Actors>()
        //        .Property(e => e.Gender)
        //        .IsFixedLength();

        //    modelBuilder.Entity<Films>()
        //        .Property(e => e.Name)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Films>()
        //        .Property(e => e.AboutFilm)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Genres>()
        //        .Property(e => e.GenreName)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Producers>()
        //        .HasMany(e => e.Films)
        //        .WithRequired(e => e.Producers)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Ratings>()
        //        .Property(e => e.Comment)
        //        .IsUnicode(false);
        //}
    }
}
