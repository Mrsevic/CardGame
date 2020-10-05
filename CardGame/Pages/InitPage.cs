using CardGame.Domain;
using CardGame.Domain.Resources;
using EasyConsole;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CardGame.Pages
{
    sealed class InitPage : Page
    {
        IGame _game;
        public InitPage(Program program, IGame game) : base(Strings.InitPage, program)
        {
            _game = game;
        }

        public override async Task Display(CancellationToken cancellationToken)
        {
            await base.Display(cancellationToken);

            Output.WriteLine("Insert the deck capacity: ");
            var deckCapacity = Input.ReadInt();
            Output.WriteLine($"You wrote: {deckCapacity.ToString()}");
            _game.Prepare(deckCapacity);
            Output.WriteLine(ConsoleColor.Green, $"Players ready for the game: {Environment.NewLine}");

            for (int i = 0; i < _game.Players.Count; i++)
            {
                Output.WriteLine($"Player No. {i.ToString()} with nick {_game.Players[i].Nick} {Environment.NewLine}");
            }

            bool confirm = string.Equals(Input.ReadString(Strings.StartGame), "Y", StringComparison.OrdinalIgnoreCase);

            if (confirm)
            {
                await Program.NavigateTo<PlayPage>(cancellationToken);
            }
            else
            {
                Input.ReadString(Strings.BackToHome);
                await Program.NavigateHome(cancellationToken);
            }
        }
    }
}
