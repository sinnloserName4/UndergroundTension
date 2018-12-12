using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    abstract class Objects
    {
        //////////////////////___MEMBER___////////////////////////////
        #region Member

        protected SFML.Graphics.IntRect m_boundingBox;
        protected SFML.Graphics.CircleShape m_circle;
        protected SFML.Graphics.Texture m_texture;
        public Vec2f m_position, m_direction;
        public float m_speed, m_leftSteps;
        protected int lifecounter;
        protected Map m_map;


        #endregion

        //////////////////////___METHODS___//////////////////////
        #region Methods

        public void draw(SFML.Graphics.RenderWindow window, Camera camera)
        {

            m_circle.Position = m_position;
            window.Draw(m_circle);
        }



        public void update()
        {
            updateMovement();
        }

        protected void setBox()
        {
            m_boundingBox = new SFML.Graphics.IntRect((int)m_position.X, (int)m_position.Y, 10, 10);
        }

        public void updateMovement()
        {
            if (m_leftSteps > 0)
            {
                SFML.Graphics.IntRect Intersect = new SFML.Graphics.IntRect(m_boundingBox.Left, m_boundingBox.Top, m_boundingBox.Width, m_boundingBox.Height);
                Intersect.Left += (int)m_direction.X;
                Intersect.Top += (int)m_direction.Y;
                Vector2u FieldIndex = getField(new Vec2f(Intersect.Left, Intersect.Top));
                bool intersection = false;
                bool computed = false;

                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if ((FieldIndex.X + i) >= 0 && (FieldIndex.Y + j) >= 0 && FieldIndex.X + i < m_map.getFieldNum().X && FieldIndex.Y + j < m_map.getFieldNum().Y)
                            intersection = Intersect.Intersects(m_map.m_map[FieldIndex.X + i, FieldIndex.Y + j].m_boundingBox);
                        else
                            if (!computed)
                            {
                                intersection = !(Intersect.Left >= 0) || !(Intersect.Top >= 0) || !(Intersect.Left + Intersect.Width < m_map.getFieldNum().X * Camera.m_FieldSize) || !(Intersect.Top + Intersect.Height < m_map.getFieldNum().Y * Camera.m_FieldSize);
                                computed = true;
                            }
                        if (intersection) break;
                    }
                    if (intersection) break;
                }

                if (!intersection)
                {
                    m_position += m_direction;
                    setBox();

                }
                else
                    m_leftSteps = 0;


            }

            m_leftSteps -= m_speed;

            if (m_leftSteps < 0 && m_leftSteps > -10)
            {
                m_direction = new Vec2f(0, -1);

            }
        }
        static SFML.Window.Vector2u getField(Vec2f Position)
        {
            return new SFML.Window.Vector2u((uint)Math.Floor((Position.X) / 80), (uint)Math.Floor((Position.Y) / 80));
        }
        #endregion


    }
}
