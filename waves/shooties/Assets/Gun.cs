using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    [SerializeField]
    GameObject bulletPrefab;

    GameObject currentBullet;
    Player currentPlayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Fire(Player shooter)
    {
        GameObject bulletGO = Instantiate<GameObject>(bulletPrefab);
        bulletGO.transform.localPosition = transform.position;
        bulletGO.transform.localRotation = transform.rotation;
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.Fire(shooter, transform.forward);
    }
}
