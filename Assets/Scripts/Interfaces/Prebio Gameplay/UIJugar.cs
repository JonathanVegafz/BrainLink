using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIJugar : UIManager
{
    public float vel;

    public void ActivarAnimacion(Button boton)
    {


        Transicion(this.gameObject, boton, this.gameObject.GetComponent<Image>().color.a == 1 ? 0 : 1);
    }

    

        
}
