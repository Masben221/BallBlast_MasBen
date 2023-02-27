using UnityEngine;
using UnityEngine.Events;

public class Freezing : MonoBehaviour
{   
    private UpgradeMenu upgradeMenu;

    private void Awake()
    {
        upgradeMenu = FindObjectOfType<UpgradeMenu>();//Находим объекты по типу
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Turret turret = collision.transform.root.GetComponent<Turret>();

        if (turret == true)
        {
            upgradeMenu.IsFreez = true;
            upgradeMenu.ResetZTimer();
            
            var stones = FindObjectsOfType<Stone>();
            foreach (var item in stones)
            {
                item.FreezingStone();                
            }
            Destroy(gameObject);
        }
    }
}