namespace Movies.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Actors
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Actors()
        {
            ActorsFilm = new HashSet<ActorsFilm>();
        }


        #region Äëÿ âûâîäà â êîíòðîëû

        public string GetFIO
        {
            get => $"{Family} {Name} {Surname}";
        }


        // Ðîëü àêòåðà
        private string _RoleActor;

        public void SetRole(string role)
        {
            _RoleActor = role;
        }

        public string RoleActor
        {
            get => _RoleActor;
        }

        public string GetActorFromLV
        {
            get => $"{Family} {Name} {Surname} - {RoleActor}";
        }
        #endregion

        [Key]
        public int IdActor { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Family { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActorsFilm> ActorsFilm { get; set; }
    }
}
