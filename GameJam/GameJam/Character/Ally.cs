using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Ally : Character
    {
        public Ally(Vec2f position, Map map)
        {
            m_texture = Helper.Ally;
            m_map = map;
            m_sprite = new SFML.Graphics.Sprite(m_texture, new SFML.Graphics.IntRect(0, 0, 80, 80));
            m_position = position;

            m_health = 110;
            m_enemy = false;
            m_active = false;
            m_controlable = false;
            m_speed = 3;
            lifecounter = 0;
            setBox();
        }


        public override UnitType getTyp()
        {
            return UnitType.Ally;
        }
    }
}
