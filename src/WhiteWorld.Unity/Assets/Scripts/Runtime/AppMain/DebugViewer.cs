#if ENABLE_DEBUGTOOLKIT

using System.Threading;
using Cysharp.Threading.Tasks;
using DebugToolkit;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;
using WhiteWorld.Domain;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.AppMain
{
    public class DebugViewer : DebugViewerBase, IStartable
    {
        private readonly IObjectResolver _resolver;
        private readonly ISceneController _sceneController;
        private readonly GameObject _tvPrefab;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugViewer"/> class.
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="sceneController"></param>
        /// <param name="tvPrefab"></param>
        public DebugViewer(IObjectResolver resolver, ISceneController sceneController, GameObject tvPrefab)
        {
            _resolver = resolver;
            _sceneController = sceneController;
            _tvPrefab = tvPrefab;
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

            /*
            // テレビ例
            firstWindow.Add(new Label(text:"テレビ制御"));

            // このようにテレビのPrefabはresolverからInstantiateしてください、Cinemachine Brainがついてるメインカメラを必ず用意すること！
            var tv = _resolver.Instantiate(_tvPrefab, new Vector3(0, 0, 10), Quaternion.Euler(0, 180, 0));
            var tvAnimationButton = new Button();
            firstWindow.Add(tvAnimationButton);
            tvAnimationButton.text = "テレビアニメーション";
            tvAnimationButton.RegisterCallback<ClickEvent, ITVController>( (evt, tvCtr) =>
            {
                tvCtr.StartTVAnimation();
            }, tv.GetComponent<ITVController>());
            */

            return root;
        }
    }
}

#endif