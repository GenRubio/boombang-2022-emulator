using Proyect_Base.app.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class GameArea : PublicArea
    {
        public GameArea(DataRow row) :
           base(row)
        {

        }
        public void loadRankingPanelHandler(Session Session, int gameId)
        {
            ServerMessage server = new ServerMessage(new byte[] { 214 });
            getRankingPanelLastWeek(server, gameId);
            getRankingPanelActualWeek(server, gameId);
            getRankingPanelUsersSumPoints(server, gameId);
            Session.SendData(server);
        }
        private ServerMessage getRankingPanelLastWeek(ServerMessage server, int gameId)
        {
            server.AppendParameter(0);
            server.AppendParameter(0);
            server.AppendParameter(1);
            server.AppendParameter("Gen");
            server.AppendParameter(1);
            server.AppendParameter("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
            return server;
        }
        private ServerMessage getRankingPanelActualWeek(ServerMessage server, int gameId)
        {
            server.AppendParameter(0);
            server.AppendParameter(0);
            server.AppendParameter(1);
            server.AppendParameter("Gen");
            server.AppendParameter(1);
            server.AppendParameter("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
            return server;
        }
        private ServerMessage getRankingPanelUsersSumPoints(ServerMessage server, int gameId)
        {
            server.AppendParameter(1);
            server.AppendParameter("Gen");
            server.AppendParameter(null);
            server.AppendParameter(null);
            return server;
        }
    }
}
