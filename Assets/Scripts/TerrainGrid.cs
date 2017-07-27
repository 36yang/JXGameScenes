using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using TerrainGridCore;


[ExecuteInEditMode]
//[InitializeOnLoad]

public class TerrainGrid : MonoBehaviour {

     public  enum TileNum
    {
        一 = 0,
        二,
        四,
        八,
        十六
    }

    public TileNum Grid_NxN;
    public bool 暂停更新 = false; //暂停更新
    public bool 显示编号 = false;
    public TerrainGridCoreDLL terrainGridCore = null;

    TerrainGrid()
    {
        
    }


    void Awake()
    {
       

    }



    // Use this for initialization
	void Start ()
    {
        terrainGridCore = new TerrainGridCoreDLL(gameObject);

        gameObject.AddComponent(typeof(MeshFilter));
        gameObject.AddComponent(typeof(MeshRenderer));
        MeshRenderer ms = gameObject.GetComponent<MeshRenderer>();
        ms.receiveShadows = false;

        EditorApplication.update += MyUpdate;
        
	}
	
	// Update is called once per frame
	void Update () 
    {


	}


    void MyUpdate()   //不用运行和挂载，在编辑器模式下每帧运行
    {
        if (!暂停更新)
        {
            terrainGridCore.MyUpdate((int)Grid_NxN);           
        }
    }


    public void OnGUI()
    {
        if (terrainGridCore.showErrorWindow)
        {
            terrainGridCore.showErrorWindow = false;
            Rect wr = new Rect(0, 0, 500, 500);
            EditorGUI.HelpBox(wr, "警告:该地形块内的贴图数目大于5张，超出5张的贴图将无法在游戏里显示\n请将地形划分成更多的网格来解决此问题", MessageType.Error);


            Rect wr2 = new Rect(0, 0, 500, 500);
            // EditorGUI.HelpBox(wr, "警告:该地形块内的贴图数目大于5张，超出5张的贴图将无法在游戏里显示\n请将地形划分成更多的网格来解决此问题", MessageType.Error);                                
            GUIContent[] opts = new GUIContent[1];
            opts[0] = new GUIContent("警告:该地形块内的贴图数目大于5张，超出5张的贴图将无法在游戏里显示\n请将地形划分成更多的网格来解决此问题");
            EditorGUI.Popup(wr2, 0, opts);
        
        }

        Debug.LogError("OnGUI");

    }


}






[CustomEditor( typeof( TerrainGrid ) )]


public class DrawLineEditor : Editor
{
    // draw lines between a chosen game object
    // and a selection of added game objects


    TerrainGrid mTerrainGrid = null;
    Terrain mTerrain = null;

    public DrawLineEditor()
    {

    }

    public void OnSceneGUI()
    {
       
        
        // get the chosen game object
        TerrainGrid t = target as TerrainGrid;
        if( t == null)
            return;
   
        if (mTerrainGrid == null)
        {
            foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
            {
                Terrain terrain = obj.GetComponent<Terrain>();
                if (terrain != null)
                {
                    mTerrainGrid = obj.GetComponent<TerrainGrid>();
                    mTerrain = terrain;
                    break;
                }          
            }
        }

        if (mTerrainGrid == null) return;

        if (mTerrainGrid.显示编号 == false) return;

         
        int grids = (int)mTerrainGrid.Grid_NxN;
        grids = (int)System.Math.Pow(2, grids);
        float skipX = mTerrain.terrainData.size.x / grids;
        float skipY = mTerrain.terrainData.size.z / grids;

        for(int y = 0; y< grids; y++)
        {
            for(int x = 0; x< grids; x++)
            {
                int terrainNumber = y * grids + x;
                Vector3 center = new Vector3(t.transform.position.x + x * skipX + skipX / 2.0f, 0, t.transform.position.z + y * skipY + skipY / 2.0f);
                Handles.DrawLine(center, new Vector3(center.x, 100, center.z));
                Handles.Label(new Vector3(center.x, 103, center.z), terrainNumber.ToString());
            }

        }



        // grab the center of the parent
       // Vector3 center = t.transform.position;
        //Handles.DrawLine(new Vector3(0, 0, 0), new Vector3(100, 200, 200));
       // Handles.Label(new Vector3(100, 200, 200), "2");
    }
}