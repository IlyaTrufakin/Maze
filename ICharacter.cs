using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    internal interface ICharacter
    {
        void Move(Direction direction);
        void Attack();
    }
}
