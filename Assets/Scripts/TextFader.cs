using UnityEngine;
using System.Collections;

public class TextFader : MonoBehaviour {

    public CanvasGroup introText;



	// Use this for initialization
	void Start () {
        introText = gameObject.GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
