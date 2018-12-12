using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Menu:IGameState
    {
        public Menu()
        {

        }

        public GameState update()
        {
            return GameState.Menu;
        }

        public void draw()
        {
            throw new NotImplementedException();
        }

        public void initialise()
        {
            throw new NotImplementedException();
        }

        public void loadContent()
        {
            throw new NotImplementedException();
        }

        public void unloadContent()
        {
            throw new NotImplementedException();
        }

        public void run()
        {
            throw new NotImplementedException();
        }

        public void clear()
        {
            throw new NotImplementedException();
        }
    }
}
