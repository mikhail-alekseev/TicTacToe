using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using TicTacToe.FSM;
using TicTacToe.UI;

namespace TicTacToe.States
{
    class Menu : BaseState
    {
        private TicTacToeGame Game;
        private List<IButton> _buttons;
        RadioMenu menu;
        private SpriteFont Font;
        private SpriteFont BigFont;
        private SpriteBatch _spriteBatch;
        private Texture2D _logo;

        public Menu(StateMachine stateMachine) : base(stateMachine)
        {
            Game = (TicTacToeGame)StateMachine.Game;

            _logo = StateMachine.Game.Content.Load<Texture2D>("logo");
            Font = StateMachine.Game.Content.Load<SpriteFont>("Arial");
            BigFont = StateMachine.Game.Content.Load<SpriteFont>("ArialBig");
            _spriteBatch = new SpriteBatch(this.StateMachine.Game.GraphicsDevice);

            

            _buttons = new List<IButton>
            {
                Game.UIManager.GetTextButton("NewGame", Game.Loc.GetString("NewGame"), new Action(NewGame), new Rectangle(400, 350, 200, 100), Color.Black),
                Game.UIManager.GetTextButton("Stats", Game.Loc.GetString("Stats"), new Action(ShowStats), new Rectangle(400, 455, 200, 100), Color.Black),
                Game.UIManager.GetTextButton("QuitGame", Game.Loc.GetString("QuitGame"), new Action(QuitGame), new Rectangle(400, 560, 200, 100), Color.Black)
            };

            string[] languages = new string[] { "russian", "english", "german" };

            menu = Game.UIManager.GetRadioMenu(
                languages, new string[] { "Русский", "English", "Deutsch" }, Array.IndexOf(languages, Game.Loc.Language), new Action(ChangeLanguage),
                new Rectangle(450, 220, 300, 50)
                );
        }

        private void ChangeLanguage()
        {
            Game.Loc.Language = menu.GetCurrentSelection();

            foreach (IButton button in _buttons)
            {
                TextButton b = (TextButton)button;
                b.Text = Game.Loc.GetString(b.Id);
            }
        }

        public override void Draw()
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_logo, Vector2.Zero, Color.White);

            _spriteBatch.DrawString(BigFont, Game.Loc.GetString("GameName"), new Vector2(450, 70), Color.Black);

            foreach (IButton button in _buttons)
            {
                button.Draw(_spriteBatch);
            }

            menu.Draw(_spriteBatch);
            _spriteBatch.End();
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            SaveLanguageSettings();
        }

        public void SaveLanguageSettings()
        {
            File.WriteAllText("settings", Game.Loc.Language);
        }

        public override void Update(float deltaTime)
        {
            foreach (IButton button in _buttons)
            {
                button.Update(deltaTime);
            }

            menu.Update(deltaTime);
        }

        public void NewGame()
        {
            StateMachine.Change("GAME");
        }

        public void ShowStats()
        {
            StateMachine.Change("STATS");
        }

        public void QuitGame()
        {
            Game.ExitGame();
        }
    }
}
