using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonogameShooter.ObjetosJuego
{
    class Player
    {
        // Animation representing the player. Imagen de la nave
        //private Texture2D _playerTexture;
        //sustituimos la imagen normal por una imagen que simula una animación
        public Animacion _jugadorAnimacion;
        // Position of the Player relative to the upper left side of the screen
        public Vector2 Position;
        // State of the player
        private bool _active;
        // Amount of hit points that player has
        private int _health;
        //Get the width of the player ship
        public int Width
        {
            get { return _jugadorAnimacion.FrameWidth; }
        }
        public int Height
        {
            get { return _jugadorAnimacion.FrameHeight; }
        }




        //public void Initialize(Texture2D texture, Vector2 position)
        public void Initialize(Animacion animation, Vector2 position)
        {

            //     _playerTexture = texture;
            _jugadorAnimacion = animation;

            // Set the starting position of the player around the middle of the screen and to the back
            Position = position;
            // Set the player to be active
            _active = true;
            // Set the player health
            _health = 100;
        }
        public void Update(GameTime gameTime)
        {
            _jugadorAnimacion.Position = Position;
            _jugadorAnimacion.Update(gameTime);


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_playerTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            _jugadorAnimacion.Draw(spriteBatch);

        }
    }
}
