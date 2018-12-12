using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    abstract class Character
    {
        //////////////////////___MEMBER___////////////////////////////
        #region Member

        protected SFML.Graphics.IntRect m_boundingBox;
        protected SFML.Graphics.Sprite m_sprite;
        protected SFML.Graphics.Sprite m_active_sprite = new SFML.Graphics.Sprite(Helper.t_DebugWhite,new SFML.Graphics.IntRect(0,0,5,5));
        protected SFML.Graphics.Texture m_texture;
        public Vec2f m_position, m_direction;
        public bool m_enemy, m_active;
        protected int m_health;
        protected float m_speed, m_leftSteps;
        protected SpriteState m_State;
        protected ViewState m_ViewState;
        protected int lifecounter;
        protected Map m_map;
        public List<Objects> m_objects = new List<Objects>();
        protected Vec2f m_target;
        protected bool m_controlable;
        

        #endregion

        //////////////////////___METHODS___//////////////////////
        #region Methods

        public void hitted()
        {
            m_health -= 10;
            if (m_health <= 0)
            {
                if (Game.RandGen.Next(2) % 2 == 0)
                {
                    Helper.HeroWin[2].Play();
                }
                else
                {
                    Helper.AllyWin.Play();
                }
                Helper.Death[Game.RandGen.Next(2)].Play();
            }

        }
        
        public abstract UnitType getTyp();

        protected void setBox()
        {
            m_boundingBox = new SFML.Graphics.IntRect((int)m_position.X,(int) m_position.Y, 80, 80);
        }

        public void attack(Vec2f target)
        {

            m_objects.Add(new Bullet(m_position, target, m_map));
            Helper.Shoot.Volume = 20;
            Helper.Shoot.Play();
            updateSprite();
        }

        public void straightMove(Vec2f target)
        {
            Helper.HeroMove[0].Stop();
            Helper.HeroMove[1].Stop();
            Helper.HeroMove[2].Stop();
            m_target = target;
            target.X -= 40;
            target.Y -= 40;
            m_target = target;
            m_direction = (target - m_position).normalized() * m_speed;
            m_leftSteps = (target - m_position).length();
            
            if (getTyp() == UnitType.Hero)
            {
                Helper.HeroMove[Game.RandGen.Next(3)].Play();
            }
            else
            {                  
                Helper.AllyMove.Play();
            }
            
                               

            updateSprite();
        }


        public void update()
        {

            
            if (m_health > 0) updateMovement();

            lifecounter = ++lifecounter % 60;

            if (m_enemy || m_health<0)
            {
                m_active = false;
                m_leftSteps = 0;
                m_objects = new List<Objects>();
            }
        }

        public void draw(SFML.Graphics.RenderWindow window, Camera camera)
        {

            if (m_active)
            {
                // Red Rectangle 
                m_active_sprite.Position = m_position;
                m_active_sprite.Color = new SFML.Graphics.Color(255, 0, 0);
                window.Draw(m_active_sprite);

            }
            updateSprite();
            m_sprite.Position = m_position;
            window.Draw(m_sprite);
            
        }

        void updateSprite()
        {
            float angle;
            if (m_direction.length() > 1) m_State = SpriteState.Moving;
            else m_State = SpriteState.Standing;
            if (m_objects.Count > 0)
            {
                m_State = SpriteState.Attack;


                angle = 180 - (float)(Math.Acos(m_objects.First().m_direction.normalized().Y) * 180.0 / Math.PI);
                if (m_objects.Last().m_direction.X < 0) angle = -angle;
            }
            else
            {
                angle = 180 - (float)(Math.Acos(m_direction.normalized().Y) * 180.0 / Math.PI);
                if (m_direction.X < 0) angle = -angle;
            }
            /*
               -20    0   20
              
            -90              90
             
              -160     0  160

            */

            if (angle < 170) m_ViewState = ViewState.Clock54;
            if (angle < 100) m_ViewState = ViewState.Clock3;
            if (angle < 80) m_ViewState = ViewState.Clock21;
            if (angle < 20) m_ViewState = ViewState.Clock0;

            if (angle < -20) m_ViewState = ViewState.Clock1110;
            if (angle < -80) m_ViewState = ViewState.Clock9;
            if (angle < -100) m_ViewState = ViewState.Clock87;
            if (angle < -160) m_ViewState = ViewState.Clock6;

            if (angle == 0) m_ViewState = ViewState.Clock6;

            int SpriteMode = 0;
            if (lifecounter > 31) SpriteMode = 1;

            if (m_State == SpriteState.Moving)
            {
                if (lifecounter > 15) SpriteMode = 1;
                if (lifecounter > 31) SpriteMode = 0;
                if (lifecounter > 45) SpriteMode = 1;
            }
            if (m_health < 0)
            {
                SpriteMode = 0;
                m_State = SpriteState.Dead;

            }

            m_sprite.TextureRect = new SFML.Graphics.IntRect((int)m_State * 2 * 80 + SpriteMode * 80, (int)m_ViewState * 80, 80, 80);

        }

        void updateMovement()
        {
            if (m_leftSteps > 0)
            {
                SFML.Graphics.IntRect Intersect = new SFML.Graphics.IntRect(m_boundingBox.Left, m_boundingBox.Top, m_boundingBox.Width,m_boundingBox.Height);
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
                    MineField();
                    m_leftSteps = 0;
                
               
            }

            m_leftSteps -= m_speed;

            if (m_leftSteps < 0 && m_leftSteps > -10)
            {
                m_direction = new Vec2f(0, -1);

                updateSprite();

            }
        }

        static SFML.Window.Vector2u getField(Vec2f Position)
        {
            return new SFML.Window.Vector2u((uint) Math.Floor((Position.X) / 80),(uint) Math.Floor((Position.Y)/80));
        }

        public void MineField()
        {
            if (this.getTyp() == UnitType.Ally)
            { 
                Vector2u fTarget = getField(m_target);
                if (fTarget.X < 50 && fTarget.Y < 50)
                {
                    if (m_target.distance(m_position) < 200 && m_map.m_map[fTarget.X, fTarget.Y].m_type == FieldType.Dirt)
                    {
                        m_map.MineField(fTarget);
                        Helper.AllyMove.Stop();
                        Helper.HeroMove[0].Stop();
                        Helper.HeroMove[1].Stop();
                        Helper.HeroMove[2].Stop();
                       // Helper.AllyDig.Play();
                    }
                }
            }
  
        }
        #endregion
    }
        
}
