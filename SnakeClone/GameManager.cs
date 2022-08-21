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

namespace SnakeClone { 
	public class GameManager
	{
        public const int tile = 20;
        const int SnakeStartSpeed = 200;
        const int foodAmount = 5;
        private System.Windows.Threading.DispatcherTimer gameTickTimer = new System.Windows.Threading.DispatcherTimer();
        public static double tileSize;
        public Snake snake;
        Canvas GameBoard;

        public GameManager(Canvas gameBoard)
        {
            tileSize = gameBoard.Width / tile;
            GameBoard = gameBoard;
        }

        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            if (snake.CollisionCheck()) { MessageBox.Show(snake.snake[0].position.ToString()); return; }
            snake.CollisionWithFood(); 
            snake.Move(GameBoard);
            GenerateFood();
        }

        public void StartGame()
        {
            gameTickTimer.Tick += GameTickTimer_Tick;
            

            gameTickTimer.Interval = TimeSpan.FromMilliseconds(SnakeStartSpeed);
            snake = new Snake(3, 10, 5);
            snake.DrawSnake(GameBoard);

            //husk at slette igen
            GenerateFood();

            gameTickTimer.IsEnabled = true;
        }

        public void KeyEvent(KeyEventArgs e)
        {
            switch (e.Key)
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

        public void DrawBoard()
        {
            IEnumerable<int> values = Enumerable.Range(0, GameManager.tile);
            foreach (int y in values)
            {
                foreach (int x in values)
                {
                    Rectangle rect = new Rectangle
                    {
                        Width = GameManager.tileSize,
                        Height = GameManager.tileSize,
                        Fill = (x + y) % 2 == 0 ? Brushes.White : Brushes.Black
                    };
                    GameBoard.Children.Add(rect);
                    Canvas.SetTop(rect, y * GameManager.tileSize);
                    Canvas.SetLeft(rect, x * GameManager.tileSize);
                }
            }
        }
        private void GenerateFood()
        {
            while (Food.foods.Count < foodAmount)
            {
                if (snake.snake.Count+Food.foods.Count > (tile * tile)) { break; }
                var food = new Food(GameBoard) { position = Food.GenerateFood(snake) };
            }
        }
    }
}
