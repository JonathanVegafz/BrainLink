using System.Collections;
using System.Collections.Generic;
using HistoryHerramientas.Cartas;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CartaConcepto : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource sonidoVuelta;
    public AudioSource sonidoSinSeleccionar;
    public AudioSource sonidoSeleccionar;
    public Animation anim;

    public TMP_Text[] conceptos = new TMP_Text[3];

    public Concepto cartaConcepto;

    public UnityEvent OnClick = new UnityEvent();

    [HideInInspector]
    public bool activarCarta;

    [HideInInspector]
    public bool cartaDesactivada;

    public void OnEnable()
    {
        //botonActivador.onClick.AddListener(() => UIElegirConceptos.ColocarConcepto(cartaConcepto)); 
    }

    public void PlaySonido()
    {
        sonidoVuelta.Play();
    }

    public void OnPointerDown(PointerEventData eventData)// Me va a servir
    {
        if (activarCarta && cartaDesactivada == false)
        {
            UIElegirConceptos.ColocarConcepto(cartaConcepto, this, this.gameObject);
            if (OnClick != null)
            {
                sonidoSeleccionar.Play();
                OnClick.Invoke();
            }
        }
    }

    public void Salida()
    {
        anim.Play("SalidaCarta");
        transform.GetChild(1).GetComponent<Canvas>().sortingOrder = 0;
    }

    public void SonidoSinSeleccionar()
    {
        sonidoSinSeleccionar.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (activarCarta && cartaDesactivada == false)
        {
            anim.Play("EntradaCarta");
            transform.GetChild(1).GetComponent<Canvas>().sortingOrder = 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (activarCarta && cartaDesactivada == false)
        {
            anim.Play("SalidaCarta");
            transform.GetChild(1).GetComponent<Canvas>().sortingOrder = 0;
        }
    }
}                     
