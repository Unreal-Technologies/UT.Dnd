using Shared.Controls;
using Shared.Interfaces;
using UT.Data.Attributes;
using System.Windows.Forms;

namespace UT.Dnd.Map
{
    [Position(int.MaxValue)]
    public class Mapeditor : ExtendedMdiModletForm
    {
        #region Constructors
        public Mapeditor() 
            : base()
        {
            Text = "Map Editor";
        }
        #endregion //Constructors

        #region Public Methods
        public override void OnMenuCreation()
        {
            if (Root is IMainFormModlet && Root is IMainMenuContainer mmc)
            {
                mmc.MenuStack.Add(this, ["D&D", "Map Editor", "New"], OpenNew);
                mmc.MenuStack.Add(this, ["D&D", "Map Editor", "Edit"], OpenEdit);
            }
        }
        #endregion //Public Methods

        #region Private Methods
        private void OpenNew()
        {
            ShowMdi<Mapeditor>();
        }

        private void OpenEdit()
        {
            ShowMdi<Mapeditor>();
        }
        #endregion //Private Methods
    }
}
