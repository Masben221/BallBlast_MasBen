using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class MoveObject : MonoBehaviour
{
  [SerializeField] Vector3 movePosition;
  [SerializeField] float moveSpeed;
  [SerializeField] [Range(0,1)] float moveProgress; // диапазон-ползунок движения от 0 до 1

  [SerializeField] float rotSpeedX;
  [SerializeField] float rotSpeedY;
  [SerializeField] float rotSpeedZ;
  
  [SerializeField] float rotSpeed;
  [SerializeField] float rotX;
  [SerializeField] float rotY;
  [SerializeField] float rotZ; 
  [SerializeField] [Range(0,1)] float rotProgress; // диапазон-ползунок движения от 0 до 1

  Vector3 startPosition;  
  Quaternion startRotation;
  
  void Start()
    {
      startPosition = transform.position;
      startRotation = transform.rotation;
    }
  void Update()
    {
      moveProgress = Mathf.PingPong (Time.time * moveSpeed,1);
      Vector3 offset = movePosition * moveProgress; // по сути лишняя переменная
      transform.position = startPosition + offset;
      
      //далее вращение

      if (rotSpeedX != 0||rotSpeedY != 0||rotSpeedZ != 0)
        transform.Rotate(rotSpeedX*Time.deltaTime, rotSpeedY*Time.deltaTime, rotSpeedZ*Time.deltaTime);// просто вращение вокруг какой то оси
      else 
        transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(rotX, rotY, rotZ), Mathf.PingPong (Time.time * rotSpeed,1));// вращение с циклом и интерполяцией
        
      //rotProgress = Mathf.PingPong (Time.time * rotSpeed,1); // по сути лишняя переменная только для удобства
      //transform.rotation = startRotation*Quaternion.Euler(rotX*rotProgress, rotY*rotProgress, rotZ*rotProgress);

      //transform.rotation = Quaternion.Euler(0f, 45f*rotProgress, 0f) * transform.rotation;// эксперимент по ускоряющемуся вращению 

      //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotX, rotY, rotZ), rotSpeed*Time.deltaTime); //вращение в одну сторону без цикла
         
    }
}
