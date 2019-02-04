using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_data : MonoBehaviour {
    [Header("Player")]
    public AudioClip shoot;
    public AudioClip[] Sword;
    public AudioClip Exposion;
    public AudioClip Heal;
    public AudioClip shooting_skill;
    public AudioClip jump_boot;

    [Header("Enemy")]
    public AudioClip enemy_sound;
    public AudioClip enemy_die;
}