using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Camera cam;

    public Transform firstPoint;
    public Transform secondPoint;

    public float turningRate = 150f;
    public float speed = 150;

    void Update()
    {
      
   
    }

    public void SettingsMenuMove()
    {
        cam.transform.position = Vector3.Lerp(transform.position, secondPoint.position, speed * Time.deltaTime);
        cam.transform.rotation = Quaternion.RotateTowards(transform.rotation, secondPoint.rotation, turningRate * Time.deltaTime);
    } 

    public void MainMenuMove()
    {
        cam.transform.position = Vector3.Lerp(firstPoint.position, firstPoint.position, speed * Time.deltaTime);
        cam.transform.rotation = Quaternion.RotateTowards(transform.rotation, firstPoint.rotation, turningRate * Time.deltaTime);
    }

}
