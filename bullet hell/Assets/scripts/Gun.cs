using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    [FMODUnity.EventRef]
    public string shoot = "event:/Shoot";
    FMOD.Studio.EventInstance shootEvent;
    FMOD.Studio.ParameterInstance bulletType;

    [SerializeField]
    GameObject bulletPrefab;

    GameObject currentBullet;
    GameObject shooter;

    [SerializeField]
    float RoF;

    float shotTime;
    float lastShotTime;

    public bool Firing
    {
        get; set;
    }



	// Use this for initialization
	void Start () {
        Firing = false;
        shotTime = 1 / RoF;
        shootEvent = FMODUnity.RuntimeManager.CreateInstance(shoot);
        shootEvent.getParameter("Bullet Type", out bulletType);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(shootEvent, this.transform, GetComponentInParent<Rigidbody>());
	}
	
	// Update is called once per frame
	void Update () {
        if (Firing)
        {
            if(Time.time - lastShotTime >= shotTime)
            {
                actullyFire();
                lastShotTime = Time.time;
                Firing = false;
            }
        }
	}

    public void Fire(GameObject _shooter)
    {
        shooter = _shooter;
        Firing = true;
    }

    void actullyFire()
    {
        if(shooter.tag == "player")
        {
            bulletType.setValue(.7f);
        }
        GameObject bulletGO = Instantiate<GameObject>(bulletPrefab);
        bulletGO.transform.localPosition = transform.position;
        bulletGO.transform.localRotation = transform.rotation;
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.Fire(shooter, transform.up);
        shootEvent.start();
    }
}
