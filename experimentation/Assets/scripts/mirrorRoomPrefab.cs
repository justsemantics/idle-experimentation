using UnityEngine;
using System.Collections;

public class mirrorRoomPrefab : MonoBehaviour {
    GameObject player;
    int index;
    public int Index
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
        }
    }
    mirrorRoom mom;
    public mirrorRoom Mom
    {
        get
        {
            return mom;
        }
        set
        {
            mom = value;
        }
    }
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other == player)
        {
            Mom.SetCurrentRoom(Index);
        }
    }
}
