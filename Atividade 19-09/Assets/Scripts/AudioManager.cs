using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : IPersistentSingleton<AudioManager>
{
    public AudioSource bgm;
    public AudioSource bgmWin;

    public bool winner = false;

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
        //Verifica se deve ou não tocar a musica de vitoria
        if (!winner)
        {
            //Para a outra Musica
            bgmWin.Stop();
            //Verifica se a musica ja está tocando
            if (!bgm.isPlaying)
            {
                //Toca a Musica
                bgm.Play();

            }
        }
        else
        {
            //Para a outra Musica
            bgm.Stop();
            //Verifica se a musica ja está tocando
            if (!bgmWin.isPlaying)
            {
                //Toca a Musica
                bgmWin.Play();

            }
        }
    }
}
