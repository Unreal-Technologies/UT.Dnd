using Microsoft.EntityFrameworkCore;
using Shared.Efc;
using Shared.Efc.Tables;
using Shared.Modules;
using UT.Data.Efc;
using UT.Data.Modlet;
using UT.Dnd.Efc.Tables;

namespace UT.Dnd.Efc
{
    public class DataHandler : Shared.Efc.DataHandler
    {
        #region Enums
        public enum DndActions
        {
            SelectMapByNameAndUserId, CreateMapByNameAndUserId, ListMapsByUserId, SelectMapById, DeleteMapById
        }
        #endregion //Enums

        #region Public Methods
        public static new byte[]? OnLocalServerAction(byte[]? stream, IModlet mod, ServerContext? serverContext)
        {
            DbContext? modContext = serverContext?.Select(mod);
            DbContext? sharedContext = serverContext == null ? null : Array.Find(serverContext.List, x => x.GetType() == typeof(SharedModContext));
            DndActions? action = ModletStream.GetInputType<DndActions>(stream);
            if (modContext is not DndModContext dmc || sharedContext is not SharedModContext smc || action == null)
            {
                return Shared.Efc.DataHandler.OnLocalServerAction(stream, mod, serverContext);
            }

            switch (action)
            {
                case DndActions.SelectMapByNameAndUserId:
                    return OnLocalServerAction_SelectMapByNameAndUserId(dmc, stream);
                case DndActions.CreateMapByNameAndUserId:
                    return OnLocalServerAction_CreateMapByNameAndUserId(dmc, smc, stream);
                case DndActions.ListMapsByUserId:
                    return OnLocalServerAction_ListMapsByUserId(dmc, stream);
                case DndActions.SelectMapById:
                    return OnLocalServerAction_SelectMapById(dmc, stream);
                case DndActions.DeleteMapById:
                    OnLocalServerAction_DeleteMapById(dmc, stream);
                    return null;
                default:
                    break;
            }
            return Shared.Efc.DataHandler.OnLocalServerAction(stream, mod, serverContext);
        }
        #endregion //Public Methods

        #region Private Methods
        private static byte[]? OnLocalServerAction_CreateMapByNameAndUserId(DndModContext dmc, SharedModContext smc, byte[]? stream)
        {
            Tuple<Guid, string>? tuple = ModletStream.GetContent<DndActions, Tuple<Guid, string>>(stream);
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

        private static byte[]? OnLocalServerAction_SelectMapByNameAndUserId(DndModContext dmc, byte[]? stream)
        {
            Tuple<Guid, string>? tuple = ModletStream.GetContent<DndActions, Tuple<Guid, string>>(stream);
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

        private static byte[]? OnLocalServerAction_ListMapsByUserId(DndModContext dmc, byte[]? stream)
        {
            Guid userId = ModletStream.GetContent<DndActions, Guid>(stream);
            if (userId == Guid.Empty)
            {
                return null;
            }

            Map[] maps = [.. dmc.Maps.Where(x => x.User != null && x.User.Id == userId)];
            return ModletStream.CreatePacket(true, maps);
        }

        private static byte[]? OnLocalServerAction_SelectMapById(DndModContext dmc, byte[]? stream)
        {
            Guid mapId = ModletStream.GetContent<DndActions, Guid>(stream);
            if (mapId == Guid.Empty)
            {
                return null;
            }

            Map? map = dmc.Maps.FirstOrDefault(x => x.Id == mapId);
            return ModletStream.CreatePacket(true, map);
        }

        private static void OnLocalServerAction_DeleteMapById(DndModContext dmc, byte[]? stream)
        {
            Guid mapId = ModletStream.GetContent<DndActions, Guid>(stream);
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
        #endregion //Private Methods
    }
}
