using System.Windows;

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
}
