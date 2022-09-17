using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgm;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlayBgm();
    }


    public void PlayBgm()
    {
        //Verifica se a musica ja está tocando
        if (!bgm.isPlaying)
        {
            //Toca a Musica
            bgm.Play();
          
        }
    }
}
