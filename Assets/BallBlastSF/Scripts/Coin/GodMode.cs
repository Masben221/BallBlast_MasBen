using UnityEngine;

public class GodMode : MonoBehaviour
{
    private LevelState levelState;
    private ParticleSystem cartParticleSystems; //������ �� ParticleSystem ������

    private void Awake()
    {
        //������� ������� �� ����
        levelState = FindObjectOfType<LevelState>();
        cartParticleSystems = FindObjectOfType<CartParticleSystem>().GetComponent<ParticleSystem>();
    }

    //�������� ���� ���� ��� ������������ � �������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Turret turret = collision.transform.root.GetComponent<Turret>();

        if (turret == true)
        {
            levelState.IsGodMode = true;
            levelState.ResetTimer(); //����� ������� ��� ��������� �������� ������ ����
            cartParticleSystems.Play();
            Destroy(gameObject);
        }
    }
}

