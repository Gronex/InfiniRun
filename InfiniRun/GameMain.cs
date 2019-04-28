using System.Collections.Generic;
using System.Linq;
using InfiniRun.Controlls;
using InfiniRun.GameObjects;
using InfiniRun.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InfiniRun
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameMain : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        // private EnvironmentContext _environmentContext;
        //private Texture2D _groundTexture;
        //private Texture2D _characterTexture;
        //private Texture2D _obstacleTexture;

        private bool _paused;

        private PopulationManager _populationManager;

        public GameMain()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _populationManager = new PopulationManager();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            _populationManager.Initialize(GraphicsDevice.Viewport.Bounds);
            _paused = false;

            //_environmentContext = new EnvironmentContext(new Ground(_groundTexture, new Vector2(0, 300)), GenerateCharacters(), GraphicsDevice.Viewport.Bounds, _obstacleTexture);

        }

        //private IEnumerable<Character> GenerateCharacters()
        //{
        //    return new List<Character>
        //    {
        //        new Character(_characterTexture, new Vector2(50, 300 - _characterTexture.Height), new KeyboardController())
        //    };
        //}

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //_characterTexture = Content.Load<Texture2D>("character");
            //_groundTexture = Content.Load<Texture2D>("ground");
            //_obstacleTexture = Content.Load<Texture2D>("obstacle");
            _populationManager.LoadContent(Content);
            TextHelper.Font = Content.Load<SpriteFont>("font");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
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

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Initialize();
                base.Update(gameTime);
                return;
            }

            // todo: add pause
            if (!_paused)
            {
                _populationManager.Update(gameTime);
            }

            //if(_environmentContext.Characters.All(x => !x.Dead))
            //{
            //    _environmentContext.Update(gameTime);
            //}

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //_environmentContext.Draw(_spriteBatch);
            _populationManager.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
