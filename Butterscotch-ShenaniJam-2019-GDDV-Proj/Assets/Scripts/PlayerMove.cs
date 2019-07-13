using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{

    public float walkSpeed;
    public float jumpForce;
    public float gravity;

    private Vector3 moveDir;
    private float verticalSpeed;

    public static float mouseSensitivity = 2f;
    float xAxisClamp = 0;

    public CharacterController charControl;
    public GameObject cameraPrefab;
    public Transform cameraTransform;

    public bool spawnedCam;

    void Update ()
    {
        if (hasAuthority)
        {
            if (!spawnedCam)
            {
                GameObject go = Instantiate(cameraPrefab, transform);
                cameraTransform = go.transform;
                spawnedCam = !spawnedCam;
            }

            MovePlayer();
            RotateCamera();
        }
    }

    void MovePlayer()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * walkSpeed * Time.deltaTime;

        moveDir = transform.rotation * moveDir;

        if (charControl.isGrounded && Input.GetButtonDown("Jump"))
        {
            verticalSpeed = jumpForce;
        }
        //else if (charControl.isGrounded)
        //{
        //    verticalSpeed = 0;
        //}
        else if (!charControl.isGrounded)
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }

        moveDir.y = verticalSpeed;
        charControl.Move(moveDir);
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;

        xAxisClamp -= rotAmountY;

        Vector3 targetRotCam = cameraTransform.rotation.eulerAngles;
        Vector3 targetRotBody = transform.rotation.eulerAngles;

        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0;
        targetRotBody.y += rotAmountX;

        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            targetRotCam.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            targetRotCam.x = 270;
        }

        //targetRot.y += rotAmountX;

        cameraTransform.rotation = Quaternion.Euler(targetRotCam);
        transform.rotation = Quaternion.Euler(targetRotBody);
    }
}