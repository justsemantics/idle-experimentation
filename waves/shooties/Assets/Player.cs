using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField]
    Gun gun;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            gun.Fire(this);
        }
	}


}
