using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPController : MonoBehaviour
{
	public float speed;
	public float jumpHeight;
	public LayerMask ground;
	public Transform feet;

	private Vector3 direction;
	private Rigidbody rbody;

	private float rotationSpeed = 1f;
	private float minY = -60f;
	private float maxY = 60f;
	private float rotationY = 0f;
	private float rotationX = 0f;

	private int count;
	public Text countText;
	public Text winText;

	public AudioSource collectSound;

	private bool gameOver;
	private bool won;
    // Start is called before the first frame update
	void Start()
	{
		won = false;
		gameOver = false;
		speed = 5.0f;
		jumpHeight = 2.0f;
		rbody = GetComponent<Rigidbody>();
		count = 0;
		SetCountText();
		winText.text = "";
		collectSound = GetComponent<AudioSource>();
	}

    // Update is called once per frame
	void Update()
	{
		direction = Vector3.zero;
		direction.x = Input.GetAxis("Horizontal");
		direction.z = Input.GetAxis("Vertical");
		direction = direction.normalized;
		if(direction.x != 0)
		rbody.MovePosition(rbody.position + transform.right * direction.x * speed * Time.deltaTime);
		if(direction.z != 0)
		rbody.MovePosition(rbody.position + transform.forward * direction.z * speed * Time.deltaTime);

		rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;
		rotationY += Input.GetAxis("Mouse Y") * rotationSpeed;
		rotationY = Mathf.Clamp(rotationY, minY, maxY);
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

		bool isGrounded = Physics.CheckSphere(feet.position, 0.1f, ground, QueryTriggerInteraction.Ignore);
		if(Input.GetButtonDown("Jump") && isGrounded) {
			rbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Pick Up") && !gameOver){
			other.gameObject.SetActive(false);
			count = count + 1;
			collectSound.Play();
			SetCountText();
		}
		if(other.gameObject.CompareTag("Enemy") && !won){
			gameOver = true;
			winText.text = "Game Over!";
		}
	}
	void SetCountText(){
		countText.text = "Score: " + count.ToString();
		if(count >= 12){
			won = true;
			winText.text = "You Win!";
		}
	}
}
