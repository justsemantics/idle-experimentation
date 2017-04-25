using UnityEngine;
using System.Collections;

public class animateClones : MonoBehaviour {

    [SerializeField]
    GameObject clone;

    [SerializeField]
    float spawnRate = 1;

    [SerializeField]
    int maxClones = 20;

    int numClones = 0;
    float lastSpawn = 0;

    // Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    if (Time.fixedTime - lastSpawn > 1 / spawnRate && numClones < maxClones)
        {
            lastSpawn = Time.fixedTime;

            GameObject newClone = (GameObject)Instantiate(clone, transform);

            newClone.SetActive(true);

            numClones++;
        }
	}
}
