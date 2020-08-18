using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class ActionEditor : EditorWindow
{
    static private ActionEditor actionEditorWindow = null;
    //private Graph stateMachineGraph = null;
    //private ActionGraphGUI stateMachineGraphGUI = null;

    private Vector2 scrollPos = new Vector2();

    private Node targetNode = new Node();
    private List<Node> nodes = new List<Node>();
    private Color editorColor = new Color(0.25f, 0.4f, 0.25f);

    [MenuItem("Window/ActionEditor")]
    static private void OnOpen()
    {
        actionEditorWindow = GetWindow<ActionEditor>();
        actionEditorWindow.titleContent.text = "ActionEditor";
        actionEditorWindow.wantsMouseMove = true;
    }

    private void Update()
    {
        Repaint();
    }

    private void OnGUI()
    {
        if (actionEditorWindow == null)
        {
            return;
        }
        MouseEventProc(Event.current);
        DrawBackGround();
        DrawNodeWindow();


        //EditorGUILayout.BeginHorizontal(EditorStyles.toolbar, new GUILayoutOption[] { GUILayout.ExpandWidth(true) });
        //GUILayout.Button("AAAAAAA", EditorStyles.toolbarButton, GUILayout.Width(70));
        //EditorGUILayout.EndVertical();

        Rect areaRect = new Rect(0.0f, 0.0f, 100, 100);
        areaRect = GUILayout.Window(50, areaRect, WindowFunction, "WindowEEEE", new GUIStyle(GUI.skin.window), //("View", new GUIStyle(GUI.skin.box),
GUILayout.ExpandWidth(true),
GUILayout.ExpandHeight(true));


        Vector2 position = new Vector2(-10, 00);
        EditorGUILayout.BeginVertical(GUILayout.Width(100),
            GUILayout.MaxWidth(100),
            GUILayout.MinWidth(100));
        EditorGUILayout.EndVertical();

        //GUILayout.BeginVertical(
        //    GUILayout.Width(100),
        //    GUILayout.MaxWidth(100),
        //    GUILayout.MinWidth(100));
        //GUILayout.Area("View", new GUIStyle(GUI.skin.box),
        //GUILayout.ExpandWidth(true),
        //GUILayout.ExpandHeight(true));

        //GUILayout.EndVertical();


        //EditorGUILayout.BeginVertical(EditorStyles.popup, new GUILayoutOption[] { GUILayout.ExpandHeight(true) });

        //EditorGUILayout.EndHorizontal();


        //this.stateMachineGraphGUI.BeginGraphGUI(actionEditorWindow, this.graphGUIWindowRect);
        //// OnGraphGUIで矩形選択が可能に
        //this.stateMachineGraphGUI.OnGraphGUI();
        //testNode.Dragged();
        //testNode.Draw();
        ////this.node = GUI.Window(0, this.node, DrawNodeWindow, "Window 0");
        //this.stateMachineGraphGUI.EndGraphGUI();
    }

    private void WindowFunction(int windowId)
    {
        GUI.DragWindow();
        GUI.Box(new Rect(5, 5, 10,10), "BBB");
        Debug.Log("AAAAA");
    }

    private void DrawNodeWindow()
    {
        Rect scrolledNode = targetNode.GetRect();
        scrolledNode.position = (scrolledNode.position - this.scrollPos);

        if(actionEditorWindow.rootVisualElement.contentRect.
           Overlaps(targetNode.GetRect()))
        {
            GUI.Box(scrolledNode, "AAA");
        }
    }

    private void MouseEventProc(Event _mouseEvent)
    {
        switch(_mouseEvent.type)
        {
            case EventType.MouseDown:
                targetNode.Hold(_mouseEvent.mousePosition);
                break;
            case EventType.MouseDrag:
                targetNode.Draged(_mouseEvent.delta);
                break;
            case EventType.MouseUp:
                targetNode.Pulled();
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
