 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab; //������ �������
    [SerializeField] private Transform shootPoint; //����� ������ �������    

    [Space(10)]
    [Header("Turret Settings")]
    [SerializeField] private float fireRate; //������� ��������
    [SerializeField] private int damage; //������� �����
    [SerializeField] private int projectileAmount; //����������� ��������

    [Space(10)]
    [Header("Projectile Settings")]
    [SerializeField] private float projectileInterval; //�������� ��������� ��������

    public int Damage { get => damage; set => damage = value; } //������� �����.
    public int ProjectileAmount { get => projectileAmount; set =>projectileAmount = value; } //����������� ��������
    public float FireRate { get => fireRate; set => fireRate = value; } //������� ��������

    private float timer;
        private void Awake()
    {
        damage = PlayerPrefs.GetInt("Turret:Damage", 1);
        projectileAmount = PlayerPrefs.GetInt("Turret:ProjectileAmount", 1);
        fireRate = PlayerPrefs.GetFloat("Turret:FireRate", 0.5f);        
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void SpawnProjectile() //����� ������� ����
    {
        float startPosX = shootPoint.position.x - projectileInterval * (projectileAmount - 1) * 0.5f;

        for (int i = 0; i < projectileAmount; i++)
        {
            Projectile projectile = Instantiate(projectilePrefab, new Vector3(startPosX + i * projectileInterval, shootPoint.position.y, shootPoint.position.z), transform.rotation);
            projectile.SetDamage(damage);
        }
    }
    public void Fire() //���������� ������� � ����������� �� ��������� �������
    {
        if (timer >= fireRate)
        {
            SpawnProjectile();

            timer = 0;
        }
    }
    
    public void SetProjectileAmount(int amount) //������ ����������� ���������.
    {
        projectileAmount = amount;
    }
}
