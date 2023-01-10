using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using HistoryHerramientas.Cartas;

public class UIManager : MonoBehaviour
{ 

    public class Animaciones_
    {
        public void AbrirVentana(float timer, GameObject objeto)
        {

        }

        public void CerrarVentana(float timer, GameObject objeto)
        {

        }     
    }
    private Animaciones_ animaciones = new Animaciones_();
    public Animaciones_ Animaciones { get { return animaciones; } }

    public void ActivarDesactivarBoton(Image imagen, bool estadoBoton = false)
    {
        imagen.color = new Color(imagen.color.r, imagen.color.g, imagen.color.b, estadoBoton == true ? 255 : 0);
    }



    public void CerrarVentana(GameObject objeto)
    {
        objeto.SetActive(false);
    }
    public void BarraTiempo(float tiempo, TMP_Text texto)
    {
        int minutos = (int)tiempo / 60;
        int segundos = (int)tiempo % 60;

        texto.text = minutos.ToString() + ":" + segundos.ToString().PadLeft(2, '0');
    }

    public void Transicion(GameObject gameObject_, int alfa)
    {
        if (gameObject_.TryGetComponent(out Image objeto))
        {

            objeto.DOFade(alfa, 0.5f).OnComplete(() =>
            {
                if (alfa == 0)
                {
                    gameObject_.SetActive(false);
                }
                //if (objeto.color.a == 1) boton.interactable = true;
            }).Delay();
            //objeto.DOFade(objeto.color.a == 1 ? 0 : 1, 1f);
        }

        if (gameObject_.TryGetComponent(out TMP_Text texto))
        {
            texto.DOFade(alfa, 0.5f);
        }

        Hijos(gameObject_.transform, alfa);
    }
    public void Transicion(GameObject gameObject_, Button boton, int alfa)
    {

        if(gameObject_.TryGetComponent(out Image objeto))
        {
            
            objeto.DOFade(alfa, 0.5f).OnComplete(()=> 
            {
                if (alfa == 0)
                {
                    gameObject_.SetActive(false);
                    boton.interactable = true;
                }
                //if (objeto.color.a == 1) boton.interactable = true;
            }).Delay();
            //objeto.DOFade(objeto.color.a == 1 ? 0 : 1, 1f);
        }

        if(gameObject_.TryGetComponent(out TMP_Text texto))
        {
            texto.DOFade(alfa, 0.5f);
        }

        Hijos(gameObject_.transform, alfa);
    }

    private void Hijos(Transform transform, int alfa)
    {
        if(transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent(out Image imagen))
                {
                    imagen.DOFade(alfa, 0.5f);
                }

                if (transform.GetChild(i).TryGetComponent(out TMP_Text texto))
                {
                    texto.DOFade(alfa, 0.5f);
                }

                if (transform.GetChild(i).childCount != 0)
                {
                    Hijos(transform.GetChild(i), alfa);
                }
            }
        }
        else
        {
            return;
        }
    }
}                                                             
