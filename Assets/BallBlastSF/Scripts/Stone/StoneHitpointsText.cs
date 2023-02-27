using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Destructible))]
public class StoneHitpointsText : MonoBehaviour
{
    [SerializeField] private Text hitpointText;

    private Destructible destructible;

    private void Awake()
    {
        destructible = GetComponent<Destructible>();

        destructible.OnDamageEvent.AddListener(OnChangeHitpoint);
    }
    private void OnDestroy()
    {
        destructible.OnDamageEvent.RemoveListener(OnChangeHitpoint);
    }    
    private void OnChangeHitpoint()
    {
        int hitPoints = destructible.GetHealth();

        if (hitPoints >= 1000)
            hitpointText.text = hitPoints / 1000 + "Ê";
        else
            hitpointText.text = hitPoints.ToString();
    }
}
