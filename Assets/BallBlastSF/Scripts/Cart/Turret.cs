 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab; //Префаб снаряда
    [SerializeField] private Transform shootPoint; //Точка спавна снаряда    

    [Space(10)]
    [Header("Turret Settings")]
    [SerializeField] private float fireRate; //Частота выстрела
    [SerializeField] private int damage; //Уровень урона
    [SerializeField] private int projectileAmount; //Колличество снарядов

    [Space(10)]
    [Header("Projectile Settings")]
    [SerializeField] private float projectileInterval; //Интервал отрисовки снарядов

    public int Damage { get => damage; set => damage = value; } //Уровень урона.
    public int ProjectileAmount { get => projectileAmount; set =>projectileAmount = value; } //Колличество снарядов
    public float FireRate { get => fireRate; set => fireRate = value; } //Частота выстрела

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

    private void SpawnProjectile() //Метод содания пуль
    {
        float startPosX = shootPoint.position.x - projectileInterval * (projectileAmount - 1) * 0.5f;

        for (int i = 0; i < projectileAmount; i++)
        {
            Projectile projectile = Instantiate(projectilePrefab, new Vector3(startPosX + i * projectileInterval, shootPoint.position.y, shootPoint.position.z), transform.rotation);
            projectile.SetDamage(damage);
        }
    }
    public void Fire() //Производит выстрел в зависимости от интервала времени
    {
        if (timer >= fireRate)
        {
            SpawnProjectile();

            timer = 0;
        }
    }
    
    public void SetProjectileAmount(int amount) //Задает колличество выстрелов.
    {
        projectileAmount = amount;
    }
}
