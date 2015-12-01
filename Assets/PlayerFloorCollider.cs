using UnityEngine;
using System.Collections;

public class PlayerFloorCollider : MonoBehaviour {

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
        playerController.setIsGrounded(true);
    }
    void OnTriggerExit(Collider other)
    {
        playerController.setIsGrounded(false);
    }
}
