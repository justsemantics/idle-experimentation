using UnityEngine;
using System.Collections;

public class cutsceneZone : MonoBehaviour {

    [SerializeField]
    public Transform CameraDock;

    Animator cameraAnimator;

    GameObject player;
    cutsceneCamera cutsceneCam;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        cutsceneCam = player.GetComponentInChildren<cutsceneCamera>();

        cameraAnimator = CameraDock.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            cutsceneCam.playScene(this);
        }
    }

    public void PlayAnim()
    {
        cameraAnimator.Play("Cutscene");
    }
}
