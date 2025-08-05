#if ENABLE_DEBUGTOOLKIT

using System.Threading;
using CardSelectFlow;
using CardSelectFlow.Interface;
using Cysharp.Threading.Tasks;
using DebugToolkit;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer.Unity;
using WhiteWorld.Domain;
using WhiteWorld.Domain.Entity;
using WhiteWorld.Domain.LifeGame.Sequences;
using WhiteWorld.Domain.Runtime.Domain.LifeGame.Sequences;

namespace WhiteWorld.AppMain
{
    public class DebugViewer : DebugViewerBase, IStartable
    {
        private readonly ISceneController _sceneController;
        private readonly CardSelectionSequence _cardSelectionSequence;
        private CardObjectManager _cardObjectManager;

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

            // _sceneController.LoadAsync(SceneName.Title, CancellationToken.None);

            //Scene遷移部分
            var sceneWindow = root.AddWindow("Scene遷移");

            Button button = new Button
            {
                text = "CardSelectScene"
            };
            button.clicked += () =>
            {
                _sceneController.LoadAsync(SceneName.CardSelectEdit, CancellationToken.None).Forget();
            };
            sceneWindow.Add(button);

            var window = root.AddWindow("カード番号指定");
            Toggle myToggle = new Toggle("カード番号を指定する(トグル)");
            window.Add(myToggle);

            //cardSelect部分
            IntegerField intField = new IntegerField("数値入力");
            intField.value = 0;
            window.Add(intField);

            // IntegerField 更新時のイベント
            intField.RegisterValueChangedCallback(evt =>
            {
                if(!myToggle.value)
                    return;

                FixedValueAlgorithm fixedAlg = new FixedValueAlgorithm(new SpaceAmount(intField.value));
                _cardObjectManager = Object.FindAnyObjectByType<CardObjectManager>();
                _cardObjectManager.UpdateAlgorithm(fixedAlg);
                _cardObjectManager.UpdateCard();
            });

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