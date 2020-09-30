using CardGame.Domain.Resources;
using EasyConsole;

namespace CardGame.Pages
{
    sealed class TitlePage : MenuPage
    {
        public TitlePage(Program program)
        : base(Strings.TitlePage, program,
              new Option(Strings.InitPage, program.NavigateTo<InitPage>))
        {
        }
    }
}