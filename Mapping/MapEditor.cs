using Microsoft.EntityFrameworkCore;
using Shared.Controls;
using Shared.Efc;
using Shared.Efc.Tables;
using Shared.Interfaces;
using Shared.Modules;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using UT.Data.Attributes;
using UT.Data.Controls;
using UT.Data.Controls.Custom;
using UT.Data.Modlet;
using UT.Dnd.Efc.Tables;

namespace UT.Dnd.Mapping
{
    [Position(int.MaxValue)]
    public partial class MapEditor : ExtendedMdiModletForm
    {
        #region Members
        private States state;
        private GridviewGuid? gridviewList;
        private Guid mapId = Guid.Empty;
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
            CheckIfNameExists, CreateMap
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
                if(map != null)
                {
                    State = States.List;
                }
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
                default:
                    break;
            }
            return null;
        }

        private static byte[]? OnLocalServerAction_CheckIfNameExists(DndModContext context, byte[]? stream)
        {
            Tuple<Guid, string>? tuple = ModletStream.GetContent<Actions, Tuple<Guid, string>>(stream);
            if (tuple == null)
            {
                return null;
            }
            Guid userId = tuple.Item1;
            if(userId == Guid.Empty)
            {
                return null;
            }

            string name = tuple.Item2;
            Map? map = context.Maps.Where(x => x.User != null && x.User.Id == userId && x.Name == name).FirstOrDefault();

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
            if(user == null)
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


    }
}
