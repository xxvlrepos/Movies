namespace Movies.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ratings
    {
        #region Вспомогательные свойства

        public string GetUserComment
        {
            get => $"{IdRating}) поставил рейтинг {Rating} \nКоммент: {Comment}";
        }

        #endregion

        [Key]
        public int IdRating { get; set; }

        public int IdFilm { get; set; }

        public int IdUser { get; set; }

        [StringLength(300)]
        public string Comment { get; set; }

        public int? Rating { get; set; }

        public virtual Films Films { get; set; }

        public virtual Users Users { get; set; }
    }
}
