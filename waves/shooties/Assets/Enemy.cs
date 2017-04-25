using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [SerializeField]
    float Health = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet") {
            Debug.Log("BULLET HIT");
            Bullet b = other.gameObject.GetComponent<Bullet>();
            TakeDamage(b.shooter, b.Damage);
            Destroy(other.gameObject);
        }
    }

    void TakeDamage(Player source, float amount)
    {
        Debug.Log(source.name + " did " + amount + " damage to " + name);
        Health -= amount;
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
