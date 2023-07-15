using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Maze
{
    public class Maze
    {
        public LevelForm Parent { get; set; } // ссылка на родительскую форму

        public Cell[,] cells;
        public static Random r = new Random();
        public int medalCount { get; set; }
        public Maze(LevelForm parent, ushort rows, ushort columns)
        {
            Parent = parent;
            cells = new Cell[rows, columns];
        }

        public void Generate()
        {
            for (ushort row = 0; row < Configuration.Rows; row++)
            {
                for (ushort col = 0; col < Configuration.Columns; col++)
                {
                    CellType cell = CellType.HALL;

                    // в 1 случае из 5 - ставим стену в текуще ячейке
                    if (r.Next(5) == 0)
                    {
                        cell = CellType.WALL;
                    }

                    // в 1 случае из 250 - кладём медаль
                    if (r.Next(250) == 0)
                    {
                        cell = CellType.MEDAL;
                    }

                    // стены по периметру лабиринта
                    if (row == 0 || col == 0 || row == Configuration.Rows - 1 || col == Configuration.Columns - 1)
                    {
                        cell = CellType.WALL;
                    }

                     // есть выход, и соседняя ячейка справа всегда свободна
                    if (col == 1 && row == 2 ||  col == Configuration.Columns - 1 &&  row == Configuration.Rows - 2)
                    {
                        cell = CellType.HALL;
                    }

                    cells[row, col] = new Cell(cell);
                    if (cell == CellType.MEDAL) 
                    { 
                        medalCount++; 
                    }

                    var picture = new PictureBox();
                    picture.Name = "pic" + row + "_" + col;
                    picture.Width = Configuration.PictureSide;
                    picture.Height = Configuration.PictureSide;
                    picture.Location = new Point(
                        col * Configuration.PictureSide,
                        row * Configuration.PictureSide);
                    picture.BackgroundImage = cells[row, col].Texture;
                    picture.Visible = true;
                    Parent.Controls.Add(picture);
                }
            }
        }


        // Проверка, что позиция внутри границ лабиринта и не является стеной
        // true, если позиция доступна, и false в противном случае
        public bool IsPositionValid(int row, int col)
        {
            if (row >= 0 && row < Configuration.Rows && col > 0 && col < Configuration.Columns)
            {
                if (cells[row, col].Type == CellType.HALL)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
