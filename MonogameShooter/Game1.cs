#region Using Statements

using System;
using System.Collections.Generic;
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


        //Para el fondo
        // Image used to display the static background
        Texture2D _mainBackground;
        Rectangle _rectBackground;
        float scale = 1f;
        private Fondo _bgLayer1;
        private Fondo _bgLayer2;


        //Laser
        // texture to hold the laser.
        private Texture2D _laserTexture;
        private List<Laser> _laserBeams;
        // govern how fast our laser can fire.
        private TimeSpan _laserSpawnTime;
        private TimeSpan _previousLaserSpawnTime;

        //Enemies
        Texture2D enemyTexture;
        //   List<Enemy> enemies;



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
            _velocidadJugador = 8.0f;


            //Background
            _bgLayer1 = new Fondo();
            _bgLayer2 = new Fondo();
            _rectBackground = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            // init our laser
            _laserBeams = new List<Laser>();
            const float SECONDS_IN_MINUTE = 60f;
            const float RATE_OF_FIRE = 200f;
            _laserSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
            _previousLaserSpawnTime = TimeSpan.Zero;


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


            // Load the parallaxing background
            _bgLayer1.Initialize(Content, "Graphics/bgLayer1", GraphicsDevice.Viewport.Width,
            GraphicsDevice.Viewport.Height, -1);
            _bgLayer2.Initialize(Content, "Graphics/bgLayer2", GraphicsDevice.Viewport.Width,
            GraphicsDevice.Viewport.Height, -2);
            _mainBackground = Content.Load<Texture2D>("Graphics/mainbackground");


            // TODO: use this.Content to load your game content here
            //  Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
            // GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            // _jugador.Initialize(Content.Load<Texture2D>("Graphics\\player"), playerPosition);
            Animacion _jugadorAnimacion = new Animacion();
            Texture2D playerTexture = Content.Load<Texture2D>("Graphics\\shipAnimation");
            //8 son las imágenes distintas que pertenecerán de alguna manera al "array" de imágenes
            _jugadorAnimacion.Initialize(playerTexture, Vector2.Zero, 115, 69, 8, 30, Color.White, 1f, true);
            //playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y+ GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            //_jugador.Initialize(_jugadorAnimacion, playerPosition);
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            _jugador.Initialize(_jugadorAnimacion, playerPosition);



            //Laser
            // load th texture to serve as the laser.
            _laserTexture = Content.Load<Texture2D>("Graphics\\laser");

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



            // Update the parallaxing background
            _bgLayer1.Update(gameTime);
            _bgLayer2.Update(gameTime);


            //    _estadoAnteriorTeclado = _estadoActualTeclado;
            // Read the current state of the keyboard and gamepad and store it
            _estadoActualTeclado = Keyboard.GetState();

            //Update the player
            UpdatePlayer(gameTime);




            // update laserbeams
            for (var i = 0; i < _laserBeams.Count; i++)
            {
                _laserBeams[i].Update(gameTime);
                // Remove the beam when its deactivated or is at the end of the screen.
                if (!_laserBeams[i].Active || _laserBeams[i].Position.X >
                GraphicsDevice.Viewport.Width)
                {
                    _laserBeams.Remove(_laserBeams[i]);
                }
            }
            base.Update(gameTime);
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            // Get Thumbstick Controls
            _jugador.Update(gameTime);



            // Use the Keyboard 
            if (_estadoActualTeclado.IsKeyDown(Keys.Left))
                _jugador.Position.X = _jugador.Position.X - _velocidadJugador;
            if (_estadoActualTeclado.IsKeyDown(Keys.Right))
            {
                _jugador.Position.X = _jugador.Position.X + _velocidadJugador;
            }
            if (_estadoActualTeclado.IsKeyDown(Keys.Up))
            {
                _jugador.Position.Y = _jugador.Position.Y - _velocidadJugador;
            }
            if (_estadoActualTeclado.IsKeyDown(Keys.Down))
            {
                _jugador.Position.Y = _jugador.Position.Y + _velocidadJugador;
            }
            // Make sure that the player does not go out of bounds
            _jugador.Position.X = MathHelper.Clamp(_jugador.Position.X, 0, GraphicsDevice.Viewport.Width - _jugador.Width);
            _jugador.Position.Y = MathHelper.Clamp(_jugador.Position.Y, 0, GraphicsDevice.Viewport.Height - _jugador.Height);


            if (_estadoActualTeclado.IsKeyDown(Keys.Space))
            {
                FireLaser(gameTime);
            }
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

            //Draw the Main Background Texture
            _spriteBatch.Draw(_mainBackground, _rectBackground, Color.White);
            // Draw the moving background
            _bgLayer1.Draw(_spriteBatch);
            _bgLayer2.Draw(_spriteBatch);

            // Draw the Player
            _jugador.Draw(_spriteBatch);

            foreach (var l in _laserBeams)
            {
                l.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            // Draw the lasers.


            base.Draw(gameTime);
        }


        protected void FireLaser(GameTime gameTime)
        {

            // govern the rate of fire for our lasers
            if (gameTime.TotalGameTime - _previousLaserSpawnTime > _laserSpawnTime)
            {
                _previousLaserSpawnTime = gameTime.TotalGameTime;
                // Add the laer to our list.
                AddLaser();
            }
        }

        protected void AddLaser()
        {
            Animacion laserAnimation = new Animacion();
            // initlize the laser animation
            laserAnimation.Initialize(_laserTexture,
                _jugador.Position,
                46,
                16,
                1,
                30,
                Color.White,
                1f,
                true);
            Laser laser = new Laser();
            // Get the starting postion of the laser.
            var laserPosition = _jugador.Position;
            // Adjust the position slightly to match the muzzle of the cannon.
            laserPosition.Y += 5;
            laserPosition.X += 30;
            // init the laser
            laser.Initialize(laserAnimation, laserPosition);
            _laserBeams.Add(laser);
            /* todo: add code to create a laser. */
            // laserSoundInstance.Play();
        }

        protected void UpdateCollions()
        {
            /*          Rectangle laserRectangle;

                      // detect collisions between the player and all enemies.
                      enemies.ForEach(e =>
                      {
          //create a retangle for the enemy
                          enemyRectangle = new Rectangle(
                              (int) e.Position.X,
                              (int) e.Position.Y,
                              e.Width,
                              e.Height);
          // now see if this enemy collide with any laser shots
                          laserBeams.ForEach(lb =>
                          {
          // create a rectangle for this laserbeam
                              laserRectangle = new Rectangle(
                                  (int) lb.Position.X,
                                  (int) lb.Position.Y,
                                  lb.Width,
                                  lb.Height);
          // test the bounds of the laer and enemy
                              if (laserRectangle.Intersects(enemyRectangle))
                              {
          // play the sound of explosion.
                                  var explosion = explosionSound.CreateInstance();
                                  explosion.Play();
          // Show the explosion where the enemy was...
                                  AddExplosion(e.Position);
          // kill off the enemy
                                  e.Health = 0;
          //record the kill
                                  myGame.Stage.EnemiesKilled++;
          // kill off the laserbeam
                                  lb.Active = false;
          // record your score
                                  myGame.Score += e.Value;
                              }
                          });
                      });*/
        }

        protected void UpdateCollision()
        {

            // we are going to use the rectangle's built in intersection
            // methods.
            /*
                        Rectangle playerRectangle;
                        Rectangle enemyRectangle;
                        Rectangle laserRectangle;

                        // create the rectangle for the player
                        playerRectangle = new Rectangle(
                            (int)_jugador.Position.X,
                            (int)_jugador.Position.Y,
                            _jugador.Width,
                            _jugador.Height);

                        // detect collisions between the player and all enemies.
                        for (var i = 0; i < enemies.Count; i++)
                        {
                            enemyRectangle = new Rectangle(
                               (int)enemies[i].Position.X,
                               (int)enemies[i].Position.Y,
                               enemies[i].Width,
                               enemies[i].Height);

                            // determine if the player and the enemy intersect.
                            if (playerRectangle.Intersects(enemyRectangle))
                            {


                                // deal damge to the player
                                _jugador.Health -= enemies[i].Damage;

                                // if the player has no health destroy it.
                                if (_jugador.Health <= 0)
                                {
                                    _jugador.Active = false;
                                }
                            }

                            for (var l = 0; l < laserBeams.Count; l++)
                            {
                                // create a rectangle for this laserbeam
                                laserRectangle = new Rectangle(
                                    (int)laserBeams[l].Position.X,
                                    (int)laserBeams[l].Position.Y,
                                    laserBeams[l].Width,
                                    laserBeams[l].Height);

                                // test the bounds of the laer and enemy
                                if (laserRectangle.Intersects(enemyRectangle))
                                {
                                    // kill off the enemy
                                    enemies[i].Health = 0;

                                    // kill off the laserbeam
                                    laserBeams[l].Active = false;
                                }
                            }
                        }*/
        }
    }
}
