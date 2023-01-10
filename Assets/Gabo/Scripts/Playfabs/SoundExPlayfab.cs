using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[RequireComponent(typeof(AudioSource))]
public class SoundExPlayfab : MonoBehaviour
{
    public List<AudioClip> SoundList = new List<AudioClip>();
    public AudioSource audioSource;
    private void Awake()
    {
        audioSource = GameObject.Find("ControlSesionPlayfab").GetComponent<AudioSource>();
    }
    /// <summary>
    /// Cargamos el clip segun el comportamiento list(0) = Boton, (1) inicio sesion, (2) error (3) ventana emergente
    /// </summary>
    /// <param name="clip"></param>
    public void AudioLogin(int clip)
    {
        if (SoundList.Count > 0)
        {
            audioSource.clip = SoundList[clip];
            audioSource.PlayOneShot(SoundList[clip]);
        }
    }
}
