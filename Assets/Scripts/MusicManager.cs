using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    //SINGLETON --------------------------------------------------------

    private static MusicManager instance;

    public static MusicManager Instance { get { return instance; } }

    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (instance != null & instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
