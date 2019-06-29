using UnityEngine;
using UnityEditor;
using UnityEditor.Graphs;

public class ActionEditor : EditorWindow
{
    static private ActionEditor actionEditorWindow = null;
    //private Graph stateMachineGraph = null;
    //private ActionGraphGUI stateMachineGraphGUI = null;

    private Vector2 scrollPos = new Vector2();
    Node testNode = new Node();
    Color editorColor = new Color(0.25f, 0.4f, 0.25f);

    [MenuItem("Window/ActionEditor")]
    static private void OnOpen()
    {
        actionEditorWindow = GetWindow<ActionEditor>();
        actionEditorWindow.titleContent.text = "ActionEditor";
        actionEditorWindow.wantsMouseMove = true;

    }

    private void Update()
    {
        MouseEventProc(Event.current);
        Repaint();
    }

    private void OnGUI()
    {
        if (actionEditorWindow == null)
        {
            return;
        }

        DrawBackGround();
        DrawNodeWindow();

        //this.stateMachineGraphGUI.BeginGraphGUI(actionEditorWindow, this.graphGUIWindowRect);
        //// OnGraphGUIで矩形選択が可能に
        //this.stateMachineGraphGUI.OnGraphGUI();
        //testNode.Dragged();
        //testNode.Draw();
        ////this.node = GUI.Window(0, this.node, DrawNodeWindow, "Window 0");
        //this.stateMachineGraphGUI.EndGraphGUI();
    }

    private void DrawNodeWindow()
    {
        Rect scrolledNode = testNode.GetRect();
        scrolledNode.position = (scrolledNode.position - this.scrollPos);

        if(actionEditorWindow.rootVisualElement.contentRect.
           Overlaps(testNode.GetRect()))
        {
            GUI.Box(scrolledNode, "AAA");
        }
    }

    private void MouseEventProc(Event _mouseEvent)
    {
        switch(_mouseEvent.type)
        {
            case EventType.MouseDown:
                testNode.Hold(_mouseEvent.mousePosition);
                break;
            case EventType.MouseDrag:
                testNode.Draged(_mouseEvent.delta);
                break;
            case EventType.MouseUp:
                testNode.Pulled();
                break;
        }
    }

    private void OnEnable()
    {

        //if(this.stateMachineGraph == null)
        //{
        //    this.stateMachineGraph = ScriptableObject.CreateInstance<Graph>();
        //    this.stateMachineGraph.hideFlags = HideFlags.HideAndDontSave;
        //}

        //if(this.stateMachineGraphGUI == null)
        //{
        //    this.stateMachineGraphGUI = GetEditor(this.stateMachineGraph);
        //}
    }

    private void OnDisable()
    {
        actionEditorWindow = null;
    }


    private void DrawBackGround()
    {

        EditorGUI.DrawRect(actionEditorWindow.rootVisualElement.contentRect, editorColor);
        Handles.BeginGUI();
        Handles.color = Color.grey;

        for (int i = 0; i <= 2; i++)
        {
            Handles.DrawLine(new Vector3(i * 20.0f, -20.0f, 0.0f), new Vector3(i * 20.0f, 100.0f, 0.0f));
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }
}
