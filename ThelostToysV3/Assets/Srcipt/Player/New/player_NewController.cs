using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    [RequireComponent(typeof(Controller))]
    [RequireComponent(typeof(joyInput))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(sound_data))]
    public class player_NewController : MonoBehaviour
    {
        //m controller
        private Controller m_controller;
        private ThirdPersonCharacter m_ThirPersonController;
        private joyInput m_joyInput;
        public Animator m_animator;
        public player_Inhand m_hand;
        private playerData m_data;
        private sound_data m_sound;
        private AudioSource m_audio;

        //camera
        public Transform m_camera;
        private Vector3 m_cameraForward;

        //boolean state check
        private bool m_jump;
        private bool m_crouch;
        private bool m_isGrass;
        private Vector3 m_move;
        public bool m_isCraftingState = false;

        //toy use
        private string toy_use = "";

        [Header("Slot")]
        private int index_slotSelect = 0;
        public newItemSlotmanager m_slotManager;

        [Header("Camera Rotate")]
        public float cam_rotation_speed;

        [Header("attack and cute treer")]
        public Collider melee_col;
        public Collider M_skill_col;
        public Transform m_skill_spawn_point;
        public float axe_range;
        public GameObject cutTree_Button;
        private int index_toy_using;
        //combo system
        private bool is_can_input_combo = true;
        private int index_combo_attack;

        //use for range attack
        [Header("R Attack")]
        public Transform bullet_spawn_point;
        private bool is_can_input_R_Combo = true;
        private int index_combo_r_attack;

        //use for shile system eiei
        [Header("Shile System")]
        public GameObject shile;
        private bool isUseShile = false;

        //Rolling
        [Header("Rolling")]
        public float cooldown_time_roll = 0.5f;
        private float last_time_roll = 0;
        public float roll_foce;

        [Header("jump")]
        public float start_jump_foce;
        private float jumpfoce;
        private float jump_duration_timer;

        [Header("Status")]
        public Image status_Image;

        //setting varible and player system
        private void Start()
        {
            m_controller = GetComponent<Controller>();
            m_ThirPersonController = GetComponent<ThirdPersonCharacter>();
            m_joyInput = GetComponent<joyInput>();
            m_animator = GetComponent<Animator>();
            m_data = GetComponent<playerData>();
            m_sound = GetComponent<sound_data>();
            m_audio = GetComponent<AudioSource>();

            //set default jump power
            start_jump_foce = m_ThirPersonController.m_JumpPower;
        }

        private void Update()
        {
            if(!m_jump)
            {
                m_jump = Input.GetButtonDown(m_controller.button3_input);
            }
        }

        private void LateUpdate()
        {
            if(m_jump && m_ThirPersonController.m_JumpPower != start_jump_foce)
            {
                m_audio.PlayOneShot(m_sound.jump_boot);
            }

            //player input movement
            float horizontal = Input.GetAxis(m_controller.leftStick_Horizontal);
            float vertical = Input.GetAxis(m_controller.leftStick_Vertical);
            Debug.Log(Input.GetButton(m_controller.triger_R2_L_button));
            m_crouch = Input.GetButton(m_controller.triger_R2_L_button);

            //player calculate movement
            m_cameraForward = Vector3.Scale(m_camera.forward, new Vector3(1, 0, 1)).normalized;
            m_move = vertical * m_cameraForward + horizontal * m_camera.right;

            //display player movement
            m_ThirPersonController.Move(m_move, m_crouch, m_jump);
            m_jump = false;

            if(jump_duration_timer > 0)
            {
                jump_duration_timer -= Time.deltaTime;
            }
            else if(jump_duration_timer <= 0)
            {
                reset_boot();
            }

            //check visible enemy
            if(m_isGrass && m_crouch)
            {
                //visible enemy false
                //hide from enemy
                //rend .a ลดลง
                this.gameObject.tag = "Untagged";
            }
            else if(!m_crouch ||! m_isGrass)
            {
                //visile enemy true
                //show to
                //rend .a ปรกติ
                this.gameObject.tag = "Player";
            }


            if(Input.GetButtonDown(m_controller.button1_input))
            {
                //use item
                use_toy_number(index_slotSelect);
                m_slotManager.remove_PlayerToy(index_slotSelect);
                index_slotSelect = 0;
            }
            else if(Input.GetButton(m_controller.triger_L2_L_button))//shile system
            {
                active_Shile();
            }
            else if(Input.GetButtonUp(m_controller.triger_L2_L_button))
            {
                de_shile();
            }
            else if(Input.GetButtonDown(m_controller.triger_L1_L_button) && (index_slotSelect + 1) < (m_slotManager.m_toys.Count - 1))
            {
                if (m_slotManager.m_slot[index_slotSelect] != null)
                {
                    m_slotManager.m_slot[index_slotSelect].unselect();
                }
                index_slotSelect++;
                m_slotManager.m_slot[index_slotSelect].select();
            }
            else if(Input.GetButtonDown(m_controller.triger_R1_L_button) && (index_slotSelect - 1) > 0)
            {
                if (m_slotManager.m_slot[index_slotSelect] != null)
                {
                    m_slotManager.m_slot[index_slotSelect].unselect();
                }
                index_slotSelect--;
                m_slotManager.m_slot[index_slotSelect].select();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                m_slotManager.add_PlayerToy(m_data.m_toys[0]);
            }

            m_animator.SetBool("Shile", isUseShile);
        }

        //use toy
        void use_toy_number(int index)
        {
            //debug error
            if(m_slotManager.m_toys[index] == null)
            {
                return;
            }

            //check frist time use toy
            if(toy_use != "")
            {
                m_hand.hide_toy(toy_use);
            }

            //show new weapon
            if (m_slotManager.m_toys[index].type != Toy.toy_type.fake_target)
            {
                toy_use = m_slotManager.m_toys[index].name;
                m_hand.active_toy(toy_use);
            }

            //setting toy number toy now
            index_toy_using = index;

            switch (m_slotManager.m_toys[index].type)
            {
                case Toy.toy_type.m_attack:
                    //m_attack
                    combo_m_attackStart();
                    break;
                case Toy.toy_type.r_attack:
                    combo_r_attackStart();
                    //r_attack
                    break;
                case Toy.toy_type.speed:
                    break;
                case Toy.toy_type.fake_target:
                    //faker item 
                    dropFaker();
                    break;
                case Toy.toy_type.heal:
                    //healing
                    healing();
                    break;
                case Toy.toy_type.default_type:
                    break;
                case Toy.toy_type.jump_boot:
                    //incraea jump fore
                    //in duration
                    jump_boot();
                    break;
                default:
                    break;
            }
        }

        #region R_attack

        //******************************************************

        //-1 is reset r_attack
        //0 is straeder var
        //next ++ is push push

        //start combo
        //check it stillcombo yet
        //push combo number
        void combo_r_attackStart()
        {
            if(is_can_input_R_Combo)
            {
                index_combo_r_attack++;
            }

            if(index_combo_r_attack == 1)
            {
                m_animator.SetInteger("R_attack", 1);
            }
        }

        //ckecker what should player do next animation
        void combo_r_attackChecker()
        {
            is_can_input_R_Combo = false;

            r_attack_to_idle_state();
        }

        //reset r attack
        //goto idle state
        void r_attack_to_idle_state()
        {
            m_animator.SetInteger("R_attack", -1);
            is_can_input_R_Combo = true;
            index_combo_r_attack = 0;
        }

        //******************************************************

        void shoot()
        {
            GameObject exposion = Instantiate(m_slotManager.m_toys[index_toy_using].exposion_fire_point, bullet_spawn_point.position, bullet_spawn_point.rotation);
            GameObject bullet = Instantiate(m_slotManager.m_toys[index_toy_using].bullet_pototype, bullet_spawn_point.position, bullet_spawn_point.rotation);
            bullet.GetComponent<bullet_Col>().dmg = m_slotManager.m_toys[index_toy_using].damge;
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6f;
            Destroy(exposion, 2);

            //play sound
            m_audio.PlayOneShot(m_sound.shoot);
        }

        #endregion

        #region m_attack

        //comboattack System
        //***********************************

        //start combo
        //check it stillcombo yet
        //push combo number
        void combo_m_attackStart()
        {
            if (is_can_input_combo)
            {
                index_combo_attack++;
            }

            if(index_combo_attack == 1)
            {
                m_animator.SetInteger("M_attack", 1);
            }
        }

        void play_sword_sound()
        {
            m_audio.PlayOneShot(m_sound.Sword);
        }

        //ckecker what should player do next animation
        void combo_checker()
        {
            is_can_input_combo = false;

            if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("M_attack1") && index_combo_attack == 1)
            {
                goto_groundState();
            }
            else if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("M_attack1") && index_combo_attack >= 2)
            {
                m_animator.SetInteger("M_attack", 2);
                is_can_input_combo = true;
            }
            else if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("M_attack2") && index_combo_attack > 2)
            {
                m_animator.SetInteger("M_attack", 3);
                is_can_input_combo = true;
            }
            else if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("M_attack2") && index_combo_attack == 2)
            {
                goto_groundState();
            }
            else if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("M_attack3") && index_combo_attack > 3)
            {
                m_animator.SetInteger("M_attack", 4);
                is_can_input_combo = true;
            }
            else if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("M_attack3") && index_combo_attack == 3)
            {
                goto_groundState();
            }
            else if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("M_attack4"))
            {
                goto_groundState();
            }
            else
            {
                goto_groundState();
            }
        }

        void goto_groundState()
        {
            m_animator.SetInteger("M_attack", -1);
            is_can_input_combo = true;
            index_combo_attack = 0;
        }

        //***********************************

        //start col visible
        //setting col dmg
        //setting col true
        void start_m_attack()
        {
            melee_col.enabled = true;
            melee_col.GetComponent<axe_col>().dmg = m_slotManager.m_toys[index_toy_using].damge;
        }

        //end colvisible
        //setting col dmg
        void end_m_attack()
        {
            melee_col.enabled = false;
        }

        #endregion

        #region faker target

        void dropFaker()
        {
            GameObject faker = Instantiate(m_slotManager.m_toys[index_toy_using].fake_target_pototype, bullet_spawn_point.position, bullet_spawn_point.rotation);

            if(m_slotManager.m_toys[index_toy_using].is_canMove)
            {
                //shoot faker and set cooldown
                faker.GetComponent<Rigidbody>().velocity = faker.transform.forward * 10f;
            }
        }

        #endregion

        #region healing

        //call my data function healing point 
        //spawn heal particle
        //play heal animation

        void healing()
        {
            m_animator.SetTrigger("Healing");
        }

        void spawn_healing_particle()
        {
            m_audio.PlayOneShot(m_sound.Heal);
            m_data.heal_hp(m_slotManager.m_toys[index_toy_using].heal_point);
            m_data.add_mana(m_slotManager.m_toys[index_toy_using].heal_point);
            GameObject heal_Fx = Instantiate(m_slotManager.m_toys[index_toy_using].Heal_Fx_pototype, transform.position, transform.rotation);
            Destroy(heal_Fx, 1f);
        }

        #endregion

        #region cute tree

        void start_cute_tree()
        {
            melee_col.GetComponent<axe_col>().dmg = 1;
            melee_col.enabled = true;
        }

        void end_cute_tree()
        {
            melee_col.enabled = false;
            m_hand.hide_toy("axe");
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, axe_range);
        }

        #endregion

        #region Rolling in the deppppppppp

        void rolling()
        {

        }

        #endregion

        #region shile

        void active_Shile()
        {
            shile.SetActive(true);
            isUseShile = true;
            this.gameObject.tag = "Untagged";
        }

        void de_shile()
        {
            shile.SetActive(false);
            isUseShile = false;
            this.gameObject.tag = "Untagged";
        }

        #endregion

        #region collision function

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "grass")
            {
                //player is in grass
                m_isGrass = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == "grass")
            {
                //player exit grass
                m_isGrass = false;
            }
        }

        #endregion

        #region jump boot

        void jump_boot()
        {
            set_show_Status(m_slotManager.m_toys[index_toy_using].status_icon);
            m_ThirPersonController.m_JumpPower = m_slotManager.m_toys[index_toy_using].var_boot_much_more;
            jump_duration_timer = m_slotManager.m_toys[index_toy_using].duration_boot;
        }

        void reset_boot()
        {
            hide_status();
            m_ThirPersonController.m_JumpPower = start_jump_foce;
        }

        #endregion

        #region status

        void set_show_Status(Sprite sp)
        {
            status_Image.sprite = sp;
            status_Image.gameObject.SetActive(true);
        }
        
        void hide_status()
        {
            status_Image.gameObject.SetActive(false);
        }

        #endregion
    }
}