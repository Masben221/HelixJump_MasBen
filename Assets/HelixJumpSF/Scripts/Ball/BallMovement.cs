using UnityEngine;


namespace HelixJump
{
    [RequireComponent(typeof(BallMovement))]
    public class BallMovement : MonoBehaviour
    {
        [Header("Fall")]
        [SerializeField] private float fallHeight;
        [SerializeField] private float fallSpeedDefault;
        [SerializeField] private float fallSpeedMax;
        [SerializeField] private float fallSpeedAxeleration;

        private Animator animator;

        private float fallSpeed;
        private float floorY;

        private void Start()
        {
            enabled = false;

            animator = GetComponent<Animator>();

            if (Player.Instance != null)
            {
                var player = Player.Instance;

                player.OnDie += Death;
                player.OnFinish += Stop;
                player.OnStart += Jump;
                player.OnDamage += Damage;                
            }
        }
        private void Update()
        {
            if (transform.position.y >= floorY)
            {
                transform.Translate(0, -fallSpeed * Time.deltaTime, 0);

                if (fallSpeed < fallSpeedMax)
                {
                    fallSpeed += fallSpeedAxeleration * Time.deltaTime;
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, floorY, transform.position.z);
                enabled = false;
            }
        }

        public void Jump()
        {
            animator.speed = 1;
            animator.SetBool("Fly", false);
            //animator.SetBool("Damage", false);
            animator.SetBool("Jump", true);            
            fallSpeed = fallSpeedDefault;            
        }

        public void Fall(float startFloorY)
        {
            enabled = true;
            animator.speed = 0;
            floorY = startFloorY - fallHeight;
        }
        public void Fly()
        {
            animator.speed = 1;
            animator.SetBool("Jump", false);
            //animator.SetBool("Damage", false);
            animator.SetBool("Fly", true);
            fallSpeed = fallSpeedDefault;
        }
        public void FlyStop()
        {
            animator.SetBool("Fly", false);
            fallSpeed = fallSpeedDefault;
        }
        public void JumpStop()
        {
            animator.SetBool("Jump", false);
            fallSpeed = fallSpeedDefault;
        }

        public void Death()
        {
            animator.speed = 1;
            animator.SetTrigger("Death");
            animator.SetBool("Jump", false);
            animator.SetBool("Fly", false);
            //animator.SetBool("Damage", false);
            Invoke(nameof(Stop), 1.5f);
        }
        public void Damage()
        {
            animator.speed = 1;
            animator.SetTrigger("Damage");
            fallSpeed = fallSpeedDefault;
        }

        public void Stop()
        {
            animator.speed = 0;
        }
    }

}

