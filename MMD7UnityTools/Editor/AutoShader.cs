using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class AutoShader : EditorWindow
{
   
    //public static Shader shader;
    //public static List<Material> materials;
    //private static List<Texture> textures;
    //private static Texture mainTex;
    //private static Texture toonTex;
    //private static Texture cubeTex;
    //public static string materialPath;
    public Shader shader1;
    public string materialPath;

#if UNITY_EDITOR
    [MenuItem("MMD����/Auto Translate Shader")]
    static void run() {
        EditorWindow.GetWindow<AutoShader>();
    }

    void OnGUI() {
        var root = new VisualElement();
        var objectField = new ObjectField("Shader");
        objectField.objectType = typeof(Shader);

        shader1 = EditorGUILayout.ObjectField("target shader", shader1, typeof(Shader)) as Shader;
        
        
        GUILayout.Label("����Ҫת���Ĳ��ʵ�·�������Ҽ�Ŀ¼Copy Path��");
        
        //EditorGUILayout.TextField("Path:",materialPath,GUILayout.Height(300));

        materialPath = EditorGUILayout.TextArea(materialPath);
        //this.Repaint();//ʵʱˢ��
    
        //@materialPath = GUILayout.TextArea(null);
        if (GUILayout.Button("��ʼִ��")) {
            Debug.Log("��ʼת��...");
            translate(shader1, materialPath);
            Debug.Log("ת������");
            AssetDatabase.Refresh();
        }
    }


    void OnSelectionChange() {
        this.Repaint();
    }

    [ExecuteInEditMode]
    static void translate(Shader shader,string materialPath) {
        List<Material> materials=new List<Material>();
        string[] paths = Directory.GetFiles(@materialPath);
        foreach (var item in paths) {
            string extension = Path.GetExtension(item).ToLower();
            if (extension == ".mat") {
                materials.Add(UnityEditor.AssetDatabase.LoadAssetAtPath<Material>(item));
            }
        }
        if (true) {
            for (int i = 0; i < materials.Count; i++) {
                Texture mainTex = materials[i].mainTexture;

                materials[i].shader = shader;

                materials[i].mainTexture = mainTex;

                mainTex = null;
            }
        } else {
            for (int i = 0; i < materials.Count; i++) {
                List<Texture> textures = GetCertainMaterialTextures(materials[i]);
                Texture mainTex = textures[0];
                Texture toonTex = textures[1];
                Texture cubeTex = textures[2];

                materials[i].shader = shader;

                materials[i].mainTexture = mainTex;
                materials[i].SetTexture("_ToonTex", toonTex);
                materials[i].SetTexture("_MaskMap", toonTex);
                materials[i].SetTexture("_SphereCube", cubeTex);

                mainTex = null;
            }
        }
    }
#endif

    //! ��ȡ���ʵ�������ͼ·��
    static string[] GetCertainMaterialTexturePaths(Material _mat) {
        List<string> results = new List<string>();

        Shader shader = _mat.shader;
        for (int i = 0; i < ShaderUtil.GetPropertyCount(shader); ++i) {
            if (ShaderUtil.GetPropertyType(shader, i) == ShaderUtil.ShaderPropertyType.TexEnv) {
                string propertyName = ShaderUtil.GetPropertyName(shader, i);
                Texture tex = _mat.GetTexture(propertyName);
                string texPath = AssetDatabase.GetAssetPath(tex.GetInstanceID());
                results.Add(texPath);
            }
        }

        return results.ToArray();
    }

    //! ��ȡ���ʵ�������ͼ
    static List<Texture> GetCertainMaterialTextures(Material _mat) {
        List<Texture> results = new List<Texture>();

        Shader shader = _mat.shader;
        for (int i = 0; i < ShaderUtil.GetPropertyCount(shader); ++i) {
            if (ShaderUtil.GetPropertyType(shader, i) == ShaderUtil.ShaderPropertyType.TexEnv) {
                string propertyName = ShaderUtil.GetPropertyName(shader, i);
                Texture tex = _mat.GetTexture(propertyName);
                results.Add(tex);
            }
        }

        return results;
    }
}
