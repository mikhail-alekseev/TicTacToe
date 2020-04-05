using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.UI
{
    public class Manager
    {
        Texture2D NormalTexture;
        Texture2D HoverTexture;
        SpriteFont Font;


        public Manager(Texture2D normalTexture, Texture2D hoverTexture, SpriteFont font)
        {
            NormalTexture = normalTexture;
            HoverTexture = hoverTexture;
            Font = font;
        }

        public TextButton GetTextButton(string id, string text, Action callback, Rectangle rect, Color color)
        {
            return new TextButton(id, text, callback, NormalTexture, HoverTexture, rect, Font, color);
        }

        public RadioMenu GetRadioMenu(string[] languages, string[] labels, int cursor, Action action, Rectangle rect)
        {
            return new RadioMenu(languages, labels, cursor, action, Font, NormalTexture, HoverTexture, rect);
        }
    }
}
