using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject StartPannel;
    [SerializeField] private GameObject CraftZonePannel;
    [SerializeField] private GameObject ShipObject;
    
    
    [Header("Buttons")]
    [SerializeField] private Button StartButton;
    [SerializeField] private Button GameStartButton;
    [SerializeField] private Button ExitButton;
    [SerializeField] private Button StoreButton;
    [SerializeField] private Button SettingButton;
    [SerializeField] private Button OpenInfoButton;
    [SerializeField] private Button UpgradeButton;

    [Header("Text")] 
    [SerializeField] private Text HaveToPayGoldText;
    [SerializeField] private Text TurretNameText;
    [SerializeField] private Text TurretInfoText;
    [SerializeField] private Text TurretDamegeBeforeText;
    [SerializeField] private Text TurretDamegeAfterText;
    [SerializeField] private Text TurretShootBeforeSpeedText;    
    [SerializeField] private Text TurretShootAfterSpeedText;    
    [SerializeField] private Text TurretLevelText;


    [Header("Animator")] 
    
    [SerializeField] private Animator CraftZoneAnimator;
    [SerializeField] private Animator StartSceneAnimator;   
    void Awake()
    {
        StartPannel = GameObject.Find("StartPanel");
        CraftZonePannel = GameObject.Find("CraftZonePanel");
        ShipObject = GameObject.Find("ShipModel");
        StartButton = GameObject.Find("StartButton").GetComponent<Button>();

        ExitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        StoreButton = GameObject.Find("StoreButton").GetComponent<Button>();
        SettingButton = GameObject.Find("Settingbutton").GetComponent<Button>();
        OpenInfoButton = GameObject.Find("InfoButton").GetComponent<Button>();
        UpgradeButton = GameObject.Find("UpgradeButton").GetComponent<Button>();
        GameStartButton = GameObject.Find("GameStartButton").GetComponent<Button>();
        
        
        
        HaveToPayGoldText = GameObject.Find("HaveToPayGoldText").GetComponent<Text>();
        TurretNameText = GameObject.Find("TurretNameText").GetComponent<Text>();
        TurretInfoText = GameObject.Find("TurretInfoText").GetComponent<Text>();
        TurretDamegeBeforeText = GameObject.Find("TurretDamegeBeforeText").GetComponent<Text>();
        TurretDamegeAfterText = GameObject.Find("TurretDamegeAfterText").GetComponent<Text>();
        TurretShootBeforeSpeedText = GameObject.Find("TurretShootBeforeSpeedText").GetComponent<Text>();
        TurretShootAfterSpeedText = GameObject.Find("TurretShootAfterSpeedText").GetComponent<Text>();    
        TurretLevelText = GameObject.Find("NowSelectTurretLevelText").GetComponent<Text>();


        CraftZoneAnimator = CraftZonePannel.GetComponent<Animator>();
        StartSceneAnimator = StartPannel.GetComponent<Animator>();
        
        
        if (StartButton != null)
        {
            StartButton.onClick.AddListener(delegate { StartButtonOnClick(); });
        }

        
        CraftZonePannel.SetActive(false);
    }

    private void StartButtonOnClick()
    {              
        StartSceneAnimator.SetBool("isOut", true);
        CraftZonePannel.SetActive(true);  
        CraftZoneAnimator.SetBool("isEnter", true);
        
        StartCoroutine(rotatingModel());
    }

    IEnumerator rotatingModel()
    {
    
        while (ShipObject.transform.rotation.x < -0.5)
        {
            ShipObject.transform.Rotate(0,1f,0f);
            
            yield return new WaitForSeconds(0.001f);
        }
        
    }
}
