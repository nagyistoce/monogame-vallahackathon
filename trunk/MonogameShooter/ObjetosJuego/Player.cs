using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameShooter.ObjetosJuego
{
    class Player
    {
        // Animation representing the player. Imagen de la nave
        private Texture2D _playerTexture;
        // Position of the Player relative to the upper left side of the screen
        private Vector2 _position;
        // State of the player
        private bool _active;
        // Amount of hit points that player has
        private int _health;
        //Get the width of the player ship
        public int Width
        {
            get { return _playerTexture.Width; }
        }
        // Get the height of the player ship
        public int Height
        {
            get { return _playerTexture.Height; }
        }


        public void Initialize(Texture2D texture, Vector2 position)
        {

            _playerTexture = texture;
            // Set the starting position of the player around the middle of the screen and to the back
            _position = position;
            // Set the player to be active
            _active = true;
            // Set the player health
            _health = 100;
        }
        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw (_playerTexture, _position, null, Color.White, 0f, Vector2.Zero, 1f,SpriteEffects.None, 0f);

        }
    }
}
