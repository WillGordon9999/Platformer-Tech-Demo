﻿//Source: http://wiki.unity3d.com/index.php/SmoothMouseLook
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class MouseLook : MonoBehaviour {
	
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 7.5F;
	public float sensitivityY = 7.5F;
	
	private float zRotation;

	public float minimumX = -360F;
	public float maximumX = 360F;
	
	public float minimumY = -360F;
	public float maximumY = 360F;
	
	float rotationX = 0F;
	float rotationY = 0F;
	
	private List<float> rotArrayX = new List<float>();
	float rotAverageX = 0F;	
	
	private List<float> rotArrayY = new List<float>();
	float rotAverageY = 0F;
	
	public float frameCounter = 20;
	
	Quaternion originalRotation;

	void Update ()
	{
        //if (!isLocalPlayer)
        //{
        //    var camera = transform.Find("Main Camera").gameObject;
        //    camera.SetActive(false);
        //    return;
        //}

		zRotation = GetComponent<Rigidbody> ().transform.rotation.eulerAngles.z;

		if (axes == RotationAxes.MouseXAndY) 
		{	
			if (zRotation >= 180) 
			{
				rotAverageY = 0f;
				rotAverageX = 0f; 

				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
				rotationX += -Input.GetAxis ("Mouse X") * sensitivityX;

				rotArrayY.Add (rotationY);
				rotArrayX.Add (rotationX);
				
				if (rotArrayY.Count >= frameCounter)
                {
					rotArrayY.RemoveAt (0);
				}
				if (rotArrayX.Count >= frameCounter)
                {
					rotArrayX.RemoveAt (0);
				}
				
				for (int j = 0; j < rotArrayY.Count; j++)
                {
					rotAverageY += rotArrayY [j];
				}
				for (int i = 0; i < rotArrayX.Count; i++)
                {
					rotAverageX += rotArrayX [i];
				}

				rotAverageY /= rotArrayY.Count;
				rotAverageX /= rotArrayX.Count;
				
				rotAverageY = ClampAngle (rotAverageY, minimumY, maximumY);
				rotAverageX = ClampAngle (rotAverageX, minimumX, maximumX);
				
				Quaternion yQuaternion = Quaternion.AngleAxis (rotAverageY, Vector3.left);
				Quaternion xQuaternion = Quaternion.AngleAxis (rotAverageX, Vector3.up);
				
				transform.localRotation = originalRotation * xQuaternion * yQuaternion;
			}

			else 
			{
				rotAverageY = 0f;
				rotAverageX = 0f;
				
				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
				rotationX += Input.GetAxis ("Mouse X") * sensitivityX;
				
				rotArrayY.Add (rotationY);
				rotArrayX.Add (rotationX);
				
				if (rotArrayY.Count >= frameCounter) {
					rotArrayY.RemoveAt (0);
				}
				if (rotArrayX.Count >= frameCounter) {
					rotArrayX.RemoveAt (0);
				}
				
				for (int j = 0; j < rotArrayY.Count; j++) {
					rotAverageY += rotArrayY [j];
				}
				for (int i = 0; i < rotArrayX.Count; i++) {
					rotAverageX += rotArrayX [i];
				}
				
				rotAverageY /= rotArrayY.Count;
				rotAverageX /= rotArrayX.Count;
				
				rotAverageY = ClampAngle (rotAverageY, minimumY, maximumY);
				rotAverageX = ClampAngle (rotAverageX, minimumX, maximumX);
				
				Quaternion yQuaternion = Quaternion.AngleAxis (rotAverageY, Vector3.left);
				Quaternion xQuaternion = Quaternion.AngleAxis (rotAverageX, Vector3.up);
				
				transform.localRotation = originalRotation * xQuaternion * yQuaternion;
			}

			if (axes == RotationAxes.MouseX) 
			{	
				rotAverageX = 0f;
			
				rotationX += Input.GetAxis ("Mouse X") * sensitivityX;

				rotArrayX.Add (rotationX);
			
				if (rotArrayX.Count >= frameCounter)
                {
					rotArrayX.RemoveAt (0);
				}
				for (int i = 0; i < rotArrayX.Count; i++)
                {
					rotAverageX += rotArrayX [i];
				}
				rotAverageX /= rotArrayX.Count;
			
				rotAverageX = ClampAngle (rotAverageX, minimumX, maximumX);
			
				Quaternion xQuaternion = Quaternion.AngleAxis (rotAverageX, Vector3.up);
				transform.localRotation = originalRotation * xQuaternion;
			} 

			else if (axes == RotationAxes.MouseY)
			{	
				rotAverageY = 0f;
			
				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;

				rotArrayY.Add (rotationY);
			
				if (rotArrayY.Count >= frameCounter)
                {
					rotArrayY.RemoveAt (0);
				}
				for (int j = 0; j < rotArrayY.Count; j++)
                {
					rotAverageY += rotArrayY [j];
				}
				rotAverageY /= rotArrayY.Count;
			
				rotAverageY = ClampAngle (rotAverageY, minimumY, maximumY);
			
				Quaternion yQuaternion = Quaternion.AngleAxis (rotAverageY, Vector3.left);
				transform.localRotation = originalRotation * yQuaternion;
			}
		}
	}
	
	void Start ()
	{			
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
		originalRotation = transform.localRotation;
	}
	
	public static float ClampAngle (float angle, float min, float max)
	{
		angle = angle % 360;
		if ((angle >= -360F) && (angle <= 360F)) {
			if (angle < -360F) {
				angle += 360F;
			}
			if (angle > 360F) {
				angle -= 360F;
			}			
		}
		return Mathf.Clamp (angle, min, max);
	}
}
