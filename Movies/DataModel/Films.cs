namespace Movies.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Films
    {
        #region Свойства

        public string DateFilm
        {
            get
            {
                return Year.Value.ToString("D");
            }
        }

        #endregion


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Films()
        {
            ActorsFilm = new HashSet<ActorsFilm>();
            Ratings = new HashSet<Ratings>();
        }

        [Key]
        public int IdFilm { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int IdProducer { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Year { get; set; }

        public int IdGenre { get; set; }

        public int? Rating { get; set; }

        [Column(TypeName = "image")]
        public byte[] Poster { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(1000)]       
        public string AboutFilm { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActorsFilm> ActorsFilm { get; set; }

        public virtual Genres Genres { get; set; }

        public virtual Producers Producers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ratings> Ratings { get; set; }
    }
}
