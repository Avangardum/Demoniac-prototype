using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const KeyCode left = KeyCode.A;
    private const KeyCode right = KeyCode.D;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        bool goLeft = Input.GetKey(left);
        bool goRight = Input.GetKey(right);
        if ((goLeft && goRight) || (!goLeft && !goRight))
            playerMovement.CurrentXAction = PlayerMovement.XAction.Brake;
        else if (goLeft)
            playerMovement.CurrentXAction = PlayerMovement.XAction.GoLeft;
        else if (goRight)
            playerMovement.CurrentXAction = PlayerMovement.XAction.GoRight;
    }
}
