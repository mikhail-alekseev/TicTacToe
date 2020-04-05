using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.UI
{
    public class TextButton : IButton
    {
        public string Text { get; set; }
        public string Id { get; set; }
        private SpriteFont _font;
        private Color _textColor;
        private Action _callback;
        private Dictionary<Button.State, Texture2D> _textures = new Dictionary<Button.State, Texture2D>();
        private Rectangle _rectangle;
        private Button.State _state;

        private bool _pressed;
        private float _lastPressedTime;

        public TextButton(string id, string text, Action callback, Texture2D normalTexture, Texture2D hoveredTexture, Rectangle rectangle, SpriteFont font, Color textColor)
        {
            Id = id;
            Text = text;
            _callback = callback;
            _textures[Button.State.None] = normalTexture;
            _textures[Button.State.Hovered] = hoveredTexture;
            _rectangle = rectangle;
            _font = font;
            _textColor = textColor;

            _pressed = false;
            _lastPressedTime = 0f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textures[_state], _rectangle, Color.White);
            Vector2 textSize = _font.MeasureString(Text);
            Vector2 offset = (_rectangle.Size.ToVector2() - textSize) / 2;
            spriteBatch.DrawString(_font, Text, _rectangle.Location.ToVector2() + offset, _textColor);
        }

        public void Update(float deltaTime)
        {
            if (_rectangle.Contains(Mouse.GetState().Position))
            {
                _state = Button.State.Hovered;
            }
            else
            {
                _pressed = false;
                _state = Button.State.None;
            }

            if (_state == Button.State.Hovered && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _pressed = true;
            }

            if (_state == Button.State.Hovered && _pressed && Mouse.GetState().LeftButton == ButtonState.Released)
            {
                _lastPressedTime = deltaTime;
                _pressed = false;
                _callback();
            }
        }
    }

}
