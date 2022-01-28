using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Models
{
    public class MiniGameInscription
    {
        public int gameId { get; set; }
        public int gameType { get; set; }
        public MiniGameInscription(int gameId, int gameType)
        {
            this.gameId = gameId;
            this.gameType = gameType;
        }
        //FUNCTIONS

        //MODEL SETTERS

        //MODEL GETTERS

        //HANDLERS
    }
}
