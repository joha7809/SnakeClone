using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Collections.Generic;
namespace SnakeClone
{
    public class Food
    {
        public int id;
        public Point position { get; set; }
        public UIElement uiElement;
        public static List<Food> foods = new List<Food>();
        static Canvas GameBoard;
        static private Random random = new Random();

        public Food(Canvas GameBoard_)
        {
            uiElement = new Rectangle
            {
                Width = GameManager.tileSize,   
                Height = GameManager.tileSize,
                Fill = new SolidColorBrush(Colors.Red),   
            };    
            GameBoard = GameBoard_;
            GameBoard.Children.Add(uiElement);
            foods.Add(this);
            RenderFood();
        }

        public static void RenderFood()
        {
            foreach (Food food in foods)
            {
                Canvas.SetLeft(food.uiElement, food.position.X * GameManager.tileSize);
                Canvas.SetTop(food.uiElement, food.position.Y * GameManager.tileSize);
            }
        }
        public static void DeleteFood(int index)
        {
            GameBoard.Children.Remove(foods[index].uiElement);
            foods.RemoveAt(index);
        }
        public static Point GenerateFood(Snake snake)
        {
            Point point = new Point(
                random.Next(0, GameManager.tile),
                random.Next(0, GameManager.tile)
            );

            foreach (Food food in foods)
            {
                if (food.position.Equals(point))
                {
                    return GenerateFood(snake);
                }
            }

            foreach (SnakePart snakePart in snake.snake)
            {
                if (snakePart.position.Equals(point))
                {
                    return GenerateFood(snake);
                }
            }

            return point;
        }
    }
}
