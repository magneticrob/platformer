using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float fireRate = 0f;
	public float damage = 10f;
	public LayerMask whatToHit;

	private float timeToFire = 0f;
	private Transform firePoint;

	void Awake() {
		firePoint = transform.FindChild ("FirePoint");
		if (firePoint == null) {
			Debug.LogError ("No firePoint!");
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")) {
				shoot ();
			}
		} else {
			if (Input.GetButton("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1 / fireRate;
				shoot ();
			}
		}
	}

	void shoot() {
		float mouseX = Camera.main.ScreenToWorldPoint (Input.mousePosition).x;
		float mouseY = Camera.main.ScreenToWorldPoint (Input.mousePosition).y;
		Vector2 mousePosition = new Vector2 (mouseX, mouseY);

		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);

		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
		Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition) * 100);

		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);
			Debug.Log ("We hit" + hit.collider.name + " and we did " + damage + " damage");
		}
	}
}
