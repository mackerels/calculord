using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculordDBLayer
{
    [Table("users")]
    public class User
    {
        public User()
        {
            Calculations = new HashSet<Calculation>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("identifier")]
        [Required]
        public string Identifier { get; set; }

        [Column("limit")]
        [Required]
        public int CalculationLimit { get; set; }

        public virtual ICollection<Calculation> Calculations { get; set; }
    }
}
