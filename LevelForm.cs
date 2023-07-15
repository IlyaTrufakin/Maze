namespace Maze
{
    public partial class LevelForm : Form
    {
        public Maze maze; // ссылка на логику всего происход€щего в лабиринте
        //public Character Hero;
        public ICharacterFactory characterFactory;
        public ICharacter MyHero;
        public List<ICharacter> computerCharacters;
        public StatusStrip statusStrip1;
        public ToolStripStatusLabel toolStripStatusLabel1, toolStripStatusLabel2;
        public Random random = new Random();

        public LevelForm()
        {
            InitializeComponent();
            InitializeMyComponent();
            StartGameProcess();
        }

        private void InitializeMyComponent()
        {
            SuspendLayout();
            StartPosition = FormStartPosition.CenterScreen;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            FormSettings();
            KeyDown += KeyDownHandler;
            ResumeLayout(false);
            PerformLayout();
        }

        public void FormSettings()
        {
            // размеры клиентской области формы
            int clientWidth = Configuration.Columns * Configuration.PictureSide;
            int clientHeight = Configuration.Rows * Configuration.PictureSide;
            int formWidth = clientWidth;
            int formHeight = clientHeight + Configuration.StatusStripHeight; // ”читываем высоту StatusStrip

            Text = Configuration.Title;
            BackColor = Configuration.Background;
            ClientSize = new Size(formWidth, formHeight);
            Name = "LevelForm";
            Text = "Form1";
            // 
            // statusStrip1
            // 
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            statusStrip1.SuspendLayout();
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2 });
            statusStrip1.Location = new Point(0, 255);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(clientWidth, Configuration.StatusStripHeight);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabel1.BorderStyle = Border3DStyle.Etched;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 17);
            toolStripStatusLabel1.Text = "";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel1.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabel1.BorderStyle = Border3DStyle.Etched;
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(118, 17);
            toolStripStatusLabel2.Text = "";

            Controls.Add(statusStrip1);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
        }


        public void StartGameProcess()
        {
            maze = new Maze(this, Configuration.Rows, Configuration.Columns);
            maze.Generate();
            characterFactory = new CharacterFactory();
            MyHero = characterFactory.CreatePlayerCharacter(this);
            computerCharacters = characterFactory.CreateComputerCharacters(this, 4);
            UpdateStatistics();
        }


        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            CellType cellType;
            if (e.KeyCode == Keys.Right)
            {

                // проверка на то, свободна ли €чейка справа
                cellType = maze.cells[MyHero.GetYPosition(), MyHero.GetXPosition() + 1].Type;
                if (cellType != CellType.WALL)
                {
                    MyHero.MoveRight();
                    MyHero.Interaction(cellType);
                }
            }

            else if (e.KeyCode == Keys.Left && MyHero.GetXPosition() != 0)
            {
                // проверка на то, свободна ли €чейка слева
                cellType = maze.cells[MyHero.GetYPosition(), MyHero.GetXPosition() - 1].Type;
                if (cellType != CellType.WALL)
                {
                    MyHero.MoveLeft();
                    MyHero.Interaction(cellType);
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                // проверка на то, свободна ли €чейка выше
                cellType = maze.cells[MyHero.GetYPosition() - 1, MyHero.GetXPosition()].Type;
                if (cellType != CellType.WALL)
                {
                    MyHero.MoveUp();
                    MyHero.Interaction(cellType);
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                // проверка на то, свободна ли €чейка ниже
                cellType = maze.cells[MyHero.GetYPosition() + 1, MyHero.GetXPosition()].Type;
                if (cellType != CellType.WALL)
                {
                    MyHero.MoveDown();
                    MyHero.Interaction(cellType);
                }
            }
            if (MyHero.GetYPosition() == Configuration.Columns - 1 && MyHero.GetXPosition() == Configuration.Rows - 2)
            {

            }
            UpdateStatistics();
        }

        public void UpdateStatistics()
        {
            toolStripStatusLabel1.Text = $"«доровье: {MyHero.GetHealth().ToString()}";
            toolStripStatusLabel2.Text = $"ћедали: {MyHero.GetMedals().ToString()}";
        }


    }
}