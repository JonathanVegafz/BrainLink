using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent onInicioAplicacion = new UnityEvent();
    [HideInInspector] public UnityEvent onInicioJuego = new UnityEvent();

    void Awake()
    {
        //GameData.textosIdioma = GameSettings.JsonIdioma(true);
        onInicioAplicacion.Invoke();
        GameData.ColocarConceptosYPreguntas();
    }

    public void Yolo(string nombre)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
