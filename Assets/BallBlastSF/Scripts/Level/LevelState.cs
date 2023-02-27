using UnityEngine;
using UnityEngine.Events;

public class LevelState : MonoBehaviour
{
    [SerializeField] private StoneSpawner spawner;
    [SerializeField] private Cart cart;
    [SerializeField] private ParticleSystem cartParticleSystems; //Ссылка на систему частиц турели
    [SerializeField] private GameObject storeMenuPanel; //Ссылка на StoreMenu
    [SerializeField] private UpgradeMenu upgradeMenu;

    [SerializeField] private float godModetime; //Время действия режима бога    
    public float GodModetime { get => godModetime; set => godModetime = value; }//Время действия режима бога - свойство

    [Space(5)]
    public UnityEvent Passed; // победа
    public UnityEvent Defeat; // поражение

    private float timer; // таймер проверки окончания уровня
    private float timerGodMode; //Таймер окнчания режима бога
    public float TimerGodMode { get => timerGodMode; set => timerGodMode = value; }//Таймер окнчания режима бога - свойство

    private bool chekPassed;  //Флаг окончания спавна камней
    private bool isGodMode; //Флаг режима бога
    private bool isMenuStore = false; //Флаг меню магазина
    public bool IsGodMode { get => isGodMode; set => isGodMode = value; } //Флаг режима бога
    public bool IsMenuStore { get => isMenuStore; set => isMenuStore = value; } //Флаг меню магазина

    private void Awake()
    {
        spawner.CompletedEvent.AddListener(OnSpawnCompleted);
        cart.CollisionStone.AddListener(OnCartCollisionStone);
    }

    private void OnDestroy()
    {
        spawner.CompletedEvent.RemoveListener(OnSpawnCompleted);
        cart.CollisionStone.RemoveListener(OnCartCollisionStone);
    }

    protected virtual void OnCartCollisionStone() // Событие проигрыша. Игра останавливается
    {
        if (isGodMode == true) return; //Если включен режим бога, то коллизия столкновения не происходит

        Defeat.Invoke();        
    }

    private void OnSpawnCompleted() // событие окончания спавна
    {        
        chekPassed = true;        
    }

    public void ResetTimer()
    {
        timerGodMode = 0;
    }       

    private void Update() 
    {
        timer += Time.deltaTime;

        if (timer > 0.5f)// таймер проверки окончания каждые 0,5с
        {
           if ( chekPassed == true)
            {
                if (FindObjectsOfType<Stone>().Length == 0 & isMenuStore == false)// Проверка наличия камней на сцене и неактивности меню
                {
                    Passed.Invoke();                    
                }
            }
            
             timer = 0;
        }

        //Проверка на окнчание времени действия режима бога
        if (isGodMode == true)
        {
            timerGodMode += Time.deltaTime;
            upgradeMenu.UpdateGodModeText();

            if (timerGodMode > godModetime)
            {
                ResetTimer();
                upgradeMenu.UpdateGodModeText();
                cartParticleSystems.Stop();
            }
        }               

    }
}
