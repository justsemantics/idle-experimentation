using UnityEngine;
using System.Collections;
using System.Collections.Generic;

    public struct mirrorRoomStruct
    {
        public GameObject baseRoom;
        public Transform playerStandin;
    }

public class mirrorRoom : MonoBehaviour {

    mirrorRoomStruct currentRoom;

    [SerializeField]
    GameObject prefab;

    [SerializeField]
    const int numReflections = 10;

    mirrorRoomStruct[] rooms = new mirrorRoomStruct[numReflections * numReflections];
    GameObject player;

    Matrix4x4 worldToLocal;

    bool updatePlayerReflections = false;

    public bool UpdatePlayerReflections
    {
        get
        {
            return updatePlayerReflections;
        }
        set
        {
            updatePlayerReflections = value;
        }
    }

    [ContextMenu("PREVIEW")]
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        int currentRoomIndex = 0;
	    for (int xNum = 0; xNum < numReflections; xNum++)
        {
            for (int zNum = 0; zNum < numReflections; zNum++)
            {
                GameObject reflection = Instantiate<GameObject>(prefab);
                Transform playerReflection = reflection.transform.Find("Player Stand-in");


                Vector3 newScale = new Vector3(1, 1, 1);
                if (xNum % 2 == 1)
                {
                    newScale.x = -1;
                }
                if (zNum % 2 == 1)
                {
                    newScale.z = -1;
                }

                reflection.transform.parent = transform;
                reflection.transform.localScale = newScale;
                reflection.transform.localPosition = new Vector3(xNum, 0, zNum);


                mirrorRoomStruct reflectedRoom = new mirrorRoomStruct();
                reflectedRoom.baseRoom = reflection;
                reflectedRoom.playerStandin = playerReflection;
                if (xNum == 0 && zNum == 0)
                {
                    reflection.SetActive(false);
                    playerReflection.gameObject.SetActive(true);
                    currentRoom = reflectedRoom;
                }

                reflectedRoom.baseRoom.GetComponent<mirrorRoomPrefab>().Index = currentRoomIndex;
                rooms[currentRoomIndex] = reflectedRoom;

                currentRoomIndex++;
            }
        }
	}
	
	void FixedUpdate () {
        if (UpdatePlayerReflections)
        {
            worldToLocal = currentRoom.baseRoom.transform.worldToLocalMatrix;

            foreach (mirrorRoomStruct curRoom in rooms)
            {
                Vector3 reflectionPos = worldToLocal.MultiplyPoint3x4(player.transform.position);
                //reflectionPos.x *= kv.Key.transform.localScale.x;
                //reflectionPos.z *= kv.Key.transform.localScale.z;
                curRoom.playerStandin.localPosition = reflectionPos;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            UpdatePlayerReflections = true;
            Debug.Log("player in field");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            //UpdatePlayerReflections = false;
            Debug.Log("Player leaving field");
        }
    }

    public void SetCurrentRoom(int index)
    {
        currentRoom = rooms[index];
    }
}
