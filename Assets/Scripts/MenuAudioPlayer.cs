using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioPlayer : MonoBehaviour
{
    public static MenuAudioPlayer Instance;

    private AudioSource player;
    AudioClip menuMusic;
    public bool isPlaySound = true;
    public bool isPlayBGM = true;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        player = GetComponent<AudioSource>();
        menuMusic = Resources.Load<AudioClip>("sounds\\MenuMusic");

        player.clip = menuMusic;
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
