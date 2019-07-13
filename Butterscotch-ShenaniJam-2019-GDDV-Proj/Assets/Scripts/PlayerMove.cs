using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerMove : NetworkBehaviour
{
    public float speedMultiplyer;
    public float maxSpeedMultiplyer;
    public float walkSpeed;
    public float jumpForce;
    public float gravity;
    public float rayLength;

    private Vector3 moveDir;
    private float verticalSpeed;

    public static float mouseSensitivity = 2f;
    float xAxisClamp = 0;

    public CharacterController charControl;
    public GameObject cameraPrefab;
    public Transform cameraTransform;
    public GameObject RayRoot;

    public bool spawnedCam;

    Sequence TextBubble;
    VoiceTesting voiceScript;
    Ease easeType;

    void Update ()
    {
        if (hasAuthority)
        {
            if (!spawnedCam)
            {
                voiceScript = GetComponent<VoiceTesting>();
                GameObject go = Instantiate(cameraPrefab, transform);
                cameraTransform = go.transform;
                spawnedCam = !spawnedCam;
            }

            MovePlayer();
            RotateCamera();
            //Debug.Log("PLAYER MOVE: " +speedMultiplyer);

        }
    }

    void MovePlayer()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * walkSpeed * speedMultiplyer * Time.deltaTime;

        moveDir = transform.rotation * moveDir;

        if (charControl.isGrounded && Input.GetButtonDown("Jump") && Physics.Raycast(RayRoot.transform.position, -Vector3.up, rayLength))
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

    [Command]
    public void CmdTextBubbles()
    {
        TextBubble.Complete();
        voiceScript.text.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
        voiceScript.text.GetComponent<Text>().color = Color.white;
        voiceScript.text.transform.localPosition = Vector3.zero;

        TextBubble = DOTween.Sequence();
        TextBubble.Prepend(voiceScript.text.transform.DOLocalMove(Vector3.up * 2f , 1f).SetEase(easeType));
        TextBubble.Join(voiceScript.text.transform.DOScale(0.01f, 1f).SetEase(easeType));
        TextBubble.Join(voiceScript.text.GetComponent<Image>().DOColor(Color.red, 1f).SetEase(Ease.Linear));
    }
}