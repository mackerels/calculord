using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculordDBLayer
{
    [Table("calculations")]
    public class Calculation
    {
        public Calculation()
        {
            Date = DateTime.Now;
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("date")]
        [Required]
        public DateTime Date { get; set; }

        [Column("expression")]
        [Required]
        public string MathExpression { get; set; }

        [Column("result")]
        public double? Result { get; set; }

        [Column("user_id")]
        public long? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
