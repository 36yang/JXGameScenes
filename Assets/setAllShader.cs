using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setAllShader : MonoBehaviour {

	// Use this for initialization
	void Start () {

        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
        {
            MeshRenderer mr = obj.GetComponent<MeshRenderer>();
            if (mr != null) 
            {
               // mr.sharedMaterial.shader = Shader.Find("Custom/defaultShader");
                obj.AddComponent<MeshCollider>();
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
