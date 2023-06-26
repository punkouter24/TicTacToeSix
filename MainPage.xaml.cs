namespace TicTacToeSix;

public partial class MainPage : ContentPage
{

    private Button[,] buttons = new Button[6, 6];
    private Label statusLabel;
    private Button newGameButton;
    private bool isPlayerXTurn = true;
    private int turnCount = 0;

    [Obsolete]
    public MainPage()
    {
        Grid grid = new Grid();
        for (int row = 0; row < 6; row++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            for (int column = 0; column < 6; column++)
            {
                if (row == 0)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }

                Button button = new Button
                {
                    Text = string.Empty,
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                    Padding = 0
                };
                button.Clicked += async (sender, args) => { await Button_Click(sender, args); };
                buttons[row, column] = button;
                Grid.SetRow(button, row);
                Grid.SetColumn(button, column);
                grid.Children.Add(button);
            }
        }

        statusLabel = new Label
        {
            Text = "Player X's Turn",
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            TextColor = Colors.Green,
            BackgroundColor = Colors.Aqua,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        Grid.SetRow(statusLabel, 6);
        Grid.SetColumn(statusLabel, 0);
        Grid.SetColumnSpan(statusLabel, 6);
        grid.Children.Add(statusLabel);

        newGameButton = new Button { Text = "New Game", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, IsVisible = false };
        newGameButton.Clicked += NewGame_Click;
        Grid.SetRow(newGameButton, 6);
        Grid.SetColumn(newGameButton, 0);
        Grid.SetColumnSpan(newGameButton, 6);
        grid.Children.Add(newGameButton);

        Content = grid;
    }


    private void ShowNewGameButton()
    {
        newGameButton.IsVisible = true;
    }

    private void NewGame_Click(object sender, EventArgs e)
    {
        NewGame();
    }

    private void NewGame()
    {
        // Reset button labels, IsEnabled property, and game state
        foreach (Button button in buttons)
        {
            button.Text = string.Empty;
            button.IsEnabled = true;
        }

        isPlayerXTurn = true;
        turnCount = 0;
        statusLabel.Text = "Player X's Turn";
        newGameButton.IsVisible = false;
    }

    private async Task Button_Click(object sender, EventArgs e)
    {
        Button button = sender as Button;
        int row = Grid.GetRow(button);
        int column = Grid.GetColumn(button);

        if (string.IsNullOrEmpty(button.Text))
        {
            button.Text = isPlayerXTurn ? "X" : "O";
            turnCount++;

            if (CheckForWin(row, column))
            {
                DisableButtons();
                await Navigation.PushModalAsync(new WinnerDialog(button.Text));
              //  await Navigation.PushAsync(new WinnerDialog(button.Text));
                NewGame();
            }
            else if (turnCount == 36)
            {
                statusLabel.Text = "It's a draw!";
                ShowNewGameButton();
            }
            else
            {
                isPlayerXTurn = !isPlayerXTurn;
                statusLabel.Text = $"Player {(isPlayerXTurn ? "X" : "O")}'s Turn";
            }
        }
    }

    private bool CheckForWin(int row, int column)
    {
        string currentPlayerSymbol = buttons[row, column].Text;

        // Horizontal
        for (int i = 0; i <= 2; i++)
        {
            if (buttons[row, i].Text == currentPlayerSymbol &&
                buttons[row, i + 1].Text == currentPlayerSymbol &&
                buttons[row, i + 2].Text == currentPlayerSymbol &&
                buttons[row, i + 3].Text == currentPlayerSymbol)
            {
                return true;
            }
        }

        // Vertical
        for (int i = 0; i <= 2; i++)
        {
            if (buttons[i, column].Text == currentPlayerSymbol && buttons[i + 1, column].Text == currentPlayerSymbol &&
                    buttons[i + 2, column].Text == currentPlayerSymbol &&
                    buttons[i + 3, column].Text == currentPlayerSymbol)
            {
                return true;
            }
        }

        // Diagonal (top-left to bottom-right)
        for (int r = 0; r <= 2; r++)
        {
            for (int c = 0; c <= 2; c++)
            {
                if (buttons[r, c].Text == currentPlayerSymbol &&
                    buttons[r + 1, c + 1].Text == currentPlayerSymbol &&
                    buttons[r + 2, c + 2].Text == currentPlayerSymbol &&
                    buttons[r + 3, c + 3].Text == currentPlayerSymbol)
                {
                    return true;
                }
            }
        }

        // Diagonal (top-right to bottom-left)
        for (int r = 0; r <= 2; r++)
        {
            for (int c = 5; c >= 3; c--)
            {
                if (buttons[r, c].Text == currentPlayerSymbol &&
                    buttons[r + 1, c - 1].Text == currentPlayerSymbol &&
                    buttons[r + 2, c - 2].Text == currentPlayerSymbol &&
                    buttons[r + 3, c - 3].Text == currentPlayerSymbol)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void DisableButtons()
    {
        foreach (Button button in buttons)
        {
            button.IsEnabled = false;
        }
    }
}


