using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    public class PCCharacter : ICharacter
    {

        // позиция главного персонажа
        public ushort PosX { get; set; }
        public ushort PosY { get; set; }
        public int health { get; set; }
        public LevelForm Parent { get; set; }

        public PCCharacter(LevelForm parent)
        {
            this.Parent = parent;
            do
            {
                PosX = (ushort)parent.random.Next(Configuration.Columns);
                PosY = (ushort)parent.random.Next(Configuration.Rows);
            } while (parent.maze.cells[PosY, PosX].Type == CellType.WALL);
            this.Show();
        }

        public void Show()
        {
            Parent.Controls["pic" + PosY + "_" + PosX].BackgroundImage =
                Parent.maze.cells[PosY, PosX].Texture =
                Cell.Images[(int)(Parent.maze.cells[PosY, PosX].Type = CellType.ENEMY)];
        }

        public void Clear()
        {
            Parent.Controls["pic" + PosY + "_" + PosX].BackgroundImage =
                Parent.maze.cells[PosY, PosX].Texture =
                Cell.Images[(int)(Parent.maze.cells[PosY, PosX].Type = CellType.HALL)];
        }

        public void MoveRight()
        {
            this.Clear();
            PosX++;
            this.Show();

        }

        public void MoveLeft()
        {
            this.Clear();
            PosX--;
            this.Show();
        }

        public void MoveUp()
        {
            this.Clear();
            PosY--;
            this.Show();
        }

        public void MoveDown()
        {
            this.Clear();
            PosY++;
            this.Show();
        }






        // Логика взаимодействия компьютерного персонажа
        public int Interaction(CellType cellType)
        {
            if (cellType == CellType.HERO)
            {
                return -1;
            }
            return 0;
        }

        public int GetXPosition()
        {
            return PosX;
        }

        public int GetYPosition()
        {
            return PosY;
        }

        public int GetHealth()
        {
            return this.health;
        }

        public int GetMedals()
        {
            return 0;
        }
    }
}
