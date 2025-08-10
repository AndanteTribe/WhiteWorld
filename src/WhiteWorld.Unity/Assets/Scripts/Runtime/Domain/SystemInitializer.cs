using System.Threading;
using System.Threading.Tasks;
using AndanteTribe.Utils;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.Domain
{
    public class SystemInitializer : IInitializable
    {
        private readonly ISceneController _controller;

        public SystemInitializer(ISceneController controller) => _controller = controller;

        public ValueTask InitializeAsync(CancellationToken cancellationToken) =>
            _controller.LoadAsync(SceneName.Title, cancellationToken);
    }
}