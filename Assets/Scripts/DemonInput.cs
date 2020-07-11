using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonInput : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private InputSwitcher inputSwitcher;
    private Queue<DemonScenarioElement> scenarioElements;
    private float timeSinceScenarioStart;

    private bool left;
    private bool right;
    private bool jump;
    private bool dashLeft;
    private bool dashRight;
    private bool dashUp;
    private bool dashDown;

    private bool previousFrameJump;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        inputSwitcher = GetComponent<InputSwitcher>();
    }

    private void FixedUpdate()
    {
        if(scenarioElements.Count == 0)
        {
            left = false;
            right = false;
            jump = false;
            dashLeft = false;
            dashRight = false;
            dashUp = false;
            dashDown = false;
            inputSwitcher.EnablePlayerInput();
        }
        else
        {
            while(scenarioElements.Count > 0 && timeSinceScenarioStart >= scenarioElements.Peek().Timing)
            {
                var currentScenarioElement = scenarioElements.Dequeue();
                var press = currentScenarioElement.ButtonAction == ButtonAction.Press;
                switch (currentScenarioElement.Button)
                {
                    case DemonButton.Left:
                        left = press;
                        break;
                    case DemonButton.Right:
                        right = press;
                        break;
                    case DemonButton.Jump:
                        jump = press;
                        break;
                    case DemonButton.DashLeft:
                        dashLeft = press;
                        break;
                    case DemonButton.DashRight:
                        dashRight = press;
                        break;
                    case DemonButton.DashUp:
                        dashUp = press;
                        break;
                    case DemonButton.DashDown:
                        dashDown = press;
                        break;
                }
            }
        }

        if ((left && right) || (!left && !right))
            playerMovement.CurrentXAction = PlayerMovement.XAction.Brake;
        else if (left)
            playerMovement.CurrentXAction = PlayerMovement.XAction.GoLeft;
        else if (right)
            playerMovement.CurrentXAction = PlayerMovement.XAction.GoRight;

        playerMovement.JumpPressed = jump;
        if (jump && !previousFrameJump)
            playerMovement.JumpDown = true;

        if(dashLeft || dashRight || dashUp || dashDown)
        {
            Vector2 dashDirection = Vector2.zero;
            if (dashLeft)
                dashDirection += Vector2.left;
            if (dashRight)
                dashDirection += Vector2.right;
            if (dashUp)
                dashDirection += Vector2.up;
            if (dashDown)
                dashDirection += Vector2.down;
            playerMovement.Dash(dashDirection);
            dashLeft = false;
            dashRight = false;
            dashUp = false;
            dashDown = false;
        }

        previousFrameJump = jump;
        timeSinceScenarioStart += Time.fixedDeltaTime;
    }

    public void SetScenario(DemonScenario scenario)
    {
        scenarioElements = scenario.ToQueue();
        timeSinceScenarioStart = 0;
    }
}
