using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    interface IGameState
    {
        void run();
        GameState update();
        void draw();
        void initialise();
        void loadContent();
        void unloadContent();
        void clear();

    }
}
