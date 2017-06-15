using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

	void Start () {
        Destroy(gameObject,10f);
	}

    public void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

}
