using UnityEngine;
using System.Collections;

public class LightRotator : MonoBehaviour {

    [Range(.05f, 5f)]
    public float rotationSpeed;

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 90, 0) * rotationSpeed * Time.deltaTime, Space.World);
	}
}
