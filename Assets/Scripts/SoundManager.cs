using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{

    //Sound Coin
    static AudioSource MusicSource;
    public static  AudioClip CoinSound;
    public static AudioClip BombSound;
    public static AudioClip RunSound;


    // Start is called before the first frame update
    void Start()
    {
        CoinSound = Resources.Load<AudioClip>("CoinSound");
        BombSound = Resources.Load<AudioClip>("BombExplosion");
        RunSound = Resources.Load<AudioClip>("ActionSound");


        MusicSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "coin":
                MusicSource.PlayOneShot(CoinSound);break;

            case "bomb":
                MusicSource.PlayOneShot(BombSound); break;


            case "run":
                MusicSource.PlayOneShot(RunSound); break;
        }
    }
}
