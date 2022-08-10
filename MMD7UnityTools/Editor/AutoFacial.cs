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
    [MenuItem("MMD����/Auto Translate Facial vmd")]
    static void run() {
        EditorWindow.GetWindow<AutoFacial>();
    }

    void OnGUI() {
        var root = new VisualElement();
        var objectField = new ObjectField("Shader");
        objectField.objectType = typeof(Shader);
        mesh = EditorGUILayout.ObjectField("target face", mesh, typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;

        GUILayout.Label("����Ҫת���ı��鶯���ļ�[vmd]��·�������Ҽ�Ŀ¼Copy Path��");

        //EditorGUILayout.TextField("Path:",materialPath,GUILayout.Height(300));


        path = EditorGUILayout.TextArea(path);
        //this.Repaint();//ʵʱˢ��

        //@materialPath = GUILayout.TextArea(null);
        if (GUILayout.Button("��ʼִ��")) {
            Debug.Log("��ʼת��...");
            MMDExtensionsEditor.CreateMorphAnimation(path, mesh);
            Debug.Log("ת������");
            AssetDatabase.Refresh();
        }
    }


    void OnSelectionChange() {
        this.Repaint();
    }

#endif
}
