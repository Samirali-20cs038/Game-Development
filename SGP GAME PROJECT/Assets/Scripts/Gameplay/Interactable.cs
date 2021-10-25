using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface is for defining the functions and not include any logic  
public interface Interactable
{
    void Interact(Transform initiator);
}
