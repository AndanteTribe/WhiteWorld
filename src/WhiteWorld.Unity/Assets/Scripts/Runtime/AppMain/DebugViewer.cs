#if ENABLE_DEBUGTOOLKIT

using DebugToolkit;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using VContainer.Unity;

namespace WhiteWorld.AppMain
{
    public class DebugViewer : DebugViewerBase, IStartable
    {
        protected override VisualElement CreateViewGUI()
        {
            var root = base.CreateViewGUI();

            var firstWindow = root.AddWindow("デバッグメニュー");
            firstWindow.AddProfileInfoLabel();

            // OpeningScene用のウィンドウを作製.
            var openingWindow = root.AddWindow("OpeningScene");
            openingWindow.style.flexDirection = FlexDirection.Column;
            // Openingシーンへ遷移するボタンを作成.
            var buttonToOpening = new Button();
            buttonToOpening.text = "Shift to Opening Scene";
            buttonToOpening.clicked += () => ShiftScene("Opening");
            openingWindow.Add(buttonToOpening);


            return root;
        }

        private void ShiftScene(string sceneName) => SceneManager.LoadScene(sceneName);
    }
}

#endif