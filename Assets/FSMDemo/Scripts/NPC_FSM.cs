using UnityEngine;
using System.Collections;

public class NPC_FSM : FSMBase
{
    float moveSpeed = 6;
    float curRotSpeed = 5f;//旋转速度
    Transform endPos;
    int pointIndex = 0;//索引

    public FSMSTATE curstate;//当前的状态
    Transform pool;//子弹的对象池
    GameObject bullet;//子弹的预制
    Transform firePoint;//开火点
   
    //状态枚举
    public enum FSMSTATE
    {
        NONE,
        PATROL,//巡逻
        CHASE,//追踪
        ATTACK//攻击
    }
    //复写基类方法
    protected override void Inst()
    {
        base.Inst();
        InstTank();
    }

    protected override void FSMUpdate()
    {
        base.FSMUpdate();
        switch (curstate)
        {
            case FSMSTATE.PATROL:
                Patroling();
                break;
            case FSMSTATE.CHASE:
                Chaseing();
                break;
            case FSMSTATE.ATTACK:
                Attacking();
                break;
        }
        elapsedTime += Time.deltaTime;
    }
    //初始化
    void InstTank()
    {
        PlayerTank = GameObject.FindGameObjectWithTag("Player").transform;
        shootRate = 1.5f;
        elapsedTime = 0;
        pool = GameObject.FindGameObjectWithTag("GameobjPool").transform;
        bullet = pool.Find("CompleteShell").gameObject;
        firePoint = transform.Find("TankRenderers/TankTurret/fire");
        curstate = FSMSTATE.PATROL;
        transform.position = GameMgr.Instance._paths.pointGroup[pointIndex].transform.position;
        pointIndex++;
        endPos = GameMgr.Instance._paths.pointGroup[pointIndex].transform;

    }

    void Patroling()
    {
        //处理坦克巡逻的逻辑
        transform.position = Vector3.MoveTowards(transform.position ,endPos.position ,Time.deltaTime*moveSpeed);
        transform.LookAt(endPos.position);
        float dist = Vector3.Distance(transform.position ,endPos.position);
        if (dist<0.3)
        {
            pointIndex++;
            pointIndex %= GameMgr.Instance._paths.pointGroup.Count;
            endPos = GameMgr.Instance._paths.pointGroup[pointIndex].transform;
        }
        //如果和玩家的距离只有20f ,状态切换
        if (Vector3.Distance(transform.position,PlayerTank.position )<=20f)
        {
            curstate = FSMSTATE.CHASE;//切换追踪状态
        }
    }
    // 追踪
    void Chaseing()
    {
         //丢失玩家,重置为巡逻
        if (Vector3.Distance(transform.position ,PlayerTank.position)>20f)
        {
            curstate = FSMSTATE.PATROL;
        }

        //射击
        if (Vector3.Distance(transform.position ,PlayerTank.position )<10f)
        {
            curstate = FSMSTATE.ATTACK;
        }
        moveTargetPoint = PlayerTank.position;
        //旋转值
        Quaternion targetRot = Quaternion.LookRotation(moveTargetPoint - transform.position);// B点-A点,获得一个向量的距离,方法可以让物体以本身前方的方向注视的转向目标点
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * curRotSpeed);//获得一个转向的插值,并旋转过去.
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    void Attacking()
    {
       
        float dist = Vector3.Distance(transform.position,PlayerTank.position);

        if (dist>20)
        {
            curstate = FSMSTATE.PATROL;
        }

        //范围内追逐
        if (dist>=10 && dist<=20)
        {
            curstate = FSMSTATE.CHASE;
        }

       

        // transform.Rotate(Vector3.up,Vector3.Angle(transform.position,moveTargetPoint));
        if (dist<10)
        {
           

            Quaternion targetRot = Quaternion.LookRotation(moveTargetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * curRotSpeed);
            transform.LookAt(PlayerTank.position);
            Debug.Log(PlayerTank.position);
            Attack();
        }







    }


     void Attack()
    {
      
        if (elapsedTime>=shootRate)
        {
            
            GameObject _bullet = Instantiate(bullet,firePoint.position ,firePoint.rotation)as GameObject;
            _bullet.AddComponent<Shell>();
            _bullet.GetComponent<Rigidbody>().velocity=_bullet.transform.forward*20f;
            elapsedTime = 0f;

        }
       
    }
}

