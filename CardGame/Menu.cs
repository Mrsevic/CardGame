using CardGame.Domain;
using CardGame.Domain.Resources;
using CardGame.Pages;
using EasyConsole;
namespace CardGame
{
    sealed class Menu : Program
    {
        public Menu(Game game) : base(Strings.CardGame, breadcrumbHeader: true)
        {
            AddPage(new TitlePage(this));

            AddPage(new InitPage(this, game));

            AddPage(new PlayPage(this, game));

            SetPage<InitPage>();
        }
    }
}