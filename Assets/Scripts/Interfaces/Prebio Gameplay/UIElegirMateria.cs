using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIElegirMateria : UIManager
{
    public Image materia;

    public TMP_Text textoMateria;
    public void CambiarTexto(TMP_Text texto)
    {
        textoMateria.text = texto.text;
    }

    public void BotonSiguiente(Button boton)
    {
        GameData.materia = textoMateria.text;
        
        Transicion(this.gameObject, boton, this.gameObject.GetComponent<Image>().color.a == 1 ? 0 : 1);
    }

    public void Retroseso (Sprite araña)
    {
        materia.sprite = araña;
    }
}
