using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl) && MegamanBasicShoot.canShoot)
        {
            if (MegamanBasicShoot.Count < 3)
            {
                SetBasicAttackTrue();
                BasicAttack();
            }
        }

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
        else if (player.canJump == false)
        {
            player.canJump = true;
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
            currentShoot.transform.localPosition += new Vector3(0.33f, 0.12f, 0);
            currentShoot.GetComponent<Rigidbody2D>().velocity += new Vector2(8, 0);

        }
        else
        {
            currentShoot.transform.localPosition += new Vector3(-0.33f, 0.12f, 0);
            currentShoot.GetComponent<Rigidbody2D>().velocity += new Vector2(-8, 0);
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
        if (lastAnimationId != animator.GetCurrentAnimatorStateInfo(0).fullPathHash)
        {
            lastAnimationId = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            //print("nova animacao");


            //guarda o tempo normalizado que a última animação estava quando saiu
            lastAnimationNormalizedTimeAtExit = lastAnimationNormalizedTime;
            //print(lastAnimationNormalizedTimeAtExit);


            //condition of animation change star
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("JumpingShooting")) //normalizedTimeTransfer
            {
                animator.Play(0, 0, lastAnimationNormalizedTimeAtExit * 1.1f); // animation transition at time defined
                //BasicAttack();
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("FallingShooting"))
            {
                //BasicAttack();
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
