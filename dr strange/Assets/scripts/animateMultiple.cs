using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animateMultiple : MonoBehaviour {

    [SerializeField]
    int maxClones = 1;
    [SerializeField]
    double timePerLoop = 1;

    GameObject target;
    double timePerSpawn = 1;
    double timeLastSpawn = 0;
    float speed = 1;
    int numClones = 1;

	// Use this for initialization
	void Start () {
        timeLastSpawn = Time.time;
        speed = 1 / (float)timePerLoop;
        timePerSpawn = timePerLoop / maxClones;
        target = transform.GetChild(0).gameObject;
        setSpeed(target);
	}
	
	// Update is called once per frame
	void Update () {
        if (numClones < maxClones)
        {
            if (Time.time > timeLastSpawn + timePerSpawn)
            {
                timeLastSpawn = Time.time;
                setSpeed(Instantiate(target, transform, false));
                numClones++;
            }
        }
	}

    void setSpeed(GameObject g)
    {
        Animator anim = g.GetComponent<Animator>();
        anim.speed = speed;
    }
}
