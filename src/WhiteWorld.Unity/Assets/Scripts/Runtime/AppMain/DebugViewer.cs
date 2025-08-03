#if ENABLE_DEBUGTOOLKIT

using DebugToolkit;
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
            
            return root;
        }
    }
}

#endif