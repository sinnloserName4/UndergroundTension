using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class GameInstructor
    {
        //////////////////////___MEMBER___////////////////////////////
        #region Member

        Control m_control;
        Camera m_cam;
        Map m_map;
        List<Character> m_chars = new List<Character>();
        Boolean Autofire = false;
        Boolean Grenade = false;
        Boolean LoopStart = false;
        Boolean soundPlay = false;


        // DEPRECATED  
        Vec2f temp;
        SFML.Graphics.Sprite m_sprite;
        double t = 0;



        #endregion

        //////////////////////___CONSTRUCTORS___//////////////////////

        #region Constructors

        public GameInstructor()
        {
            Helper.Forward.Loop = true;
            Helper.Start.Volume = 40;
            Helper.Forward.Volume = 40;
            Helper.Start.Play();
        }

        #endregion

        //////////////////////___METHODS___//////////////////////
        #region Methods

        public void update(GameTime time)
        {
            m_control.check();
            t += time.ElapsedTime.TotalSeconds;
            if (t > 1)
            {
                soundPlay = false;
                t = 0;
            }

            if (Helper.Start.Status == SFML.Audio.SoundStatus.Stopped && !LoopStart)
            {
                Helper.Forward.Play();
                LoopStart = true;
            }


            if (m_control.Keyboard.isPressed(SFML.Window.Keyboard.Key.LControl))
                Autofire = true;

#if DEBUG

            if (m_control.Keyboard.isReleased(SFML.Window.Keyboard.Key.A))
                Debug.DrawMousePosition = !Debug.DrawMousePosition;
            if (m_control.Keyboard.isReleased(SFML.Window.Keyboard.Key.W))
                Debug.DrawFields = !Debug.DrawFields;
            if (m_control.Keyboard.isReleased(SFML.Window.Keyboard.Key.S))
                Debug.DrawFPS = !Debug.DrawFPS;
            if (m_control.Keyboard.isReleased(SFML.Window.Keyboard.Key.D))
                Debug.DrawChars = !Debug.DrawChars;
            if (m_control.Keyboard.isReleased(SFML.Window.Keyboard.Key.Q))
                Debug.ChangeView = !Debug.ChangeView;


#endif


            if (m_control.Mouse.leftClicked())
            {
                temp = (Vec2f)m_control.Mouse.getMousePos() + m_cam.m_topPosition;
                


            }
            if (m_control.Mouse.leftReleased())
            {
                setActive(temp, (Vec2f)m_control.Mouse.getMousePos() + m_cam.m_topPosition);

                if (m_cam.M_AutofireBox.Contains((int)temp.X, (int)temp.Y))
                {
                    Autofire = true;
                    Grenade = false;
                    Console.WriteLine("Autofire");
                    

                }

                if (m_cam.M_grenadeBox.Contains((int)temp.X, (int)temp.Y))
                {
                    Autofire = false;
                    Grenade = true;
                    Console.WriteLine("Grenade");
                }

                Autofire = false;
                Grenade = false;
            }


            foreach (Character Element in m_chars)
            {
                Element.update();
                if (m_control.Mouse.rightPressed() && Element.m_active && !m_control.Keyboard.isPressed(SFML.Window.Keyboard.Key.LControl))
                {

                    Element.straightMove((Vec2f)m_control.Mouse.getMousePos() + m_cam.m_topPosition);
                    
                }
                else if (Element.m_active && m_control.Mouse.rightReleased() && Autofire)
                {
                    Element.attack((Vec2f)m_control.Mouse.getMousePos() + m_cam.m_topPosition);

                    if (!soundPlay)
                    {

                        if (Element.getTyp() == UnitType.Hero)
                        {
                            Helper.HeroAttack[Game.RandGen.Next(6)].Play();
                            soundPlay = true;
                        }
                        else
                            Helper.AllyAttack[Game.RandGen.Next(3)].Play();
                            soundPlay = true;
                            Autofire = false;
                    }
                }

            }




            if (Debug.DrawMousePosition)
                Console.WriteLine((new Vec2f(m_control.Mouse.getMousePos()) + m_cam.m_topPosition) / 80);
            if (Debug.ChangeView)
                m_cam.update(m_control);

        }

        public void draw(SFML.Graphics.RenderWindow window, GameTime time)
        {
            List<Objects> m_objects_copy = new List<Objects>();
            m_map.drawGround(window, m_cam);
            foreach (Character Element in m_chars)
            {

                Element.draw(window, m_cam);
                foreach (Objects obj in Element.m_objects)
                {
                    obj.update();

                    if (obj.m_leftSteps < -100)
                    {
                        continue;
                    }

                    foreach (Character Element2 in m_chars)
                    {
                        if (Element == Element2 || !Element2.m_enemy) continue;
                        if (inRadius(25, obj.m_position, Element2.m_position))
                        {
                            Element2.hitted();
                            obj.m_leftSteps = -200;
                        }
                    }

                    obj.draw(window, m_cam);
                    m_objects_copy.Add(obj);
                }
                Element.m_objects = new List<Objects>(m_objects_copy);
                m_objects_copy = new List<Objects>();

            }
            m_map.drawFront(window, m_cam);

            if (Debug.DrawChars)
                foreach (Character Element in m_chars)
                {

                    Element.draw(window, m_cam);

                }

            // DEBUG
            if (Debug.DrawFields)
                m_map.drawDebug(window, m_cam);
            t += time.ElapsedTime.TotalSeconds;
            if (Debug.DrawFPS)
            {
                if (t > 0.1)
                {
                    Helper.FPS.DisplayedString = "FPS: " + (1.0f / (float)time.ElapsedTime.TotalSeconds).ToString();
                    t = 0;
                }
                Helper.FPS.Position = m_cam.m_topPosition;
                window.Draw(Helper.FPS);

            }

            if (m_control.Mouse.leftPressed())
            {
                Vec2f currentMPos = (Vec2f)m_control.Mouse.getMousePos() + m_cam.m_topPosition;
                drawSelecRect(window, temp, currentMPos);
            }
            // DEPRECATED
            m_map.drawMinimap(window, m_cam);

            m_cam.drawBottomLayer();
            m_cam.drawGrenadeButton();
            m_cam.drawAutoFireButton();
            m_cam.drawHeroIcon(new Vec2f(0, 0));
            m_cam.drawAllyIcon(new Vec2f(1, 0));


        }

        public void Add(Map map)
        {
            m_map = map;
        }

        public void Add(Camera cam)
        {
            m_cam = cam;
        }

        public void Add(Control control)
        {
            m_control = control;
        }

        public void Add(Character char1)
        {
            m_chars.Add(char1);
        }

        #endregion

        public void setActive(Vec2f topLeft, Vec2f bottomRight)
        {

            float Xstart = Math.Min(topLeft.X, bottomRight.X);
            float Xend = Math.Max(topLeft.X, bottomRight.X);
            float Ystart = Math.Min(topLeft.Y, bottomRight.Y);
            float Yend = Math.Max(topLeft.Y, bottomRight.Y);


            foreach (Character Element in m_chars)
            {

                if (Element.m_position.X > Xstart && Element.m_position.X < Xend && Element.m_position.Y > Ystart && Element.m_position.Y < Yend)
                {
                    Element.m_active = true;
                }
                else
                {
                    if (!(Xstart > m_cam.m_bottomLayerPosition.X && Ystart > m_cam.m_bottomLayerPosition.Y))
                        Element.m_active = false;
                }
            }
        }

        public void drawSelecRect(SFML.Graphics.RenderWindow window, Vec2f topLeft, Vec2f bottomRight)
        {

            float Xstart = Math.Min(topLeft.X, bottomRight.X);
            float Xend = Math.Max(topLeft.X, bottomRight.X);
            float Ystart = Math.Min(topLeft.Y, bottomRight.Y);
            float Yend = Math.Max(topLeft.Y, bottomRight.Y);

            m_sprite = new SFML.Graphics.Sprite(Helper.t_DebugWhite, new SFML.Graphics.IntRect(0, 0, (int)(Xstart - Xend), (int)(Ystart - Yend)));
            m_sprite.Position = new Vec2f(Xstart, Ystart);
            m_sprite.Color = new SFML.Graphics.Color(0, 128, 0, 128);
            window.Draw(m_sprite);

        }

        public bool inRadius(float radius, Vec2f point, Vec2f position)
        {



            double x = Math.Pow(point.X - position.X - 40, 2);
            double y = Math.Pow(point.Y - position.Y - 40, 2);
            double r = Math.Pow(radius, 2);
            return (x + y <= r);

        }
    }
}