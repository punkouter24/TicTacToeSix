using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace TicTacToeSix
{
    public class WinnerDialog : ContentPage
    {
        public WinnerDialog(string winner)
        {
            BackgroundColor = Color.FromRgba(0, 0, 0, 0.7);

            Label winnerLabel = new Label
            {
                Text = $"{winner} Wins!",
                FontSize = 24,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Colors.White
            };

            Button newGameButton = new Button
            {
                Text = "New Game",
                FontSize = 24,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Colors.Green,
                TextColor = Colors.White
            };

            newGameButton.Clicked += (sender, e) => { Navigation.PopModalAsync(); };

            Content = new StackLayout
            {
                Children = { winnerLabel, newGameButton }
            };
        }
    }
}
