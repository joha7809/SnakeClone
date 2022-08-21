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
        GameManager gameManager;    
        public MainWindow()
        {
            InitializeComponent();
            gameManager = new GameManager(GameBoard);

        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            gameManager.KeyEvent(e);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            gameManager.DrawBoard();
            gameManager.StartGame();
                      
        }
    }
}