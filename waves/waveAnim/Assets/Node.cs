using UnityEngine;
using System.Collections;
[RequireComponent(typeof (Animator))]
public class Node : MonoBehaviour {

    Animator anim;
    bool activated = false;
    public bool Activated
    {
        get { return activated; }
        private set { activated = value; }
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetPosition(float pos)
    {
        //transform.localRotation = Quaternion.Euler(90 * pos, 0, 0);
        anim.Play("move1", 0, (pos+1)/2);
        anim.speed = 0;
    }

    public void Activate()
    {
        if (!Activated)
        {
            //Activated = true;
            anim.SetTrigger("Activate");
        }
    }
}
