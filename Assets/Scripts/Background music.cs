using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundmusic : MonoBehaviour
{
    public AudioSource backgroundMusic;

    void Start()
    {
        backgroundMusic.Play();
    }
}
