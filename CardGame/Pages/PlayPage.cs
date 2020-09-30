using CardGame.Domain;
using CardGame.Domain.Resources;
using EasyConsole;
using System.Threading;
using System.Threading.Tasks;

namespace CardGame.Pages
{
    sealed class PlayPage : Page
    {
        private Game _game;

        public PlayPage(Program program, Game game) : base(Strings.PlayPage, program)
        {
            _game = game;
        }

        public override async Task Display(CancellationToken cancellationToken)
        {
            await base.Display(cancellationToken);
            _game.Play();
            Input.ReadString(Strings.AnotherGame);
            await Program.NavigateHome(cancellationToken);
        }
    }
}
