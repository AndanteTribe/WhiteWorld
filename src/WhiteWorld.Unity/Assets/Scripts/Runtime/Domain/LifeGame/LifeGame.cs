using System.Threading;
using System.Threading.Tasks;
using AndanteTribe.Utils;

namespace WhiteWorld.Domain.LifeGame
{
    public class LifeGame : IInitializable
    {
        private readonly LifeGameMainSequence _mainSequence;

        public LifeGame(LifeGameMainSequence mainSequence)
        {
            _mainSequence = mainSequence;
        }

        public ValueTask InitializeAsync(CancellationToken cancellationToken)
        {
            // ライフゲームの初期化処理をここに記述
            return default;
        }
    }
}