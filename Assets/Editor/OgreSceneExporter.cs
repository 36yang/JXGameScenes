using UnityEngine;
using UnityEditor;
using NUnit.Framework;

using System.Runtime.InteropServices;  //调用c++托管的dll
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using OgreSceneExporterCore;


public class OgreSceneExporter : Editor
{

/*
    [MenuItem("new Menu/old/new new")]
    static void showDialog()
    {
        EditorUtility.DisplayDialog("title", "message", "ok");
    }

 */
    [MenuItem("OgreExpoter/导出场景")]
    static void exportOgreScene()
    {
        OgreSceneExporterCore.SceneExporterCoreDLL.exportOgreScene();
    }







}//class end
