using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class EventosAnim : MonoBehaviour //Prodia poner herencia
{
    public static Button botonActivar;
    public static TMP_Text timerText;

    public static Animator anim;

    public void Awake()
    {
        botonActivar = transform.GetChild(2).GetComponent<Button>();
        timerText = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
    }

    public void ActivarBoton()
    {
        transform.GetChild(2).GetComponent<Button>().interactable = true;
    }

    public void CambiarImagen1()
    {

        transform.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>(UIElegirEventos.evento1.Atributos_.idImagen);
    }
    public void CambiarImagen2()
    {
        transform.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>(UIElegirEventos.evento2.Atributos_.idImagen);
    }


}
