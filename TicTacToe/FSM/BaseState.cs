using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.FSM
{
    public abstract class BaseState : IState
    {
        public StateMachine StateMachine { get; }

        public BaseState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract void Draw();

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update(float deltaTime);
    }
}
