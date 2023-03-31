using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;
    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 175;
    public int scorePerPerfectNote = 150;
    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;
    public float delay = .5f;
    public Text scoreText;
    public Text multiText;
    public float totalNotes ;
    public float normalHits, goodHits, perfectHits, missedHits;
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;
    public static GameManager instance;
    string rankVal = "F";
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = "Carp:0";
        currentMultiplier= 1;
        multiplierTracker = 0;
        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying= true;
                theBS.hasStarted = true;
                theMusic.Play();
            }
                }
        else { if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy) {
                resultsScreen.SetActive(true);

                normalsText.text = "" + normalHits;
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = "" + missedHits;
                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";
              
                if(percentHit> 40) {
                    rankVal = "D";
                    if(percentHit > 55)
                    {
                        rankVal = "C";
                            if(percentHit > 70)
                        {
                            rankVal = "B";
                                if(percentHit > 85)
                            {
                                rankVal = "A";
                                    if(percentHit > 95)
                                {
                                    rankVal = "S";  
                                }
                            }
                        }
                    }
                }
            }
            rankText.text = rankVal;
            finalScoreText.text = currentScore.ToString();
        }

    }
    public void NoteHit()
    {
        Debug.Log("Hit on Time");
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        multiText.text = "Multiplier: x" + currentMultiplier;
        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Carp:" + currentScore;
    }
    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        StartCoroutine(CountdownCoroutine(0.5f, hitEffect));
        NoteHit();
        normalHits++;
    }
    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        StartCoroutine(CountdownCoroutine(0.5f, goodEffect));
        NoteHit();  
        goodHits++;
    }
    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        StartCoroutine(CountdownCoroutine(0.5f, perfectEffect));
        NoteHit();
        perfectHits++;  
    }
    public void NoteMissed()
    {
        Debug.Log("Missed Note");
        StartCoroutine(CountdownCoroutine(0.5f, missEffect));
        currentMultiplier = 1;
        multiplierTracker= 0; 
        missedHits++;
    }
    public IEnumerator CountdownCoroutine(float duration, GameObject objectToActivate)
    {
        objectToActivate.SetActive(true);
        float timer = duration;
        while (timer > 0f)
        {
            
            yield return new WaitForSeconds(0.1f);
            timer -= 0.1f;
        }
        
        objectToActivate.SetActive(false);
    }
}
