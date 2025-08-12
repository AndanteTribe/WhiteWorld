using AndanteTribe.Utils.Unity.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;

namespace WhiteWorld.Editor
{
    [InitializeOnLoad]
    public static class ToolbarManager
    {
        static ToolbarManager()
        {
            UnityEditorToolbarUtils.AddCenter(false, () =>
            {
                var button = new Button { text = "Systemシーンをロード" };
                button.RegisterCallback<ClickEvent>(static _ =>
                {
                    EditorSceneManager.OpenScene("Assets/Scenes/System.unity");
                    EditorApplication.isPlaying = true;
                });
                return button;
            });
        }
    }
}