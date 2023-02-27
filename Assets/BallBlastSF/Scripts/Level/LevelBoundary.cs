using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    public static LevelBoundary Instance; // √раница уровн€  Ёкземпл€р;

    [SerializeField] Vector2 screenResolution; // разрешени€ экрана
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Ќе уничтожать при загрузке

        if (Application.isEditor == false && Application.isPlaying == true)
        {
            screenResolution.x = Screen.width;
            screenResolution.y = Screen.height;
        }
    }

    public float LeftBorder // левый край
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        }
    }

    public float RightBorder // правый край
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(screenResolution.x, 0, 0)).x;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(LeftBorder, -10, 0), new Vector3(LeftBorder, 10, 0));
        Gizmos.DrawLine(new Vector3(RightBorder, -10, 0), new Vector3(RightBorder, 10, 0));
    }
#endif
}
