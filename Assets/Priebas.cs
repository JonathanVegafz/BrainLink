using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Priebas : UIManager
{
    public void Boton(Button boton)
    {
        Transicion(this.gameObject, boton, 0);
    }
}
