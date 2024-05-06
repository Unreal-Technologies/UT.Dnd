using Shared.Controls;
using UT.Data.Attributes;

namespace UT.Dnd.Map
{
    [Position(int.MaxValue)]
    public class Mapeditor : ExtendedMdiModletForm
    {
        #region Constructors
        public Mapeditor() 
            : base()
        {
            this.Load += Mapeditor_Load;
        }
        #endregion //Constructors

        #region Public Methods
        private void Mapeditor_Load(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion //Public Methods
    }
}
