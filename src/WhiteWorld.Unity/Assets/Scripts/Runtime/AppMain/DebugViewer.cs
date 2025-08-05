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
    public class DebugViewer : DebugViewerBase, IStartable
    {
        private readonly ISceneController _sceneController;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugViewer"/> class.
        /// </summary>
        /// <param name="sceneController"></param>
        public DebugViewer(ISceneController sceneController)
        {
            _sceneController = sceneController;
        }

        protected override VisualElement CreateViewGUI()
        {
            var root = base.CreateViewGUI();

            var firstWindow = root.AddWindow("デバッグメニュー");
            firstWindow.AddProfileInfoLabel();

            _sceneController.LoadAsync(SceneName.Title, CancellationToken.None).Forget();

            //Openingシーンに遷移するボタンを作成
            var openingWindow = root.AddWindow("Opening");
            var buttonToOpening = new Button();
            buttonToOpening.text = "Shift to Opening";
            buttonToOpening.clicked += UniTask.Action(async () =>
            {
                await _sceneController.UnloadAllAsync(CancellationToken.None);
                await _sceneController.LoadAsync(SceneName.Opening, CancellationToken.None);
            });
            openingWindow.Add(buttonToOpening);
            return root;
        }
    }
}

#endif