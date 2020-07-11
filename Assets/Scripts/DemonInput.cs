using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonInput : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private bool left;
    private bool right;
    private bool jump;
    private bool previousFrameJump;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if ((left && right) || (!left && !right))
            playerMovement.CurrentXAction = PlayerMovement.XAction.Brake;
        else if (left)
            playerMovement.CurrentXAction = PlayerMovement.XAction.GoLeft;
        else if (right)
            playerMovement.CurrentXAction = PlayerMovement.XAction.GoRight;

        playerMovement.JumpPressed = jump;
        if (jump && !previousFrameJump)
            playerMovement.JumpDown = true;

        previousFrameJump = jump;
    }
}
