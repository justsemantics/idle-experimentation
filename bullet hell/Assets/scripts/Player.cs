using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField]
    Gun gun;

    [SerializeField]
    float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1"))
        {
            gun.Fire(this.gameObject);
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            transform.position += new Vector3(horizontal * speed, vertical * speed, 0) * Time.deltaTime;
        }
	}


}
