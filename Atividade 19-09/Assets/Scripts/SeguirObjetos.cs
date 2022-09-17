using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirObjetos : MonoBehaviour
{

    public GameObject ObjSeguido;   //objeto que a camera deve seguir
    Vector3 espaco; // distancia entre obj e camera
    // Start is called before the first frame update
    void Start()
    {
        espaco = transform.position - ObjSeguido.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = ObjSeguido.transform.position + espaco;
    }
}
