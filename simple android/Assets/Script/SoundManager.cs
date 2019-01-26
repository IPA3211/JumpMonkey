using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioClip jumpSound
                    , dashSound
                    , deadSound
                    , getMoneySound;

    public enum Sound {Jump, Dash, Dead, getMoney }
    private AudioSource audio;

    // Use this for initialization
    void Start () {
        audio = gameObject.GetComponent<AudioSource>();
	}

    public void PlayOneShot(SoundManager.Sound sound) {
        switch (sound) {
            case SoundManager.Sound.Jump:
                audio.PlayOneShot(jumpSound);
                break;
            case SoundManager.Sound.Dash:
                audio.PlayOneShot(dashSound);
                break;
            case SoundManager.Sound.Dead:
                audio.PlayOneShot(deadSound);
                break;
            case SoundManager.Sound.getMoney:
                audio.PlayOneShot(getMoneySound);
                break;
        }
    }
}
