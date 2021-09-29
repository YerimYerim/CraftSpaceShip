using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
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
      ExitButton = GameObject.Find("ExitButton").GetComponent<Button>();
      pannelAnimator = GameObject.Find("SocreBoardPannel").GetComponent<Animator>();
      SocreBoardPannel = GameObject.Find("SocreBoardPannel");
      PlayerStatus.GetScorePannel();
      SocreBoardPannel.SetActive(false);
   }

   void Update()
   {
      if (PlayerStatus.isGameEnd)
      {
         SocreBoardPannel.SetActive(true);
      }
      if (PlayerStatus.isGameEnd == true && isScoringStart == false && pannelAnimator.GetCurrentAnimatorStateInfo(0).IsName("GameScoreAni"))
      {
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
