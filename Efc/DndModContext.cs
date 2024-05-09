using Microsoft.EntityFrameworkCore;
using UT.Data.Efc;
using UT.Dnd.Efc.Tables;

namespace Shared.Efc
{
    public class DndModContext(ExtendedDbContext.Configuration configuration) : ExtendedDbContext(configuration)
    {
        #region Constructors
        public DndModContext() : this(
            new Configuration(
                "server=127.0.0.1;port=3306;database=dnd-manager;user=root;password=;",
                Types.Mysql
            )
        )
        {

        }
        #endregion //Constructors

        #region Public Methods
        public override bool Migrate()
        {
            if (Database.GetPendingMigrations().Any())
            {
                try
                {
                    Database.Migrate();
                    return true;
                }
                catch(Exception)
                {
                    return false;
                }
            }
            return false;
        }
        #endregion //Public Methods

        #region Properties
        public DbSet<Map> Maps { get; set; }
        #endregion //Properties
    }
}
