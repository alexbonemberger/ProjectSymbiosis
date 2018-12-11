using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//controla as animações
public class Body : MonoBehaviour {

    [SerializeField]
    private Controller2D controller;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Timer timer;
    private Animator animator;
    [SerializeField]
    private GameObject basicShoot;
    private GameObject currentShoot;

    private float lastAnimationNormalizedTimeAtExit;
    private float lastAnimationNormalizedTime;
    private int lastAnimationId;

    //shoot mode with aim
    //mouse position
    [SerializeField]
    private GameObject MySprite_ShootModeUpBody;
    private Vector3 MySprite_ShootModeUpBodyLocalPositionStart;
    private Vector3 mousePositionStart;
    private float degrees;
    private float shootModeDirection; // 1f = right, -1f = left
    [SerializeField]
    Text textAimDegrees;
    private float sensibilityMultiplier;



    private void Awake()
    {
        animator = GetComponent<Animator>();
        MySprite_ShootModeUpBody.SetActive(false);
        sensibilityMultiplier = 3f;
        MySprite_ShootModeUpBodyLocalPositionStart = MySprite_ShootModeUpBody.transform.localPosition;
        shootModeDirection = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Mouse0) && MegamanBasicShoot.canShoot)
        {
            if (MegamanBasicShoot.Count < 3)
            {
                SetBasicAttackTrue();
                BasicAttack();
            }
        }

        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (animator.GetBool("ShootModeActive"))
            {
                animator.SetBool("ShootModeActive", false);
                MySprite_ShootModeUpBody.SetActive(false);
                player.canMove = true;
                //reset movement
                if (controller.directionLookRight)
                    player.velocity = Vector2.zero; //direita
                else
                    player.velocity = new Vector2(-0.01f,0); //esquerda
            }
            else if(animator.GetBool("Grounded"))
            {
                animator.SetBool("ShootModeActive", true);
                MySprite_ShootModeUpBody.SetActive(true);
                mousePositionStart = Input.mousePosition;
                MySprite_ShootModeUpBody.transform.localPosition = MySprite_ShootModeUpBodyLocalPositionStart;
                player.canMove = false;
            }
        }

        ShootMode();
        AnimationCode();
    }

    private void AnimationCode()
    {
        OnAnimationTransition();
        //bool changing

        if (controller.collisions.below == true && animator.GetBool("Grounded") == false)
        {
            animator.SetBool("Grounded", true);
        }
        if (controller.collisions.below == false && animator.GetBool("Grounded") == true)
        {
            animator.SetBool("Grounded", false);
        }
        if (player.velocity.y > 0 && animator.GetBool("VelocityYPositive") == false)
        {
            animator.SetBool("VelocityYPositive", true);
            animator.SetBool("BasicAttack", false);
            //print("up");
        }
        else if (player.velocity.y <= 0 && animator.GetBool("VelocityYPositive") == true)
        {
            animator.SetBool("VelocityYPositive", false);
            animator.SetBool("BasicAttack", false);
            //print("down");
            //print ("negativoStart()");
        }

        if (player.velocity.x > -0.8f && player.velocity.x < 0.8f && animator.GetCurrentAnimatorStateInfo(0).IsName("RunningShooting"))
            animator.SetBool("BasicAttack", false);



        //animation GroundCollision
        // #info controla a fisica fazendo com que o personagem do jogador tenha metade da velocidade quando aterrisar de um pulo, e so pode pular novamente quando terminar a animacao de aterrisagem;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("GroundCollision"))
        {
            player.canJump = false;
            player.velocity.x /= 2;
            animator.SetBool("BasicAttack", false);
        }
    }

    private void SetBasicAttackTrue()
    {
        animator.SetBool("BasicAttack", true);
    }

    private void BasicAttack()
    {
        currentShoot = Instantiate(basicShoot, transform.position, Quaternion.identity);
        if (controller.directionLookRight)
        {
            if (animator.GetBool("ShootModeActive"))
            {
                currentShoot.transform.localPosition += new Vector3((90f - Mathf.Abs(degrees)) * 0.33f / 90 - Mathf.Abs(degrees) * 0.06f / 90 + ((45 - Mathf.Abs(degrees -45)) * 0.033f / 45), degrees * 0.33f / 90 + 0.14f + ((45 - Mathf.Abs(degrees - 45)) * 0.033f / 45), 0);
                currentShoot.GetComponent<Rigidbody2D>().velocity += new Vector2(8 * (90 - Mathf.Abs(degrees)) / 90, 8 * degrees / 90);
            }
            else
            {
                currentShoot.transform.localPosition += new Vector3(0.33f, 0.12f, 0);
                currentShoot.GetComponent<Rigidbody2D>().velocity += new Vector2(8, 0);
            }

        }
        else
        {
            if (animator.GetBool("ShootModeActive"))
            {
                currentShoot.transform.localPosition += new Vector3(-(90f - Mathf.Abs(degrees)) * 0.33f / 90 - Mathf.Abs(degrees) * 0.06f / 90 + ((45 - Mathf.Abs(degrees - 45)) * 0.033f / 45), degrees * 0.33f / 90 + 0.14f + ((45 - Mathf.Abs(degrees - 45)) * 0.033f / 45), 0);
                currentShoot.GetComponent<Rigidbody2D>().velocity += new Vector2(-8 * (90 - Mathf.Abs(degrees)) / 90, 8 * degrees / 90);
            }
            else
            {
                currentShoot.transform.localPosition += new Vector3(-0.33f, 0.12f, 0);
                currentShoot.GetComponent<Rigidbody2D>().velocity -= new Vector2(8, 0);
            }
        }
    }

    private void ShootMode()
    {
        //Aim
        if (animator.GetBool("ShootModeActive"))
        {
            degrees = Input.mousePosition.y - mousePositionStart.y;
            //limits
            if (degrees > 90 * sensibilityMultiplier)
            {
                degrees = 90 * sensibilityMultiplier;
                mousePositionStart.y = Input.mousePosition.y - 90 * sensibilityMultiplier; // reseta o novo inicio para nao precisar ficar puxando o mouse
            }
            if (degrees < -45 * sensibilityMultiplier)
            {
                degrees = -45 * sensibilityMultiplier;
                mousePositionStart.y = Input.mousePosition.y + 45 * sensibilityMultiplier; // reseta o novo inicio para nao precisar ficar puxando o mouse
            }
            degrees /= sensibilityMultiplier; // sensibility
            textAimDegrees.text = degrees.ToString("n2");

            //process result
            if (controller.directionLookRight) // flip the animation of upper body part
            {
                shootModeDirection = 1f;
            }
            else
            {
                shootModeDirection = -1f;
            }
            MySprite_ShootModeUpBody.transform.eulerAngles = new Vector3(0, 0, shootModeDirection * degrees);
            MySprite_ShootModeUpBody.transform.localPosition = new Vector3(MySprite_ShootModeUpBodyLocalPositionStart.x + Mathf.Abs(degrees) * -0.3f / 90, MySprite_ShootModeUpBodyLocalPositionStart.y + Mathf.Abs(degrees) * 0.06f / 90, 0); // smoth inclination

            //flip x when in shootmode
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                controller.ChangeFaceDir(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) //com else testa apenas um if, pois essa funcao eh chamada no update
            {
                controller.ChangeFaceDir(1);
            }
        }
    }

    public void AnimationShootingEnds()
    {
        //print("End");
        animator.SetBool("BasicAttack", false);
    }

    public void AnimationRunningShootingEnds()
    {
        //print("REnd");
    }

    private void OnAnimationTransition()
    {
        //animation change code starts
        if (lastAnimationId != animator.GetCurrentAnimatorStateInfo(0).shortNameHash)
        {
            lastAnimationId = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            //print("nova animacao");
            //guarda o tempo normalizado que a última animação estava quando saiu
            lastAnimationNormalizedTimeAtExit = lastAnimationNormalizedTime;
            //print(lastAnimationNormalizedTimeAtExit);

            //condition to all any animation change
            if (animator.GetBool("Grounded") && player.canJump == false)
            {
                player.canJump = true;
            }

            //condition of an animation change happend at this frame
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("JumpingShooting")) //normalizedTimeTransfer
            {
                animator.Play(0, 0, lastAnimationNormalizedTimeAtExit * 1.1f); // animation transition at time defined
                //BasicAttack();
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("FallingShooting"))
            {

            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunningShooting"))
            {
                //BasicAttack();
            }
            //conditions of animation change end
        }
        lastAnimationNormalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //animation change code ends
    }
}
