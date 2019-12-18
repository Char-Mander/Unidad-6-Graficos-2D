using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Transform posiDisp;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float cadencia;

    Character2DController controller2D;
    Animator anim;

    bool isJumping = false;
    bool isCrouching = false;
    bool canJumpOrShoot = true;
    bool canShoot = true;
    float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        controller2D = GetComponent<Character2DController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Run", Mathf.Abs(horizontal));
        if (Input.GetKeyDown(KeyCode.UpArrow) && canJumpOrShoot)
        {
            isJumping = true;
            anim.SetTrigger("Jump");
        }

        if (Input.GetKey(KeyCode.Space) && canJumpOrShoot)
        {
            if (canShoot)
            {
                canShoot = false;
                Shoot();
            }
            anim.SetLayerWeight(1, 1);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isCrouching = false;
        }
    }

    private void FixedUpdate()
    {
        controller2D.Move(horizontal*moveSpeed*Time.deltaTime, isCrouching, isJumping);
    }

    public void hitGround()
    {
        isJumping = false;
    }
    public void OnCrouch(bool value)
    {
        anim.SetBool("Crouch", value);
        canJumpOrShoot = !value;
    }

    private void Shoot()
    {
        Instantiate(bullet, posiDisp.position, Quaternion.LookRotation(transform.forward * transform.localScale.x));
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(cadencia);
        canShoot = true;
    }
}
