using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    int rotateSpeed = 3;
    int moveSpeed = 9;


 
    void Update()
    {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        if (ver<0)
        {
            hor = -hor;
        }
        Vector3 _vt = new Vector3(0,0,ver);
        transform.Translate(_vt*Time.deltaTime*moveSpeed);
        transform.Rotate(new Vector3(0,hor,0)*rotateSpeed);

    }


}
