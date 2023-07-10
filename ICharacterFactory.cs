using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    internal interface ICharacterFactory
    {
        ICharacter CreatePlayerCharacter();
        List<ICharacter> CreateComputerCharacters(int count);
    }
}
