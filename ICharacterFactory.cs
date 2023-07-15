using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    public interface ICharacterFactory
    {
        ICharacter CreatePlayerCharacter(LevelForm parent);
        List<ICharacter> CreateComputerCharacters(LevelForm parent, int count);
    }
}
