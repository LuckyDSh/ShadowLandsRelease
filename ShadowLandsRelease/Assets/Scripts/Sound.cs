/*
* TickLuck Team
* All rights reserved
*/

using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    [Tooltip("0 is for 2D, 1 for 3D")]
    public float blend;

    public bool loop;
    public bool playOnAwake;

    [Header("3D sound")]
    [Space]
    public float minDistance;
    public float maxDistance;

    [HideInInspector]
    public AudioSource source;
}