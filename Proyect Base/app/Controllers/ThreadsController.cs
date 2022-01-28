using Proyect_Base.app.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_Base.app.Controllers
{
    class ThreadsController
    {
        public static void initThreads()
        {
            PathfindingThread.init();
            ItemAreaThread.init();
            NpcPathfindingThread.init();
            MiniGamesThread.init();
        }
    }
}
