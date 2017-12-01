using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    GameManager gameManager;
    Pause pause;

    public float playerWalkingSpeed = 6f;
    public float playerRunningSpeed = 9f;
    public float jumpStrength = 3.5f;
    public float verticalRotationLimit = 12f;

    float forwardMovement;
    float sidewaysMovement;
    float verticalRotation = 0;
    float verticalVelocity;

    CharacterController cc;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameManager = FindObjectOfType<GameManager>();
        pause = FindObjectOfType<Pause>();
    }

    void Update()
    {
        if (gameManager.isDead == false && pause.isPaused == false)
        {
            float horizontalRotation = Input.GetAxis("Mouse X");
            transform.Rotate(0, horizontalRotation, 0);

            verticalRotation -= Input.GetAxis("Mouse Y");
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);
            Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

            if (cc.isGrounded)
            {
                forwardMovement = Input.GetAxis("Vertical") * playerWalkingSpeed;
                sidewaysMovement = Input.GetAxis("Horizontal") * playerWalkingSpeed;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    forwardMovement = Input.GetAxis("Vertical") * playerRunningSpeed;
                    sidewaysMovement = Input.GetAxis("Horizontal") * playerRunningSpeed;
                }

                if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        DynamicCrosshair.spread = DynamicCrosshair.RUN_SPREAD;
                    }
                    else
                    {
                        DynamicCrosshair.spread = DynamicCrosshair.WALK_SPREAD;
                    }
                }

            }
            else
            {
                DynamicCrosshair.spread = DynamicCrosshair.JUMP_SPREAD;
            }

            verticalVelocity += Physics.gravity.y * Time.deltaTime;

            if (Input.GetButtonDown("Jump") && cc.isGrounded)
            {
                verticalVelocity = jumpStrength;
            }

            Vector3 playerMovement = new Vector3(sidewaysMovement, verticalVelocity, forwardMovement);

            cc.Move(transform.rotation * playerMovement * Time.deltaTime);
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

}
