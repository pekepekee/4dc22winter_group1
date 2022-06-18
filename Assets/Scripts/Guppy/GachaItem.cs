using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GachaItem", menuName = "Guppy/GachaItem", order = 1100)]
public class GachaItem : ScriptableObject
{
    public string itemName = "‚¨‚³‚©‚È";
    public Sprite itemImage;
    public GameObject live2DModel;
    public float live2DShowDuration = 1.0f;
    public bool isGameClearItem = false;
}
