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
        private bool isCanmove = true;

        //toy use
        private string toy_use = "";
        private bool isShooingState = false;

        [Header("Slot")]
        private int index_slotSelect = 0;
        public newItemSlotmanager m_slotManager;

        [Header("Camera Rotate")]
        public float cam_rotation_speed;

        [Header("attack and cute treer")]
        public Collider melee_col;
        public Collider M_skill_col;
        public Transform m_skill_spawn_point;
        private int index_toy_using;
        //combo system
        private bool is_can_input_combo = true;
        private int index_combo_attack;

        //use for range attack
        [Header("R Attack")]
        public Transform bullet_spawn_point;
        public GameObject ChartPototype;
        public GameObject exposion_pototype;
        public GameObject bullet_pototype;
        public GameObject shooting_view;
        private bool is_can_input_R_Combo = true;
        private int index_combo_r_attack;

        [Header("speed")]
        public float start_speed_Limit;
        private float speed_duration_timer;
        public GameObject speetril;
        private GameObject speedToy;

        [Header("jump")]
        public float start_jump_foce;
        private float jumpfoce;
        private float jump_duration_timer;

        [Header("Shilde")]
        public GameObject shileFx;
        private float shile_duration_timer;

        [Header("Status")]
        public GameObject attack_powerUppartic;
        public float playerInRange;
        public float powerPush;
        public Image status_Image;
        public GameObject power_icon;
        private float powerUpTimer;
        public Transform my_friend;
        public GameObject healing_Fx_pototype;
        public GameObject jump_boot_icon_powerUp;

        [Header("toy by time")]
        public Toy[] toyRandom_byTime;
        private float random_Toy_timer;

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
            start_speed_Limit = m_ThirPersonController.m_speedLimit;
            start_jump_foce = m_ThirPersonController.m_JumpPower;
        }

        private void Update()
        {
            if (!m_jump)
            {
                m_jump = Input.GetButtonDown(m_controller.button3_input);
            }
        }

        private void LateUpdate()
        {
            if(!isCanmove)
            {
                return;
            }

            if (m_jump && m_ThirPersonController.m_JumpPower != start_jump_foce)
            {
                m_audio.PlayOneShot(m_sound.jump_boot);
            }

            //player input movement
            float horizontal = Input.GetAxis(m_controller.leftStick_Horizontal);
            float vertical = Input.GetAxis(m_controller.leftStick_Vertical);
            //m_crouch = Input.GetButton(m_controller.triger_R2_L_button);

            //player calculate movement
            m_cameraForward = Vector3.Scale(m_camera.forward, new Vector3(1, 0, 1)).normalized;
            m_move = vertical * m_cameraForward + horizontal * m_camera.right;

            //shooting state
            if(isShooingState)
            {
                m_move = Vector3.zero;
                float turn = Mathf.Atan2(Input.GetAxis(m_controller.leftStick_Horizontal), Input.GetAxis(m_controller.leftStick_Vertical));
                transform.Rotate(0, turn  * 50f * Time.deltaTime, 0);

                if(Input.GetButtonDown(m_controller.triger_L2_L_button))
                {
                    //shooting 
                    ChartPototype.SetActive(false);
                    m_animator.SetTrigger("Shoot");
                    shooting_view.SetActive(false);
                    return;
                }
            }

            //display player movement
            m_ThirPersonController.Move(m_move, m_crouch, m_jump);
            m_jump = false;

            //**************random toy by timer************ */

            if(random_Toy_timer > 0)
            {
                random_Toy_timer -= Time.deltaTime;
            }
            else if(random_Toy_timer <= 0)
            {
                if(m_slotManager.m_toys.Count < 5)
                {
                        m_audio.PlayOneShot(m_sound.getToy);
                }
                m_slotManager.add_PlayerToy(toyRandom_byTime[Random.Range(0,  toyRandom_byTime.Length - 1)]);
                random_Toy_timer = 10f;
            }

            //*/************************ */

            //************time checker******************
            //attack power up
            if(powerUpTimer > 0)
            {
                Debug.Log("qq");
                power_icon.SetActive(true);
                attack_powerUppartic.SetActive(true);
                powerUpTimer -= Time.deltaTime;
            }
            else if(powerUpTimer <= 0)
            {
                //end
                powerPush = 0;
                attack_powerUppartic.SetActive(false);
                power_icon.SetActive(false);
            }

            //jump boot power up
            if(jump_duration_timer > 0)
            {
                jump_duration_timer -= Time.deltaTime;
            }else if(jump_duration_timer <= 0)
            {
                reset_boot();
            }

            if (jump_duration_timer > 0)
            {
                jump_duration_timer -= Time.deltaTime;
            }
            else if (jump_duration_timer <= 0)
            {
                reset_boot();
            }

            if(speed_duration_timer > 0)
            {
                speed_duration_timer -= Time.deltaTime;
            }
            else if(speed_duration_timer <= 0)
            {
                //reset 
                if(speedToy != null)
                {
                    speedToy.SetActive(false);
                }
                resetSpeed();
            }

            if(shile_duration_timer > 0)
            {
                shileFx.SetActive(true);
                shile_duration_timer -= Time.deltaTime;
            }
            else if(shile_duration_timer <= 0)
            {
                shileFx.SetActive(false);
            }

            //************************************************

            //check visible enemy
            if (m_isGrass && m_crouch)
            {
                //visible enemy false
                //hide from enemy
                //rend .a ลดลง
                this.gameObject.tag = "Untagged";
            }
            else if (!m_crouch || !m_isGrass)
            {
                //visile enemy true
                //show to
                //rend .a ปรกติ
                this.gameObject.tag = "Player";
            }

            if(Input.GetButtonDown(m_controller.triger_L2_L_button))
            {
                if(m_slotManager.m_toys.Count == 0)
                {
                    return;
                }
                if(m_slotManager.m_toys[index_slotSelect] != null)
                {
                    use_toy_number(index_slotSelect);
                    m_slotManager.remove_PlayerToy(index_slotSelect);
                    if(index_slotSelect != 0)
                    {
                        index_slotSelect -= 1;
                    }
                    else
                    {
                        index_slotSelect = 0;
                    }
                }
            }
            else if(Input.GetButtonDown(m_controller.triger_R2_L_button))
            {
                if(m_slotManager.m_toys.Count == 0)
                {
                    return;
                }
                if (m_slotManager.m_toys[index_slotSelect] != null)
                {
                    //use toy for friend
                    useToyForFriend(index_slotSelect);
                    m_slotManager.remove_PlayerToy(index_slotSelect);
                    if (index_slotSelect != 0)
                    {
                        index_slotSelect -= 1;
                    }
                    else
                    {
                        index_slotSelect = 0;
                    }
                }  
            }
            else if (Input.GetButtonDown(m_controller.triger_L1_L_button) && index_slotSelect < (m_slotManager.m_toys.Count - 1))
            {
                if (m_slotManager.m_slot[index_slotSelect] != null)
                {
                    m_slotManager.m_slot[index_slotSelect].unselect();
                }
                index_slotSelect++;
                m_slotManager.m_slot[index_slotSelect].select();
            }
            else if (Input.GetButtonDown(m_controller.triger_R1_L_button) && index_slotSelect > 0)
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
        }

        //use toy
        void use_toy_number(int index)
        {
            //debug error
            if (m_slotManager.m_toys[index] == null)
            {
                return;
            }

            //check frist time use toy
            if (toy_use != "")
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
                    //powerUpNear_operation();
                    break;
                case Toy.toy_type.r_attack:
                    m_audio.PlayOneShot(m_sound.chartShoot);
                    m_animator.SetTrigger("R_attack");
                    ChartPototype.SetActive(true);
                    isShooingState = true;
                    shooting_view.SetActive(true);
                    //r_attack
                    break;
                case Toy.toy_type.speed:
                    speed_duration_timer = 5f;
                    for (int i = 0; i < m_hand.toys_po.Length; i++)
                    {
                        if(m_hand.toys_po[i].name == toy_use)
                        {
                            speedToy = m_hand.toys_po[i];
                        }
                    }
                    speedUp();
                    break;
                case Toy.toy_type.fake_target:
                    //faker item 
                    dropFaker();
                    break;
                case Toy.toy_type.heal:
                    //healPowerUp();
                    healing();
                    break;
                case Toy.toy_type.default_type:
                    break;
                case Toy.toy_type.jump_boot:
                    //incraea jump fore
                    //in duration
                    jump_boot();
                    break;
                case Toy.toy_type.shile:
                    active_shile();
                    m_audio.PlayOneShot(m_sound.ShieldBuft);
                    break;
                default:
                    break;
            }
        }

        void useToyForFriend(int index)
        {
            //debug error
            if (m_slotManager.m_toys[index] == null)
            {
                return;
            }

            //check frist time use toy
            if (toy_use != "")
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
                    my_friend.GetComponent<player_NewController>().PowerUp();
                    my_friend.GetComponent<AudioSource>().PlayOneShot(m_sound.attackBuft);
                    break;
                case Toy.toy_type.r_attack:
                    break;
                case Toy.toy_type.speed:
                    break;
                case Toy.toy_type.fake_target:
                    break;
                case Toy.toy_type.heal:
                    my_friend.GetComponent<player_NewController>().healing_fromMyfriend();
                    my_friend.GetComponent<AudioSource>().PlayOneShot(m_sound.Heal);
                    break;
                case Toy.toy_type.default_type:
                    break;
                case Toy.toy_type.jump_boot:
                    my_friend.GetComponent<player_NewController>().jumpPowerUpFromFriend();
                    break;
                case Toy.toy_type.shile:
                    my_friend.GetComponent<player_NewController>().active_shile();
                    my_friend.GetComponent<AudioSource>().PlayOneShot(m_sound.ShieldBuft);
                    break;
                default:
                    break;
            } 
        }

        #region power up

        void powerUpNear_operation()
        {
            if(Vector3.Distance(my_friend.position, transform.position) <= playerInRange)
            {
                //power up
                my_friend.GetComponent<player_NewController>().PowerUp();
            }
        }

        public void PowerUp()
        {
            power_icon.SetActive(true);
            powerPush = 5;
            powerUpTimer = 7f;
        }

        void healPowerUp()
        {
            if(Vector3.Distance(transform.position,my_friend.position) <= playerInRange)
            {
                my_friend.GetComponent<player_NewController>().healing_fromMyfriend();
            }
        }

        void healing_fromMyfriend()
        {
            GameObject heal_Fx = Instantiate(healing_Fx_pototype, transform.position, transform.rotation);
            m_data.heal_hp(5);
            Destroy(heal_Fx, 1f);
        }

        public void jumpPowerUpFromFriend()
        {
            jump_boot_icon_powerUp.SetActive(true);
            m_ThirPersonController.m_JumpPower += 5;
            jump_duration_timer = 5f;
        }

        #endregion

        #region speed

        void speedUp()
        {
            m_ThirPersonController.m_speedLimit -= 2f;
            speetril.SetActive(true);
        }

        void resetSpeed()
        {
            m_ThirPersonController.m_speedLimit = start_speed_Limit;
            speetril.SetActive(false);
        }

        #endregion

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
            if (is_can_input_R_Combo)
            {
                index_combo_r_attack++;
            }

            if (index_combo_r_attack == 1)
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
            GameObject exposion = Instantiate(exposion_pototype, bullet_spawn_point.position, bullet_spawn_point.rotation);
            GameObject bullet = Instantiate(bullet_pototype, bullet_spawn_point.position, bullet_spawn_point.rotation);
            bullet.GetComponent<bullet_Col>().dmg = 4;
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6f;
            Destroy(exposion, 2);

            //play sound
            m_audio.PlayOneShot(m_sound.shoot);
        }

        void endShoot()
        {
            isShooingState = false;
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

            if (index_combo_attack == 1)
            {
                m_animator.SetInteger("M_attack", 1);
            }
        }

        void play_sword_sound()
        {
            //m_audio.PlayOneShot(m_sound.Sword);
        }

        //ckecker what should player do next animation
        void combo_checker()
        {
            is_can_input_combo = false;

            if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("M_attack1") && index_combo_attack == 1)
            {
                goto_groundState();
            }
            else if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("M_attack1") && index_combo_attack >= 2)
            {
                m_animator.SetInteger("M_attack", 2);
                is_can_input_combo = true;
            }
            else if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("M_attack2") && index_combo_attack > 2)
            {
                m_animator.SetInteger("M_attack", 3);
                is_can_input_combo = true;
            }
            else if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("M_attack2") && index_combo_attack == 2)
            {
                goto_groundState();
            }
            else if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("M_attack3") && index_combo_attack > 3)
            {
                m_animator.SetInteger("M_attack", 4);
                is_can_input_combo = true;
            }
            else if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("M_attack3") && index_combo_attack == 3)
            {
                goto_groundState();
            }
            else if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("M_attack4"))
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
            m_hand.hide_toy(toy_use);
        }

        //***********************************

        //start col visible
        //setting col dmg
        //setting col true
        void start_m_attack()
        {
            m_audio.PlayOneShot(m_sound.Sword[Random.Range(0, (m_sound.Sword.Length - 1))]);
            melee_col.enabled = true;
            melee_col.GetComponent<axe_col>().dmg = 5 + powerPush;
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

            if (m_slotManager.m_toys[index_toy_using].is_canMove)
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
            m_data.heal_hp(5);
            m_data.add_mana(5);
            GameObject heal_Fx = Instantiate(healing_Fx_pototype, transform.position, transform.rotation);
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

        #endregion

        #region Rolling in the deppppppppp

        void rolling()
        {

        }

        #endregion

        #region collision function

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "grass")
            {
                //player is in grass
                m_isGrass = true;
            }
            if(other.gameObject.tag == "ChangeCamera")
            {
                m_camera.gameObject.GetComponent<Animator>().SetTrigger("Next");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "grass")
            {
                //player exit grass
                m_isGrass = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "dropingToy")
            {
                m_slotManager.add_PlayerToy(collision.gameObject.GetComponent<dropItem>().giveToy);
                m_audio.PlayOneShot(m_sound.getToy);
                if(m_slotManager.m_toys.Count == 0)
                {
                    Debug.Log("In");
                    m_slotManager.m_slot[0].select();
                }
                Destroy(collision.gameObject);
            }
        }

        #endregion

        #region jump boot

        void jump_boot()
        {
            //set_show_Status(m_slotManager.m_toys[index_toy_using].status_icon);
            jump_boot_icon_powerUp.SetActive(true);
            m_ThirPersonController.m_JumpPower = m_slotManager.m_toys[index_toy_using].var_boot_much_more;
            jump_duration_timer = m_slotManager.m_toys[index_toy_using].duration_boot;
        }

        void reset_boot()
        {
            //hide_status();
            jump_boot_icon_powerUp.SetActive(false);
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

        #region

        public void active_shile()
        {
            shile_duration_timer = 7f;
        }

        #endregion

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, playerInRange);
        }
        
        void frezzeMove()
        {
            isCanmove = false;
        }

        void unfrezzeMove()
        {
            isCanmove = true;
        }
    }
}