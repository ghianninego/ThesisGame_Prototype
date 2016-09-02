using UnityEngine;
using System.Collections;

public class animation01 : MonoBehaviour {

	public float x, y;
	public Animator anim;
	// Update is called once per frame

	void Start(){
		anim = GetComponent<Animator>();
	}
	void Update (){

//		for (int i = 0; i < Things.thingsToGet.Length; i++) {
	//		Debug.Log (Things.thingsToGet[i].name);
	//	}


		x = Input.GetAxis("Horizontal");
		y = Input.GetAxis("Vertical");
		anim.SetFloat ("x",x);
		anim.SetFloat ("y",y);
		if (x >= 0) {
			this.gameObject.transform.localScale = new Vector3 (-1, 1, 1);

		} else if (x < 0) {
			this.gameObject.transform.localScale = new Vector3 (1, 1, 1);

		}
		this.gameObject.transform.position += new Vector3 (x, 0, 0) *Time.deltaTime;
		this.gameObject.transform.position += new Vector3 (0, y, 0) *Time.deltaTime;
	}

	void OnCollisionEnter (Collision col)
	{
		Debug.Log (col.gameObject.name + " was Obtained");
		Destroy(col.gameObject);
	}


}
