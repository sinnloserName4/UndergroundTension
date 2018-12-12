using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Hero : Character
    {
        //////////////////////___CONSTRUCTORS___//////////////////////
        #region Constructors

        public override UnitType getTyp()
        {
            return UnitType.Hero;
        }

        public Hero(Vec2f position,Map map)
        {
            m_texture = Helper.Hero;
            m_map = map;
            m_sprite = new SFML.Graphics.Sprite(m_texture, new SFML.Graphics.IntRect(0, 0, 80, 80));   // wie genau funzt das mit dem weiterschieben ?
            m_position = position;

            m_health = 200;
            m_enemy = false;
            m_active = true;
            m_speed = 5;
            lifecounter = 0;
            setBox();
        }


        #endregion


        //////////////////////___METHODS___//////////////////////
        #region Methods

        #endregion

    }
}
