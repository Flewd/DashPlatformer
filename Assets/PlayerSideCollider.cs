using UnityEngine;
using System.Collections;

public class PlayerSideCollider : MonoBehaviour {

    public bool isRightSide;

    public GameObject PlayerObj;
    PlayerController playerController;


    // Use this for initialization
	void Start () {
        playerController = PlayerObj.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (isRightSide)
        {
            playerController.setRightSide(true);
        }
        else
        {
            playerController.setLeftSide(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
    }

    void OnTriggerExit(Collider other)
    {
        if (isRightSide)
        {
            playerController.setRightSide(false);
        }
        else
        {
            playerController.setLeftSide(false);
        }
    }
}
