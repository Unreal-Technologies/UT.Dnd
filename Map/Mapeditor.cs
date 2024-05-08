using Shared.Controls;
using Shared.Interfaces;
using UT.Data.Attributes;

namespace UT.Dnd.Map
{
    [Position(int.MaxValue)]
    public class MapEditor : ExtendedMdiModletForm
    {
        #region Constructors
        public MapEditor() 
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
            ShowMdi<MapEditor>();
        }

        private void OpenEdit()
        {
            ShowMdi<MapEditor>();
        }
        #endregion //Private Methods
    }
}
