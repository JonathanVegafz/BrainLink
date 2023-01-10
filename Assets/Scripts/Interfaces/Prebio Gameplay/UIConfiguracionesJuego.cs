using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class UIConfiguracionesJuego : UIManager
{
    public SistemaIdiomas sistemaIdiomas;

    public void Idioma(bool spanish)
    {
        GameData.textosIdioma = GameSettings.JsonIdioma(spanish);
        sistemaIdiomas.CambioIdioma();
    }
}
