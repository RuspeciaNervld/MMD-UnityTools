using MMDExtensions;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class AutoFacial : EditorWindow {


    SkinnedMeshRenderer mesh = null;
    string path = "";

#if UNITY_EDITOR
    [MenuItem("MMD工具/Auto Translate Facial vmd")]
    static void run() {
        EditorWindow.GetWindow<AutoFacial>();
    }

    void OnGUI() {
        var root = new VisualElement();
        var objectField = new ObjectField("Shader");
        objectField.objectType = typeof(Shader);
        mesh = EditorGUILayout.ObjectField("target face", mesh, typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;

        GUILayout.Label("输入要转换的表情动画文件[vmd]的路径（可右键目录Copy Path）");

        //EditorGUILayout.TextField("Path:",materialPath,GUILayout.Height(300));


        path = EditorGUILayout.TextArea(path);
        //this.Repaint();//实时刷新

        //@materialPath = GUILayout.TextArea(null);
        if (GUILayout.Button("开始执行")) {
            Debug.Log("开始转换...");
            MMDExtensionsEditor.CreateMorphAnimation(path, mesh);
            Debug.Log("转换结束");
            AssetDatabase.Refresh();
        }
    }


    void OnSelectionChange() {
        this.Repaint();
    }

#endif
}
