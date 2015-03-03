using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameShooter.ObjetosJuego
{
    class Fondo
    {
        // The image representing the parallaxing background
        private Texture2D _texture;
        // An array of positions of the parallaxing background
        private Vector2[] _positions;
        // The speed which the background is moving
        private int _speed;
        private int _bgWidth;
        private int _bgHeight;

        public void Initialize(ContentManager content, String texturePath, int screenWidth, int screenHeight, int speed)
        {
            _bgHeight = screenHeight;
            _bgWidth = screenWidth;

            _texture = content.Load<Texture2D>(texturePath);

            _speed = speed;

            // If we divide the screen with the texture width then we can determine the number of tiles needed.
            // We add 1 to it so that we won't have a gap in the tiling.

            _positions = new Vector2[screenWidth / _texture.Width + 1];

            // Set the initial positions of the parallazing background
            for (int i = 0; i < _positions.Length; i++)
            {
                _positions[i] = new Vector2(i * _texture.Width, 0);
            }
        }

        public void Update(GameTime gametime)
        {
            // Update the positions of the background
            for (int i = 0; i < _positions.Length; i++)
            {
// Update the position of the screen by adding the speed
                _positions[i].X += _speed;
// If the speed has the background moving to the left
                if (_speed <= 0)
                {
// Check the texture is out of view then put that texture at the end of the screen
                    if (_positions[i].X <= -_texture.Width)
                    {
                        _positions[i].X = _texture.Width*(_positions.Length - 1);
                    }
                }
// If the speed has the background moving to the right
                else
                {
// Check if the texture is out of view then position it to the start of thescreen
                    if (_positions[i].X >= _texture.Width*(_positions.Length - 1))
                    {
                        _positions[i].X = -_texture.Width;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _positions.Length; i++)
            {
                Rectangle rectBg = new Rectangle((int)_positions[i].X, (int)_positions[i].Y, _bgWidth, _bgHeight);
                spriteBatch.Draw(_texture, rectBg, Color.White);
            }
        }
    }
}
