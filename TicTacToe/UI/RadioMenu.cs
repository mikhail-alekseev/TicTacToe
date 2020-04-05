using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.UI
{
    public class RadioMenu
    {
        Rectangle _rect;
        string[] _choices;
        string[] _labels;
        int _cursor;
        Action _onChange;

        IButton[] _buttons;

        SpriteFont _font;

        public RadioMenu(string[] choices, string[] labels, int cursor, Action onChange, SpriteFont font, Texture2D normal, Texture2D hovered, Rectangle rect)
        {
            _rect = rect;
            _choices = choices;
            _labels = labels;
            _cursor = cursor;
            _font = font;
            _onChange = onChange;
            _buttons = new IButton[]
            {
                new TextButton("", ">", new Action(Increment), normal, hovered, new Rectangle(rect.X + rect.Width - 50, rect.Y, 50, rect.Height), font, Color.Black),
                new TextButton("", "<", new Action(Decrement), normal, hovered, new Rectangle(rect.X, rect.Y, 50, rect.Height), font, Color.Black)
            };
        }

        public string GetCurrentSelection()
        {
            return _choices[_cursor];
        }

        public string GetCurrentSelectionLabel()
        {
            return _labels[_cursor];
        }

        public void Increment()
        {
            _cursor++;

            if (_cursor >= _choices.Length)
            {
                _cursor = 0;
            }

            _onChange();
        }

        public void Decrement()
        {
            _cursor--;

            if (_cursor < 0)
            {
                _cursor = _choices.Length - 1;
            }

            _onChange();
        }

        public void Update(float deltaTime)
        {
            foreach (IButton button in _buttons)
            {
                button.Update(deltaTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IButton button in _buttons)
            {
                button.Draw(spriteBatch);
            }

            string currSelection = GetCurrentSelectionLabel();

            Vector2 size = _font.MeasureString(currSelection);

            Vector2 mid = new Vector2((_rect.Width - 100) / 2, _rect.Height / 2);

            spriteBatch.DrawString(_font, currSelection, new Vector2(_rect.X + 50 + mid.X - size.X / 2, _rect.Y + mid.Y - size.Y / 2), Color.Black);
        }
    }
}
