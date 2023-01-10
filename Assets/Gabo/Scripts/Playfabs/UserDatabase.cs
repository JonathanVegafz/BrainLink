using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
//Esta clase es usada para almacenar los usuarios que se registraron con exito en un archivo json
public class UserDatabase
{
    public string userDatabase;
    public string password;
    public UserDatabase()
    {
        userDatabase = "";
        password = "";
    }
    
}
