using Shared.Controls;
using Shared.Efc.Tables;
using Shared.Interfaces;
using System.Drawing;
using System.Windows.Forms;
using UT.Data.Attributes;
using UT.Data.Controls;
using UT.Data.Controls.Custom;
using UT.Data.Modlet;
using Shared.Modules;
using UT.Dnd.Efc.Tables;
using System.Configuration;
using System.Net;

namespace UT.Dnd.Mapping
{
    [Position(int.MaxValue)]
    public partial class MapEditor : ExtendedMdiModletForm
    {
        #region Members
        private States state;
        private GridviewGuid? gridviewList;
        #endregion //Members

        #region Constructors
        public MapEditor()
            : base()
        {
            Text = "Map Editor";
            InitializeComponent();

            Load += MapEditor_Load;
        }
        #endregion //Constructors

        #region Properties
        public States State
        {
            get { return state; }
            set { state = value; UpdateState(); }
        }
        #endregion //Properties

        #region Enums
        public enum States
        {
            New, List
        }

        public enum Actions
        {
            CheckIfNameExists
        }
        #endregion //Enums

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
        private void MapEditor_Load(object? sender, EventArgs e)
        {
            gridviewList = new GridviewGuid();
            gridviewList.SetColumns([new GridviewGuid.Column()
            {
                Text = "Name"
            }]);
            gridviewList.OnAdd += OnAdd;

            tabPage_List.Controls.Add(gridviewList);
        }

        private void OnAdd(Guid? id)
        {
            State = States.New;
        }

        private void UpdateState()
        {
            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.ItemSize = new Size(0, 1);
            tabControl.SizeMode = TabSizeMode.Fixed;

            if (InfoBar != null)
            {
                switch (state)
                {
                    case States.New:
                        InfoBar.Subtitle = "* new";
                        tabControl.SelectTab(tabPage_add);
                        break;
                    case States.List:
                        InfoBar.Subtitle = "* Select Map";
                        tabControl.SelectTab(tabPage_List);
                        RenderList();
                        break;
                }
            }
        }

        private void RenderList()
        {

        }

        private void OpenNew()
        {
            MapEditor? me = ShowMdi<MapEditor>();
            if (me != null)
            {
                me.State = States.New;
            }
        }

        private void OpenEdit()
        {
            MapEditor? me = ShowMdi<MapEditor>();
            if (me != null)
            {
                me.State = States.List;
            }
        }

        private void TabPage_add_btn_save_Click(object sender, EventArgs e)
        {
            Validator validator = new();
            validator.Add(tabPage_add_vtb_name);
            validator.Validate();

            if (
                validator.IsValid &&
                Session != null &&
                Session.TryGetValue("User-Authentication", out object? value) &&
                value is User user
            )
            {
                Map? map = ModletStream.GetContent<bool, Map>(
                    Client?.Send(
                        ModletStream.CreatePacket(
                            Actions.CheckIfNameExists,
                            new Tuple<Guid, string>(user.Id, tabPage_add_vtb_name.Control.Text)
                        ),
                        ModletCommands.Commands.Action,
                        this
                    )
                );
            }
        }

        public override byte[]? OnLocalServerAction(byte[]? stream, IPAddress ip)
        {

            return null;
        }
        #endregion //Private Methods


    }
}
