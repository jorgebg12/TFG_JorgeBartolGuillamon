using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Item",menuName ="Player item")]
public class PlayerItem : ScriptableObject
{
    public new string name;
    public Sprite image;

    public GameObject itemModel;

    public virtual void OnUse()
    {
        Debug.Log("Not implemented");
    }

}
