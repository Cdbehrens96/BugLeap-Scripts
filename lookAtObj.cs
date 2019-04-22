using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lookAtObj : MonoBehaviour {

	GameObject mainCamera;
	bool looking;
	GameObject lookedAtObj;
	private GameObject tankUI,iPadUI, player, labUI;
	private Animator tankUIAnim, iPadUIAnim, playerAnim;

	private float lightVal, labUIval;
	private bool lit;
	private GameObject stars;
	private ParticleSystem system;

	private Image uiImg;
	private Color labUIcolor;

	private CanvasGroup aiCG, ipadCG, tankCG;
	private float fadeTime = 2;

	public static bool peeped;
	public static bool levelsUp;

	void Start () {
		mainCamera = GameObject.FindWithTag ("MainCamera");

		tankUI = GameObject.FindGameObjectWithTag ("tankUI");
		tankUIAnim = tankUI.GetComponent<Animator>();

		iPadUI = GameObject.FindGameObjectWithTag ("iPadUI");
		iPadUIAnim = iPadUI.GetComponent<Animator>();

		player = GameObject.FindWithTag ("Player");
		playerAnim = player.GetComponent<Animator>();

		stars = GameObject.FindWithTag ("stars");
		system = stars.GetComponent<ParticleSystem>();

		labUI = GameObject.FindWithTag ("labUI");
		//uiImg = labUI.GetComponent<Image>();
		//labUIcolor = uiImg.color;

		lightVal = 1.0f;

		aiCG = labUI.GetComponent<CanvasGroup>();
		ipadCG = iPadUI.GetComponent<CanvasGroup>();
		tankCG = tankUI.GetComponent<CanvasGroup>();
	}
	
	void Update () {
		RenderSettings.ambientIntensity = lightVal;
		labUIcolor.a = labUIval;

		observe ();

		/*if(lit == true && Input.GetMouseButtonDown(1)){
			playerAnim.SetBool ("padAppear", false);
			StartCoroutine (Brighten());
			lit = false;
			system.Stop();
			stars.SetActive(false);
		}*/
	}

	void observe() {
		int x = Screen.width / 2;
		int y = Screen.height / 2;

		Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)) {
			if (hit.transform.tag == "tank"){
				tankUIAnim.SetBool ("peeped", true);
				if(Input.GetButtonDown("Jump")){
					tankUIAnim.SetBool ("moveDown", true);
					StartCoroutine(levelsTrue());
				}
				if(levelsUp){
					if(Input.GetButtonDown("Back")){
						tankUIAnim.SetBool ("moveDown", false);
						levelsUp = false;
					}
				}
			} else {
				tankUIAnim.SetBool ("peeped", false);
			}

			if (hit.transform.tag == "iPad"){
				iPadUIAnim.SetBool ("peeped", true);
				if(Input.GetButtonDown("Jump")){
					playerAnim.SetBool ("padAppear", true);
					StartCoroutine (Dim());
					lit = true;
					stars.SetActive(true);
					system.Play();
				}

				if (lit){
					if(Input.GetButtonDown("Back")){
						playerAnim.SetBool ("padAppear", false);
						StartCoroutine (Brighten());
						lit = false;
						system.Stop();
						stars.SetActive(false);
					}
				}

			} else {
				iPadUIAnim.SetBool ("peeped", false);
			}
		}
	}

	IEnumerator levelsTrue() {
		yield return new WaitForSeconds(0.5f);
		levelsUp = true;
	}

	IEnumerator Dim () {
		for (int i = 0; i < 10; i++){
			yield return new WaitForSeconds(0.05f);
			if (lightVal > 0.15f){
				lightVal -= 0.1f;
			}
			
			peeped = true;
		}
		
		while (aiCG.alpha > 0){
			aiCG.alpha -= Time.deltaTime * fadeTime;
			ipadCG.alpha -= Time.deltaTime * fadeTime;
			tankCG.alpha -= Time.deltaTime * fadeTime;
			yield return null;
		}
		
		aiCG.interactable = false;
		ipadCG.interactable = false;
		tankCG.interactable = false;
		yield return null;
	}

	IEnumerator Brighten () {
		for (int i = 0; i < 10; i++){
			yield return new WaitForSeconds(0.05f);
			if (lightVal < 1.0f){
				lightVal += 0.1f;
			}
		}

		while (aiCG.alpha <= 1){
			aiCG.alpha += Time.deltaTime * fadeTime;
			ipadCG.alpha += Time.deltaTime * fadeTime;
			tankCG.alpha += Time.deltaTime * fadeTime;
			yield return null;
		}
		
		aiCG.interactable = true;
		ipadCG.interactable = true;
		tankCG.interactable = true;
		yield return null;
	}
}
