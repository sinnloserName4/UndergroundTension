using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Map
    {//////////////////////___MEMBER___////////////////////////////
        #region Member

        public Field[,] m_map;
        Vec2f m_fields;
        SFML.Graphics.Sprite m_minimapSprite;
        
        
        #endregion

        //////////////////////___CONSTRUCTORS___//////////////////////

        #region Constructors

        public Map(Vec2f Fields)
        {
           
            m_fields = Fields;
            m_map = new Field[(int)m_fields.X, (int)m_fields.Y];
           
        }

        
        
        #endregion

        //////////////////////___METHODS___//////////////////////
        #region Methods

        public Vec2f getFieldNum()
        {
            return m_fields;
        }

        public void InitializeFields(Camera cam)
        {
            m_minimapSprite = new SFML.Graphics.Sprite(Helper.t_DebugWhite, new SFML.Graphics.IntRect(0, 0, (int)cam.m_minimapSize.X, (int)cam.m_minimapSize.X));
            for (uint i = 0; i < m_fields.X; i+=2)
            {
                for (uint j = 0; j < m_fields.Y; j+=2)
                {
                    m_map[i, j]     = new Field((FieldType)(((i * i + j * 5) / 13) % 2), this.m_map, new Vec2f(i, j));
                    m_map[i+1, j]   = new Field((FieldType)(((i * i + j * 5) / 13) % 2), this.m_map, new Vec2f(i+1, j));
                    m_map[i, j+1]   = new Field((FieldType)(((i * i + j * 5) / 13) % 2), this.m_map, new Vec2f(i, j+1));
                    m_map[i+1, j+1] = new Field((FieldType)(((i * i + j * 5) / 13) % 2), this.m_map, new Vec2f(i+1, j+1));
                    
                }
            }
            for (uint i = 0; i < m_fields.X; i ++)
            {
                for (uint j = 0; j < m_fields.Y; j ++)
                {
                    m_map[i, j].initialize();
                   

                }
            }
        }

        public void InitializeRandom(Camera cam)
        {
            m_minimapSprite = new SFML.Graphics.Sprite(Helper.t_DebugWhite, new SFML.Graphics.IntRect(0, 0, (int)cam.m_minimapSize.X, (int)cam.m_minimapSize.X));
            for (uint i = 0; i < m_fields.X; i += 2)
            {
                for (uint j = 0; j < m_fields.Y; j += 2)
                {
                    int ranNum = Game.RandGen.Next(20);
                    m_map[i, j] = new Field((FieldType)((( ranNum * (i * i + j * 5) / 13)) % 2), this.m_map, new Vec2f(i, j));
                    m_map[i + 1, j] = new Field((FieldType)(((ranNum * (i * i + j * 5) / 13)) % 2), this.m_map, new Vec2f(i + 1, j));
                    m_map[i, j + 1] = new Field((FieldType)(((ranNum * (i * i + j * 5) / 13)) % 2), this.m_map, new Vec2f(i, j + 1));
                    m_map[i + 1, j + 1] = new Field((FieldType)(((ranNum * (i * i + j * 5) / 13)) % 2), this.m_map, new Vec2f(i + 1, j + 1));


                }
            }
            for (uint i = 0; i < m_fields.X; i++)
            {
                for (uint j = 0; j < m_fields.Y; j++)
                {
                    m_map[i, j].initialize();


                }
            }
        }

        public void drawGround(SFML.Graphics.RenderWindow window,Camera cam)
        {

            for (int i=0;i<m_fields.X;i++)
            {   
                for (int j=0;j<m_fields.Y;j++)
                {
                    if ((!(i * Camera.m_FieldSize < cam.m_topPosition.X - 100)) &&
                       (!(j * Camera.m_FieldSize < cam.m_topPosition.Y - 100)) &&
                       (!(i * Camera.m_FieldSize > cam.m_topPosition.X + window.Size.X + 100)) &&
                       (!(j * Camera.m_FieldSize > cam.m_topPosition.Y + window.Size.Y + 100)))

                    
                   m_map[i, j].drawGround(window, cam, new Vec2f(i, j));
                }
            }
        }

        

        public void drawFront(SFML.Graphics.RenderWindow window, Camera cam)
        {
            for (int i = 0; i < m_fields.X; i++)
            {
                for (int j = 0; j < m_fields.Y; j++)
                {
                    if ((!(i * Camera.m_FieldSize < cam.m_topPosition.X - 100)) &&
                       (!(j * Camera.m_FieldSize < cam.m_topPosition.Y - 100)) &&
                       (!(i * Camera.m_FieldSize > cam.m_topPosition.X + window.Size.X + 100)) &&
                       (!(j * Camera.m_FieldSize > cam.m_topPosition.Y + window.Size.Y + 100)))

                    m_map[i, j].drawFront(window, cam, new Vec2f(i, j));
                }
            }
        }
        public void drawDebug(SFML.Graphics.RenderWindow window, Camera cam)
        {
            for (int i = 0; i < m_fields.X; i++)
            {
                for (int j = 0; j < m_fields.Y; j++)
                {
                    if ((!(i * Camera.m_FieldSize < cam.m_topPosition.X - 100)) &&
                       (!(j * Camera.m_FieldSize < cam.m_topPosition.Y - 100)) &&
                       (!(i * Camera.m_FieldSize > cam.m_topPosition.X + window.Size.X + 100)) &&
                       (!(j * Camera.m_FieldSize > cam.m_topPosition.Y + window.Size.Y + 100)))

                    m_map[i, j].drawDebug(window, cam, new Vec2f(i, j));
                }
            }
        }

        public void drawMinimap(SFML.Graphics.RenderWindow window, Camera cam)
        {
            m_minimapSprite.Color = new SFML.Graphics.Color(0, 0, 0);
            m_minimapSprite.Position = cam.m_minimapPosition;
            window.Draw(m_minimapSprite);
            for (int i = 0; i < m_fields.X; i++)
            {
                for (int j = 0; j < m_fields.Y; j++)
                {
                    m_map[i, j].drawMiniMap(window, cam, new Vec2f(i, j));
                }
            }
        }

        public void MineField(SFML.Window.Vector2u Index)
        {
            Index = Index / 2;
            Index = Index * 2;
            {
                for (int i=0; i<2; i++)
                {
                    for (int j=0;j<2;j++)
                    {
                        m_map[Index.X + i, Index.Y + j].m_type = FieldType.Free;
                    }
                }
             }

            for (int i=-3; i<3; i++)
            {
                for (int j = -3; j < 3; j++)
                {
                    if (Index.X + i >= 0 && Index.Y + j >= 0 && Index.X + i < 50 && Index.Y + j < 50)
                    m_map[Index.X + i, Index.Y + j].initialize();
                }
            }
            

        }



        #endregion
    }
}
