using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.FSM
{
    public class StateMachine
    {
        private readonly Dictionary<string, IState> _states = new Dictionary<string, IState>();
        private IState _currentState;
        public Game Game { get; }

        public StateMachine(Game game)
        {
            Game = game;
        }

        public void Add(string stateName, IState state)
        {
            _states[stateName] = state;
        }

        public void Change(string stateName)
        {
            if (!_states.ContainsKey(stateName))
            {
                throw new KeyNotFoundException($"{stateName} is not in states");
            }

            _currentState?.Exit();
            _currentState = _states[stateName];
            _currentState.Enter();
        }

        public void Draw()
        {
            _currentState?.Draw();
        }

        public void Update(float deltaTime)
        {
            _currentState?.Update(deltaTime);
        }
    }
}
