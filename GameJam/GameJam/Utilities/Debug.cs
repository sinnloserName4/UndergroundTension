using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Debug
    {

        //////////////////////___MEMBER___////////////////////////////
        #region Member

        SFML.Graphics.RenderWindow m_window;

   
        public static bool DrawFields = false;
        public static bool DrawMousePosition = false;
        public static bool DrawFPS = false;
        public static bool DrawChars = false;
        public static bool ChangeView = true;







        #endregion


        //////////////////////___CONSTRUCTORS___//////////////////////
        #region Constructors
        
        Debug(SFML.Graphics.RenderWindow window)
        {
            m_window = window;
        }

        #endregion

        //////////////////////___METHODS___//////////////////////
        #region Methods
        
        #endregion



        
    }
}
