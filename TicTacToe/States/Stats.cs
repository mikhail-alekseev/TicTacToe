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
    class Stats : BaseState
    {
        private TicTacToeGame Game;
        private List<IButton> Buttons;
        private SpriteFont Font;
        private SpriteFont BigFont;
        private SpriteBatch SpriteBatch;
        private Texture2D Logo;

        public Stats(StateMachine stateMachine) : base(stateMachine)
        {
            Game = (TicTacToeGame)StateMachine.Game;

            Logo = StateMachine.Game.Content.Load<Texture2D>("logo");
            Font = StateMachine.Game.Content.Load<SpriteFont>("Arial");
            BigFont = StateMachine.Game.Content.Load<SpriteFont>("ArialBig");
            SpriteBatch = new SpriteBatch(this.StateMachine.Game.GraphicsDevice);

        }

        public override void Draw()
        {
            SpriteBatch.Begin();
            SpriteBatch.DrawString(BigFont, Game.Loc.GetString("Stats"), new Vector2(300, 20), Color.Black);

            SpriteBatch.DrawString(BigFont, Game.Loc.GetString("Wins") + ": " + Game.Save.Wins.ToString(), new Vector2(300, 150), Color.Black);
            SpriteBatch.DrawString(BigFont, Game.Loc.GetString("Losses") + ": " + Game.Save.Losses.ToString(), new Vector2(300, 200), Color.Black);

            string winRatio = Math.Round(Game.Save.WinRatio * 100, 2).ToString() + "%";

            SpriteBatch.DrawString(BigFont, Game.Loc.GetString("WinRatio") + ": " + winRatio, new Vector2(300, 250), Color.Black);

            foreach (IButton btn in Buttons)
            {
                btn.Draw(SpriteBatch);
            }
            SpriteBatch.End();
        }

        public override void Enter()
        {
            Buttons = new List<IButton>
            {
                Game.UIManager.GetTextButton("MainMenu", Game.Loc.GetString("MainMenu"), () => Game.StateMachine.Change("MENU"), new Rectangle(400, 555, 200, 100), Color.Black),
                Game.UIManager.GetTextButton("QuitGame", Game.Loc.GetString("QuitGame"), () => Game.ExitGame(), new Rectangle(400, 660, 200, 100), Color.Black)
            };
        }

        public override void Exit()
        {
            
        }

        public override void Update(float deltaTime)
        {
            foreach (IButton button in Buttons)
            {
                button.Update(deltaTime);
            }
        }
    }
}
