using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Maze
{
    public class CharacterFactory: ICharacterFactory
    {
// Создание и инициализация персонажа игрока
        public ICharacter CreatePlayerCharacter(LevelForm parent)
        {
            return new PlayerCharacter(parent);
        }


// Создание и инициализация персонажей, управляемых компьютером
        public List<ICharacter> CreateComputerCharacters(LevelForm parent, int count)
        {
            List<ICharacter> computerCharacters = new List<ICharacter>();

            for (int i = 0; i < count; i++)
            {
                
                computerCharacters.Add(new PCCharacter(parent));
            }
            return computerCharacters;
        }
    }
}
