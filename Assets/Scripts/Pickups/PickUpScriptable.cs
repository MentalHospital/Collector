using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup", menuName = "Pickup/New Pickup")]
public class PickUpScriptable : ScriptableObject
{
    public PickupRedux pickup;
    public GameObject model;
    public Sprite spriteUI;
    public GameObject particleObject;
    public new string name;
    public int value;
    public float time;
    public AudioClip audioClip;
}
