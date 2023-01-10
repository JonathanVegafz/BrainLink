using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using History.Jugadores;
using System.Linq;

public class UIPuntajes : UIManager
{

    public List<Image> iamgenPodio;
    public List<Image> imagenBarra;
    public List<TMP_Text> puntajes;
    public List<TMP_Text> nombres;


    public void OnEnable()
    {
        List<Expositor> expositores = new List<Expositor>(GameData.expositores);
        for (int i = 0; i < expositores.Count - 1; i++)
        {
            for (int j = 0; j < expositores.Count - i - 1; j++)
            {
                if (expositores[j].puntajeTotal < expositores[j + 1].puntajeTotal)
                {
                    Expositor aux = expositores[j];
                    expositores[j] = expositores[j + 1];
                    expositores[j + 1] = aux;
                }
            }
        }

        for (int i = 0; i < expositores.Count; i++)
        {
            iamgenPodio[i].sprite = expositores[i].avatar;
            imagenBarra[i].sprite = expositores[i].avatar;
            puntajes[i].text = expositores[i].puntajes.puntajeTotal + " Pts.";
            nombres[i].text = expositores[i].nombre;
        }

    }

}
