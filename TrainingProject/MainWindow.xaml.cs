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

namespace TrainingProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int gameSideSize = 3;

        public MainWindow()
        {
            InitializeComponent();
            Buttons_are_arranged += Ending;
        }


        private void RandomizeAndEnableButtons(object sender, RoutedEventArgs e)
        {
            RandomizeButtons();
            EnableAllNumberButtons();
        }

        private void RandomizeButtons()
        {
            Random rand = new Random();
            List<Point> allSlots = new List<Point>() 
            { 
                new Point(0,0),
                new Point(0,1),
                new Point(0,2),
                new Point(1,0),
                new Point(1,1),
                new Point(1,2),
                new Point(2,0),
                new Point(2,1),
            };

            foreach (Button button in MainGrid.Children)
            {
                int currentRandomPointIndex = rand.Next(0, allSlots.Count);
                Point currentPoint = allSlots[currentRandomPointIndex];
                allSlots.Remove(currentPoint);
                button.SetValue(Grid.RowProperty, (int)currentPoint.X);
                button.SetValue(Grid.ColumnProperty, (int)currentPoint.Y);
            }

        }

        private void EnableAllNumberButtons()
        {
            foreach (Button button in MainGrid.Children)
            {
                button.IsEnabled = true;
            }
        }

        private void On_every_Button_Click(object sender, RoutedEventArgs e)
        {
            bool[,] allSlots = new bool[gameSideSize, gameSideSize];

            UIElementCollection allButtons = MainGrid.Children;
            foreach (Button button in allButtons)
            {
                int currentRow = (int)button.GetValue(Grid.RowProperty);
                int currentCol = (int)button.GetValue(Grid.ColumnProperty);

                allSlots[currentRow, currentCol] = true;
            }

            int emptySpaceRow = -1;
            int emptySpaceCol = -1;

            for (int i = 0; i < allSlots.GetLength(0); i++)
            {
                for (int u = 0; u < allSlots.GetLength(1); u++)
                {
                    if (allSlots[i, u] == false)
                    {
                        emptySpaceRow = i;
                        emptySpaceCol = u;
                    }
                }
            }

            int senderRow = (int)(sender as Button).GetValue(Grid.RowProperty);
            int senderCol = (int)(sender as Button).GetValue(Grid.ColumnProperty);

            bool emptySlotIsAdjacent = false;
            if ((emptySpaceCol == senderCol - 1 && emptySpaceRow == senderRow) ||
                (emptySpaceCol == senderCol + 1 && emptySpaceRow == senderRow) ||
                (emptySpaceCol == senderCol && emptySpaceRow == senderRow + 1) ||
                (emptySpaceCol == senderCol && emptySpaceRow == senderRow - 1))
            {
                emptySlotIsAdjacent = true;
            }

            if (emptySlotIsAdjacent)
            {
                (sender as Button).SetValue(Grid.RowProperty, emptySpaceRow);
                (sender as Button).SetValue(Grid.ColumnProperty, emptySpaceCol);
            }

            CheckIfArranged();
        }

        private void CheckIfArranged()
        {
            if (((int)One.GetValue(Grid.RowProperty) == 0 && (int)One.GetValue(Grid.ColumnProperty) == 0) &&
                ((int)Two.GetValue(Grid.RowProperty) == 0 && (int)Two.GetValue(Grid.ColumnProperty) == 1) &&
                ((int)Three.GetValue(Grid.RowProperty) == 0 && (int)Three.GetValue(Grid.ColumnProperty) == 2) &&
                ((int)Four.GetValue(Grid.RowProperty) == 1 && (int)Four.GetValue(Grid.ColumnProperty) == 0) &&
                ((int)Five.GetValue(Grid.RowProperty) == 1 && (int)Five.GetValue(Grid.ColumnProperty) == 1) &&
                ((int)Six.GetValue(Grid.RowProperty) == 1 && (int)Six.GetValue(Grid.ColumnProperty) == 2) &&
                ((int)Seven.GetValue(Grid.RowProperty) == 2 && (int)Seven.GetValue(Grid.ColumnProperty) == 0) &&
                ((int)Eight.GetValue(Grid.RowProperty) == 2 && (int)Eight.GetValue(Grid.ColumnProperty) == 1))
            {
                Buttons_are_arranged(this, new EventArgs());
            }
        }

        EventHandler Buttons_are_arranged;
        
        private void Ending(object sender, EventArgs e)
        {
            foreach (Button button in MainGrid.Children)
            {
                button.IsEnabled = false;
            }

            Randomize.IsEnabled = false;

            Button startOver = new Button();
            startOver.SetValue(Grid.ColumnProperty, 2);
            startOver.SetValue(Grid.RowProperty, 2);
            startOver.Width = 100;
            startOver.Height = 30;
            startOver.Content = "Start over?";
            startOver.Click += On_start_over;

            MainGrid.Children.Add(startOver);
        }

        private void On_start_over(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Remove(sender as Button);
            Randomize.IsEnabled = true;
        }
    }
}
