using UnityEngine;
using System.Collections;

public class movingPlatform : MonoBehaviour {

    public float x;
    public float y;
    public float z;
    public float speed;

	// Use this for initialization
    void Start()
    {

        Hashtable h = new Hashtable();
        h.Add("x", x);
        h.Add("y", y);
        h.Add("z", z);
        h.Add("speed", speed);
        h.Add("looptype", "pingPong");
        h.Add("easetype", "linear");

        iTween.MoveBy(gameObject, h);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
