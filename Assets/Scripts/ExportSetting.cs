using UnityEngine;
using System.Collections;

public class ExportSetting : MonoBehaviour {

 public  enum SceneObjType
    {
        自动,
        模型,
        声音,
        碰撞体,
        水面,
        空节点
    }

    public enum WaterType
    {
        普通纹理 = 0,
        菲涅尔书面简化反射,
        菲涅尔水面反射,
        菲涅尔水面反射和折射
    }


    public string 模型名字;
    public float 模型单位缩放 = 1.0f;
    public int 数字标记 = -1;
    public SceneObjType 物体类型;
    public bool 模型z轴修正 = false;
    public WaterType 水面质量;
    public string 水面材质名;

    private int id;  //用来唯一标识一个物体，以确定父子关系

    public int getId()
    {
        return id;
    }

    public void setId(int id)
    {
        this.id = id; 
    }


	// Use this for initialization
	void Start () {
        //不能在这里初始化，会覆盖编辑器里设置的值
        //ResourceFile = "";
       //id = -1;
	}


	
}
