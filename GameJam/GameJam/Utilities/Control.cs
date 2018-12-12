using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Control
    {
        //////////////////////___MEMBER___////////////////////////////
        #region Member
        public KeyboardInput Keyboard;
        public MouseInput Mouse;
        #endregion

        //////////////////////___CONSTRUCTORS___//////////////////////

        #region Constructors

        public Control(SFML.Graphics.RenderWindow window)
        {
            List<SFML.Window.Keyboard.Key> usedkeys = new List<SFML.Window.Keyboard.Key>();

            usedkeys.Add(SFML.Window.Keyboard.Key.W);
            usedkeys.Add(SFML.Window.Keyboard.Key.A);
            usedkeys.Add(SFML.Window.Keyboard.Key.S);
            usedkeys.Add(SFML.Window.Keyboard.Key.D);
            usedkeys.Add(SFML.Window.Keyboard.Key.Q);
            usedkeys.Add(SFML.Window.Keyboard.Key.LControl);


            usedkeys.Add(SFML.Window.Keyboard.Key.Space);

            Keyboard = new KeyboardInput(usedkeys);
            Mouse = new MouseInput(window);

        }

        #endregion

        //////////////////////___METHODS___//////////////////////
        #region Methods

        public void check()
        {
            Keyboard.update();
            Mouse.update();


            if (Keyboard.isClicked(SFML.Window.Keyboard.Key.A))
            {
                Console.WriteLine('A');
            }
        }


        #endregion
    }
}