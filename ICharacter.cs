using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    public interface ICharacter
    {
  
        public int Interaction(CellType cellType);
        public void Clear();
        public void MoveRight();

        public void MoveLeft();

        public void MoveUp();

        public void MoveDown();

        public void Show();

        public int GetXPosition();
        public int GetYPosition();
        public int GetHealth();
        public int GetMedals();
    }
}
