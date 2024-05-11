using Shared.Controls;
using Shared.Efc.Tables;
using Shared.Interfaces;
using System.Net;
using UT.Data.Attributes;
using UT.Data.Controls;
using UT.Data.Controls.Custom;
using UT.Dnd.Efc.Tables;

namespace UT.Dnd.Modules.Mapping
{
    [Position(int.MaxValue)]
    public partial class MapEditor : ExtendedMdiModletForm
    {
        #region Members
        private States state;
        private Gridview<Map>? gridviewList;
        private Guid tempMapId = Guid.Empty;
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

        #region Enums
        public enum States
        {
            New, List, Edit, Delete
        }
        #endregion //Enums

        #region Public Methods
        public void SetState(States state)
        {
            this.state = state;
            UpdateState();
        }

        public override void OnMenuCreation()
        {
            if (Root is IMainFormModlet && Root is IMainMenuContainer mmc)
            {
                mmc.MenuStack.Add(this, ["D&D", "Map Editor"], OpenEdit);
            }
        }

        public override byte[]? OnLocalServerAction(byte[]? stream, IPAddress ip)
        {
            return Efc.DataHandler.OnLocalServerAction(stream, this, Context);
        }
        #endregion //Public Methods

        #region Private Methods

        private void MapEditor_Load(object? sender, EventArgs e)
        {
            gridviewList = new Gridview<Map>(x => x.Id);
            gridviewList.SetColumns([
                gridviewList.Column("Name", x => x.Name ?? ""),
                gridviewList.Column("Created", x => x.Created.ToString("dd-MM-yyyy HH:mm")),
                gridviewList.Column("Last Update", x => x.TransStartDate.ToString("dd-MM-yyyy HH:mm"))
            ]);
            gridviewList.OnAdd += OnAdd;
            //gridviewList.OnEdit += OnEdit;
            gridviewList.OnRemove += OnRemove;

            tabPage_List.Controls.Add(gridviewList);
        }

        private void OnAdd(Guid? id)
        {
            SetState(States.New);
        }

        private void OnEdit(Guid? id)
        {
            if (id != null && id != Guid.Empty)
            {
                tempMapId = id.Value;
                SetState(States.Edit);
                tempMapId = Guid.Empty;
            }
        }

        private void OnRemove(Guid? id)
        {
            if (id != null && id != Guid.Empty)
            {
                tempMapId = id.Value;
                SetState(States.Delete);
                tempMapId = Guid.Empty;
            }
        }

        private void UpdateState()
        {
            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.ItemSize = new Size(0, 1);
            tabControl.SizeMode = TabSizeMode.Fixed;

            if (InfoBar == null)
            {
                return;
            }

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
                case States.Delete:
                    InfoBar.Subtitle = "Delete: " + tempMapId.ToString();
                    tabControl.SelectTab(tabPage_delete);
                    RenderDelete();
                    break;
                case States.Edit:
                    InfoBar.Subtitle = "Edit: " + tempMapId.ToString();
                    tabControl.SelectTab(tabPage_edit);
                    break;
            }
        }

        private void RenderDelete()
        {
            if (InfoBar == null)
            {
                return;
            }

            Map? map = Request<Map, Efc.DataHandler.DndActions, Guid>(Efc.DataHandler.DndActions.SelectMapById, tempMapId);

            if (map == null)
            {
                return;
            }

            tabPage_delete_lbl_message.Text = string.Format(tabPage_delete_lbl_message.Text, map.Name);
            tabPage_delete_tb_id.Text = tempMapId.ToString();

            InfoBar.Subtitle = tabPage_delete_lbl_message.Text;
        }

        private void RenderList()
        {
            if (gridviewList == null)
            {
                return;
            }

            if (
                Session != null &&
                Session.TryGetValue("User-Authentication", out object? value) &&
                value is User user
            )
            {
                Map[]? maps = Request<Map[], Efc.DataHandler.DndActions, Guid>(Efc.DataHandler.DndActions.ListMapsByUserId, user.Id);
                if (maps != null)
                {
                    gridviewList.Dataset(maps);
                }
            }
        }

        private void OpenEdit()
        {
            MapEditor? me = ShowMdi<MapEditor>();
            me?.SetState(States.List);
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
                Tuple<Guid, string> data = new(user.Id, tabPage_add_vtb_name.Control.Text);
                Map? map = Request<Map, Efc.DataHandler.DndActions, Tuple<Guid, string>>(Efc.DataHandler.DndActions.SelectMapByNameAndUserId, data);
                map ??= Request<Map, Efc.DataHandler.DndActions, Tuple<Guid, string>>(Efc.DataHandler.DndActions.CreateMapByNameAndUserId, data);
                if (map != null)
                {
                    SetState(States.List);
                }
            }
        }

        private void TabPage_delete_btn_yes_Click(object sender, EventArgs e)
        {
            Guid mapId = Guid.Parse(tabPage_delete_tb_id.Text);
            Request<Map, Efc.DataHandler.DndActions, Guid>(Efc.DataHandler.DndActions.DeleteMapById, mapId);
            SetState(States.List);
        }

        private void TabPage_delete_btn_no_Click(object sender, EventArgs e)
        {
            SetState(States.List);
        }
        #endregion //Private Methods

        private void button1_Click(object sender, EventArgs e)
        {
            Shared.Modules.Content? cu = ShowMdi<Shared.Modules.Content>();
            cu?.SetState(Shared.Modules.Content.States.Upload);
        }
    }
}
