using UnityEngine;
using System.Collections;

public class FireControl : MonoBehaviour {

    [SerializeField]
    float RateOfFire;

    float timeBetweenShots;
    float accumulatedTime = 0;
    Gun gun;

	// Use this for initialization
	void Start () {
        timeBetweenShots = 1 / RateOfFire;

        gun = GetComponentInChildren<Gun>();
	}
	
	// Update is called once per frame
	void Update () {
        accumulatedTime += Time.deltaTime;
        if(accumulatedTime >= timeBetweenShots)
        {
            accumulatedTime = 0;
            gun.Fire(this.gameObject);
        }
	}
}
