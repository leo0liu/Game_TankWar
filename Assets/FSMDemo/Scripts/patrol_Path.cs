using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//处理路径
public class patrol_Path : MonoBehaviour
{
    public List<GameObject> pointGroup = new List<GameObject>();
    GameObject temp_point;
    const int pointNum = 5;//巡逻点数量
    string pointStr = "Point_";

    //实例化路径

    public void InstPoint()
    {
        pointGroup.Clear();
        for (int i=0;i<pointNum;++i)
        {
            temp_point = transform.Find(string.Format("{0}{1}",pointStr,i+1)).gameObject;
            pointGroup.Add(temp_point);
        }
    }
}
