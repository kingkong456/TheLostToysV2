using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toy : MonoBehaviour {

    public enum toy_type
    {
        m_attack,
        r_attack,
        speed,
        fake_target,
        heal,
        jump_boot,
        shile
        ,default_type
    };

    public string name;
    public toy_type type;
    public Sprite icon;
    public float damge;
    public float skill_damge;
    public float mana_skill_use;
    public Sprite status_icon;
    public Sprite Skill_icon;

    [Header("M attack")]
    public GameObject skill_fx_pototype;

    [Header("R Attack")]
    public GameObject bullet_pototype;
    public GameObject bullet_skill_pototype;
    public GameObject exposion_fire_point;

    [Header("Faker target")]
    public GameObject fake_target_pototype;
    public bool is_canMove;

    [Header("Heal Hp")]
    public float heal_point;
    public GameObject Heal_Fx_pototype;

    [Header("jump boot")]
    public float duration_boot;
    public float var_boot_much_more;

    [Header("UnKnow")]
    public GameObject poto;
}