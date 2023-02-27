using UnityEngine;

public enum Size
{
    Small,
    Normal,
    Big,
    Huge
}

[RequireComponent(typeof(StoneMovement))]
public class Stone : Destructible
{   
    [SerializeField] private Size size; //Размер камня
    [SerializeField] private float spawnUpForce; //Время спавна
    [SerializeField] private StoneColorChange stoneColorChange;//ссылка на класс изменения цвета камня

    [SerializeField] private int maxCoinRandom; //Максимальное число рандома монет
    [SerializeField] private int maxFreezingRandom; //Максимальное число рандома заморозки
    [SerializeField] private int maxGodModeRandom; //Максимальное число рандома режима бога
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private Freezing freezingPrefab;
    [SerializeField] private GodMode godModePrefab;

    private Vector3 stoneOffset; //сдвиг камней по Z для норм отображения
   
    private UILevelProgress levelProgress;
    private StoneMovement stoneMovement;

    [SerializeField] private float freezTime; //Время заморозки
    private float timerZ; //Таймер заморозки
    private bool isFreezing; //Флаг заморозки
    public float FreezTime { get => freezTime; set => freezTime = value; } //Время заморозки - свойство
    public float TimerZ { get => timerZ; set => timerZ = value; } //Таймер заморозки - свойство
    public bool IsFreezing { get => isFreezing; set => isFreezing = value; } //Флаг заморозки - свойство

    private void Awake()
    {
        stoneMovement = GetComponent<StoneMovement>();       
        levelProgress = FindObjectOfType<UILevelProgress>();

        base.OnDestroyEvent.AddListener(OnStoneDestroyed);

        SetSize(size);
    }

    private void Update()
    {
        if (isFreezing == false) return;

        timerZ += Time.deltaTime;

        if (timerZ > freezTime)
        {
            timerZ = 0;
            isFreezing = false;
            stoneMovement.IsFreezing = false;
        }
    }

    protected void OnDestroy() // Событие уничтожения камня
    {
        base.OnDestroyEvent.RemoveListener(OnStoneDestroyed);
    }

    private void OnStoneDestroyed() //Уничтожение камня и спавн новых если размер не Small
    {
        if (size != Size.Small)
        {
            SpawnStones();
        }

        levelProgress.UpdateProgressBar();

        Destroy(gameObject);
    }
    private void SpawnStones() //Спавн двух новых новых камней и по рандому возможность спавна монет, заморозки и режим бога
    {
        int rndCoin = Random.Range(1, maxCoinRandom);
        if (rndCoin == 2)
        {
            Coin coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.GetComponent<Movement>().AddVertiсalVelocity(spawnUpForce / 2);
        }

        int rndFreezing = Random.Range(1, maxFreezingRandom);
        if (rndFreezing == 3)
        {
            Freezing freezing = Instantiate(freezingPrefab, transform.position, Quaternion.identity);
            freezing.GetComponent<Movement>().AddVertiсalVelocity(spawnUpForce / 2);
        }

        int rndGodMode = Random.Range(1, maxGodModeRandom);
        if (rndGodMode == 4)
        {
            GodMode godmode = Instantiate(godModePrefab, transform.position, Quaternion.identity);
            godmode.GetComponent<Movement>().AddVertiсalVelocity(spawnUpForce / 2);
        }

        for (int i = 0; i < 2; i++)
        {
            stoneColorChange.ChangeColor();
            Stone stone = Instantiate(this, transform.position, Quaternion.identity);
            stoneOffset.z += 0.003f;
            transform.position += stoneOffset;

            stone.SetSize(size - 1);
            stone.maxHealth = Mathf.Clamp(maxHealth / 2, 1, maxHealth/2);
            stone.stoneMovement.AddVerticalVelocity(spawnUpForce);
            stone.stoneMovement.SetHorizontalDirection((i % 2 * 2) - 1);            
        }
    }

    //Задает размер камня в зависимости от типа
    private Vector3 GetVectorFromSize(Size size)
    {
        if (size == Size.Huge) return new Vector3(1, 1, 1);
        if (size == Size.Big) return new Vector3(0.75f, 0.75f, 0.75f);
        if (size == Size.Normal) return new Vector3(0.6f, 0.6f, 0.6f);
        if (size == Size.Small) return new Vector3(0.4f, 0.4f, 0.4f);

        return Vector3.one;
    }

    //Задает размер
    public void SetSize(Size size)
    {
        if (size < 0) return;

        transform.localScale = GetVectorFromSize(size);
        this.size = size;
    }

    //Получить размер.
    public Size GetSize()
    {
        return size;
    }

    //Метод заморозки камня.
    public void FreezingStone()
    {
        timerZ = 0;
        isFreezing = true;

        stoneMovement.IsFreezing = true;
    }
}
