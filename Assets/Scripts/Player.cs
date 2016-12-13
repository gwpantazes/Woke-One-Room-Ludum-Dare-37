using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public GameObject clock;
    private Clock clockScript;

    public GameObject arm;
    public GameObject eyes;



	// Use this for initialization
	void Start () {
        clockScript = clock.GetComponent<Clock>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            clockScript.alarmOff();
        }
	}

    public void openEyes() {
        eyes.SetActive(true);
    }

    public void closeEyes() {
        eyes.SetActive(false);
    }

    public void useArm()
    {

    }
}
