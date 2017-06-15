using UnityEngine;
using System.Collections;

//状态机基类
public class FSMBase : MonoBehaviour
{
    protected Transform PlayerTank;//玩家坦克
    protected Vector3 moveTargetPoint;//移动目标
    protected GameObject[] pointList;//移动目标点集合
    protected float shootRate;//射击频率
    protected float elapsedTime;//发射间隔时间

    //实例化的抽象方法
    protected virtual void Inst()
    {

    }
    //状态刷新的抽象方法
    protected virtual void FSMUpdate()
    {

    }

    void Start()
    {
        Inst();
    }

    void Update()
    {
        FSMUpdate();
    }
}
