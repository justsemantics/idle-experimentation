using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [SerializeField]
    float Health = 5;
    TargetingSystem targetingSystem;

	// Use this for initialization
	void Start () {
        targetingSystem = GetComponentInChildren<TargetingSystem>();
	}
	
	// Update is called once per frame
	void Update () {
       targetingSystem.Aim();
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet") {
            Bullet b = other.gameObject.GetComponent<Bullet>();
            if (b.shooter != this.gameObject)
            {
                TakeDamage(b.shooter, b.Damage);
                Destroy(other.gameObject);
            }
        }
    }

    void TakeDamage(GameObject source, float amount)
    {
        Debug.Log(source.name + " did " + amount + " damage to " + name);
        Health -= amount;
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
