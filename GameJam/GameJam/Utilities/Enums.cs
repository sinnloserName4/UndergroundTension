using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    enum FieldType
    {
        Free,           //  0
        Dirt,           //  1
        TotalNum        //  2
      //  Building,       //  3
        
    };

    enum Direction
    {
        Left,
        Up,
        Right,
        Down
    };

    enum GameState
    {
        Menu,
        Editor
    };

    enum SpriteState
    {
        Standing,
        Moving,
        Attack,
        Dead
    };


    enum ViewState
    {
        //counterclockwiese starting with hero facing us
        Clock6,
        Clock54,
        Clock3,
        Clock21,
        Clock0,
        Clock1110,
        Clock9,
        Clock87
    };

    enum UnitType
    {
        Hero,
        Ally
    }
}