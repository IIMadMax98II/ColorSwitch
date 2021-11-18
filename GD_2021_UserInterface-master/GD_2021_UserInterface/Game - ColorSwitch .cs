//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;

///*

// */

//namespace GD_2021_UserInterface
//{
//    public class ColorSwitch : Game
//    {
//        private GraphicsDeviceManager _graphics;
//        private SpriteBatch _spriteBatch;
//        private SpriteFont spriteFont;
//        private Texture2D progressTexture;
//        private float rotationInDegrees = 0;

//        public ColorSwitch()
//        {
//            _graphics = new GraphicsDeviceManager(this);
//            Content.RootDirectory = "Content";
//            IsMouseVisible = true;
//        }

//        protected override void Initialize()
//        {
//            // TODO: Add your initialization logic here

//            base.Initialize();
//        }

//        protected override void LoadContent()
//        {
//            _spriteBatch = new SpriteBatch(GraphicsDevice);

//            spriteFont = Content.Load<SpriteFont>("ui_font");
//            //progressTexture = Content.Load<Texture2D>("UI_ProgressBar_32_8");
//            progressTexture = Content.Load<Texture2D>("UI_Blue_Wall");
//        }

//        protected override void Update(GameTime gameTime)
//        {
//            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
//                Exit();

//            // TODO: Add your update logic here
//            rotationInDegrees += 1/60.0f;
//            base.Update(gameTime);
//        }

//        protected override void Draw(GameTime gameTime)
//        {
//            GraphicsDevice.Clear(Color.Black);

//            string str = "Perish";
//            var dimensions = spriteFont.MeasureString(str);
//            var origin = new Vector2(dimensions.X / 2, dimensions.Y / 2);

//            _spriteBatch.Begin();
//            _spriteBatch.Draw(progressTexture, new Vector2(0, 0), new Rectangle(0,0,1600,800), Color.Red, 0, Vector2.Zero, new Vector2(4,1), SpriteEffects.None, 0);
//            _spriteBatch.End();

//            //How to draw text/image?
//            _spriteBatch.Begin();
//            _spriteBatch.DrawString(spriteFont, str, new Vector2(400, 250),
//                                    Color.Red, rotationInDegrees, origin,
//                                    1, SpriteEffects.None, 1);
//            _spriteBatch.End();


//            base.Draw(gameTime);
//        }
//    }
//}