using UnityEngine;
using System.Collections;

public class activateOnKey : MonoBehaviour {

    [SerializeField]
    GameObject target;

    Activateable aTarget;
	// Use this for initialization
	void Start () {
	    aTarget = target.GetComponent(typeof(Activateable)) as Activateable;
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            aTarget.Activate();
        }
	}
}
