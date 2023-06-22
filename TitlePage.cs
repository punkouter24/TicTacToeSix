using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeSix
{
    public class TitlePage : ContentPage
    {
        public TitlePage()
        {
            Title = "TIC TAC TOE 6";
            BackgroundColor = Colors.White;

            Label titleLabel = new Label
            {
                Text = "TIC TAC TOE 6",
                FontSize = 32,
                TextColor = Colors.Black,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Button beginButton = new Button
            {
                Text = "BEGIN!",
                FontSize = 24,
                BackgroundColor = Colors.Green,
                TextColor = Colors.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            beginButton.Clicked += BeginButton_Clicked;

            Content = new StackLayout
            {
                Children = { titleLabel, beginButton }
            };
        }

        private async void BeginButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}
