using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.FSM;
using TicTacToe.UI;

namespace TicTacToe.States
{
    class GameInProgress : BaseState
    {
        private SpriteBatch _spriteBatch;
        SpriteFont Font;
        SpriteFont BigFont;
        Board Board;
        TicTacToeGame Game;
        CellType Player;
        List<IButton> Buttons;

        bool ResultRecorded;

        public GameInProgress(StateMachine stateMachine) : base(stateMachine)
        {
            Game = (TicTacToeGame)StateMachine.Game;
            _spriteBatch = new SpriteBatch(this.StateMachine.Game.GraphicsDevice);
            Font = StateMachine.Game.Content.Load<SpriteFont>("Arial");
            BigFont = StateMachine.Game.Content.Load<SpriteFont>("ArialBig");       
        }

        public void Save()
        {
            switch (Board.GameState)
            {
                case GameState.XWin:
                case GameState.OWin:
                    if (Board.GetWinner() == Player)
                    {
                        Game.Save.Wins++;
                    }
                    else
                    {
                        Game.Save.Losses++;
                    }
                    break;
                case GameState.Draw:
                    Game.Save.Draws++;
                    break;
            }


            ResultRecorded = true;
        }

        public override void Draw()
        {
            Board.Draw(_spriteBatch);

            _spriteBatch.Begin();
            string text;
            switch (Board.GameState)
            {
                case GameState.XWin:
                case GameState.OWin:
                    if (Board.GetWinner() == Player)
                    {
                        text = "YouWin";
                    }
                    else
                    {
                        text = "YouLose";
                    }
                    break;
                case GameState.Draw:
                    text = "Draw";
                    break;
                case GameState.InProgress:
                    text = "YourTurn";
                    break;
                default:
                    text = "";
                    break;
            }

            _spriteBatch.DrawString(BigFont, Game.Loc.GetString(text), new Vector2(450, 500), Color.White);

            foreach (IButton btn in Buttons)
            {
                btn.Draw(_spriteBatch);
            }
            _spriteBatch.End();
        }

        public override void Enter()
        {
            Buttons = new List<IButton>()
            {
                Game.UIManager.GetTextButton("Retry", Game.Loc.GetString("Retry"), () => Restart(), new Rectangle(600, 150, 200, 100), Color.Black),
                Game.UIManager.GetTextButton("MainMenu", Game.Loc.GetString("MainMenu"), () => Game.StateMachine.Change("MENU"), new Rectangle(600, 255, 200, 100), Color.Black),
                Game.UIManager.GetTextButton("QuitGame", Game.Loc.GetString("QuitGame"), () => Game.ExitGame(), new Rectangle(600, 360, 200, 100), Color.Black),
            };

            Restart();
        }

        public override void Exit()
        {
            
        }

        public override void Update(float deltaTime)
        {
            if (Board.GameState != GameState.InProgress && !ResultRecorded)
            {
                Save();
            }

            foreach (IButton btn in Buttons)
            {
                btn.Update(deltaTime);
            }

            if (Board.Current == Player)
            {
                Board.Update(deltaTime);
            }

            else if (Board.GameState == GameState.InProgress)
            {
                Board.MakeMove(ChooseMove());
            }
        }

        public void Restart()
        {
            Texture2D X = StateMachine.Game.Content.Load<Texture2D>("x");
            Texture2D O = StateMachine.Game.Content.Load<Texture2D>("o");

            Board = new Board(200, 150, Font, X, O);

            Player = CellType.X;

            ResultRecorded = false;
        }

        public int ChooseMove()
        {
            List<int> free = new List<int>();

            for (int i = 0; i < Board.Array.Length; i++)
            {
                if (Board.Array[i].Type == CellType.EMPTY)
                {
                    free.Add(i);
                }
            }

            Random rnd = new Random();
            return free[rnd.Next(free.Count)];
        }
    }
}
