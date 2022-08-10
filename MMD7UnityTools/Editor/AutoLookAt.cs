using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class AutoLookAt : EditorWindow
{
    public GameObject target;


    [SerializeField]//����Ҫ��
    protected List<UnityEngine.Object> lookers = new List<UnityEngine.Object>();

    //���л�����
    protected SerializedObject _serializedObject;

    //���л�����
    protected SerializedProperty _assetLstProperty;

    private string countStr;

    protected void OnEnable() {
        //ʹ�õ�ǰ���ʼ��
        _serializedObject = new SerializedObject(this);
        //��ȡ��ǰ���п����л�������
        _assetLstProperty = _serializedObject.FindProperty("lookers");
    }

    [MenuItem("MMD����/Auto Look At One")]
    static void run() {
        EditorWindow.GetWindow<AutoLookAt>();
    }

    void OnGUI() {


        target = EditorGUILayout.ObjectField("target camera", target, typeof(GameObject)) as GameObject;

        //����
        _serializedObject.Update();

        //��ʼ����Ƿ����޸�
        EditorGUI.BeginChangeCheck();

        //��ʾ����
        //�ڶ�����������Ϊtrue�������޷���ʾ�ӽڵ㼴List����
        EditorGUILayout.PropertyField(_assetLstProperty, true);

        //��������Ƿ����޸�
        if (EditorGUI.EndChangeCheck()) {//�ύ�޸�
            _serializedObject.ApplyModifiedProperties();
        }
        

        //EditorGUILayout.TextField("Path:",materialPath,GUILayout.Height(300));

        //this.Repaint();//ʵʱˢ��

        //@materialPath = GUILayout.TextArea(null);
        if (GUILayout.Button("���泯��")) {
            Debug.Log("��ʼִ��...");
            foreach(GameObject looker in lookers) {
                looker.transform.LookAt(target.transform);
            }
            Debug.Log("ִ�н���");
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("���泯��")) {
            Debug.Log("��ʼִ��...");
            foreach (GameObject looker in lookers) {
                looker.transform.LookAt(target.transform);
                looker.transform.Rotate(Vector3.up, 180);
            }
            Debug.Log("ִ�н���");
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("ˮƽ�����泯��")) {
            Debug.Log("��ʼִ��...");
            foreach (GameObject looker in lookers) {
                looker.transform.LookAt(new Vector3(target.transform.position.x, looker.transform.position.y, target.transform.position.z));
            }
            Debug.Log("ִ�н���");
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("ˮƽ�����泯��")) {
            Debug.Log("��ʼִ��...");
            foreach (GameObject looker in lookers) {
                looker.transform.LookAt(new Vector3(target.transform.position.x, looker.transform.position.y, target.transform.position.z));
                looker.transform.Rotate(Vector3.up, 180);
            }
            Debug.Log("ִ�н���");
            AssetDatabase.Refresh();
        }
    }



    [ExecuteInEditMode]

    void OnSelectionChange() {
        this.Repaint();
    }

}
