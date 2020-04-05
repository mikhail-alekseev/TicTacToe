using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public enum GameState
    {
        XWin,
        OWin,
        Draw,
        InProgress
    }

    public enum CellType
    {
        X,
        O,
        EMPTY
    }

    class Cell
    {
        public CellType Type;
        public Rectangle Rectangle;

        public Cell(CellType type, Rectangle rect)
        {
            Type = type;
            Rectangle = rect;
        }

    }

    class Board
    {
        static int[][] WinCombinations = new int[][] {
            new int[] { 0, 1, 2 },
            new int[] { 3, 4, 5 },
            new int[] { 6, 7, 8 },
            new int[] { 0, 3, 6 },
            new int[] { 1, 4, 7 },
            new int[] { 2, 5, 8 },
            new int[] { 0, 4, 8 },
            new int[] { 2, 4, 6 }
        };

        int PosX;
        int PosY;

        int SelectedIndex = -1;

        public Cell[] Array { get; }
        public int TurnCount { get; set; } = 0;
        private SpriteFont _font;
        private Texture2D _XTexture;
        private Texture2D _OTexture;

        public CellType Current { get; set; } = CellType.X;
        public GameState GameState = GameState.InProgress;


        public Board(int startx, int starty, SpriteFont font, Texture2D xtexture, Texture2D otexture)
        {
            PosX = startx;
            PosY = starty;

            _font = font;
            _XTexture = xtexture;
            _OTexture = otexture;

            Array = new Cell[9];


            for (int i = 0; i < 9; i++)
            {
                int x = i % 3;
                int y = i / 3;
                Rectangle rect = new Rectangle(startx + x * (100 + 3), starty + y * (100 + 3), 100, 100);

                Array[i] = new Cell(CellType.EMPTY, rect);
            }
        }

        public void MakeMove(int i)
        {
            Array[i].Type = Current;

            if (Current == CellType.X)
            {
                Current = CellType.O;
            }
            else
            {
                Current = CellType.X;
            }

            TurnCount++;

            CheckForWin();
        }

        public CellType GetWinner()
        {
            if (!CheckForWin())
            {
                return CellType.EMPTY;
            }

            if (Current == CellType.O)
            {
                return CellType.X;
            }
            else
            {
                return CellType.O;
            }
        }

        public bool CheckForWin()
        {
            foreach (int[] combination in WinCombinations)
            {
                int a = combination[0];
                int b = combination[1];
                int c = combination[2];

                if (Array[a].Type != CellType.EMPTY && Array[a].Type == Array[b].Type && Array[b].Type == Array[c].Type)
                {
                    switch (Array[a].Type)
                    {
                        case CellType.X:
                            GameState = GameState.XWin;
                            break;
                        case CellType.O:
                            GameState = GameState.OWin;
                            break;
                    }

                    return true;
                }
            }

            foreach (Cell cell in Array)
            {
                if (cell.Type == CellType.EMPTY)
                {
                    return false;
                }
            }

            GameState = GameState.Draw;
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            texture.SetData(new[] { Color.White });

            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(texture, new Rectangle(PosX + i * (100 + 3), PosY, 3, 3 * (100 + 3)), Color.Black);
                spriteBatch.Draw(texture, new Rectangle(PosX + 3, PosY + i * (100 + 3), 3 * (100 + 3), 3), Color.Black);
            }

            for (int i = 0; i < Array.Length; i++)
            {
                Cell cell = Array[i];

                int x = i % 3;
                int y = i / 3;

                switch (cell.Type)
                {
                    case CellType.X:
                        spriteBatch.Draw(_XTexture, cell.Rectangle, Color.White);
                        break;
                    case CellType.O:
                        spriteBatch.Draw(_OTexture, cell.Rectangle, Color.White);
                        break;
                    case CellType.EMPTY:
                        break;
                }
            }

            spriteBatch.End();
        }

        public void Update(float deltaTime)
        {
            if (GameState != GameState.InProgress)
            {
                return;
            }
            
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < Array.Length; i++)
                {
                    Cell cell = Array[i];

                    if (cell.Type == CellType.EMPTY && cell.Rectangle.Contains(Mouse.GetState().Position))
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }            

            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                if (SelectedIndex >= 0 && Array[SelectedIndex].Rectangle.Contains(Mouse.GetState().Position))
                {
                    MakeMove(SelectedIndex);
                }
                
                SelectedIndex = -1;
            }
        }
    }
}
