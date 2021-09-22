using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPannel : MonoBehaviour
{
   private Text BestScoreText;
   private Text NowScoreText;
   private Text MaxComboText;
   private Button RestartButton;
   private Button ExitButton;
   
   
   void Awake()
   {
      BestScoreText = GameObject.Find("BestScoreText").GetComponent<Text>();
      NowScoreText = GameObject.Find("NowScoreText").GetComponent<Text>();
      MaxComboText = GameObject.Find("MaxComboText").GetComponent<Text>();
      RestartButton = GameObject.Find("RestartButton").GetComponent<Button>();
      ExitButton = GameObject.Find("ExitButton").GetComponent<Button>();
      
   }

   IEnumerator BestScoreCoroutine()
   {
      yield return new WaitForSeconds(0.1f);
   }
   IEnumerator NowScoreCoroutine()
   {
      yield return new WaitForSeconds(0.1f);
   }
   IEnumerator MaxComboCoroutine()
   {
      yield return new WaitForSeconds(0.1f);
   }
   

}
