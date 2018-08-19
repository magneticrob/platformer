using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float fireRate = 0f;
	public float damage = 10f;
	public float bulletTrailSpawnRate = 10;
	public bool showRayTrace = false;
	public LayerMask whatToHit;
	public Transform bulletTrailPrefab;
	public Transform muzzleFlashPrefab;

	private float timeToFire = 0f;
	private float timeToSpawnBulletTrail = 0;
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

		if (Time.time >= timeToSpawnBulletTrail) {
			CreateBulletTrail ();
			timeToSpawnBulletTrail = Time.time + 1 / bulletTrailSpawnRate;
		}

		if (showRayTrace) {
			Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition) * 100);
		}

		if (hit.collider != null) {

			if (showRayTrace) {
				Debug.DrawLine (firePointPosition, hit.point, Color.red);
			}

			Debug.Log ("We hit" + hit.collider.name + " and we did " + damage + " damage");
		}
	}

	void CreateBulletTrail() {
		Instantiate (bulletTrailPrefab, firePoint.position, firePoint.rotation);
		bulletTrailPrefab.GetComponent<LineRenderer>().sortingLayerName = "Player";

		Transform muzzleFlash = Instantiate (muzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		muzzleFlash.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
		muzzleFlash.parent = firePoint;
		float size = Random.Range (0.6f, 0.9f);
		muzzleFlash.localScale = new Vector3 (size, size, size);
		Destroy (muzzleFlash.gameObject, 0.02f);
	}
}
