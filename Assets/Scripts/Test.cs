using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private DemonScenario scenario;

    private void Start()
    {
        GetComponent<InputSwitcher>().EnableDemonInput(scenario);
    }
}
