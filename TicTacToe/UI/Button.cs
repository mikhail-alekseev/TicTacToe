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
    public class Button : IButton
    {
        public enum State
        {
            None,
            Hovered
        }

        private Texture2D _texture;
        private Rectangle _rectangle;
        private State _state;
        private Dictionary<State, Rectangle> _textureRectangles;
        private Action _callback;

        private float _lastPressedTime;
        private bool _pressed;

        public Button(Rectangle rectangle, Texture2D texture, Rectangle NormalTextureRectangle, Rectangle HoveredTextureRectangle, Action callback)
        {
            _texture = texture;
            _textureRectangles = new Dictionary<State, Rectangle>();
            _rectangle = rectangle;
            _textureRectangles[State.None] = NormalTextureRectangle;
            _textureRectangles[State.Hovered] = HoveredTextureRectangle;
            _callback = callback;

            _lastPressedTime = 0f;
            _pressed = false;
        }

        public void Update(float deltaTime)
        {
            if (_rectangle.Contains(Mouse.GetState().Position))
            {
                _state = State.Hovered;
            }
            else
            {
                _state = State.None;
            }

            if (_state == State.Hovered && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _pressed = true;
            }

            if (_state == State.Hovered && _pressed && Mouse.GetState().LeftButton == ButtonState.Released && deltaTime - _lastPressedTime > 500)
            {
                _lastPressedTime = deltaTime;
                _pressed = false;
                _callback();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, _textureRectangles[_state], Color.White);
        }
    }
}
