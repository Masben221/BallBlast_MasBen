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

    [SerializeField] private int maxFireRate; //������������ ����������������
    [SerializeField] private int maxProjectileAmount; //������������ ����������� ��������
    [SerializeField] private int maxDamage; //������������ ������� �����
    [SerializeField] private int maxCartSpeed; //������������ �������� �����

    public int MaxFireRate { get => maxFireRate; set => maxFireRate = value; } //������������ ���������������� - ��������
    public int MaxProjectileAmount { get => maxProjectileAmount; set => maxProjectileAmount = value; } //������������ ���������� ���� - ��������
    public int MaxDamage { get => maxDamage; set => maxDamage = value; } //������������ ���� - ��������
    public int MaxCartSpeed { get => maxCartSpeed; set => maxCartSpeed = value; } //������������ �������� ����� - ��������


    [SerializeField] private GameObject storePanel; //������ ��������
    [SerializeField] private GameObject levelMenuPanel; //������ ������
    [SerializeField] private LevelState levelState;
    [SerializeField] private UpgradeMenu upgradeMenu;

    private float fillAmountStepFireRate; //��� � �������� ������ ����������������
    private float fillAmountStepProjectile; //��� � �������� ������ ���������
    private float fillAmountStepDamage; //��� � �������� ������ �����
    private float fillAmountStepCartSpeed; //��� � �������� �������� �����

    private void Awake()
    {
        fireRateImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:FireRate", 0f);
        projectileAmountImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:ProjectileAmount", 0f);
        damageImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:Damage", 0f);
        cartSpeedImage.fillAmount = PlayerPrefs.GetFloat("StoreMenu:CartSpeed", 0f);

        //������ ����� ��� �������� �������
        fillAmountStepFireRate = (float)1f / (maxFireRate);
        fillAmountStepProjectile = (float)1f / (maxProjectileAmount);
        fillAmountStepDamage = (float)1f / (maxDamage);
        fillAmountStepCartSpeed = (float)1f / (maxCartSpeed);
    }

    //����������� ����������������
    public void SetFareRate()
    {
        if (turret.FireRate <= 1/ maxFireRate) return;

        if (bag.PullCoin(2) == false) return;

        turret.FireRate -= 0.02f;
        fireRateImage.fillAmount += fillAmountStepFireRate;

        SaveStore();
    }

    //����������� ����������� ��������
    public void SetProjectileAmount()
    {
        if (turret.ProjectileAmount >= maxProjectileAmount) return;

        if (bag.PullCoin(5) == false) return;

        turret.ProjectileAmount += 1;
        projectileAmountImage.fillAmount += fillAmountStepProjectile;

        SaveStore();
    }

    //����������� ����
    public void SetDamage()
    {
        if (turret.Damage >= maxDamage) return;

        if (bag.PullCoin(3) == false) return;

        turret.Damage += 1;
        damageImage.fillAmount += fillAmountStepDamage;

        SaveStore();
    }

    //����������� ����������������
    public void SetCartSpeed()
    {
        if (cart.CartSpeed >= maxCartSpeed) return;

        if (bag.PullCoin(1) == false) return;

        cart.CartSpeed += 1f;
        cartSpeedImage.fillAmount += fillAmountStepCartSpeed;

        SaveStore();
    }

    //��������� ������ � ����
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

    //������� � ���������� ����
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
