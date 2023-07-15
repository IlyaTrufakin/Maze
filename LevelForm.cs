using System;

namespace Maze
{
    public partial class LevelForm : Form
    {
        public Maze maze; // ссылка на логику всего происходящего в лабиринте
        //public Character Hero;
        public ICharacterFactory characterFactory;
        public ICharacter MyHero;
        public List<ICharacter> computerCharacters;
        public StatusStrip statusStrip1;
        public ToolStripStatusLabel toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3, toolStripStatusLabel4;
        public Random random = new Random();
        public int playerState = 0;
        public int madeSteps = 0;
        public string Message;
        private System.Windows.Forms.Timer clickCountTimer = new System.Windows.Forms.Timer();
        DateTime startTime = DateTime.Now;
        private System.Windows.Forms.Timer CountTimer = new System.Windows.Forms.Timer();
        private long elapsedSeconds = 0;


        public LevelForm()
        {
            InitializeComponent();
            InitializeMyComponent();
            StartGameProcess();
            InitializeTimers();
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

        private void InitializeTimers()
        {
            clickCountTimer.Interval = 500; // Интервал таймера 0,5 секунда
            clickCountTimer.Tick += ClickCountTimerTick;
            clickCountTimer.Start();
            CountTimer.Interval = 1000; // Интервал таймера 0,5 секунда
            CountTimer.Tick += CountTimerTick;
            CountTimer.Start();
        }


        public void FormSettings()
        {
            // размеры клиентской области формы
            int clientWidth = Configuration.Columns * Configuration.PictureSide;
            int clientHeight = Configuration.Rows * Configuration.PictureSide;
            int formWidth = clientWidth;
            int formHeight = clientHeight + Configuration.StatusStripHeight; // Учитываем высоту StatusStrip

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
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            toolStripStatusLabel4 = new ToolStripStatusLabel();
            statusStrip1.SuspendLayout();
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3, toolStripStatusLabel4 });
            statusStrip1.Location = new Point(0, 255);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(clientWidth, Configuration.StatusStripHeight);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabel1.BackColor = SystemColors.ControlDark;
            toolStripStatusLabel1.BorderStyle = Border3DStyle.Etched;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 17);
            toolStripStatusLabel1.Text = "";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabel2.BackColor = SystemColors.ControlDark;
            toolStripStatusLabel2.BorderStyle = Border3DStyle.Etched;
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(118, 17);
            toolStripStatusLabel2.Text = "";
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabel3.BackColor = SystemColors.ControlDark;
            toolStripStatusLabel3.BorderStyle = Border3DStyle.Etched;
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new Size(118, 17);
            toolStripStatusLabel3.Text = "";
            // 
            // toolStripStatusLabel4
            // 
            toolStripStatusLabel4.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabel4.BackColor = SystemColors.ControlDark;
            toolStripStatusLabel4.BorderStyle = Border3DStyle.Etched;
            toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            toolStripStatusLabel4.Size = new Size(118, 17);
            toolStripStatusLabel4.Text = "";

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
            computerCharacters = characterFactory.CreateComputerCharacters(this, 5);
            UpdateStatistics();
        }


        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            CellType cellType;
            if (e.KeyCode == Keys.Right)
            {

                // проверка на то, свободна ли ячейка справа
                cellType = maze.cells[MyHero.GetYPosition(), MyHero.GetXPosition() + 1].Type;
                if (cellType != CellType.WALL)
                {
                    MyHero.MoveRight();
                    playerState = MyHero.Interaction(cellType);
                    madeSteps++;
                }
            }

            else if (e.KeyCode == Keys.Left && MyHero.GetXPosition() != 0)
            {
                // проверка на то, свободна ли ячейка слева
                cellType = maze.cells[MyHero.GetYPosition(), MyHero.GetXPosition() - 1].Type;
                if (cellType != CellType.WALL)
                {
                    MyHero.MoveLeft();
                    playerState = MyHero.Interaction(cellType);
                    madeSteps++;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                // проверка на то, свободна ли ячейка выше
                cellType = maze.cells[MyHero.GetYPosition() - 1, MyHero.GetXPosition()].Type;
                if (cellType != CellType.WALL)
                {
                    MyHero.MoveUp();
                    playerState = MyHero.Interaction(cellType);
                    madeSteps++;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                // проверка на то, свободна ли ячейка ниже
                cellType = maze.cells[MyHero.GetYPosition() + 1, MyHero.GetXPosition()].Type;
                if (cellType != CellType.WALL)
                {
                    MyHero.MoveDown();
                    playerState = MyHero.Interaction(cellType);
                    madeSteps++;
                }
            }
            if (MyHero.GetXPosition() == Configuration.Columns - 2 && MyHero.GetYPosition() == Configuration.Rows - 3)
            {
                playerState = 2;
            }

            UpdateStatistics();
        }




        private void ClickCountTimerTick(object? sender, EventArgs e)
        {
            CellType cellType;
            foreach (var PcCharacter in computerCharacters)
            {

                int way = random.Next(4);
                switch (way)
                {
                    case 0:
                        {
                            // проверка на то, свободна ли ячейка справа
                            cellType = maze.cells[PcCharacter.GetYPosition(), PcCharacter.GetXPosition() + 1].Type;
                            if (cellType != CellType.WALL)
                            {
                                PcCharacter.MoveRight();
                                PcCharacter.Interaction(cellType);
                            }
                            break;
                        }

                    case 1:
                        {
                            // проверка на то, свободна ли ячейка слева
                            cellType = maze.cells[PcCharacter.GetYPosition(), PcCharacter.GetXPosition() - 1].Type;
                            if (cellType != CellType.WALL)
                            {
                                PcCharacter.MoveLeft();
                                PcCharacter.Interaction(cellType);
                            }
                            break;
                        }

                    case 2:
                        {
                            // проверка на то, свободна ли ячейка выше
                            cellType = maze.cells[PcCharacter.GetYPosition() - 1, PcCharacter.GetXPosition()].Type;
                            if (cellType != CellType.WALL)
                            {
                                PcCharacter.MoveUp();
                                PcCharacter.Interaction(cellType);
                            }
                            break;
                        }


                    case 3:
                        {
                            // проверка на то, свободна ли ячейка ниже
                            cellType = maze.cells[PcCharacter.GetYPosition() + 1, PcCharacter.GetXPosition()].Type;
                            if (cellType != CellType.WALL)
                            {
                                PcCharacter.MoveDown();
                                PcCharacter.Interaction(cellType);
                            }
                            break;
                        }

                    default:
                        break;
                }


            }
        }

        private void CountTimerTick(object? sender, EventArgs e)
        {
            elapsedSeconds++;
            UpdateStatistics();
        }

        public void UpdateStatistics()
        {
            toolStripStatusLabel1.Text = $"Здоровье: {MyHero.GetHealth().ToString()}";
            toolStripStatusLabel2.Text = $"Медали: {MyHero.GetMedals().ToString()}";
            toolStripStatusLabel3.Text = $"Сделано шагов: {this.madeSteps}";
            TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedSeconds);
            string formattedTime = timeSpan.ToString(@"hh\:mm\:ss");
            toolStripStatusLabel4.Text = $"Прошло времени: {formattedTime}";

            switch (playerState)
            {
                case -1:
                    {
                        Message = "Конец Игры. Вы утратили все здоровье.";
                        break;
                    }

                case 1:
                    {
                        Message = "Конец Игры. Вы собрали все медали.";
                        break;
                    }

                case 2:
                    {
                        Message = "Конец Игры. Вы нашли выход.";
                        break;
                    }

                default:
                    break;
            }


            if (playerState != 0)
            {
                clickCountTimer.Stop();
                CountTimer.Stop();
                DialogResult result = MessageBox.Show(Message, "", MessageBoxButtons.OKCancel);
                // Проверить результат диалогового окна.
                if (result == DialogResult.OK)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Environment.Exit(0);
                }
            }

        }

    }
}