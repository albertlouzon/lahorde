using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth = 100;

	public int pointValueOnKill = 10;

	bool isDead = false;
	Animator anim;
	UnityEngine.AI.NavMeshAgent agent;

	ParticleSystem hitParticles;

	AudioSource audio;
	public AudioClip hurt1;
	public AudioClip hurt2;
	public AudioClip hurt3;
	public AudioClip scoreUp;


	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		anim = GetComponent<Animator> ();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		hitParticles = GetComponent<ParticleSystem> ();
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void TakeDamage(int amount, Vector3 hitPoint){
		// we need to find out if the enemy is already or not
		if (isDead)
			return;

		currentHealth -= amount;
		hitParticles.transform.position = hitPoint;
		hitParticles.Play ();
		if (currentHealth <= 0) {
			
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			IronManBehaviorScript playerScript = player.GetComponent<IronManBehaviorScript> ();
		//	playerScript.score += pointValueOnKill;
			audio.PlayOneShot (scoreUp, 1.0f);
			Death ();
		}

		int randomNumber = Random.Range (1, 3);
		switch (randomNumber) {
		case 1:
			audio.PlayOneShot (hurt1, 0.3f);
			break;

		case 2:
			audio.PlayOneShot (hurt2, 0.3f);
			break;

		case 3:
			audio.PlayOneShot (hurt3, 0.3f);
			break;

		}
	}

	public void Death(){
		anim.SetTrigger ("death");
		//agent.enabled = false;
		agent.speed = 0;
		isDead = true;
		Destroy (gameObject, 2.5f);
	}
		
}
