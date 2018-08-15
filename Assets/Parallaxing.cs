using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds;
	private float[] parallaxScales;
	public float smoothing = 1f;

	private Transform cam;
	private Vector3 previousCameraPosition;

	void Awake() {
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		previousCameraPosition = cam.position;

		parallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales [i] = backgrounds [i].position.z * -1;
				
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < backgrounds.Length; i++) {
			float parallax = (previousCameraPosition.x - cam.position.x) * parallaxScales[i];

			float backgroundTargetXPos = backgrounds [i].position.x + parallax;
			Vector3 targetPosition = new Vector3 (backgroundTargetXPos, backgrounds[i].position.y, backgrounds[i].position.z);
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPosition, smoothing * Time.deltaTime);
		}

		previousCameraPosition = cam.position;
	}
}
