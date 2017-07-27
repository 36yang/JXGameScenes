using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class ShowRegion : MonoBehaviour {


    public GameObject wallPrefab = null;

    public  bool regionEnabled = false;
    public  bool showRegion = false;
    public  Rect sceneRegion = new Rect(-250.0f, -250.0f, 500.0f, 500.0f);
    public  int gridX = 1;
    public  int gridY = 1;

    private Rect mSceneRect = new Rect(-250.0f, -250.0f, 500.0f, 500.0f);
    private int X = 1;
    private int Y = 1;
    GameObject mObjectGroup = null;

   
  
	// Use this for initialization
	void Start () 
    {
     
        EditorApplication.update += MyUpdate;    
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}


    void MyUpdate()
    {
       
        if(showRegion)
        {
            if(mObjectGroup == null)
            {

                mObjectGroup = new GameObject("showRegionGroup");
                GameObject XObjs = new GameObject("XObjs");
                GameObject YObjs = new GameObject("YObjs");
                XObjs.transform.parent = mObjectGroup.transform;
                YObjs.transform.parent = mObjectGroup.transform;
            

                float xSkip = mSceneRect.width / X;
                float ySkip = mSceneRect.height / Y;

                for(int i=0; i<= X; i++)
                {
                    //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    
                    GameObject go = Instantiate(wallPrefab);
                    go.transform.localScale = new Vector3(mSceneRect.height, 20, 0.01f);
                    go.transform.Rotate(new Vector3(0, 90, 0));
                    go.transform.position = new Vector3(mSceneRect.x + i * xSkip, 10, mSceneRect.y + mSceneRect.height / 2.0f);
                    go.transform.parent = XObjs.transform;
                }

                for(int i=0; i<= Y; i++)
                {
                    //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    GameObject go = Instantiate(wallPrefab);
                    go.transform.localScale = new Vector3(mSceneRect.width, 20, 0.01f);
                    go.transform.position = new Vector3(mSceneRect.x + mSceneRect.width / 2.0f, 10, mSceneRect.y + i * ySkip);
                    go.transform.parent = YObjs.transform;
                }

            }
            
            if(X != gridX || Y != gridY || mSceneRect.x != sceneRegion.x || mSceneRect.y != sceneRegion.y 
                || mSceneRect.width != sceneRegion.width || mSceneRect.height != sceneRegion.height)
            {

                X = gridX; Y = gridY;
                mSceneRect.x = sceneRegion.x;  mSceneRect.y = sceneRegion.y;
                mSceneRect.width = sceneRegion.width; mSceneRect.height = sceneRegion.height;

                GameObject.DestroyImmediate(mObjectGroup);
                mObjectGroup = null;
            }

        }
        else
        {

            if(mObjectGroup != null)
            {
                GameObject.DestroyImmediate(mObjectGroup);
                mObjectGroup = null;
            }
           

        }


    }
}
