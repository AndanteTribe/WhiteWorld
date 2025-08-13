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
            UnityEditorToolbarUtils.AddCenter(false, static () =>
            {
                var button = new Button()
                {
                    text = "システムシーンをロード",
                };
                button.RegisterCallback<ClickEvent>(static _ =>
                {
                    if (EditorApplication.isPlaying)
                    {
                        EditorApplication.isPlaying = false;
                    }
                    else
                    {
                        EditorSceneManager.OpenScene("Assets/Scenes/System.unity");
                        EditorApplication.isPlaying = true;
                    }
                });
                return button;
            });
        }
    }
}