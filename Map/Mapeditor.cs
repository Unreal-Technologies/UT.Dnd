using Shared.Controls;
using Shared.Interfaces;
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
            Load += Mapeditor_Load;
        }
        #endregion //Constructors

        #region Public Methods
        private void Mapeditor_Load(object? sender, EventArgs e)
        {
            Text = "Map Editor";
        }

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
            Mapeditor me = new()
            {
                MdiParent = MdiParent,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle,
                Title = Title
            };
            me.Show();
        }

        private void OpenEdit()
        {
            Mapeditor me = new()
            {
                MdiParent = MdiParent,
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle,
                Title = Title
            };
            me.Show();
        }
        #endregion //Private Methods
    }
}
