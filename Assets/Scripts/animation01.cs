using UnityEngine;
using System.Collections;

public class animation01 : MonoBehaviour {

	public float x, y;
	public Animator anim;
	public bool walled = false;
	private float playerx , playery;

	// Update is called once per frame

	void Start(){
		anim = GetComponent<Animator>();
		playerx = this.gameObject.transform.localScale.x;
		playery = this.gameObject.transform.localScale.y;
	}
	void Update (){

		x = Input.GetAxis("Horizontal");
		y = Input.GetAxis("Vertical");
		anim.SetFloat ("x",x);
		anim.SetFloat ("y",y);
		if (x >= 0) {
			this.gameObject.transform.localScale = new Vector3 (-1 * playerx, playery, 1);

		} else if (x < 0) {
			this.gameObject.transform.localScale = new Vector3 (playerx, playery, 1);

		}
		this.gameObject.transform.position += new Vector3 (x, 0, 0) *Time.deltaTime;
		this.gameObject.transform.position += new Vector3 (0, y, 0) *Time.deltaTime;
	}
		
}
