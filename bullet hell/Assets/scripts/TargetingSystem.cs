using UnityEngine;
using System.Collections;


public class TargetingSystem : MonoBehaviour {

    Transform target;

	// Use this for initialization
	void Start () {
        target = FindObjectOfType<Player>().transform;	    
	}
	
    public void Aim()
    {
        Vector3 offset = target.position - transform.position;
        float angle = Mathf.Atan2(offset.x, offset.y);
        transform.localRotation = Quaternion.Euler(0, Mathf.Rad2Deg * angle, 0);
    }
}
