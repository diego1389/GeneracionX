using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//using UnityEngine.ParticleSystem;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public int EnergyValue = 10;
    public AudioClip DeathClip;
    //public Slider EnergySlider;
   


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem[] _myParticleSystemArray;
    ParticleSystem _hitParticles;
    ParticleSystem _fireParticles;
    ParticleSystem _smokeParticles;
    ParticleSystem _energyParticles;

    static bool doorIsOpen = false;
    private Transform _playersTransform;
    CapsuleCollider capsuleCollider;
    GameObject _door;
    bool isDead;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        _door = GameObject.FindGameObjectWithTag("Door");
        _playersTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAudio = GetComponent <AudioSource> ();
        _myParticleSystemArray = GetComponentsInChildren <ParticleSystem> ();
        fillEnemyParticles(_myParticleSystemArray);
        capsuleCollider = GetComponent <CapsuleCollider> ();
        currentHealth = startingHealth;
        //EnergySlider.maxValue = EnergyToOpenDoor;
    }


    void Update ()
    {
        //if (isSinking)
        //{
        //    transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        //}
    }


    void fillEnemyParticles(ParticleSystem[] particles)
    {
        if(particles.Length > 0)
        {
            _hitParticles = particles[0];
            _fireParticles = particles[1];
            _smokeParticles = particles[2];
            _energyParticles = particles[3];
        }
    }

    private void LateUpdate()
    {
        if (_energyParticles.isPlaying && _playersTransform != null)
        {
            // Extract copy
            //private ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[10];

            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[_energyParticles.main.maxParticles];
            int numParticlesAlive = _energyParticles.GetParticles(particles);
            // Do changes
            for (int i = 0; i < numParticlesAlive; i++)
            {
                particles[i].position = Vector3.MoveTowards(particles[i].position, _playersTransform.position, Time.deltaTime * 10);
            }
            // Reassign back to emitter
            _energyParticles.SetParticles(particles, numParticlesAlive);

            //particleEmitter.particles = particles;
            //int length = _energyParticles.GetParticles(_particles);
            //Vector3 attractorPosition = _playersTransform.position;
            //print("Enemy health: x: " + attractorPosition.x + " y: " + attractorPosition.y + " z: " + attractorPosition.z);

            //for (int i = 0; i < length; i++)
            //{
            //    _particles[i].position = _particles[i].position + (attractorPosition - _particles[i].position) / (_particles[i].remainingLifetime) * Time.deltaTime;
            //}
            //_energyParticles.SetParticles(_particles, length);
        }

    }

    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;

        _hitParticles.transform.position = hitPoint;
        _hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;
        displayDeath();
    }

    private void displayDeath()
    {
        /*Display death effects*/
        displayDeathEffects();
        /*Stop moving*/
        NavMeshAgent navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.isStopped = true;
        //navMeshAgent.enabled = false;
        Rigidbody enemyRigidBody = GetComponent<Rigidbody>();
        enemyRigidBody.isKinematic = true;
        /*Score points*/
        ScoreManager.energy += EnergyValue;

        checkDoor(ScoreManager.energy);
        /*Fill player energy*/
        fillPlayerEnemy();
        /*Destroy enemy*/
        Destroy(gameObject, 10f);
    }

    private void checkDoor(int score)
    {
        if(score >= ScoreManager.EnergyToOpenDoor && !doorIsOpen)
        {
            Animator doorAnimator;
            BoxCollider doorCollider;
            AudioSource doorSound;

            if (_door != null)
            {
                doorAnimator = _door.GetComponentInChildren<Animator>();
                doorCollider = _door.GetComponentInChildren<BoxCollider>();
                doorSound = _door.GetComponentInChildren<AudioSource>();
                if (doorAnimator != null && doorCollider != null && doorSound != null)
                {
                    doorAnimator.SetTrigger("OpenDoor");
                    doorCollider.isTrigger = true;
                    doorSound.Play();
                    doorIsOpen = true;
                }
                    
            }
        }

    }

    void fillPlayerEnemy()
    {
        //displayMoveEnergyToPlayer();
    }



    private void displayMoveEnergyToPlayer()
    {
        throw new NotImplementedException();
    }

    void displayDeathEffects()
    {
        /*Show death animation*/
        anim.SetTrigger("Dead");

        /*Play death sound*/
        enemyAudio.clip = DeathClip;
        enemyAudio.Play();

        /*Show fire and smoke*/
        if(_fireParticles!=null)
            _fireParticles.Play();
        if (_smokeParticles != null)
            _smokeParticles.Play();
        if (_energyParticles != null)
            _energyParticles.Play();
    }
 
}
