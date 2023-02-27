using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private Text textFireRate;
    [SerializeField] private Text textProjectileAmount;
    [SerializeField] private Text textDamage;
    [SerializeField] private Text textCartSpeed;

    [SerializeField] private Text textFreezing;
    [SerializeField] private GameObject freezImage;

    [SerializeField] private Text textGodMode;
    [SerializeField] private GameObject godModeImage;

    private float fireRate; //Частота выстрела
    private int damage; //Уровень урона
    private int projectileAmount; //Колличество снарядов
    private float cartSpeed; // скорость перемещения
    
    private float zTime; // счетчик заморозки
    private bool isFreez; //Флаг заморозки
    
    public bool IsFreez { get => isFreez; set => isFreez = value; } //Флаг заморозки - свойство

    [SerializeField] private Image fireRateImage;
    [SerializeField] private Image projectileAmountImage;
    [SerializeField] private Image damageImage;
    [SerializeField] private Image cartSpeedImage;

    [SerializeField] private Turret turret;
    [SerializeField] private Bag bag;
    [SerializeField] private Cart cart;
    [SerializeField] private StoreMenu storeMenu;    
         
    [SerializeField] private LevelState levelState;
    [SerializeField] private Stone stone; //ссылка на класс
    [SerializeField] Freezing freezing;

    private void Awake()
    {
        fireRateImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:FireRate", 0f);
        projectileAmountImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:ProjectileAmount", 0f);
        damageImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:Damage", 0f);
        cartSpeedImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:CartSpeed", 0f);

        damage = PlayerPrefs.GetInt("Turret:Damage", 1);
        projectileAmount = PlayerPrefs.GetInt("Turret:ProjectileAmount", 1);
        fireRate = 1/PlayerPrefs.GetFloat("Turret:FireRate", 0.5f) - 1;
        cartSpeed = PlayerPrefs.GetFloat("Cart:CartSpeed", 10f) - 9;         
    }   

    private void Update() //Проверка на окнчание времени действия заморозки
    {
        if (isFreez == false) 
        {
            freezImage.SetActive(false);
            return; 
        }

        zTime += Time.deltaTime;
        UpdateFreezingText();
        freezImage.SetActive(true);
    }
    public void UpdateMenuText()
    {
        textFireRate.text = fireRate.ToString();
        textDamage.text = damage.ToString();
        textProjectileAmount.text = projectileAmount.ToString();
        textCartSpeed.text = cartSpeed.ToString();
    }
    public void UpdateFreezingText()
    {
        textFreezing.text = (stone.FreezTime - zTime).ToString();
        if (zTime > stone.FreezTime)
        {
            ResetZTimer();
            textFreezing.text = zTime.ToString();            
            isFreez = false;
        }
    }
    public void ResetZTimer() // сброс таймера заморозки
    {
        zTime = 0f;
    }

    public void UpdateGodModeText()
    {
        textGodMode.text = (levelState.GodModetime - levelState.TimerGodMode).ToString();
        godModeImage.SetActive(true);

        if (levelState.TimerGodMode == 0)
        {
            textGodMode.text = levelState.TimerGodMode.ToString();
            godModeImage.SetActive(false);
            levelState.IsGodMode = false;
        }
    }
}
