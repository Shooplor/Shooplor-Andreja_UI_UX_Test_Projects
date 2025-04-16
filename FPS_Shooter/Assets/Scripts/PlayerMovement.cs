using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    // Moving/Jumping
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
   public bool canMove = true;
    private bool wasGrounded = true;
    private bool isInAir = false;

    // Shooting
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 50f;
    public float shootCooldown = 0.5f;
    public bool canShoot = true;
    private bool isShooting = false;

    // UI & Audio
    [SerializeField] GameObject gameManager;
    [SerializeField] Animator weaponAnimator;
    [SerializeField] Animator crosshairAnimator;
    [SerializeField] Animator helmetAnimator; // New helmet animator reference
    public AudioSource footStepsSFX;
    public Image deathImage;
    public Image imageToDisable1;
    public Image imageToDisable2;

    // Shell ejection
    [SerializeField] private GameObject leftShell;
    [SerializeField] private GameObject rightShell;
    [SerializeField] private float shellDisplayTime = 0.5f;

    [Header("Death Sequence")]
    public float deathFallSpeed = 5f;
    private bool isFalling = false;
    private bool isDead = false;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gameManager = GameObject.FindWithTag("GameManager");

        // Deactivate shells at start
        if (leftShell != null) leftShell.SetActive(false);
        if (rightShell != null) rightShell.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (isDead) return;

        // Keep track of grounded state
        bool isGrounded = characterController.isGrounded;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = isRunning ? runningSpeed * Input.GetAxis("Vertical") : walkingSpeed * Input.GetAxis("Vertical");
        float curSpeedY = isRunning ? runningSpeed * Input.GetAxis("Horizontal") : walkingSpeed * Input.GetAxis("Horizontal");

        bool isMoving = (Mathf.Abs(curSpeedX) > 0.1f || Mathf.Abs(curSpeedY) > 0.1f);

        footStepsSFX.mute = !isMoving;

        float movementDirectionY = moveDirection.y;
        if (canMove)
        {
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        }

        // Handle helmet animations
        if (helmetAnimator != null)
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            // Set walking animation
            helmetAnimator.SetBool("isWalking", isMoving);

            // Set strafing animations
            bool movingLeft = horizontalInput < -0.1f && Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput);
            bool movingRight = horizontalInput > 0.1f && Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput);

            helmetAnimator.SetBool("isStrafingLeft", movingLeft);
            helmetAnimator.SetBool("isStrafingRight", movingRight);
        }

        // Track if player is in air for animation purposes
        isInAir = !isGrounded;
        if (weaponAnimator != null)
        {
            weaponAnimator.SetBool("isInAir", isInAir);
        }

        // Update walking animation state (only if not shooting and on ground)
        if (!isShooting && isGrounded && weaponAnimator != null)
        {
            weaponAnimator.SetBool("isWalking", isMoving);
        }

        // Handle jumping input and animation
        if (Input.GetButtonDown("Jump") && canMove && isGrounded)
        {
            moveDirection.y = jumpSpeed;

            // Trigger jump animation only if not shooting
            if (weaponAnimator != null && !isShooting)
            {
                weaponAnimator.SetTrigger("isJumping");
            }

            // Trigger helmet jump animation
            if (helmetAnimator != null)
            {
                helmetAnimator.SetTrigger("isJumping");
            }
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // If we just landed from being in air
        if (!wasGrounded && isGrounded)
        {
            // Reset states as needed
            isInAir = false;
            if (weaponAnimator != null)
            {
                weaponAnimator.SetBool("isInAir", false);
            }
        }

        // Update wasGrounded for next frame
        wasGrounded = isGrounded;

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2.5f);
        foreach (Collider hit in hitColliders)
        {
            if (hit.CompareTag("Enemy") && !isDead)
            {
                StartCoroutine(DamageAndDeathSequence());
                break;
            }
        }

        // Only allow shooting if not currently shooting and canShoot is true
        if (Input.GetButtonDown("Fire1") && canShoot && !isShooting && canMove && !isDead)
        {
            Shoot();
        }
    }

    IEnumerator ShowDeathImageAndDie()
    {
        isDead = true;
        canMove = false;

        // Show death image
        if (deathImage != null)
        {
            deathImage.gameObject.SetActive(true);
            deathImage.enabled = true;
        }

        // Disable the other two images
        if (imageToDisable1 != null)
            imageToDisable1.enabled = false;
        if (imageToDisable2 != null)
            imageToDisable2.enabled = false;

        // Freeze enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
            if (enemyScript != null)
            {
                if (enemyScript.animator != null)
                    enemyScript.animator.enabled = false;

                var agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
                if (agent != null)
                    agent.isStopped = true;
            }
        }

        yield return new WaitForSeconds(5f);
        Die();
    }

    void Die()
    {
        SceneManager.LoadScene(2);
    }

    void Shoot()
    {
        canShoot = false;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // Activate and animate shells
        if (leftShell != null)
        {
            leftShell.SetActive(true);
            Animator leftAnim = leftShell.GetComponent<Animator>();
            if (leftAnim != null) leftAnim.Play("ShellEject", -1, 0f);
            StartCoroutine(DeactivateShell(leftShell, shellDisplayTime));
        }

        if (rightShell != null)
        {
            rightShell.SetActive(true);
            Animator rightAnim = rightShell.GetComponent<Animator>();
            if (rightAnim != null) rightAnim.Play("ShellEject", -1, 0f);
            StartCoroutine(DeactivateShell(rightShell, shellDisplayTime));
        }

        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("Rigidbody component is NULL!!");
        }

        crosshairAnimator.SetBool("isFireing", true);

        isShooting = true;
        StartCoroutine(PlayShootAnimation());
    }

    IEnumerator DeactivateShell(GameObject shell, float delay)
    {
        yield return new WaitForSeconds(delay);
        shell.SetActive(false);
    }

    IEnumerator PlayShootAnimation()
    {
        if (weaponAnimator != null)
        {
            // Stop walking animation if it's playing
            weaponAnimator.SetBool("isWalking", false);
            // Set shooting parameter
            weaponAnimator.SetBool("isShooting", true);
        }

        yield return new WaitForSeconds(0.7f);

        if (weaponAnimator != null)
        {
            // Reset shooting parameter
            weaponAnimator.SetBool("isShooting", false);

            // If we're in air, we might need to return to jump state
            if (isInAir)
            {
                weaponAnimator.SetTrigger("isJumping");
            }
            else
            {
                // Otherwise check if we should go back to walking
                bool isCurrentlyMoving = (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f);
                weaponAnimator.SetBool("isWalking", isCurrentlyMoving);
            }
        }

        crosshairAnimator.SetBool("isFireing", false);
        isShooting = false;
        canShoot = true;  // Allow shooting again only after the animation completes
    }
    private IEnumerator DamageAndDeathSequence()
    {
        isDead = true;
        canMove = false;
        canShoot = false;

        // Show damage indicator for 0.5 seconds
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            yield return gameManager.StartCoroutine(gameManager.ShowDamageIndicator());
        }

        // Instantly put player on the ground
        transform.position = new Vector3(
            transform.position.x,
            2.2f, // Slightly above ground
            transform.position.z
        );

        // Tilt camera while waiting for death sequence
        if (gameManager != null)
        {
            StartCoroutine(gameManager.TiltCamera(playerCamera.transform));
        }

        // Proceed with existing death sequence
        StartCoroutine(ShowDeathImageAndDie());
    }

    IEnumerator camShakeDamage()
    {
        playerCamera.transform.localPosition = Vector3.Lerp(new Vector3(0, 0.75f, 0), new Vector3(0.7f, 0.75f, 0), 0.1f);
        yield return new WaitForSeconds(0.1f);
        playerCamera.transform.localPosition = Vector3.Lerp(new Vector3(0.7f, 0.75f, 0), new Vector3(-0.7f, 0.75f, 0), 0.1f);
        yield return new WaitForSeconds(0.1f);
        playerCamera.transform.localPosition = Vector3.Lerp(new Vector3(-0.7f, 0.75f, 0), new Vector3(0, 0.75f, 0), 0.1f);

        yield break;
    }
}