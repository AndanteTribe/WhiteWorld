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
using WhiteWorld.Domain.LifeGame.Sequences;
using WhiteWorld.Presentation;

namespace WhiteWorld.AppMain
{
    public class DebugViewer : DebugViewerBase, IStartable
    {
        private readonly IObjectResolver _resolver;
        private readonly ISceneController _sceneController;
        private readonly GameObject _tvPrefab;
        private readonly CardSelectionSequence _cardSelectionSequence;
        private CardObjectManager _cardObjectManager;

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

            // _sceneController.LoadAsync(SceneName.Title, CancellationToken.None);

            //CardSelectScene遷移
            var cardSelectWindow = root.AddWindow("cardSelect");

            Button button = new Button
            {
                text = "Shift To CardSelect"
            };
            button.clicked += UniTask.Action(async () =>
            {
                await _sceneController.UnloadAllAsync(CancellationToken.None);
                await _sceneController.LoadAsync(SceneName.CardSelectEdit, CancellationToken.None);
            });
            cardSelectWindow.Add(button);

            //cardSelect部分
            IntegerField intField = new IntegerField("カード数値入力");
            intField.value = 0;
            cardSelectWindow.Add(intField);

            // IntegerField 更新時のイベント
            intField.RegisterValueChangedCallback(evt =>
            {
                FixedValueAlgorithm fixedAlg = new FixedValueAlgorithm(new SpaceAmount(intField.value));
                _cardObjectManager = Object.FindAnyObjectByType<CardObjectManager>();
                _cardObjectManager.UpdateAlgorithm(fixedAlg);
                _cardObjectManager.UpdateCard();
            });

            //タイトルシーンへ遷移
            var titleWindow = root.AddWindow("Title");
            var titleButton = new Button();
            titleButton.text = "Shift to Title";
            titleButton.clicked += UniTask.Action(async () =>
            {
                await _sceneController.UnloadAllAsync(CancellationToken.None);
                await _sceneController.LoadAsync(SceneName.Title, CancellationToken.None);
            });
            titleWindow.Add(titleButton);

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

            var lifeGameWindow = root.AddWindow("LifeGame");
            var lifeGameButton = new Button();
            lifeGameButton.text = "Shift to LifeGame";
            lifeGameButton.RegisterCallback<ClickEvent, ISceneController>(static (_, sceneController) =>
            {
                sceneController.LoadAsync(SceneName.LifeGame, Application.exitCancellationToken).Forget();
            }, _sceneController);
            lifeGameWindow.Add(lifeGameButton);

        /*
          // テレビ例
          var tvWindow = root.AddWindow("Television Control");
          tvWindow.Add(new Label(text:"テレビ制御"));

          // このようにテレビのPrefabはresolverからInstantiateしてください、Cinemachine Brainがついてるメインカメラを必ず用意すること！
          var tv = _resolver.Instantiate(_tvPrefab, new Vector3(0, 0, 10), Quaternion.Euler(0, 180, 0));
          var tvAnimationButton = new Button();
          tvWindow.Add(tvAnimationButton);
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