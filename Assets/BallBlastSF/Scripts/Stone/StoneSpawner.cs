using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoneSpawner : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private Stone stonePrefab; //������ �����.
    [SerializeField] private Transform[] spawnPoints; //������ ����� ������ ������.
    [SerializeField] private float spawnInterval; //�������� ������ ������.


    [Header("Balance")]
    [SerializeField] private Turret turret;
    [SerializeField] private Cart cart;
    [SerializeField] private int amountStone; //����������� ������ ������

    [SerializeField] [Range(0.0f, 1.0f)] private float minHitpointsPercentage; //������� �� ������������� ����������� ������
    [SerializeField] private float maxHitpointsRate; //��������� ������ ��������� ����� ��������������� ������ �����.

    [Space(10)] public UnityEvent CompletedEvent;  //������� ���������� ��������� ������  

    [SerializeField] private StoneColorChange stoneColorChange;

    private float timer; //������ ������
    private int amountSpawned; //������� ��������� ������
    
    private int stoneMaxHitpoints; //������������ ����������� ������ �����
    private int stoneMinHitpoints; //����������� ����������� ������ �����
    private int currentLevel; //������� �������
    private List<Size> sizes = new List<Size>(); //������ �������� ������, ��� ���������� ����������� ������ ������
    private int progressCountStone; //������ ����������� ������ �� ������, ��� ����������� ������ ���������

    private Vector3 stoneOffset;// ����� �� ��� Z ����� �� ������������� ������ �� ������

    public int AmountSpawner { get => amountSpawned; set => amountSpawned = value; } //������� ��������� ������
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; } //������� �������
    public int ProgressCountStone { get => progressCountStone; } //������ ����������� ������ �� ������, ��� ����������� ������ ���������

    private void Awake()
    {
        CurrentLevel = PlayerPrefs.GetInt("LevelMenu:CurrentLevel", 1);

        int damagePerSecond = 1 + (int)(turret.Damage  * turret.ProjectileAmount * cart.CartSpeed * (1 / turret.FireRate)/400);
             
        maxHitpointsRate += CurrentLevel * CurrentLevel * 0.1f;

        stoneMaxHitpoints = (int)(damagePerSecond * damagePerSecond * maxHitpointsRate);
        stoneMinHitpoints = (int)(stoneMaxHitpoints * minHitpointsPercentage);

        timer = spawnInterval;
        amountStone += currentLevel;

        //������ ����������� ������ �� ������
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

    private void Update() //��������� ������ ������ �� �������
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

    //����� ������ ������
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
