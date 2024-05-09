using Shared.Efc.Tables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UT.Dnd.Efc.Tables
{
    [Table("UT.Dnd.Map")]
    public class Map
    {
        #region Properties
        [Required, Key]
        public Guid Id { get; set; }
        [Required]
        public virtual User? User { get; set; }
        [Required]
        public string? Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime TransStartDate { get; set; }
        #endregion //Properties

        #region Constructors
        public Map()
        {
            Created = DateTime.Now;
        }
        #endregion //Constructors
    }
}
