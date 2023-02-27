using UnityEngine;

public class GodMode : MonoBehaviour
{
    private LevelState levelState;
    private ParticleSystem cartParticleSystems; //Ссылка на ParticleSystem турели

    private void Awake()
    {
        //Находим объекты по типу
        levelState = FindObjectOfType<LevelState>();
        cartParticleSystems = FindObjectOfType<CartParticleSystem>().GetComponent<ParticleSystem>();
    }

    //Включаем режи бога при столкновении с турелью
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Turret turret = collision.transform.root.GetComponent<Turret>();

        if (turret == true)
        {
            levelState.IsGodMode = true;
            levelState.ResetTimer(); //Сброс таймера при повторном поднятии режима бога
            cartParticleSystems.Play();
            Destroy(gameObject);
        }
    }
}

