using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSwitcher : MonoBehaviour
{
    private PlayerInput playerInput;
    private DemonInput demonInput;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        demonInput = GetComponent<DemonInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void EnablePlayerInput()
    {
        demonInput.enabled = false;
        playerInput.enabled = true;
        spriteRenderer.color = Color.white;
    }

    public void EnableDemonInput(DemonScenario scenario)
    {
        playerInput.enabled = false;
        demonInput.enabled = true;
        demonInput.SetScenario(scenario);
        spriteRenderer.color = Color.red;
    }
}
