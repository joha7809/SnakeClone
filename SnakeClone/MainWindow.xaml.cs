using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnakeClone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int tile = 20;
        const int SnakeStartSpeed = 200;
        private System.Windows.Threading.DispatcherTimer gameTickTimer = new System.Windows.Threading.DispatcherTimer();
        public static double tileSize;
        Snake snake;

        public MainWindow()
        {
            InitializeComponent();
            gameTickTimer.Tick += GameTickTimer_Tick;
            tileSize = GameBoard.Width / tile;
        }

        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            if (snake.CollisionCheck()) { MessageBox.Show(snake.snake[0].position.ToString()); return; }
            snake.Move(GameBoard);
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.W:
                    if (snake.directions != Snake.Directions.Up && snake.directions != Snake.Directions.Down)
                        snake.directions = Snake.Directions.Up;
                    break;

                case Key.S:
                    if (snake.directions != Snake.Directions.Down && snake.directions != Snake.Directions.Up)
                        snake.directions = Snake.Directions.Down;
                    break;
                case Key.A:
                    if (snake.directions != Snake.Directions.Left && snake.directions != Snake.Directions.Right)
                        snake.directions = Snake.Directions.Left;
                    break;
                case Key.D:
                    if (snake.directions != Snake.Directions.Right && snake.directions != Snake.Directions.Left)
                        snake.directions = Snake.Directions.Right;
                    break;
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawBoard();
            StartGame();
        }

        private void DrawBoard()
        {
            IEnumerable<int> values = Enumerable.Range(0, tile);
            foreach (int y in values)
            {
                foreach (int x in values)
                {
                    Rectangle rect = new Rectangle
                    {
                        Width = tileSize,
                        Height = tileSize,
                        Fill = (x + y) % 2 == 0 ? Brushes.White : Brushes.Black
                    };
                    GameBoard.Children.Add(rect);
                    Canvas.SetTop(rect, y * tileSize);
                    Canvas.SetLeft(rect, x * tileSize);
                }
            }
        }

        private void StartGame()
        {
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(SnakeStartSpeed);
            snake = new Snake(3, 10, 5);
            snake.DrawSnake(GameBoard);

            gameTickTimer.IsEnabled = true;
        }
    }
}

