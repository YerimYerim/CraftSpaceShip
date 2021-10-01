using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPannelManager : MonoBehaviour
{
   private GameObject SocreBoardPannel;
   private Text BestScoreText;
   private Text NowScoreText;
   private Text MaxComboText;
   private Button RestartButton;
   private Button ExitButton;
   private Animator pannelAnimator;
   private bool isScoringStart = false;
   void Awake()
   {
      BestScoreText = GameObject.Find("BestScoreText").GetComponent<Text>();
      NowScoreText = GameObject.Find("NowScoreText").GetComponent<Text>();
      MaxComboText = GameObject.Find("MaxComboText").GetComponent<Text>();
      RestartButton = GameObject.Find("RestartButton").GetComponent<Button>();
      RestartButton.onClick.AddListener(delegate { reStartButtonOnClick(); });

      ExitButton = GameObject.Find("ExitButton").GetComponent<Button>();
      ExitButton.onClick.AddListener(delegate { ExitButtonOnClick(); });
      
      pannelAnimator = GameObject.Find("SocreBoardPannel").GetComponent<Animator>();
      SocreBoardPannel = GameObject.Find("SocreBoardPannel");
      PlayerStatus.GetScorePannel();
      SocreBoardPannel.SetActive(false);
   }

   void reStartButtonOnClick()
   {
      PlayerStatus.ResetScore();
      PlayerStatus.isGameEnd = false;
      
      SceneManager.LoadScene("StartScene");
   }
   void ExitButtonOnClick()
   {
      Application.Quit();
   }
   void Update()
   {
      if (PlayerStatus.isGameEnd)
      {
         SocreBoardPannel.SetActive(true);
      }
      if (PlayerStatus.isGameEnd == true && isScoringStart == false && pannelAnimator.GetCurrentAnimatorStateInfo(0).IsName("GameScoreAni"))
      {
         if (PlayerStatus._BestScore < PlayerStatus._ScoreCount)
         {
            PlayerStatus._BestScore = PlayerStatus._ScoreCount;
            PlayerPrefs.SetInt("MaxScore", PlayerStatus._ScoreCount);
         }
         if (PlayerStatus._MaxCombo < PlayerStatus._ComboCount)
         {
            PlayerStatus._MaxCombo = PlayerStatus._ComboCount;
            PlayerPrefs.SetInt("MaxCombo", PlayerStatus._ComboCount);
         }
         

         
         StartCoroutine(BestScoreCoroutine());
         
         isScoringStart = true;

      }
   }
   IEnumerator BestScoreCoroutine()
   {
      int tempBestScore = 0;
      while (tempBestScore < PlayerStatus._BestScore)
      {
         ++tempBestScore;
         BestScoreText.text = tempBestScore.ToString();
         yield return new WaitForSeconds(0.01f);
      }

      StartCoroutine(NowScoreCoroutine());
   }
   IEnumerator NowScoreCoroutine()
   {
      
      int tempNowScore = 0;
      while (tempNowScore < PlayerStatus._ScoreCount)
      {
         ++tempNowScore;
         NowScoreText.text = tempNowScore.ToString();
         yield return new WaitForSeconds(0.01f);
      }
      StartCoroutine(MaxComboCoroutine());
   }
   IEnumerator MaxComboCoroutine()
   {
      int tmepMaxCombo = 0;
      while (tmepMaxCombo < PlayerStatus._MaxCombo)
      {
         ++tmepMaxCombo;
         MaxComboText.text = tmepMaxCombo.ToString();
         yield return new WaitForSeconds(0.01f);
      }

   }
   
   

}
