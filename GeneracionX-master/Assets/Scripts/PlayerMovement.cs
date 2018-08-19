using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed =10f;
    public bool Aiming = false;
    public AudioClip LoadingEnergy;
    //private bool _running = false;
    Vector3 _movement;
    Animator _animator;
    Rigidbody _playerRigidBody;
    int _floorMask; /*for the raycast to hit only the floor you need to use a layer mask.*/
    float _camRayLength = 100f;
    ParticleSystem[] _myParticleSystemArray;
    ParticleSystem _energyParticles;
    AudioSource _playerAudio;
    //Controls
    void Update()
    {
        manageKeyboardControls();
    }

    private void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
        _animator = GetComponent<Animator>();
        _playerRigidBody = GetComponent<Rigidbody>();
        _myParticleSystemArray = GetComponentsInChildren<ParticleSystem>();
        _energyParticles = _myParticleSystemArray[1];
        _playerAudio = GetComponent<AudioSource>();
    }

    //for physics update
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Walking(h, v);
        Move(h, v);
        Turning();
        print("Player movement: x: " + transform.position.x + " y: " + transform.position.y + " z: " + transform.position.z);
    }

    private void manageKeyboardControls()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Aiming = (Aiming) ? false : true;
            Aming(Aiming);
        }
        //if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        //{
        //    _running = true;
        //    Running(_running);
        //}
        //if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        //{
        //    _running = false;
        //    Running(_running);
        //}
      
    }

    private void Aming(bool aim)
    {
        if (Aiming)
        {
            if (_energyParticles != null)
                _energyParticles.Play();

            _playerAudio.clip = LoadingEnergy;
            _playerAudio.Play();
            _animator.SetBool("IsWalking", false);
            _animator.SetBool("IsAiming", true);
        }
        else
        {
            if (_energyParticles != null)
                _energyParticles.Stop();
            _playerAudio.Stop();
            _animator.SetBool("IsAiming", false);
        }

    }



    public void Move(float h, float v) 
    {
        if (!Aiming)
        {
            _movement.Set(h, 0, v);
            _movement = _movement.normalized * Speed * Time.deltaTime;
            _playerRigidBody.MovePosition(transform.position + _movement);
        }
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if(Physics.Raycast(camRay, out floorHit, _camRayLength, _floorMask))
        {
            //_animator.SetBool("IsAiming", true);
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            _playerRigidBody.MoveRotation(newRotation);
        }
    }

    void Walking(float h, float v)
    {
        if (!Aiming)
        {
            bool walking = h != 0 || v != 0;
            _animator.SetBool("IsWalking", walking);
        }
    }

    //private void Running(bool run)
    //{
    //    if (!Aiming)
    //    {
    //        if (run)
    //        {
    //            _animator.SetBool("IsRunning", run);
    //            Speed *= 1.3f;
    //            print(Speed);
    //        }
    //        else
    //        {
    //            _animator.SetBool("IsRunning", run);
    //            Speed /= 1.3f;
    //            print(Speed);
    //        }
    //    }
    //}


}
