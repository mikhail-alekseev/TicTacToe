using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using TicTacToe.FSM;

namespace TicTacToe
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TicTacToeGame : Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public StateMachine StateMachine;
        public Localization Loc { get; }
        public Texture2D Logo;
        public SpriteFont Font;
        public SaveGame Save;

        public UI.Manager UIManager;

        public TicTacToeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;

            
            Content.RootDirectory = "Content";
            if (File.Exists("settings"))
            {
                Loc = new Localization("loc.csv", File.ReadAllText("settings"));
            }
            else
            {
                Loc = new Localization("loc.csv", "english");
            }

            Save = new SaveGame();
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Font = this.Content.Load<SpriteFont>("Arial");

            Texture2D normalTexture = Content.Load<Texture2D>("buttonbignormal");
            Texture2D hoverTexture = Content.Load<Texture2D>("buttonbighovered");
            UIManager = new UI.Manager(normalTexture, hoverTexture, Font);

            StateMachine = new StateMachine(this);
            StateMachine.Add("MENU", new States.Menu(StateMachine));
            StateMachine.Add("GAME", new States.GameInProgress(StateMachine));
            StateMachine.Add("STATS", new States.Stats(StateMachine));
            StateMachine.Change("MENU");

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Logo = this.Content.Load<Texture2D>("logo");
            

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float deltaTime = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            StateMachine.Update(deltaTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            StateMachine.Draw();

            base.Draw(gameTime);
        }

        public void ExitGame()
        {
            StateMachine.Change("MENU");
            Save.SaveToDisk();
            Exit();
        }
    }
}
