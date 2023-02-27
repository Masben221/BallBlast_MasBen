using UnityEngine;
using UnityEngine.UI;

public class StoreMenu : MonoBehaviour
{
    [SerializeField] private Image fireRateImage;
    [SerializeField] private Image projectileAmountImage;
    [SerializeField] private Image damageImage;
    [SerializeField] private Image cartSpeedImage;

    [SerializeField] private Turret turret;
    [SerializeField] private Bag bag;
    [SerializeField] private Cart cart;

    [SerializeField] private int maxFireRate; //Максимальная скорострельность
    [SerializeField] private int maxProjectileAmount; //Максимальное колличество патронов
    [SerializeField] private int maxDamage; //Максимальный уровень урона
    [SerializeField] private int maxCartSpeed; //Максимальная скорость тачки

    public int MaxFireRate { get => maxFireRate; set => maxFireRate = value; } //Максимальная скорострельность - свойство
    public int MaxProjectileAmount { get => maxProjectileAmount; set => maxProjectileAmount = value; } //Максимальная количество пуль - свойство
    public int MaxDamage { get => maxDamage; set => maxDamage = value; } //Максимальный урон - свойство
    public int MaxCartSpeed { get => maxCartSpeed; set => maxCartSpeed = value; } //Максимальная скорость тачки - свойство


    [SerializeField] private GameObject storePanel; //Панель магазина
    [SerializeField] private GameObject levelMenuPanel; //Панель уровня
    [SerializeField] private LevelState levelState;
    [SerializeField] private UpgradeMenu upgradeMenu;

    private float fillAmountStepFireRate; //Шаг в прогресс панели скорострельности
    private float fillAmountStepProjectile; //Шаг в прогресс панели выстрелов
    private float fillAmountStepDamage; //Шаг в прогресс панели урона
    private float fillAmountStepCartSpeed; //Шаг в прогресс скорости тачки

    private void Awake()
    {
        fireRateImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:FireRate", 0f);
        projectileAmountImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:ProjectileAmount", 0f);
        damageImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:Damage", 0f);
        cartSpeedImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:CartSpeed", 0f);

        //Расчет шагов для прогресс панелей
        fillAmountStepFireRate = (float)1f / (maxFireRate);
        fillAmountStepProjectile = (float)1f / (maxProjectileAmount);
        fillAmountStepDamage = (float)1f / (maxDamage);
        fillAmountStepCartSpeed = (float)1f / (maxCartSpeed);
    }

    //Увеличивает скорострельность
    public void SetFareRate()
    {
        if (turret.FireRate <= 1/ maxFireRate) return;

        if (bag.PullCoin(2) == false) return;

        turret.FireRate -= 0.02f;
        fireRateImage.fillAmount += fillAmountStepFireRate;

        SaveStore();
    }

    //Увеличивает колличество патронов
    public void SetProjectileAmount()
    {
        if (turret.ProjectileAmount >= maxProjectileAmount) return;

        if (bag.PullCoin(5) == false) return;

        turret.ProjectileAmount += 1;
        projectileAmountImage.fillAmount += fillAmountStepProjectile;

        SaveStore();
    }

    //Увеличивает урон
    public void SetDamage()
    {
        if (turret.Damage >= maxDamage) return;

        if (bag.PullCoin(3) == false) return;

        turret.Damage += 1;
        damageImage.fillAmount += fillAmountStepDamage;

        SaveStore();
    }

    //Увеличивает скорострельность
    public void SetCartSpeed()
    {
        if (cart.CartSpeed >= maxCartSpeed) return;

        if (bag.PullCoin(1) == false) return;

        cart.CartSpeed += 1f;
        cartSpeedImage.fillAmount += fillAmountStepCartSpeed;

        SaveStore();
    }

    //Сохраняет данные в файл
    public void SaveStore()
    {
        PlayerPrefs.SetInt("Turret:Damage", turret.Damage);
        PlayerPrefs.SetInt("Turret:ProjectileAmount", turret.ProjectileAmount);
        PlayerPrefs.SetFloat("Turret:FireRate", turret.FireRate);
        PlayerPrefs.SetFloat("Cart:CartSpeed", cart.CartSpeed);
        
        PlayerPrefs.SetFloat("StoreMenu:FireRate", fireRateImage.fillAmount);
        PlayerPrefs.SetFloat("StoreMenu:ProjectileAmount", projectileAmountImage.fillAmount);
        PlayerPrefs.SetFloat("StoreMenu:Damage", damageImage.fillAmount);
        PlayerPrefs.SetFloat("StoreMenu:CartSpeed", cartSpeedImage.fillAmount);
    }

    //Возврат в предыдущее меню
    public void LevelMenu()
    {
        storePanel.SetActive(false);
        levelMenuPanel.SetActive(true);
        SaveStore();
        Time.timeScale = 0f;
        levelState.IsMenuStore = false;
        upgradeMenu.UpdateMenuText();
    }
}
