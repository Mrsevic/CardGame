using CardGame.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace CardGame
{
    sealed class Playground
    {
        IGame Game { get; }
        public Playground() => Game = new Game();

        static Task Main(string[] args)
        {
            var playground = new Playground();
            return new Menu(playground.Game).Run(CancellationToken.None);
        }
    }
}
