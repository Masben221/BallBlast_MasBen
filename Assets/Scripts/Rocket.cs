using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
  Rigidbody rigidBody;
  AudioSource audioSource;
  [SerializeField] int energyToAdd;
  [SerializeField] Text energyText;
  [SerializeField] int energyTotal = 2000;
  [SerializeField] int energyApply = 5;
  [SerializeField] float rotSpeed = 100f;
  [SerializeField] float flySpeed = 100f;
  [SerializeField] AudioClip flySound;
  [SerializeField] AudioClip boomSound;
  [SerializeField] AudioClip finishSound;
  [SerializeField] AudioClip finishlySound;
   
  [SerializeField] ParticleSystem FinishParticles;
  [SerializeField] ParticleSystem FlyParticles;
  [SerializeField] ParticleSystem DeadParticles;
   
  enum State {Playing, Dead, Nextlevel};
  State state = State.Playing;
  bool collisionOff = false;
  int currentlevelIndex;
  int nextlevelIndex;
  
void Start()
  {  
     energyText.text = energyTotal.ToString();
     DeadParticles.Stop();
     state = State.Playing;
     rigidBody=GetComponent<Rigidbody>();
     audioSource=GetComponent<AudioSource>();
  }

void Update()
  {
        if (state == State.Playing) 
      { 
       Launch();
       Rotation();
       Quit();
       if (Debug.isDebugBuild) 
       {
        DebugKeys();
       }
     }
  }
void Quit()
  {
      if (Input.GetKeyDown(KeyCode.Q))  // если нажата клавиша Q 
       {
         Application.Quit(); // закрыть приложение
         Debug.Log("Нажал кнопку выхода");   
       }
      if (Input.GetKeyDown(KeyCode.Escape))  // если нажата клавиша Escape
       {
         SceneManager.LoadScene(0);    // загрузить главное меню
       }
  }
void DebugKeys()
  {
     if (Input.GetKeyDown(KeyCode.L))
      {
        LoadNextLevel();
      }
     else if (Input.GetKeyDown(KeyCode.C))
      {
       collisionOff = !collisionOff;
      } 
  }
void OnCollisionEnter (Collision collision) 
  { 
      if (state == State.Dead||state == State.Nextlevel||collisionOff) // или состояние мертвый или следующий уровень или коллизииофф - правда
       {
       return;
       }
      switch(collision.gameObject.tag) 
      {
        case "Friendly":
          print("OK");
        break;
        case "Finish": 
          Finish();
        break;
        case "Battery": 
          PlusEnergy(energyToAdd, collision.gameObject);
        break;
        default: 
          Dead();
        break;
      }
  }
void PlusEnergy(int energyToAdd, GameObject batteryObj)
  { 
    batteryObj.GetComponent<CapsuleCollider>().enabled = false;
    energyTotal += energyToAdd;
    energyText.text = energyTotal.ToString();
    Destroy(batteryObj, 0.1f);
  }
void Dead()
  {
    state = State.Dead;
    audioSource.Stop();
    audioSource.PlayOneShot(boomSound);
    DeadParticles.Play();
    FlyParticles.Stop();
    Invoke ("LoadFirstLevel", 3f);
  }
void Finish()
  {
    state = State.Nextlevel;
    audioSource.Stop();
    audioSource.PlayOneShot(finishSound);
    audioSource.PlayOneShot(finishlySound);
    FinishParticles.Play();
    DeadParticles.Stop();
    Invoke ("LoadNextLevel", 10f);  
  }
void LoadNextLevel() // Finish
  {
    currentlevelIndex = SceneManager.GetActiveScene().buildIndex;
    nextlevelIndex = currentlevelIndex+1;
    
    if (nextlevelIndex == SceneManager.sceneCountInBuildSettings)
     {
       nextlevelIndex = 0; // цикл
     }
    SceneManager.LoadScene(nextlevelIndex);
  }
void LoadFirstLevel() // Lose
  { 
    currentlevelIndex = SceneManager.GetActiveScene().buildIndex;
    nextlevelIndex = currentlevelIndex-1;
    SceneManager.LoadScene(nextlevelIndex);
  }
void Launch()
  {
    float LaunchSpeed = flySpeed * Time.deltaTime;
    if (Input.GetKey(KeyCode.Space) && energyTotal > 0)
        { 
          energyTotal -= Mathf.RoundToInt(energyApply*Time.deltaTime);
          energyText.text = energyTotal.ToString();
          print(energyTotal);
          rigidBody.AddRelativeForce(Vector3.up*LaunchSpeed);
                    if(audioSource.isPlaying==false)
           {
            audioSource.PlayOneShot(flySound);
            FlyParticles.Play();
           }
        }
    else 
        {
            audioSource.Pause();
            FlyParticles.Stop();
        }
  }
void Rotation() 
  {
    float rotationSpeed = rotSpeed * Time.deltaTime;

    rigidBody.freezeRotation=true;
      if (Input.GetKey(KeyCode.A))     
        {
          transform.Rotate(Vector3.forward*rotationSpeed); // другой вариант с регулировкой силы вращения transform.Rotate(new Vector3(0,0,1))
        }
      else if (Input.GetKey(KeyCode.D))     
        {
          transform.Rotate(Vector3.back*rotationSpeed); // transform.Rotate(new Vector3(0,0,-1))
        }
    rigidBody.freezeRotation=false;
  }

}