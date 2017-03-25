using UnityEngine;
using System.Collections;

public class DungeonCamera : MonoBehaviour {
	public GameObject target;
	public float damping = 50;
	Vector3 offset;
	
	void Start() 
	{
		offset = transform.position - target.transform.position;
	}
	
	void LateUpdate() 
	{
		resizeOrthographicView();
		
		Vector3 desiredPosition = target.transform.position + offset;
		Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
		
		this.transform.position = position;
		this.transform.LookAt(target.transform);
	}
	
	
	void resizeOrthographicView()
	{
		if (Input.GetKey (KeyCode.Comma)) {
			if(this.camera.orthographicSize > 10.0f)
				this.camera.orthographicSize /= 1.005f;
		} else if (Input.GetKey (KeyCode.M)) {
			if(this.camera.orthographicSize < 100.0f)
				this.camera.orthographicSize *= 1.005f;
		} else {
			if (this.camera.orthographicSize > 35.0f) {
				this.camera.orthographicSize /= 1.005f;
			} else if (this.camera.orthographicSize < 35.0f){
				this.camera.orthographicSize *= 1.005f;
			} else {
				this.camera.orthographicSize = 35.0f;
			}
		}
	}
}
