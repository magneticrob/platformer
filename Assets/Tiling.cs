using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offsetX = 2;
	public bool hasARightBuddy = false;
	public bool hasALeftBuddy = false;
	public bool reverseScale = false;

	private float spriteWidth = 0f;
	private Camera cam;
	private Transform myTransform;

	void Awake() {
		cam = Camera.main;
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = spriteRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasALeftBuddy || !hasARightBuddy) {
			float camHorizontalExtent = cam.orthographicSize * Screen.width / Screen.height;
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtent;
			float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtent;

			if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && !hasARightBuddy) {
				makeNewBuddy (1);
				hasARightBuddy = true;
			} else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && !hasALeftBuddy) {
				makeNewBuddy (-1);
				hasALeftBuddy = true;
			}
		}
	}

	void makeNewBuddy(int rightOrLeft) {
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
		Transform newBuddy = Instantiate (myTransform, newPosition, myTransform.rotation) as Transform;

		if (reverseScale) {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
		}

		newBuddy.parent = myTransform.parent;

		if (rightOrLeft > 0) {
			newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
		} else if (rightOrLeft < 0) {
			newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
		}
	}
}
