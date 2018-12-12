using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Camera
    {//////////////////////___MEMBER___////////////////////////////
        #region Member

        public Vec2f m_topPosition;
        public Vec2f m_Windowsize;
        public static readonly uint m_FieldSize = 80;
        public Vec2f m_mapSize; 
        public Vec2f m_bottomLayerSize; 
        public Vec2f m_bottomLayerPosition;
        public Vec2f m_minimapSize; 
        public Vec2f m_minimapPosition;
        public Vec2f m_ContextMenuSize; 
        public Vec2f m_ContextMenuPosition;
        public RenderWindow m_window;
        public Vec2f m_minimapFieldSize;
        public Sprite m_LayerSprite;

        public IntRect M_grenadeBox = new IntRect(0, 0, 60, 60);
        public IntRect M_AutofireBox = new IntRect(0, 0, 60, 60);
                
        public Sprite m_grenadeSprite  = Helper.Grenade;
        public Sprite m_AutofireSprite = Helper.ManualFire;
        public Sprite m_HeroIconSprite = Helper.HeldIcon;
        public Sprite m_AllyIconSprite = Helper.AllyIcon;

        float speed = 25f;
        public static int FieldDelta = 0;
        

        #endregion

        //////////////////////___CONSTRUCTORS___//////////////////////

        #region Constructors

        public Camera(Vec2f FieldNum, SFML.Graphics.RenderWindow window)
        {
            m_window = window;
            updateWindow();
            m_minimapFieldSize = (m_minimapSize / FieldNum);
            m_LayerSprite = Helper.UI;
            
            

        }
        
        #endregion

        //////////////////////___METHODS___//////////////////////
        #region Methods

        public void update(Control Control)
        {
            if (Control.Mouse.getMousePos().X > ( m_window.Size.X* 19 / 20 ))
                moveRight();
            if (Control.Mouse.getMousePos().X < (m_window.Size.X * 1 /  20))
                moveLeft();
            if (Control.Mouse.getMousePos().Y > (m_window.Size.Y * 19 / 20))
                moveDown();
            if (Control.Mouse.getMousePos().Y < (m_window.Size.Y * 1 /  20))
               moveUp();
                        
            updateWindow();
        }

        public void updateWindow()
        {
            m_Windowsize = new Vec2f(m_window.Size);
            m_topPosition = new Vec2f(m_window.GetView().Center) - (new Vec2f(m_window.Size) / 2);
            m_mapSize = new Vec2f(m_Windowsize.X, m_Windowsize.Y * 7 / 9);
            m_bottomLayerSize = new Vec2f(m_Windowsize.X,m_Windowsize.Y - m_mapSize.Y);
            m_bottomLayerPosition = m_topPosition + new Vec2f(0, m_mapSize.Y);
            m_minimapSize = new Vec2f(m_bottomLayerSize.Y * 0.8f, m_bottomLayerSize.Y * 0.8f);
            m_minimapPosition = m_bottomLayerPosition + new Vec2f(m_bottomLayerSize.Y * 0.1f, m_bottomLayerSize.Y * 0.1f) + new Vec2f(5,5);
            m_ContextMenuSize = m_bottomLayerSize - new Vec2f(2 * m_FieldSize, 0);
            m_ContextMenuPosition = m_bottomLayerPosition + new Vec2f(2 * m_FieldSize, 0);
        }

       

        public void moveRight()
        {
            View v = m_window.GetView();
            v.Move(new SFML.Window.Vector2f(speed,0));
            m_window.SetView(v);
            
        }

        public void moveLeft()
        {
            View v = m_window.GetView();
            v.Move(new SFML.Window.Vector2f(- speed,0));
            m_window.SetView(v);
           
        }

        public void moveUp()
        {
            View v = m_window.GetView();
            v.Move(new SFML.Window.Vector2f(0, -speed));
            m_window.SetView(v);
                       
        }

        public void moveDown()
        {
            View v = m_window.GetView();
            v.Move(new SFML.Window.Vector2f(0, speed));
            m_window.SetView(v);
                       
        }

        public void drawBottomLayer()
        {
            m_LayerSprite.Position = m_bottomLayerPosition;
            m_window.Draw(m_LayerSprite);
        }

        public void drawGrenadeButton()
        {
            m_grenadeSprite.Position = m_bottomLayerPosition + new Vec2f(929, 108);
            M_grenadeBox.Left = (int)m_grenadeSprite.Position.X;
            M_grenadeBox.Top = (int)m_grenadeSprite.Position.Y;
            m_window.Draw(m_grenadeSprite);
        }

        public void drawAutoFireButton()
        {
            m_AutofireSprite.Scale = new Vec2f(1.5f, 1.5f);
            m_AutofireSprite.Position = m_bottomLayerPosition + new Vec2f(987, 81);
            m_AutofireSprite.Color = new Color(150, 50, 50, 200);
            M_AutofireBox.Left = (int)m_AutofireSprite.Position.X;
            M_AutofireBox.Top = (int)m_AutofireSprite.Position.Y;
            m_window.Draw(m_AutofireSprite);
        }

        public void drawHeroIcon(Vec2f Position)
        {
            m_HeroIconSprite.Position = m_bottomLayerPosition + new Vec2f(260, 25) +(Position) * new Vec2f(65,0);
            m_window.Draw(m_HeroIconSprite);
        }

        public void drawAllyIcon(Vec2f Position)
        {
            m_AllyIconSprite.Position = m_bottomLayerPosition + new Vec2f(260, 25) + (Position) * new Vec2f(65, 0);
            m_window.Draw(m_AllyIconSprite);
        }
        #endregion
    }
}

 