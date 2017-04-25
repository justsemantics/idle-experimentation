using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    [SerializeField]
    public float InitialSpeed = 5;
    [SerializeField]
    public float Damage = 1;

    public GameObject shooter;
    Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
	}
	
	void FixedUpdate () {
        transform.position += velocity * Time.fixedDeltaTime;
	}

    public void Fire(GameObject _shooter, Vector3 heading)
    {
        shooter = _shooter;
        velocity = heading * InitialSpeed;
    }
}
