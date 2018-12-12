using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Bullet : Objects
    {
        //////////////////////___CONSTRUCTORS___//////////////////////
        #region Constructors

        public Bullet(Vec2f startPosition, Vec2f targetPos, Map map)
        {
            m_map = map;
            m_circle = new SFML.Graphics.CircleShape(5);
            m_circle.FillColor = new SFML.Graphics.Color(255, 0, 0, 255);
            m_position = startPosition;
            m_speed = 20;
            lifecounter = 0;
            setBox();


            m_position.X += 40;
            m_position.Y += 40;

            m_direction = (targetPos - m_position).normalized() * m_speed;
            m_leftSteps = (targetPos - m_position).length() + 200;

        }



        #endregion


    }
}
