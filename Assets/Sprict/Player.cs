using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator anim = null;

    private Rigidbody rb = null;
    private PlayerInput p_Input = null;

    static readonly int walkId = Animator.StringToHash("Walk");
    static readonly int attackId = Animator.StringToHash("Attack");

    private Vector3 moveVec = Vector3.zero;

    private bool dontMove = false;

    [SerializeField]
    private float moveSpeed = 3;

    [SerializeField]
    private Transform camPos = null;
    
    void Start()
    {
        TryGetComponent(out rb);
        TryGetComponent(out p_Input);

        var _player = p_Input.actions.FindActionMap("Player");

        _player["Move"].performed += Move;
        _player["Fire"].performed += Attack;
    }

    // Update is called once per frame

    float timeScale = 0;

    void Update()
    {
        var moveVelocityValue = camPos.right * moveVec.x * moveSpeed;
        moveVelocityValue += camPos.forward * moveVec.z * moveSpeed;
        moveVelocityValue.y = rb.velocity.y;

        if (moveVec.magnitude > 0.1f)
        {
            if (timeScale < 1)
                timeScale += Time.deltaTime * 4;

            transform.rotation = Quaternion.LookRotation(moveVelocityValue);
        }
        else
            timeScale = 0;

        if (!dontMove)
        {
            var SmoothMag = moveVec.magnitude * timeScale;

            anim.SetFloat(walkId, SmoothMag);

            rb.velocity = moveVelocityValue;
        }
    }

    private void Move(InputAction.CallbackContext info)
    {
        moveVec = new Vector3(info.ReadValue<Vector2>().x, 0, info.ReadValue<Vector2>().y);
    }

    private void Attack(InputAction.CallbackContext info)
    {
        if (!dontMove)
            anim.SetTrigger(attackId);

        StartCoroutine(DontMove());

        dontMove = true;
    }

    IEnumerator DontMove()
    {
        yield return new WaitForSeconds(1.5f);

        dontMove = false;

        yield return null;
    }
}