using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    [SerializeField]
    public float InitialSpeed = 500;
    [SerializeField]
    public float Damage = 1;
    [SerializeField]
    public float Lifespan = 5;

    public Player shooter;
    Vector3 velocity = Vector3.zero;
    Rigidbody rb = null;

    bool ready = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb.velocity);
        ready = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (ready)
        {
            rb.velocity = velocity;
            ready = false;
        }
	}

    public void Fire(Player _shooter, Vector3 heading)
    {
            shooter = _shooter;
            velocity = heading * InitialSpeed;     
    }
}
