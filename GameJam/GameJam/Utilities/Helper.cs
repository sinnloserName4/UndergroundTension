using SFML.Graphics;
using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Helper
    {
        //////////////////////___MEMBER___////////////////////////////
        #region Member

        public static readonly Music Start                  = new Music("../../Resources/Soundtrack/UndergroundTension.wav");
        public static readonly Music Forward                = new Music("../../Resources/Soundtrack/Forward.wav");

        public static readonly SoundBuffer[] b_HeroAttack   = new SoundBuffer[6];
        public static readonly SoundBuffer b_HeroStart      = new SoundBuffer("../../Resources/fx/Hero/hConquer1.wav");
        public static readonly SoundBuffer[] b_HeroWin      = new SoundBuffer[3];
        public static readonly SoundBuffer[] b_HeroMove     = new SoundBuffer[3];

        public static readonly SoundBuffer[] b_AllyAttack   = new SoundBuffer[3];
        public static readonly SoundBuffer b_AllyWin        = new SoundBuffer("../../Resources/fx/Ally/aWin1.wav");
        public static readonly SoundBuffer b_AllyDig        = new SoundBuffer("../../Resources/fx/Ally/aDig.wav");
        public static readonly SoundBuffer b_AllyMove        = new SoundBuffer("../../Resources/fx/Ally/aMove1.wav");

        public static readonly SoundBuffer[] b_Death        = new SoundBuffer[2];
        public static readonly SoundBuffer b_Shoot          = new SoundBuffer("../../Resources/fx/Shoot.wav");

        
        
        public static readonly Sound[] HeroAttack           = new Sound[6];
        public static readonly Sound HeroStart              = new Sound(b_HeroStart);
        public static readonly Sound[] HeroWin              = new Sound[3];
        public static readonly Sound[] HeroMove             = new Sound[3];

        public static readonly Sound[] AllyAttack = new Sound[6];
        public static readonly Sound AllyWin = new Sound(b_AllyWin);
        public static readonly Sound AllyDig = new Sound(b_AllyDig);
        public static readonly Sound AllyMove = new Sound(b_AllyMove);

        public static readonly Sound[] Death = new Sound[2];
        public static readonly Sound Shoot = new Sound(b_Shoot);




       

       




        public static readonly Texture t_DebugWhite = new Texture("../../Resources/White.bmp");
        public static readonly Texture t_Ground = new Texture("../../Resources/Tiles/Floor1.png");
        public static readonly Texture t_Left = new Texture("../../Resources/Tiles/Left.png");
        public static readonly Texture t_LowerLeft = new Texture("../../Resources/Tiles/LowerLeft.png");
        public static readonly Texture t_LowerMid = new Texture("../../Resources/Tiles/LowerMid.png");
        public static readonly Texture t_LowerRight = new Texture("../../Resources/Tiles/LowerRight.png");
        public static readonly Texture t_Right = new Texture("../../Resources/Tiles/Right.png");
        public static readonly Texture t_Upper = new Texture("../../Resources/Tiles/Upper.png");
        public static readonly Texture t_UpperLeft = new SFML.Graphics.Texture("../../Resources/Tiles/UpperLeft.png");
        public static readonly Texture t_UpperMid = new SFML.Graphics.Texture("../../Resources/Tiles/UpperMid.png");
        public static readonly Texture t_UpperRight = new SFML.Graphics.Texture("../../Resources/Tiles/UpperRight.png");
        public static readonly Texture t_DownRightInner = new Texture("../../Resources/Tiles/DownRightInner.png");
        public static readonly Texture t_DownLeftInner = new Texture("../../Resources/Tiles/DownLeftInner.png");
        public static readonly Texture t_UpRightInner = new Texture("../../Resources/Tiles/UpRightInner.png");
        public static readonly Texture t_UpLeftInner = new Texture("../../Resources/Tiles/UpLeftInner.png");
        
         
        // Hero

        public static readonly Texture Hero = new SFML.Graphics.Texture("../../Resources/CharTile.png");

        public static readonly Texture Enemy = new SFML.Graphics.Texture("../../Resources/enemy1Tile.png");
        public static readonly Texture Enemy2 = new SFML.Graphics.Texture("../../Resources/enemy2Tile.png");
        public static readonly Texture Ally = new SFML.Graphics.Texture("../../Resources/allyTile.png");
        // UI

        public static readonly Texture t_UI = new Texture("../../Resources/UserInterface.png");
        public static readonly Texture t_grenade = new Texture("../../Resources/UI_Icons/IconGranate.png");
        public static readonly Texture t_AllyIcon = new Texture("../../Resources/UI_Icons/IconAlly.png");
        public static readonly Texture t_HeldIcon = new Texture("../../Resources/UI_Icons/IconHeld.png");
        public static readonly Texture t_ManualFire = new Texture("../../Resources/UI_Icons/IconManualFire.png");

        #endregion

        static readonly IntRect rect= new IntRect(0,0,80,80);

        public static readonly Sprite DebugWhite  = new Sprite(t_DebugWhite,rect);
        public static readonly Sprite Ground      = new Sprite(t_Ground, rect);
        public static readonly Sprite Left        = new Sprite(t_Left, rect);
        public static readonly Sprite LowerLeft   = new Sprite(t_LowerLeft, rect);
        public static readonly Sprite LowerMid    = new Sprite(t_LowerMid, rect);
        public static readonly Sprite LowerRight  = new Sprite(t_LowerRight, rect);
        public static readonly Sprite Right       = new Sprite(t_Right, rect);
        public static readonly Sprite Upper       = new Sprite(t_Upper, rect);
        public static readonly Sprite UpperLeft   = new Sprite(t_UpperLeft, rect);
        public static readonly Sprite UpperMid    = new Sprite(t_UpperMid, rect);
        public static readonly Sprite UpperRight  = new Sprite(t_UpperRight, rect);
        public static readonly Sprite DownRightInner = new Sprite(t_DownRightInner, rect);
        public static readonly Sprite DownLeftInner = new Sprite(t_DownLeftInner, rect);
        public static readonly Sprite UpRightInner = new Sprite(t_UpRightInner, rect);
        public static readonly Sprite UpLeftInner = new Sprite(t_UpLeftInner, rect);


        public static readonly Sprite UI = new Sprite(t_UI, new SFML.Graphics.IntRect(0, 0, 1280, 160));
        public static readonly Sprite Grenade = new Sprite(t_grenade, new SFML.Graphics.IntRect(0, 0, 60, 60));
        public static readonly Sprite AllyIcon = new Sprite(t_AllyIcon, new SFML.Graphics.IntRect(0, 0, 60, 60));
        public static readonly Sprite HeldIcon = new Sprite(t_HeldIcon, new SFML.Graphics.IntRect(0, 0, 60, 60));
        public static readonly Sprite ManualFire = new Sprite(t_ManualFire, new SFML.Graphics.IntRect(0, 0, 60, 60));


        static Font Arial = new Font("../../Resources/arial.ttf");
        public static Text FPS = new Text("", Arial);

        
      
        //////////////////////___CONSTRUCTORS___//////////////////////

        #region Constructors

        public Helper()
        {
            b_HeroAttack[0] = new SoundBuffer("../../Resources/fx/Hero/hAttack1.wav");
            b_HeroAttack[1] = new SoundBuffer("../../Resources/fx/Hero/hAttack2.wav");
            b_HeroAttack[2] = new SoundBuffer("../../Resources/fx/Hero/hAttack3.wav");
            b_HeroAttack[3] = new SoundBuffer("../../Resources/fx/Hero/hAttack4.wav");
            b_HeroAttack[4] = new SoundBuffer("../../Resources/fx/Hero/hAttack5.wav");
            b_HeroAttack[5] = new SoundBuffer("../../Resources/fx/Hero/hAttack6.wav");

            b_HeroWin[0] = new SoundBuffer("../../Resources/fx/Hero/hFun1.wav");
            b_HeroWin[1] = new SoundBuffer("../../Resources/fx/Hero/hFun2.wav");
            b_HeroWin[2] = new SoundBuffer("../../Resources/fx/Hero/hFun3.wav");

            b_HeroMove[0] = new SoundBuffer("../../Resources/fx/Hero/hMove1.wav");
            b_HeroMove[1] = new SoundBuffer("../../Resources/fx/Hero/hMove2.wav");
            b_HeroMove[2] = new SoundBuffer("../../Resources/fx/Hero/hMove3.wav");

            b_AllyAttack[0] = new SoundBuffer("../../Resources/fx/Ally/aAttack1.wav");
            b_AllyAttack[1] = new SoundBuffer("../../Resources/fx/Ally/aAttack2.wav");
            b_AllyAttack[2] = new SoundBuffer("../../Resources/fx/Ally/aAttack3.wav");

            b_Death[0] = new SoundBuffer("../../Resources/fx/death1.wav");
            b_Death[1] = new SoundBuffer("../../Resources/fx/death2.wav");
           
            for (int i = 0;i<6;i++)
            {
                HeroAttack[i] = new Sound(b_HeroAttack[i]);
                HeroAttack[i].Volume = 1500;
            }

            for (int i = 0; i < 3; i++)
            {
                HeroWin[i] = new Sound(b_HeroWin[i]);
                HeroWin[i].Volume = 150;
            }

            for (int i = 0; i < 3; i++)
            {
                HeroMove[i] = new Sound(b_HeroMove[i]);
                
            }

            for (int i = 0; i < 3; i++)
            {
                AllyAttack[i] = new Sound(b_AllyAttack[i]);
            }

            for (int i = 0; i < 2; i++)
            {
                Death[i] = new Sound(b_Death[i]);
            }
       
        }
        
        #endregion

        //////////////////////___METHODS___//////////////////////
        #region Methods
                                
        #endregion
    }
}

