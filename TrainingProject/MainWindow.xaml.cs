using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TrainingProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int GameSideSize = 4;

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
            const int NecessarySwaps = 100;

            int emptySlotRow = GameSideSize - 1;
            int emptySlotCol = GameSideSize - 1;
            int randomHorizontalMove;
            int randomVerticalMove;
            int swapsMade = 0;

            while (swapsMade < NecessarySwaps)
            {
                randomHorizontalMove = rand.Next(-1, 2);
                randomVerticalMove = rand.Next(-1, 2);

                if (IsMovePossible(emptySlotRow, emptySlotCol, randomHorizontalMove, randomVerticalMove))
                {
                    Button currentButton = GetButtonFromGridPosition(emptySlotRow + randomVerticalMove, emptySlotCol + randomHorizontalMove);

                    currentButton.SetValue(Grid.RowProperty, emptySlotRow);
                    currentButton.SetValue(Grid.ColumnProperty, emptySlotCol);

                    emptySlotCol += randomHorizontalMove;
                    emptySlotRow += randomVerticalMove;

                    swapsMade++;
                }
            }
        }

        //private Tuple<int, int> GetEmptySlotCoordinates()
        //{

        //    for (int i = 0; i < GameSideSize; i++)
        //    {
        //        for (int u = 0; u < GameSideSize; u++)
        //        {
                    
        //        }
        //    }
        //}

        private Button GetButtonFromGridPosition(int row, int col)
        {
            UIElementCollection allButtons = MainGrid.Children;

            foreach (Button button in allButtons)
            {
                int buttonRow = (int)button.GetValue(Grid.RowProperty);
                int buttonCol = (int)button.GetValue(Grid.ColumnProperty);

                if (buttonRow == row && buttonCol == col)
                {
                    return button;
                }
            }

            return null;
        }

        private bool IsMovePossible(int emptySlotRow, int emptySlotCol, int horizontalMove, int verticalMove)
        {
            // If both moves are 0, the move is not possible, because a button will not be found
            // at this part of the Grid.
            if (horizontalMove == 0 && verticalMove == 0)
            {
                return false;
            }

            // The button cannot move diagonally also.
            if ((horizontalMove == -1 && verticalMove == -1) ||
                (horizontalMove == 1 && verticalMove == 1))
            {
                return false;
            }

            if (emptySlotCol + horizontalMove < 0 ||
                emptySlotCol + horizontalMove >= GameSideSize ||
                emptySlotRow + verticalMove < 0 ||
                emptySlotRow + verticalMove >= GameSideSize)
            {
                return false;
            }
            else
            {
                return true;
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
            bool[,] allSlots = new bool[GameSideSize, GameSideSize];

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
                ((int)Four.GetValue(Grid.RowProperty) == 0 && (int)Four.GetValue(Grid.ColumnProperty) == 3) &&
                ((int)Five.GetValue(Grid.RowProperty) == 1 && (int)Five.GetValue(Grid.ColumnProperty) == 0) &&
                ((int)Six.GetValue(Grid.RowProperty) == 1 && (int)Six.GetValue(Grid.ColumnProperty) == 1) &&
                ((int)Seven.GetValue(Grid.RowProperty) == 1 && (int)Seven.GetValue(Grid.ColumnProperty) == 2) &&
                ((int)Eight.GetValue(Grid.RowProperty) == 1 && (int)Eight.GetValue(Grid.ColumnProperty) == 3) &&
                ((int)Nine.GetValue(Grid.RowProperty) == 2 && (int)Nine.GetValue(Grid.ColumnProperty) == 0) &&
                ((int)Ten.GetValue(Grid.RowProperty) == 2 && (int)Ten.GetValue(Grid.ColumnProperty) == 1) &&
                ((int)Eleven.GetValue(Grid.RowProperty) == 2 && (int)Eleven.GetValue(Grid.ColumnProperty) == 2) &&
                ((int)Twelve.GetValue(Grid.RowProperty) == 2 && (int)Twelve.GetValue(Grid.ColumnProperty) == 3) &&
                ((int)Thirteen.GetValue(Grid.RowProperty) == 3 && (int)Thirteen.GetValue(Grid.ColumnProperty) == 0) &&
                ((int)Fourteen.GetValue(Grid.RowProperty) == 3 && (int)Fourteen.GetValue(Grid.ColumnProperty) == 1) &&
                ((int)Fifteen.GetValue(Grid.RowProperty) == 3 && (int)Fifteen.GetValue(Grid.ColumnProperty) == 2))
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

            StartOver.Visibility = Visibility.Visible;
        }

        private void On_start_over(object sender, RoutedEventArgs e)
        {
            StartOver.Visibility = Visibility.Collapsed;
            Randomize.IsEnabled = true;
        }
    }
}