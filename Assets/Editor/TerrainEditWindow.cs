using UnityEngine;
using System.Collections;
using UnityEditor;
using OgreSceneExporterCore;

/// <summary>
/// 自定义的编辑器窗口
/// </summary>
public class TerrainEditWindow : EditorWindow {

    TerrainEditCoreDLL editCore = new TerrainEditCoreDLL();

   
    //string myString = "Hello World !"; // 文本内容 
    //bool myBool = true;    // 复选框状态
    int terrainIndex = 0; // 滑动条的值，第几块地图
    int textureIndex = 0; //滑动条的值，第几层贴图
    float minAlpha = 1.0f; //小于该alpha值的通道会被清理
   
   

   

    [MenuItem("OgreExpoter/地形贴图编辑")]
    public static void ShowWindow()
    {
        // 显示某个编辑器窗口。传参即是要显示的窗口类型（类名）
        EditorWindow.GetWindow(typeof(TerrainEditWindow));
    }


    TerrainEditWindow()
    {
        this.titleContent = new GUIContent("地形贴图清理");    
    }

    public void Awake()
    {
       
    }

    //更新
    void Update()
    {

    }

    void OnFocus()
    {
        // Debug.Log("当窗口获得焦点时调用一次");
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
        {
            ExportSetting seting = obj.GetComponent<ExportSetting>();
            if (seting != null && seting.isActiveAndEnabled)  //需要导出
            {
                Terrain terrain = obj.GetComponent<Terrain>();
                if (terrain != null)
                {
                    editCore.terrainObj = obj;
                    TerrainGrid terrainGrid = obj.GetComponent<TerrainGrid>();
                    if (terrainGrid != null)
                    {
                        editCore.grids = (int)terrainGrid.Grid_NxN;
                        editCore.grids = (int)System.Math.Pow(2, editCore.grids);
                        editCore.groupEnabled = true;
                        break;
                    }

                }
            }
        }
    }

    void OnLostFocus()
    {
       // Debug.Log("当窗口丢失焦点时调用一次");
    }

    void OnHierarchyChange()
    {
       // Debug.Log("当Hierarchy视图中的任何对象发生改变时调用一次");
    }

    void OnProjectChange()
    {
        //Debug.Log("当Project视图中的资源发生改变时调用一次");
    }

    void OnInspectorUpdate()
    {
        //Debug.Log("窗口面板的更新");
        //这里开启窗口的重绘，不然窗口信息不会刷新
        //this.Repaint();
    }

    void OnSelectionChange()
    {
        //当窗口出去开启状态，并且在Hierarchy视图中选择某游戏对象时调用
        foreach (Transform t in Selection.transforms)
        {
            //有可能是多选，这里开启一个循环打印选中游戏对象的名称
           // Debug.Log("OnSelectionChange" + t.name);
        }
    }

    void OnDestroy()
    {
        //Debug.Log("当窗口关闭时调用");
    }



    void OnGUI()
    {
        // 文本
        GUILayout.Label("说明:当某块地形中出现了多余的贴图通道，并且透明度十分小的时候，\n人眼不能分辨可借助下面的条件进行清理。");
        // 可以编辑，编辑后用同一个变量保存结果
      //  myString = EditorGUILayout.TextField("这是文本", myString);
    
        // 开启一组选项
        editCore.groupEnabled = EditorGUILayout.BeginToggleGroup("清理参数设置", editCore.groupEnabled);
        // 复选框
       // myBool = EditorGUILayout.Toggle("这是复选框", myBool);
        // 滑动条
        terrainIndex = (int)EditorGUILayout.Slider("地形编号：", terrainIndex, -1, editCore.grids * editCore.grids - 1);
        //滑动条
        Terrain terrain = editCore.terrainObj.GetComponent<Terrain>();
        int textureNum = 0;
        if (terrain != null)
        {
            textureNum = terrain.terrainData.alphamapLayers;
        }
        textureIndex = (int)EditorGUILayout.Slider("贴图编号：", textureIndex, -1, textureNum - 1);
        minAlpha =  EditorGUILayout.Slider("alpha阈值(小于的通道被清理)：", minAlpha, 0.0f, 1.0f);
        //添加名为"Save Bug with Screenshot"按钮，用于调用SaveBugWithScreenshot() 函数
        if (GUILayout.Button("清理指定条件的通道"))
        {
            clearAlphaMap(terrainIndex, textureIndex, minAlpha);
        }
        if (GUILayout.Button("填充指定条件的通道"))
        {
            fillAlphaMap(terrainIndex, textureIndex, minAlpha);
        }
        if (GUILayout.Button("保存指定编号的地形"))
        {
            saveTerrain(terrainIndex);
        }
        if (GUILayout.Button("恢复指定编号的地形"))
        {
            recoveryTerrain(terrainIndex);
        }
       
        // 结束这组选项
        EditorGUILayout.EndToggleGroup();

        GUILayout.Label("帮助:当某块地形编号为-1时会作用于所有地形块，当贴图编号为-1时会作用于所有贴图。\n恢复指定地形只能恢复最近4次保存的地形");

    }



    void clearAlphaMap(int terrainIndex, int textureIndex, float alpha)
    {
        editCore.clearAlphaMap(terrainIndex, textureIndex, alpha);
    }


     void fillAlphaMap(int terrainIndex, int textureIndex, float alpha)
    {
         editCore.fillAlphaMap(terrainIndex, textureIndex, alpha);
    }


    void saveTerrain(int terrainIndex)
    {
        editCore.saveTerrain(terrainIndex);

     }


    void recoveryTerrain(int terrainIndex)
    {
        editCore.recoveryTerrain(terrainIndex);       
    }


}