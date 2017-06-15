using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour
{
    //单例
    private static GameMgr Inst;
    public static GameMgr Instance
    {
        get
        {
            return Inst;
        }

    }

    void Awake()
    {
        Inst = this;
        InstMgrInfo();
    }

    public patrol_Path _paths;
    GameObject _parent;//坦克父物体

    void InstMgrInfo()
    {
        _paths = GameObject.FindGameObjectWithTag("patrol_path").GetComponent<patrol_Path>();
        _paths.InstPoint();
        _parent = GameObject.FindGameObjectWithTag("ComTankParent").gameObject;
    }
}
