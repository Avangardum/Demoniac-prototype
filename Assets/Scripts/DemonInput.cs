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

        previousFrameJump = jump;
        timeSinceScenarioStart += Time.fixedDeltaTime;
    }

    public void SetScenario(DemonScenario scenario)
    {
        scenarioElements = scenario.ToQueue();
        timeSinceScenarioStart = 0;
    }
}
