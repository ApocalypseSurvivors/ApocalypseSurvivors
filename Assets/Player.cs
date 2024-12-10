using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UltimateXR.Mechanics.Weapons;
using UltimateXR.Locomotion;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Devices;
using UltimateXR.Manipulation;

using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private UxrActor actor;
    private Rigidbody rb;

    [Header("Damage effect")]
    public Volume vol;
    private Vignette vignette;

    [Header("Health")]
    [SerializeField] float maxHealth = 100;
    private PlayerHealthBar healthBar;

    [SerializeField] float damageForceMultiplier = 0.1f;
    [SerializeField] private AudioClip _takeDamageAudioClip;
    [SerializeField] private AudioClip _dieAudioClip;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private AudioClip heartBeat;
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI deathTimer;

    public AudioSource gameOver;
    private Dictionary<string, List<Coroutine>> coroutineDictionary = new Dictionary<string, List<Coroutine>>();

    // [SerializeField] private AudioClip heartBeat;
    [Header("Auto Damage")]
    public int autoDamageAmount = 10; // Damage to apply
    public float autoDamageInterval = 40f; // Interval in seconds

    [Header("Footstep")]
    public AudioSource foot;
    public AudioClip[] footsteps;
    private bool isGrounded = false;          // Checks if the player is on the ground
    private Vector3 lastPos;


    public Transform[] spawnLocations;

    public Coroutine StartManagedCoroutine(string name, IEnumerator routine)
    {
        Coroutine coroutine = StartCoroutine(routine);
        if (!coroutineDictionary.ContainsKey(name))
        {
            coroutineDictionary[name] = new List<Coroutine>();
        }
        coroutineDictionary[name].Add(coroutine);
        return coroutine;
    }

    public void StopManagedCoroutines(string name)
    {
        if (coroutineDictionary.ContainsKey(name))
        {
            foreach (Coroutine coroutine in coroutineDictionary[name])
            {
                StopCoroutine(coroutine);
            }
            coroutineDictionary.Remove(name);
        }
    }

    private void spawnRandomLoc() {
        int len = spawnLocations.Length; 
        if (len > 0) {
           int randomIndex = Random.Range(0, len); 
           Vector3 spawnPosition = spawnLocations[randomIndex].position;
           transform.position = spawnPosition;
           transform.rotation = spawnLocations[randomIndex].rotation;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        actor = GetComponent<UxrActor>();
        actor.Life = maxHealth;

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        healthBar = GetComponentInChildren<PlayerHealthBar>();
        // vol = GetComponentInChildren<PostProcessVolume>();

        if (!vol) {
            Debug.Log("Debug null vol");
        }
        VolumeProfile volumeProfile = vol.profile;
        if (!volumeProfile) {
            Debug.Log("Debug null profile");
        }
        vol.profile.TryGet<Vignette>(out vignette);
        if (!vignette) {
            Debug.Log("Debug null vignette");
        } else {
            vignette.active = false;
        }
        
        if (healthBar != null) {  
            UpdateHealthBar();
        } else {
            Debug.Log("Debug null healthBar 1");
        }
        deathText.gameObject.SetActive(false);
        deathTimer.gameObject.SetActive(false);
        playHeartBeat();
        StartCoroutine(ApplyDamageOverTime());
        foot.enabled = false;

        spawnRandomLoc();
        lastPos = transform.position;
        Debug.Log("Debug player init success");
 
    }
    private void restart() {
        UxrGrabManager.Instance.IsGrabbingAllowed = true;
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }

    private bool dead() {
        return actor.Life <= 0;
    }

    private void OnCollisionStay()
    {
        // Check if we landed on the ground
        isGrounded = true;
    }

    private void OnCollisionExit()
    {
        // Check if we landed on the ground
        isGrounded = false;
    }

    private bool grounded() {
        if (isGrounded) {
            // isGrounded = false;
            return true;
        } else {
            return false;
        }
    }

    void PlayFootstep()
    {
        if (footsteps.Length > 0 && !foot.enabled)
        {
            // Debug.Log("Debug foot 1");
            // Select a random footstep sound from the array
            int index = Random.Range(0, footsteps.Length);
            foot.clip = footsteps[index];
            foot.loop = true;
            foot.enabled = true;
            foot.Play();
        }
    }

    private IEnumerator TakeDamageEffect() {
        float lifeRatio = healthRatio();
        float lostLifeRatio = 1f - lifeRatio;
        float intensity = lostLifeRatio > 0.7f? lostLifeRatio: 0.7f;

        // Enable the vignette and set initial intensity
        vignette.active = true;
        vignette.intensity.Override(intensity);

        // Debug.Log("Debug enable vigne");
        // Wait for the initial delay
        yield return new WaitForSeconds(0.4f);

        // Gradually reduce the vignette intensity
        while (intensity > lostLifeRatio / 2f)
        {
            intensity = vignette.intensity.value;  
            intensity -= 0.01f;

            // Ensure intensity does not go below zero
            if (intensity < 0) intensity = 0;

            // Update the vignette intensity
            vignette.intensity.Override(intensity);

            // Wait for a short delay before the next update
            yield return new WaitForSeconds(0.1f);
        }

        // Disable the vignette
        // vignette.active = false;

        // End the coroutine
        yield break;
    }

    // Because of the UXR smooth motion I cannot use rb.velocity 
    private float speedCal() {
        float speed = ((transform.position - lastPos) / Time.deltaTime).magnitude;
        lastPos = transform.position;
        return speed;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = Application.isFocused? 1: 0;
        if (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Button2))
        {
            restart();
        }

        float time = Time.deltaTime;

        updateSourceVolume();

        bool g = grounded();
        if (speedCal() > 0.1f && g) {
            // Debug.Log("Debug foot");
            PlayFootstep();
        } else {
            foot.enabled = false;
        }

        if (healthBar != null) { 
            UpdateHealthBar();
        } else {
            Debug.Log("Debug null healthBar 2");
        }
    }

    void UpdateHealthBar() {
        healthBar.UpdateHealthBar(actor.Life, maxHealth);
    }

    private void playAudio(AudioClip clip) {
        if (!clip) {
            Debug.Log("Debug null clip");
        } else {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }

    private void releaseGrabs() {
        // For unknown reason this function has an exception
        UxrGrabManager manager = UxrGrabManager.Instance;

        IEnumerable<UxrGrabbableObject> grabbedObjects = manager?.CurrentGrabbedObjects;
        Debug.Log("Debug get grabs ");

        // Release each grabbed object
        foreach (UxrGrabbableObject obj in grabbedObjects)
        {
            Debug.Log("Debug release get grabs ");
            obj.ReleaseGrabs(true);
        }   
        Debug.Log("Debug release all grabs ");
    }

    public void TakeDamage(float damageAmount, Transform attacker = null)
    {
        if (!dead()) {
            actor.Life -= damageAmount;
            UpdateHealthBar();
            AudioSource.PlayClipAtPoint(_takeDamageAudioClip, transform.position);
            StopManagedCoroutines("TakeDamageEffect"); 
            StartManagedCoroutine("TakeDamageEffect", TakeDamageEffect());
            if (dead()) {
                PlayerDie();
            }
        }
        if (attacker) {
            knockBack(attacker, damageAmount);
        }
    }

    void playClipAfterDeath() {
        gameOver.volume = 0.8f;
        gameOver.clip = gameOverMusic;
        gameOver.loop = true;
        gameOver.Play();
    } 

    void playHeartBeat() {
        updateSourceVolume();
        gameOver.clip = heartBeat;
        gameOver.loop = true;
        gameOver.Play();
    }

    // Method to format time in seconds to "HH:MM:SS"
    public string FormatTime(float totalSeconds)
    {
        // Calculate hours, minutes, and seconds
        int hours = Mathf.FloorToInt(totalSeconds / 3600);
        int minutes = Mathf.FloorToInt((totalSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(totalSeconds % 60);
        // Format as "HH:MM:SS"
        string formattedTime = string.Format("💀 {0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        return formattedTime;
    }

    void PlayerDie() {
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
        deathText.gameObject.SetActive(true);
        deathTimer.gameObject.SetActive(true);
        deathTimer.text = FormatTime(Time.timeSinceLevelLoad);

        StopManagedCoroutines("TakeDamageEffect"); 
        vignette.intensity.Override(1f);

        UxrGrabManager.Instance.IsGrabbingAllowed = false;
        Invoke("releaseGrabs", 1);
        Invoke("releaseGrabs", 2);
        UxrSmoothLocomotion motion = GetComponent<UxrSmoothLocomotion>(); 
        // UxrAvatar.LocalAvatarInput.SetIgnoreControllerInput(UxrHandSide.Left, true);
        // UxrAvatar.LocalAvatarInput.SetIgnoreControllerInput(UxrHandSide.Right, true);
        if (!motion) {
            Debug.Log("Debug null motion");
        } else {
            motion.enabled = false;
        }
        playAudio(_dieAudioClip);
        Invoke("playClipAfterDeath", 2f);
    }

    public float healthRatio() {
        return actor.Life / maxHealth;
    }

    void updateSourceVolume() {
        if (!dead()) {
            gameOver.volume = 1f - healthRatio(); 
        }
    }

    private IEnumerator ShowGameOverUI() {
        yield return new WaitForSeconds(1f);
    }

    private void knockBack(Transform attacker, float damage) {
            Vector3 knockbackDirection = (transform.position - attacker.position).normalized;
            rb.AddForce(knockbackDirection * damage * damageForceMultiplier, ForceMode.Impulse);
    }

    public void heal(float heal) {
        actor.Life += heal;
        if (actor.Life > maxHealth) {
            actor.Life = maxHealth;
        }
        UpdateHealthBar();
    }

   private IEnumerator ApplyDamageOverTime()
   {
       while (true) // Continuous loop
       {
           // Wait for the specified interval
           yield return new WaitForSeconds(autoDamageInterval);
           TakeDamage(autoDamageAmount); 
           // Apply damage
       }
   }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("enemy_hand"))
    //     {
    //         
    //         Debug.Log("Debug attack Damage");
    //         float damageAmount = other.gameObject.GetComponent<crypto_enemy_hand>().damage * 0.5f;
    //         TakeDamage(damageAmount);
    //
    //         Vector3 knockbackDirection = (transform.position - other.transform.position).normalized;
    //         rb.AddForce(knockbackDirection * 1f, ForceMode.Impulse);
    //         // actor.ReceiveDamage(other.gameObject.GetComponent<crypto_enemy_hand>().damage);
    //     }
    // }
}
