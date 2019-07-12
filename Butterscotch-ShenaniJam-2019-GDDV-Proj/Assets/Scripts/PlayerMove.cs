using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController charControl;

    public float walkSpeed;
    public float jumpForce;
    public float gravity;

    private Vector3 moveDir;
    private float verticalSpeed;


    void Update ()
    {
        MovePlayer();
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



        //float horiz = Input.GetAxis("Horizontal");
        //float vert = Input.GetAxis("Vertical");

        ////Multiplaying the inputs with direction vectors and speed, normilizing with time.
        //Vector3 moveDirSide = transform.right * horiz * walkSpeed * Time.deltaTime;
        //Vector3 moveDirForward = transform.forward * vert * walkSpeed * Time.deltaTime;


        //Vector3 finalMoveDirection = (moveDirSide + moveDirForward).normalized * walkSpeed * Time.deltaTime;
        //Making the char controller do a Move by passing our desired vectors, The physics calculations are done in the PlayerMotor class.
        charControl.Move(moveDir);

    }



    //public Vector3 Tick()
    //{
    //    if ((charController.isGrounded && internalTimer > 0))
    //    {
    //        if (Input.GetButtonDown("Jump"))
    //        {
    //            internalTimer = -0.5f;
    //            Jump();
    //        }
    //    }
    //    else
    //    {
    //        if (internalTimer < 1.5f)
    //        {
    //            internalTimer += Time.deltaTime;
    //        }

    //        verticalVelocity -= gravity * Time.deltaTime;
    //    }
    //    Vector3 moveDir = new Vector3(0, verticalVelocity, 0);

    //    return moveDir * Time.deltaTime;
    //}
    //void Jump()
    //{
    //    verticalVelocity = jumpForce;
    //}
}