using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace SnakeClone
{
    public class SnakePart
    {
        public Point position;
        public UIElement uiElement;
        public bool isHead;
        

        public SnakePart(Point position, UIElement uiElement, bool isHead)
        {
            this.position = position;
            this.uiElement = uiElement;
            this.isHead = isHead;
        }
    }

    public class Snake
    {
        private static SolidColorBrush snakeBodyColor = Brushes.YellowGreen;
        private static SolidColorBrush snakeHeadColor = Brushes.Green;

        public List<SnakePart> snake = new List<SnakePart>();
        public int length;

        public enum Directions { Left, Right, Up, Down };
        public Directions directions = Directions.Right;

        public Snake(int length, int x, int y)
        {
            this.length = length;

            for (int i = 0; i < length; i++)
            {
                if (i == 0)
                {
                    this.snake.Add(new SnakePart(new Point(x, y), null, true));
                }
                else
                {
                    this.snake.Add(new SnakePart(new Point(x-i, y), null, false));
                }
            }

        }

        public bool CollisionCheck()
        {
            if (snake[0].position.X > MainWindow.tile-1 || snake[0].position.X < 0 || snake[0].position.Y > MainWindow.tile-1 || snake[0].position.Y < 0)
            {
                return true;
            }
            return false;
        }

        public void DrawSnake(Canvas GameBoard)
        {
            foreach (SnakePart snakePart in snake)
            {
                if (snakePart.uiElement == null)
                {
                    snakePart.uiElement = new Rectangle
                    {
                        Width = MainWindow.tileSize,
                        Height = MainWindow.tileSize,
                        Fill = snakePart.isHead ? snakeBodyColor : snakeHeadColor
                    };
                    
                    GameBoard.Children.Add(snakePart.uiElement);
                    Canvas.SetLeft(snakePart.uiElement, snakePart.position.X * MainWindow.tileSize);
                    Canvas.SetTop(snakePart.uiElement, snakePart.position.Y * MainWindow.tileSize);
                }
            }
        }

        public void Move(Canvas GameBoard)
        {
            int nextX = 0;
            int nextY = 0;

            

            switch (directions)
            {
                case Directions.Left:
                    nextX = -1;
                    break;
                case Directions.Right:
                    nextX = 1;
                    break;
                case Directions.Up:
                    nextY = -1;
                    break;
                case Directions.Down:
                    nextY = 1;
                    break;
            }

            var snakeCopy = new List<SnakePart>();
            foreach (SnakePart snakePart in snake)
            {
                snakeCopy.Add(snakePart);
            }
            snakeCopy.RemoveAt(snakeCopy.Count - 1);
            GameBoard.Children.Remove(snake[snake.Count - 1].uiElement);
            GameBoard.Children.Remove(snake[0].uiElement);
            snakeCopy[0].isHead = false;
            snakeCopy[0].uiElement = null;
            
            

            snake.Clear();

            snake.Add(new SnakePart(
                new Point(snakeCopy[0].position.X + nextX, snakeCopy[0].position.Y + nextY), 
                null,
                true
            ));

            
            foreach (SnakePart snakePartCopy in snakeCopy)
            {
                snake.Add(snakePartCopy);
            }

            foreach (SnakePart snakePart in snake)
            {

                if (snakePart.uiElement == null)
                { //Create new Rectangle to display the snakepart
                    snakePart.uiElement = new Rectangle
                    {
                        Width = MainWindow.tileSize,
                        Height = MainWindow.tileSize,
                        Fill = snakePart.isHead ? snakeBodyColor : snakeHeadColor
                    };

                    GameBoard.Children.Add(snakePart.uiElement);
                }

                //sets the position of the snakepart
                Canvas.SetLeft(snakePart.uiElement, (snakePart.position.X) * MainWindow.tileSize);
                Canvas.SetTop(snakePart.uiElement, (snakePart.position.Y) * MainWindow.tileSize);
            }
        }
    }
}