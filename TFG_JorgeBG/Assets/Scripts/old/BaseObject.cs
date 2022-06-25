using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public new string name;
    public Sprite image;
    public GameObject itemModel;

    public virtual void OnUse()
    {

    }
}
