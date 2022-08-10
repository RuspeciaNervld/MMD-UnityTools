using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class AutoLookAt : EditorWindow
{
    public GameObject target;


    [SerializeField]//必须要加
    protected List<UnityEngine.Object> lookers = new List<UnityEngine.Object>();

    //序列化对象
    protected SerializedObject _serializedObject;

    //序列化属性
    protected SerializedProperty _assetLstProperty;

    private string countStr;

    protected void OnEnable() {
        //使用当前类初始化
        _serializedObject = new SerializedObject(this);
        //获取当前类中可序列话的属性
        _assetLstProperty = _serializedObject.FindProperty("lookers");
    }

    [MenuItem("MMD工具/Auto Look At One")]
    static void run() {
        EditorWindow.GetWindow<AutoLookAt>();
    }

    void OnGUI() {


        target = EditorGUILayout.ObjectField("target camera", target, typeof(GameObject)) as GameObject;

        //更新
        _serializedObject.Update();

        //开始检查是否有修改
        EditorGUI.BeginChangeCheck();

        //显示属性
        //第二个参数必须为true，否则无法显示子节点即List内容
        EditorGUILayout.PropertyField(_assetLstProperty, true);

        //结束检查是否有修改
        if (EditorGUI.EndChangeCheck()) {//提交修改
            _serializedObject.ApplyModifiedProperties();
        }
        

        //EditorGUILayout.TextField("Path:",materialPath,GUILayout.Height(300));

        //this.Repaint();//实时刷新

        //@materialPath = GUILayout.TextArea(null);
        if (GUILayout.Button("正面朝向")) {
            Debug.Log("开始执行...");
            foreach(GameObject looker in lookers) {
                looker.transform.LookAt(target.transform);
            }
            Debug.Log("执行结束");
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("背面朝向")) {
            Debug.Log("开始执行...");
            foreach (GameObject looker in lookers) {
                looker.transform.LookAt(target.transform);
                looker.transform.Rotate(Vector3.up, 180);
            }
            Debug.Log("执行结束");
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("水平化正面朝向")) {
            Debug.Log("开始执行...");
            foreach (GameObject looker in lookers) {
                looker.transform.LookAt(new Vector3(target.transform.position.x, looker.transform.position.y, target.transform.position.z));
            }
            Debug.Log("执行结束");
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("水平化背面朝向")) {
            Debug.Log("开始执行...");
            foreach (GameObject looker in lookers) {
                looker.transform.LookAt(new Vector3(target.transform.position.x, looker.transform.position.y, target.transform.position.z));
                looker.transform.Rotate(Vector3.up, 180);
            }
            Debug.Log("执行结束");
            AssetDatabase.Refresh();
        }
    }



    [ExecuteInEditMode]

    void OnSelectionChange() {
        this.Repaint();
    }

}
