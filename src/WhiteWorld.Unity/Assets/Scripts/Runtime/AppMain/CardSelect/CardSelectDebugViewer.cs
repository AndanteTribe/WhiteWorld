#if ENABLE_DEBUGTOOLKIT
using CardSelectFlow;
using CardSelectFlow.Interface;
using DebugToolkit;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer.Unity;
using WhiteWorld.Domain;
using WhiteWorld.Domain.Entity;

namespace WhiteWorld.AppMain
{
    public class CardSelectDebugViewer : DebugViewerBase, IStartable
    {
        private readonly ISceneController _sceneController;
        private readonly ICardObjectManager _manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugViewer"/> class.
        /// </summary>
        /// <param name="sceneController"></param>
        public CardSelectDebugViewer(ISceneController sceneController,ICardObjectManager manager)
        {
            _sceneController = sceneController;
            _manager = manager;
        }

        protected override VisualElement CreateViewGUI()
        {
            var root = base.CreateViewGUI();

            var window = root.AddWindow("カード番号指定");
            Toggle myToggle = new Toggle("カード番号を指定する(トグル)");
            window.Add(myToggle);

            // IntegerField 作成
            IntegerField intField = new IntegerField("数値入力");
            intField.value = 0;
            window.Add(intField);

            // IntegerField 更新時イベント
            intField.RegisterValueChangedCallback(evt =>
            {
                if(!myToggle.value)
                    return;

                Debug.Log("更新");

                FixedValueAlgorithm fixedAlg = new FixedValueAlgorithm(new SpaceAmount(intField.value));
                _manager.UpdateAlgorithm(fixedAlg);
                _manager.UpdateCard();
            });

            return root;
        }
    }
}

#endif