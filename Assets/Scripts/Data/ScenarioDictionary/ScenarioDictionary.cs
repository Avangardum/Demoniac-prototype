using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario Dfictionary", menuName = "Scenario Dictionary")]
public class ScenarioDictionary : ScriptableObject
{
    [SerializeField] private ScenarioDictionaryElement[] elements;

    public Sprite GetImage(DemonScenario demonScenario) => elements.SingleOrDefault(x => x.Scenario == demonScenario).Image;
}
