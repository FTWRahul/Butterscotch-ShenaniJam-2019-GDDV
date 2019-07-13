using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{


    //void Awake()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
    //}

    //void Update()
    //{
    //    RotateCamera();
    //}

    //void RotateCamera()
    //{
    //    float mouseX = Input.GetAxisRaw("Mouse X");
    //    float mouseY = Input.GetAxisRaw("Mouse Y");

    //    float rotAmountX = mouseX * mouseSensitivity;
    //    float rotAmountY = mouseY * mouseSensitivity;

    //    xAxisClamp -= rotAmountY;

    //    Vector3 targetRotCam = transform.rotation.eulerAngles;
    //    Vector3 targetRotBody = playerBody.rotation.eulerAngles;

    //    targetRotCam.x -= rotAmountY;
    //    targetRotCam.z = 0;
    //    targetRotBody.y += rotAmountX;

    //    if (xAxisClamp > 90)
    //    {
    //        xAxisClamp = 90;
    //        targetRotCam.x = 90;
    //    }
    //    else if (xAxisClamp < -90)
    //    {
    //        xAxisClamp = -90;
    //        targetRotCam.x = 270;
    //    }

    //    //targetRot.y += rotAmountX;

    //    transform.rotation = Quaternion.Euler(targetRotCam);
    //    playerBody.rotation = Quaternion.Euler(targetRotBody);
    //}

}
