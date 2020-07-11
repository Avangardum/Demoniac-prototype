using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class InputSwitcher : MonoBehaviour
{
    [SerializeField] private DemonScenario[] scenarios;
    [SerializeField] private float playerControlTime;

    private PlayerInput playerInput;
    private DemonInput demonInput;
    private SpriteRenderer spriteRenderer;
    private int nextScenarioPointer;
    private bool possessed;

    public float playerControlTimeLeft { get; private set; }

    public DemonScenario NextDemonScenario => scenarios[nextScenarioPointer];

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        demonInput = GetComponent<DemonInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerControlTimeLeft = playerControlTime;
    }

    public void EnablePlayerInput()
    {
        demonInput.enabled = false;
        playerInput.enabled = true;
        spriteRenderer.color = Color.white;
        possessed = false;
        playerControlTimeLeft = playerControlTime;
    }

    public void EnableDemonInput(DemonScenario scenario)
    {
        playerInput.enabled = false;
        demonInput.enabled = true;
        demonInput.SetScenario(scenario);
        spriteRenderer.color = Color.red;
        possessed = true;
    }

    private void FixedUpdate()
    {
        if(!possessed)
        {
            playerControlTimeLeft -= Time.fixedDeltaTime;
            if(playerControlTimeLeft <= 0)
            {
                playerControlTimeLeft = 0;
                EnableDemonInput(NextDemonScenario);
                nextScenarioPointer++;
                if (nextScenarioPointer >= scenarios.Length)
                    nextScenarioPointer = 0;
            }
        }
    }
}
