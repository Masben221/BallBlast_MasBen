using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	[SerializeField] private Transform targetTransform;
	[SerializeField] private Vector3 speed;
	
	void Update()
	{
		targetTransform.Rotate(speed * Time.deltaTime, Space.Self); 
	}
}
