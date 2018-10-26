using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight = 4f;
    public float minJumpHeight = 1f;
    public float timeToJumpApex = .4f;
    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    private float moveSpeed = 6f;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public bool canDoubleJump;
    private bool isDoubleJumping = false;

    public float wallSlideSpeedMax = 3f;
    public float wallStickTime = .25f;
    private float timeToWallUnstick;

    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;

    private Controller2D controller;

    private Vector2 directionalInput;
    private bool wallSliding;
    private int wallDirX;

	//added
	private Animator animator;
	private bool canJump = true;
	//private GameObject body;

    private void Start()
    {
        controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

		//added
		animator = GetComponentInChildren<Animator>();
		//body = GameObject.Find ("body");
    }

    private void Update()
    {
        CalculateVelocity();
        HandleWallSliding();

        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0f;
        }

		if (Input.GetAxisRaw ("Fire1") == 1) {
			OnBasicAttack ();
		}
		animationCode ();
    }

	private void OnBasicAttack () {
		animator.SetBool ("BasicAttack", true);
	}

	/*public void OnBasicAttackAnimationEnds ()
	{
		animator.SetBool ("BasicAttack", false);
	}*/

	private void animationCode () {
		//animation jumping code

		//print(velocity.y);
		if (controller.collisions.below == true && animator.GetBool("Grounded")==false) {
			animator.SetBool ("Grounded", true);
		}
		if (controller.collisions.below == false && animator.GetBool("Grounded")==true) {
			animator.SetBool ("Grounded", false);
		}
		if (velocity.y>0 && animator.GetBool("VelocityYPositive")==false) {
			animator.SetBool ("VelocityYPositive", true);
            animator.SetBool("BasicAttack", false);
            print("up");
        } else if (velocity.y<0) { // && animator.GetBool("VelocityYPositive")==true) {
			animator.SetBool ("VelocityYPositive", false);
            animator.SetBool("BasicAttack", false);
            print("down");
            //print ("negativoStart()");
        }
		//animation GroundCollision
		// #info controla a fisica fazendo com que o personagem do jogador tenha metade da velocidade quando aterrisar de um pulo, e so pode pular novamente quando terminar a animacao de aterrisagem;
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("GroundCollision")) {
			canJump = false;
			velocity.x /= 2;
		} else if (canJump == false) {
			canJump = true;
		}
		//animation shooting
		//normalizedTime equivale a 1 segundo para completar a animacao independete da velocidade;
		if (animator.GetBool("BasicAttack") && (animator.GetCurrentAnimatorStateInfo (0).IsName ("Shooting") || animator.GetCurrentAnimatorStateInfo(0).IsName("RunningShooting")) && animator.GetCurrentAnimatorStateInfo (0).normalizedTime>1 && Input.GetAxisRaw("Fire1") != 1) {
			animator.SetBool("BasicAttack", false);
		}

        if (velocity.x > -0.8f && velocity.x < 0.8f && animator.GetCurrentAnimatorStateInfo(0).IsName("RunningShooting"))
            animator.SetBool("BasicAttack", false);
	}

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
		if (canJump) {
			if (wallSliding) {
				if (wallDirX == directionalInput.x) {
					velocity.x = -wallDirX * wallJumpClimb.x;
					velocity.y = wallJumpClimb.y;
				} else if (directionalInput.x == 0) {
					velocity.x = -wallDirX * wallJumpOff.x;
					velocity.y = wallJumpOff.y;
				} else {
					velocity.x = -wallDirX * wallLeap.x;
					velocity.y = wallLeap.y;
				}
				isDoubleJumping = false;
			}
			if (controller.collisions.below) {
				velocity.y = maxJumpVelocity;
				isDoubleJumping = false;
			}
			if (canDoubleJump && !controller.collisions.below && !isDoubleJumping && !wallSliding) {
				velocity.y = maxJumpVelocity;
				isDoubleJumping = true;
			}
		}
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    private void HandleWallSliding()
    {
        wallDirX = (controller.collisions.left) ? -1 : 1;
		if (wallSliding == true) {
			wallSliding = false;
			//added
			animator.SetBool ("Sliding", false);
		}
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;
			//added
			animator.SetBool("Sliding",true);

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0f)
            {
                velocityXSmoothing = 0f;
                velocity.x = 0f;
                if (directionalInput.x != wallDirX && directionalInput.x != 0f)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }
    }

    private void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne));
        velocity.y += gravity * Time.deltaTime;

		//animation running/moving code
		if (velocity.x >= 1 && animator.GetBool ("Moving") == false) {
			//print ("right " + velocity.x);
			animator.SetBool ("Moving", true);
		} else if (velocity.x <= -1 && animator.GetBool ("Moving") == false) {
			//print ("left " + velocity.x);
			animator.SetBool ("Moving", true);
		} else if (animator.GetBool ("Moving") == true && velocity.x<1 && velocity.x>-1) animator.SetBool ("Moving", false);
	}
}
