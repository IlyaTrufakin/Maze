using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    internal class PlayerCharacter : ICharacter
    {

        public ushort PosX { get; set; }
        public ushort PosY { get; set; }
        public int medalCount { get; set; }
        public int health { get; set; }
        public LevelForm Parent { get; set; }

        public PlayerCharacter(LevelForm parent)
        {
            PosX = 0;
            PosY = 2;
            Parent = parent;
            health = 100;
            this.Show();
        }


        public void Clear()
        {
            Parent.Controls["pic" + PosY + "_" + PosX].BackgroundImage =
                Parent.maze.cells[PosY, PosX].Texture =
                Cell.Images[(int)(Parent.maze.cells[PosY, PosX].Type = CellType.HALL)];
        }


        public void Show()
        {
            Parent.Controls["pic" + PosY + "_" + PosX].BackgroundImage =
                Parent.maze.cells[PosY, PosX].Texture =
                Cell.Images[(int)(Parent.maze.cells[PosY, PosX].Type = CellType.HERO)];
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



        public int Interaction(CellType cellType)
        {
            if (cellType == CellType.MEDAL)
            {
                this.medalCount++;
                if (this.medalCount >= Parent.maze.medalCount)
                {
                    return 1;
                }

            }
            else if (cellType == CellType.ENEMY)
            {

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
            return this.medalCount;
        }
    }
}
