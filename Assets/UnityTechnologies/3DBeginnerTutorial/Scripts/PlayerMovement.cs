using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f; // public member variables appear in the Inspector window and can therefore be tweaked


    Animator m_Animator;
    Rigidbody m_Rigidbody; //move the Rigidbody instead of using any other technique
    AudioSource m_AudioSource;

    Vector3 m_Movement; // non-public member variables start with the m_ prefix
    // they belong to a class rather than a specific method

    Quaternion m_Rotation = Quaternion.identity; // Create and Store a Rotation


    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();

    }




    // Update is called once per frame
    void FixedUpdate ()    
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize (); //ensure the movement vector always has the same magnitude
        
        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);

        if(isWalking)
        {
            if(!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        // instead of making the change per frame, you deal with a change per second
        m_Rotation = Quaternion.LookRotation (desiredForward);
    }

       void OnAnimatorMove ()
    {
        // apply root motion as you want, which means that movement and rotation can be applied separately.
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        // deltaPosition is the change in position due to root motion that would have been applied to this frame
        m_Rigidbody.MoveRotation (m_Rotation);

    }
}