using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace SimpleFrame.Tool
{
    public class MissingScriptsToolWindow : EditorWindow
    {
        [MenuItem("Tools/Missing Scripts Window")]
        public static void ShowWindow()
        {
            GetWindow(typeof(MissingScriptsToolWindow), false, "Missing Scripts GameObjects");
        }

        List<GameObject> missGamoObjList = new List<GameObject>();

        private void OnEnable()
        {
            SelectAssetMissingGameObjects(ref missGamoObjList);
        }

        private Vector2 scrollPosition = Vector2.zero;

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Box("丢失脚本物体列表:");
            if (GUILayout.Button("刷新Assets列表", GUILayout.Width(150)))
            {
                missGamoObjList.Clear();
                SelectAssetMissingGameObjects(ref missGamoObjList);
            }
            if (GUILayout.Button("刷新场景列表", GUILayout.Width(150)))
            {
                missGamoObjList.Clear();
                SelectSceneMissingGameObjects(ref missGamoObjList);
            }
            if (GUILayout.Button("清除无效脚本", GUILayout.Width(150)))
            {
                RemoveMissingGameObjects(ref missGamoObjList);
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            if (missGamoObjList.Count == 0)
            {
                GUILayout.Label("没有丢失脚本的物体");
                return;
            }

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            for (int i = 0; i < missGamoObjList.Count; i++)
            {
                GUILayout.BeginHorizontal("Box", GUILayout.Height(20));

                GUILayout.Label(i.ToString(), "AssetLabel", GUILayout.Width(40));
                GUILayout.Space(10);
                if (GUILayout.Button(missGamoObjList[i].name, "AnimLeftPaneSeparator"))
                {
                    GameObject selectTarget = missGamoObjList[i];
                    while (selectTarget.transform.parent != null)
                    {
                        selectTarget = selectTarget.transform.parent.gameObject;
                    }

                    EditorGUIUtility.PingObject(selectTarget);
                    Selection.activeObject = selectTarget;
                }

                GUILayout.EndHorizontal();
                GUILayout.Space(1);
            }

            GUILayout.EndScrollView();
        }

        private static void SelectAssetMissingGameObjects(ref List<GameObject> gamoObjList)
        {
            gamoObjList.Clear();

            string[] paths = Directory.GetFiles("Assets", "*.prefab", SearchOption.AllDirectories);
            for (int i = 0; i < paths.Length; i++)
            {
                if (EditorUtility.DisplayCancelableProgressBar("查找GameObject···", "数量 : " + i, (float)i / paths.Length))
                {
                    EditorUtility.ClearProgressBar();
                    break;
                }

                GameObject tempObj = AssetDatabase.LoadAssetAtPath<GameObject>(paths[i]);
                if (tempObj != null)
                {
                    Transform[] transArr = tempObj.GetComponentsInChildren<Transform>(true);
                    for (int j = 0; j < transArr.Length; j++)
                    {
                        Component[] components = transArr[j].GetComponents<Component>();
                        for (int k = 0; k < components.Length; k++)
                        {
                            if (components[k] == null)
                            {
                                if (!gamoObjList.Contains(transArr[j].gameObject))
                                    gamoObjList.Add(transArr[j].gameObject);
                            }
                        }
                    }
                }
            }

            EditorUtility.ClearProgressBar();
        }

        private static void RemoveMissingGameObjects(ref List<GameObject> gamoObjList)
        {
            if (gamoObjList.Count == 0)
                return;

            for (int i = 0; i < gamoObjList.Count; i++)
            {
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gamoObjList[i]);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            gamoObjList.Clear();
        }

        
        private static void SelectSceneMissingGameObjects(ref List<GameObject> gamoObjList)
        {
            gamoObjList.Clear();

            var allObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject));

            foreach (GameObject obj in allObjects)
            {
                Transform[] transArr = obj.GetComponentsInChildren<Transform>(true);
                for (int i = 0; i < transArr.Length; i++)
                {
                    if (EditorUtility.DisplayCancelableProgressBar("查找GameObject···", "数量 : " + i, (float)i / allObjects.Length))
                    {
                        EditorUtility.ClearProgressBar();
                        break;
                    }
                    
                    Component[] components = transArr[i].GetComponents<Component>();
                    for (int k = 0; k < components.Length; k++)
                    {
                        if (components[k] == null)
                        {
                            if (!gamoObjList.Contains(transArr[i].gameObject))
                                gamoObjList.Add(transArr[i].gameObject);
                        }
                    }
                }
            }
            EditorUtility.ClearProgressBar();
        }
    }
}