#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MonogameShooter.ObjetosJuego;

#endregion

namespace MonogameShooter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        private Player _jugador;

        // Keyboard states used to determine key presses
        private KeyboardState _estadoActualTeclado;
        private KeyboardState _estadoAnteriorTeclado;
        //Mouse states used to track Mouse button press
        // A movement speed for the player
        private float _velocidadJugador;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _jugador = new Player();
            _velocidadJugador=  8.0f;
        
            //Enable the FreeDrag gesture.
       //     TouchPanel.EnabledGestures = GestureType.FreeDrag;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
            GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            _jugador.Initialize(Content.Load<Texture2D>("Graphics\\player"), playerPosition);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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

            // TODO: Add your update logic here
            // Save the previous state of the keyboard and game pad so we can determine single key/button presses

        //    _estadoAnteriorTeclado = _estadoActualTeclado;
            // Read the current state of the keyboard and gamepad and store it
            _estadoActualTeclado = Keyboard.GetState();

            //Update the player
            UpdatePlayer(gameTime);

            base.Update(gameTime);
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            // Get Thumbstick Controls


          

            // Use the Keyboard 
            if (_estadoActualTeclado.IsKeyDown(Keys.Left) )
                _jugador.Position.X = _jugador.Position.X - _velocidadJugador;
            if (_estadoActualTeclado.IsKeyDown(Keys.Right) )
            {
                _jugador.Position.X = _jugador.Position.X + _velocidadJugador;
            }
            if (_estadoActualTeclado.IsKeyDown(Keys.Up) )
            {
                _jugador.Position.Y  = _jugador.Position.Y -_velocidadJugador;
            }
            if (_estadoActualTeclado.IsKeyDown(Keys.Down) )
            {
                _jugador.Position.Y  = _jugador.Position.Y+ _velocidadJugador;
            }
            // Make sure that the player does not go out of bounds
            _jugador.Position.X = MathHelper.Clamp(_jugador.Position.X, 0, GraphicsDevice.Viewport.Width - _jugador.Width);
            _jugador.Position.Y = MathHelper.Clamp(_jugador.Position.Y, 0, GraphicsDevice.Viewport.Height - _jugador.Height);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Start drawing
            _spriteBatch.Begin();

            // Draw the Player
            _jugador.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
