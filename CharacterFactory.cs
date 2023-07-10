using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    internal class CharacterFactory: ICharacterFactory
    {
        public ICharacter CreatePlayerCharacter()
        {
            // Создание и инициализация персонажа игрока
            return new PlayerCharacter();
        }

        public List<ICharacter> CreateComputerCharacters(int count)
        {
            List<ICharacter> computerCharacters = new List<ICharacter>();

            for (int i = 0; i < count; i++)
            {
                // Создание и инициализация персонажей, управляемых компьютером
                computerCharacters.Add(new PCCharacter());
            }

            return computerCharacters;
        }
    }
}
