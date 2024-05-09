using Microsoft.EntityFrameworkCore;
using Shared.Controls;
using Shared.Efc;
using Shared.Efc.Tables;
using Shared.Interfaces;
using Shared.Modules;
using System.Net;
using UT.Data.Attributes;
using UT.Data.Controls;
using UT.Data.Controls.Custom;
using UT.Data.Modlet;
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

        private enum Actions
        {
            CheckIfNameExists, CreateMap, ListMaps, SelectMap, DeleteMap
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
            DbContext? modContext = Context?.Select(this);
            DbContext? sharedContext = Context == null ? null : Array.Find(Context.List, x => x.GetType() == typeof(SharedModContext));
            Actions? action = ModletStream.GetInputType<Actions>(stream);
            if (modContext is not DndModContext dmc || sharedContext is not SharedModContext smc || action == null)
            {
                return null;
            }

            switch (action)
            {
                case Actions.CheckIfNameExists:
                    return OnLocalServerAction_CheckIfNameExists(dmc, stream);
                case Actions.CreateMap:
                    return OnLocalServerAction_CreateMap(dmc, smc, stream);
                case Actions.ListMaps:
                    return OnLocalServerAction_ListMaps(dmc, stream);
                case Actions.SelectMap:
                    return OnLocalServerAction_SelectMap(dmc, stream);
                case Actions.DeleteMap:
                    OnLocalServerAction_DeleteMap(dmc, stream);
                    return null;
                default:
                    break;
            }
            return null;
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
            gridviewList.OnEdit += OnEdit;
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

            Map? map = ModletStream.GetContent<bool, Map>(
                Client?.Send(
                    ModletStream.CreatePacket(
                        Actions.SelectMap,
                        tempMapId
                    ),
                    ModletCommands.Commands.Action,
                    this
                )
            );

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
                Map[]? maps = ModletStream.GetContent<bool, Map[]>(
                    Client?.Send(
                        ModletStream.CreatePacket(
                            Actions.ListMaps,
                            user.Id
                        ),
                        ModletCommands.Commands.Action,
                        this
                    )
                );
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
                Map? map = ModletStream.GetContent<bool, Map>(
                    Client?.Send(
                        ModletStream.CreatePacket(
                            Actions.CheckIfNameExists,
                            data
                        ),
                        ModletCommands.Commands.Action,
                        this
                    )
                );
                map ??= ModletStream.GetContent<bool, Map>(
                    Client?.Send(
                        ModletStream.CreatePacket(
                            Actions.CreateMap,
                            data
                        ),
                        ModletCommands.Commands.Action,
                        this
                    )
                );
                if (map != null)
                {
                    SetState(States.List);
                }
            }
        }

        private void TabPage_delete_btn_yes_Click(object sender, EventArgs e)
        {
            Guid mapId = Guid.Parse(tabPage_delete_tb_id.Text);
            ModletStream.GetContent<bool, Map>(
                Client?.Send(
                    ModletStream.CreatePacket(
                        Actions.DeleteMap,
                        mapId
                    ),
                    ModletCommands.Commands.Action,
                    this
                )
            );
            SetState(States.List);
        }

        private void TabPage_delete_btn_no_Click(object sender, EventArgs e)
        {
            SetState(States.List);
        }

        private static void OnLocalServerAction_DeleteMap(DndModContext dmc, byte[]? stream)
        {
            Guid mapId = ModletStream.GetContent<Actions, Guid>(stream);
            if (mapId == Guid.Empty)
            {
                return;
            }

            Map? map = dmc.Maps.FirstOrDefault(x => x.Id == mapId);
            if (map != null)
            {
                dmc.Remove(map);
                dmc.SaveChanges();
            }
        }

        private static byte[]? OnLocalServerAction_SelectMap(DndModContext dmc, byte[]? stream)
        {
            Guid mapId = ModletStream.GetContent<Actions, Guid>(stream);
            if (mapId == Guid.Empty)
            {
                return null;
            }

            Map? map = dmc.Maps.FirstOrDefault(x => x.Id == mapId);
            return ModletStream.CreatePacket(true, map);
        }

        private static byte[]? OnLocalServerAction_ListMaps(DndModContext dmc, byte[]? stream)
        {
            Guid userId = ModletStream.GetContent<Actions, Guid>(stream);
            if (userId == Guid.Empty)
            {
                return null;
            }

            Map[] maps = [.. dmc.Maps.Where(x => x.User != null && x.User.Id == userId)];
            return ModletStream.CreatePacket(true, maps);
        }

        private static byte[]? OnLocalServerAction_CheckIfNameExists(DndModContext dmc, byte[]? stream)
        {
            Tuple<Guid, string>? tuple = ModletStream.GetContent<Actions, Tuple<Guid, string>>(stream);
            if (tuple == null)
            {
                return null;
            }

            Guid userId = tuple.Item1;
            if (userId == Guid.Empty)
            {
                return null;
            }

            string name = tuple.Item2;
            Map? map = dmc.Maps.Where(x => x.User != null && x.User.Id == userId && x.Name == name).FirstOrDefault();

            return ModletStream.CreatePacket(true, map);
        }

        private static byte[]? OnLocalServerAction_CreateMap(DndModContext dmc, SharedModContext smc, byte[]? stream)
        {
            Tuple<Guid, string>? tuple = ModletStream.GetContent<Actions, Tuple<Guid, string>>(stream);
            if (tuple == null)
            {
                return null;
            }

            Guid userId = tuple.Item1;
            if (userId == Guid.Empty)
            {
                return null;
            }

            User? user = smc.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }
            dmc.Attach(user);

            string name = tuple.Item2;
            Map map = new()
            {
                Name = name,
                User = user
            };

            dmc.Add(map);
            dmc.SaveChanges();

            return ModletStream.CreatePacket(true, map);
        }
        #endregion //Private Methods

        private void button1_Click(object sender, EventArgs e)
        {
            Shared.Modules.Content? cu = ShowMdi<Shared.Modules.Content>();
            cu?.SetState(Shared.Modules.Content.States.Upload);
        }
    }
}
