namespace Movies.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActorsFilm")]
    public partial class ActorsFilm
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdFilm { get; set; }


        public ActorsFilm(int IdFilm, int IdActor, string Role)
        {
            this.IdFilm = IdFilm;
            this.IdActor = IdActor;
            this.Role = Role;
        }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdActor { get; set; }

        [StringLength(50)]
        public string Role { get; set; }

        public virtual Actors Actors { get; set; }

        public virtual Films Films { get; set; }
    }
}
