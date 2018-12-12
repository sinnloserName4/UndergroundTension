using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    
    class Field
    {
        



        //////////////////////___MEMBER___////////////////////////////
        #region Member
        SFML.Graphics.Sprite m_sprite_ground;
        SFML.Graphics.Sprite m_sprite_front;
        SFML.Graphics.Sprite m_sprite_minimap;
        SFML.Graphics.Sprite m_sprite_debug = Helper.DebugWhite;
                
        public FieldType m_type;
        Field[,] m_map;
        Vec2f m_index;
        public FieldType[] m_4neighbors = new FieldType[4];
        public FieldType[] m_8neighbors = new FieldType[4];
        int m_4neighborSum;
        public SFML.Graphics.IntRect m_boundingBox;
        #endregion

        //////////////////////___CONSTRUCTORS___//////////////////////
        #region Constructors
        public Field(FieldType type, Field[,] map, Vec2f index)
         {
            m_type      = type;
            m_map = map;
            m_index = index;
            
            
         }

        #endregion

        //////////////////////___METHODS___//////////////////////
        #region Methods
        public void initializeStatics()
        {
        SFML.Graphics.IntRect rect = new SFML.Graphics.IntRect(0, 0, (int)Camera.m_FieldSize,(int) Camera.m_FieldSize);
        
        }

        public void initialize()
        {
            m_sprite_ground = Helper.Ground;
            m_sprite_minimap = new SFML.Graphics.Sprite(Helper.t_DebugWhite, new SFML.Graphics.IntRect(0, 0, 2, 2));
            
            initializeStatics();

            if (m_index.X - 1 >= 0)
            {
                m_4neighbors[0] = m_map[(int)m_index.X - 1, (int)m_index.Y].m_type; // left
            }
            else
                m_4neighbors[0] = FieldType.Dirt;



            if (m_index.X - 1 >= 0 && m_index.Y - 1 >= 0)
            {
                m_8neighbors[0] = m_map[(int)m_index.X - 1, (int)m_index.Y-1].m_type; // upper left
            }
            else
                m_8neighbors[0] = FieldType.Dirt;



            if (m_index.Y - 1 >= 0)
            {
                m_4neighbors[1] = m_map[(int)m_index.X, (int)m_index.Y - 1].m_type; // upper
            }
            else
                m_4neighbors[1] = FieldType.Dirt;



            if (m_index.X + 1 <= m_map.GetLength(0)-1 && m_index.Y - 1 >= 0)
            {
                m_8neighbors[1] = m_map[(int)m_index.X +1, (int)m_index.Y - 1].m_type; // upper right
            }
            else
                m_8neighbors[1] = FieldType.Dirt;



            if (m_index.X + 1 <= m_map.GetLength(0)-1)
            {
                m_4neighbors[2] = m_map[(int)m_index.X + 1, (int)m_index.Y].m_type; // Right
            }
            else
                m_4neighbors[2] = FieldType.Dirt;



            if (m_index.X + 1 <= m_map.GetLength(0) - 1 && m_index.Y + 1 <= m_map.GetLength(1) - 1)
            {
                m_8neighbors[2] = m_map[(int)m_index.X + 1, (int)m_index.Y+1].m_type; // Lower Right
            }
            else
                m_8neighbors[2] = FieldType.Dirt;



            if (m_index.Y + 1 <= m_map.GetLength(1)-1)
            {
                m_4neighbors[3] = m_map[(int)m_index.X, (int)m_index.Y + 1].m_type; // Lower
            }
            else
                m_4neighbors[3] = FieldType.Dirt;



            if (m_index.X - 1 >= 0 && m_index.Y + 1 <= m_map.GetLength(1) - 1)
            {
                m_8neighbors[3] = m_map[(int)m_index.X - 1, (int)m_index.Y + 1].m_type; // Lower Left
            }
            else
                m_8neighbors[3] = FieldType.Dirt;



            m_4neighborSum = (int)m_4neighbors[0] + (int)m_4neighbors[1] + (int)m_4neighbors[2] + (int)m_4neighbors[3]; 
            

            updateSprites();
        }

       public void setType(FieldType Type)
        {
            m_type = Type;
            updateSprites();
        }

        private void updateSprites()
        {
            if (m_type == FieldType.Dirt)
            {
                m_sprite_ground = Helper.Upper;
                m_sprite_front = null;
                m_sprite_minimap.Color = new SFML.Graphics.Color(139, 105, 20); //GoldenRod
                m_boundingBox = new SFML.Graphics.IntRect((int)m_index.X * 80 - Camera.FieldDelta, (int)m_index.Y * 80 - Camera.FieldDelta, 80 + 2 * (Camera.FieldDelta), 80 + 2 * (Camera.FieldDelta));
            }
            else 
            {
                m_boundingBox = new SFML.Graphics.IntRect(0, 0, 0, 0);
                m_sprite_minimap.Color = new SFML.Graphics.Color(210,180,140); //tan
                if (m_4neighborSum == 0)
                {
                    m_sprite_ground = Helper.Ground;
                    m_sprite_front = null;

                    if (m_8neighbors[0] == FieldType.Dirt)
                    {
                        m_sprite_ground = Helper.UpLeftInner;
                        m_sprite_front = null;
                    }

                    if (m_8neighbors[1] == FieldType.Dirt)
                    {
                        m_sprite_ground = Helper.UpRightInner;
                        m_sprite_front = null;
                    }

                    if (m_8neighbors[2] == FieldType.Dirt)
                        m_sprite_front = Helper.DownRightInner;

                    if (m_8neighbors[3] == FieldType.Dirt)
                        m_sprite_front = Helper.DownLeftInner;
                }
                else
                {
                    if (m_4neighborSum == 1)
                    {
                        if (m_4neighbors[0] == FieldType.Dirt)
                        {
                            m_sprite_front = Helper.Left;
                            m_sprite_ground = Helper.Ground;
                        }

                        if (m_4neighbors[1] == FieldType.Dirt)
                        {
                            m_sprite_ground = Helper.UpperMid;
                            m_sprite_front = null;
                        }

                        if (m_4neighbors[2] == FieldType.Dirt)
                        {
                            m_sprite_ground = Helper.Right;
                            m_sprite_front = null;
                        }

                        if (m_4neighbors[3] == FieldType.Dirt)
                        {
                            m_sprite_front = Helper.LowerMid;
                            m_sprite_ground = Helper.Ground;
                        }
                   }
                   else
                   {
                        if(m_4neighbors[1] == FieldType.Dirt)
                        {
                            m_sprite_front = null;

                            if (m_4neighbors[0] == FieldType.Dirt)
                            {
                                m_sprite_ground = Helper.UpperLeft;
                                
                            }
                            else
                            {
                                m_sprite_ground = Helper.UpperRight;
                                
                            }
                        }
                        if (m_4neighbors[3] == FieldType.Dirt)
                        {
                            if (m_4neighbors[0] == FieldType.Dirt)
                            {
                                m_sprite_front = Helper.LowerLeft;
                                m_sprite_ground = Helper.Ground;
                            }
                            else
                            {
                                m_sprite_front = Helper.LowerRight;
                                m_sprite_ground = Helper.Ground;
                            }
                        }
                   }

   
                }
                
                    
                           
            }
        }

        

        #endregion

        public void drawGround(SFML.Graphics.RenderWindow window, Camera cam, Vec2f relPos)
        {
            if (m_sprite_ground != null)
            {
                m_sprite_ground.Position = relPos * Camera.m_FieldSize;
                window.Draw(m_sprite_ground);
            }
        }
        public void drawFront(SFML.Graphics.RenderWindow window, Camera cam, Vec2f relPos)
        {
            if (m_sprite_front != null)
            {
                m_sprite_front.Position = relPos * Camera.m_FieldSize;
                window.Draw(m_sprite_front);
            }
        }

        public void drawMiniMap(SFML.Graphics.RenderWindow window, Camera cam, Vec2f relPos)
        {
                
                m_sprite_minimap.Position = cam.m_minimapPosition + cam.m_minimapFieldSize * relPos;
                window.Draw(m_sprite_minimap);
            
        }

        internal void drawDebug(SFML.Graphics.RenderWindow window, Camera cam, Vec2f relPos)
        {
            if (m_sprite_debug != null)
            {
                m_sprite_debug.Position = relPos * Camera.m_FieldSize;
                if (relPos.X % 2 == 0 && relPos.Y % 2 == 0)
                m_sprite_debug.Color = new SFML.Graphics.Color(255, 0, 0,100);
                if (relPos.X % 2 == 1 && relPos.Y % 2 == 0)
                    m_sprite_debug.Color = new SFML.Graphics.Color(0, 255, 0, 100);
                if (relPos.X % 2 == 0 && relPos.Y % 2 == 1)
                    m_sprite_debug.Color = new SFML.Graphics.Color(0, 0, 255, 100);
                if (relPos.X % 2 == 1 && relPos.Y % 2 == 1)
                    m_sprite_debug.Color = new SFML.Graphics.Color(255, 255, 0, 100);
                window.Draw(m_sprite_debug);
            }
        }

        public void setFrontSprite(SFML.Graphics.Sprite sprite)
        {
            m_sprite_front = sprite;
        }

        public void setGroundSprite(SFML.Graphics.Sprite sprite)
        {
            m_sprite_ground = sprite;
        }


        
    }
}
