using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text timeLeftText;
    [SerializeField] private Image nextManeuverImage;

    [SerializeField] private InputSwitcher inputSwitcher;
    [SerializeField] private ScenarioDictionary scenarioDictionary;

    private void Update()
    {
        timeLeftText.text = Mathf.CeilToInt(inputSwitcher.playerControlTimeLeft).ToString();
        nextManeuverImage.sprite = scenarioDictionary.GetImage(inputSwitcher.NextDemonScenario);
    }
}
