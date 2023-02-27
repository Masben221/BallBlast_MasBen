using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoneSpawner : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private Stone stonePrefab; //Префаб камня.
    [SerializeField] private Transform[] spawnPoints; //Массив точек спавна камней.
    [SerializeField] private float spawnInterval; //Интервал спавна камней.


    [Header("Balance")]
    [SerializeField] private Turret turret;
    [SerializeField] private Cart cart;
    [SerializeField] private int amountStone; //Колличество камней спавна

    [SerializeField] [Range(0.0f, 1.0f)] private float minHitpointsPercentage; //Процент от максимального колличества камней
    [SerializeField] private float maxHitpointsRate; //Повышение уровня сложности прямо пропорционально жизням камня.

    [Space(10)] public UnityEvent CompletedEvent;  //Событие завершения генерации спавна  

    [SerializeField] private StoneColorChange stoneColorChange;

    private float timer; //Таймер спавна
    private int amountSpawned; //Счетчик созданных камней
    
    private int stoneMaxHitpoints; //Максимальное колличество жизней камня
    private int stoneMinHitpoints; //Минимальное колличество жизней камня
    private int currentLevel; //Текущий уровень
    private List<Size> sizes = new List<Size>(); //Массив размеров камней, для вычисления колличества спавна камней
    private int progressCountStone; //Расчет колличества камней на уровне, для отображения полосы прогресса

    private Vector3 stoneOffset;// сдвиг по оси Z чтобы не перекрывались тексты на камнях

    public int AmountSpawner { get => amountSpawned; set => amountSpawned = value; } //Счетчик созданных камней
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; } //Текущий уровень
    public int ProgressCountStone { get => progressCountStone; } //Расчет колличества камней на уровне, для отображения полосы прогресса

    private void Awake()
    {
        CurrentLevel = PlayerPrefs.GetInt("LevelMenu:CurrentLevel", 1);

        int damagePerSecond = 1 + (int)(turret.Damage  * turret.ProjectileAmount * cart.CartSpeed * (1 / turret.FireRate)/400);
             
        maxHitpointsRate += CurrentLevel * CurrentLevel * 0.1f;

        stoneMaxHitpoints = (int)(damagePerSecond * damagePerSecond * maxHitpointsRate);
        stoneMinHitpoints = (int)(stoneMaxHitpoints * minHitpointsPercentage);

        timer = spawnInterval;
        amountStone += currentLevel;

        //Расчет колличества камней на уровне
        for (int i = 0; i < amountStone; i++)
        {
            int result = 2;
            sizes.Add((Size)Random.Range(1, 4));

            for (int j = 0; j < (int)(sizes[i]) * 2; j += 2)
            {
                result = result + j * 2;
            }

            progressCountStone += result + 1;
        }
    }

    private void Update() //Генерация спавна камней от времени
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Spawn();

            timer = 0;
        }
        if (amountSpawned == amountStone)
        {
            enabled = false;
            
            CompletedEvent.Invoke();
        }
    }

    //Метод спавна камней
    private void Spawn()
    {
        stoneColorChange.ChangeColor();

        Stone stone = Instantiate(stonePrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        stoneOffset.z += 0.001f;
        stone.transform.position = new Vector3(stone.transform.position.x, stone.transform.position.y, stone.transform.position.z + stoneOffset.z);
        //transform.position += stoneOffset;

        stone.SetSize(sizes[amountSpawned]);

        float result = ((float)Random.Range(stoneMinHitpoints, stoneMaxHitpoints + 1) * (int)sizes[amountSpawned]);
        //if (result < 1) result = 1f;

        if (result < CurrentLevel) result = CurrentLevel+ (int)sizes[amountSpawned];
        stone.maxHealth = (int)result;        

        amountSpawned++;
    }
}
