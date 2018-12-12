using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Game : Template.AbstractGame
    {


        //////////////////////___MEMBER___////////////////////////////
        #region Member
       
        Helper help = new Helper();
        Map map;
        Camera cam;
        Control control;
        GameInstructor Instructor;
        Vec2f Fields = new Vec2f(60, 60);
        List<Character> chars = new List<Character>();
        public static Random RandGen = new Random();
        

        #endregion

        //////////////////////___CONSTRUCTORS___//////////////////////
        #region Constructors
        public Game() : base(1280, 720, "lelele", SFML.Window.Styles.Default) { }

        #endregion

        //////////////////////___METHODS___//////////////////////
        #region Methods

        public override void loadContent()
        {
            Instructor = new GameInstructor();
            cam = new Camera(Fields,window);
            map = new Map(Fields);
            control = new Control(window);
            chars.Add(new Hero(new Vec2f(20, 20), map));
            chars.Add(new Enemy(0, new Vec2f(1000, 200), map));
            chars.Add(new Enemy(1, new Vec2f(200, 1000), map));
            chars.Add(new Ally(new Vec2f(200, 200), map));
                        
        }

        public override void initialize()
        {
            map.InitializeRandom(cam);
            Instructor.Add(map);
            Instructor.Add(cam);
            Instructor.Add(control);

            foreach (Character chara in chars)
            {
                Instructor.Add(chara);
            }

        }
                
        public override void update(GameTime gameTime)
        {
            Instructor.update(gameTime);
        }

        public override void draw(GameTime gameTime, SFML.Graphics.RenderWindow window)
        {
           window.Clear();
           Instructor.draw(window,gameTime);

        }

        #endregion
    }
}
