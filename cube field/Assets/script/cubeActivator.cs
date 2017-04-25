using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cubeActivator : MonoBehaviour {

    cubeField cf;

    [SerializeField]
    float innerRadius, outerRadius;

    [SerializeField]
    Color nearColor, farColor;

	// Use this for initialization
	void Start () {
        cf = FindObjectOfType<cubeField>();
    }
	
	// Update is called once per frame
	void Update () {
        List<GameObject> nearbyCubes = cf.getCubesNear(transform.position, outerRadius);

        foreach(GameObject g in nearbyCubes)
        {
            float distance = Vector3.Distance(transform.position, g.transform.position);

            if(distance > outerRadius)
            {
                g.transform.localScale = Vector3.zero;
            }
            else if(distance > innerRadius && distance < outerRadius)
            {
                float t = (distance - innerRadius) / (outerRadius - innerRadius); //normalized distance between radii

                g.transform.localScale = Vector3.one * (1 - t);
            }
            else
            {
                g.transform.localScale = Vector3.one;
            }
        }
	}
}
