#if ENABLE_DEBUGTOOLKIT

using System.Threading;
using Cysharp.Threading.Tasks;
using DebugToolkit;
using UnityEngine.UIElements;
using VContainer.Unity;
using WhiteWorld.Domain;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.AppMain
{
    public class TitleDebugViewer : DebugViewerBase, IStartable
    {
        private readonly ISceneController _sceneController;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugViewer"/> class.
        /// </summary>
        /// <param name="sceneController"></param>
        public TitleDebugViewer(ISceneController sceneController)
        {
            _sceneController = sceneController;
        }

        protected override VisualElement CreateViewGUI()
        {
            var root = base.CreateViewGUI();

            var firstWindow = root.AddWindow("Scene遷移");

            Button button = new Button
            {
                text = "CardSelectScene"
            };
            button.clicked += () =>
            {
                _sceneController.LoadAsync(SceneName.CardSelectEdit, CancellationToken.None).Forget();
            };
            firstWindow.Add(button);
            return root;
        }
    }
}

#endif