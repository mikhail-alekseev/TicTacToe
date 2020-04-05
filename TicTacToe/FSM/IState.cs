using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.FSM
{
    public interface IState
    {
        void Enter();
        void Update(float deltaTime);
        void Draw();
        void Exit();        
    }
}
