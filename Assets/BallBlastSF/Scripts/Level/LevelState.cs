using UnityEngine;
using UnityEngine.Events;

public class LevelState : MonoBehaviour
{
    [SerializeField] private StoneSpawner spawner;
    [SerializeField] private Cart cart;
    [SerializeField] private ParticleSystem cartParticleSystems; //������ �� ������� ������ ������
    [SerializeField] private GameObject storeMenuPanel; //������ �� StoreMenu
    [SerializeField] private UpgradeMenu upgradeMenu;

    [SerializeField] private float godModetime; //����� �������� ������ ����    
    public float GodModetime { get => godModetime; set => godModetime = value; }//����� �������� ������ ���� - ��������

    [Space(5)]
    public UnityEvent Passed; // ������
    public UnityEvent Defeat; // ���������

    private float timer; // ������ �������� ��������� ������
    private float timerGodMode; //������ �������� ������ ����
    public float TimerGodMode { get => timerGodMode; set => timerGodMode = value; }//������ �������� ������ ���� - ��������

    private bool chekPassed;  //���� ��������� ������ ������
    private bool isGodMode; //���� ������ ����
    private bool isMenuStore = false; //���� ���� ��������
    public bool IsGodMode { get => isGodMode; set => isGodMode = value; } //���� ������ ����
    public bool IsMenuStore { get => isMenuStore; set => isMenuStore = value; } //���� ���� ��������

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

    protected virtual void OnCartCollisionStone() // ������� ���������. ���� ���������������
    {
        if (isGodMode == true) return; //���� ������� ����� ����, �� �������� ������������ �� ����������

        Defeat.Invoke();        
    }

    private void OnSpawnCompleted() // ������� ��������� ������
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

        if (timer > 0.5f)// ������ �������� ��������� ������ 0,5�
        {
           if ( chekPassed == true)
            {
                if (FindObjectsOfType<Stone>().Length == 0 & isMenuStore == false)// �������� ������� ������ �� ����� � ������������ ����
                {
                    Passed.Invoke();                    
                }
            }
            
             timer = 0;
        }

        //�������� �� �������� ������� �������� ������ ����
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
