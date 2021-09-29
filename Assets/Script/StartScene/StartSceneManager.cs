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
    [SerializeField] private Button BackButton;
    [SerializeField] private List<Button> TurretPosButtons;
    [SerializeField] private List<Button> TurretTypeButtons;

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
    [SerializeField] private Animator ShipModelAnimator;

    [Header("LineRenderer")]
    [SerializeField] private List<LineRenderer> TurretToButtonLines;

    [Header("spriteIMAGE")]
    [SerializeField] private List<Sprite> TurretSpriteImage;




    
    [Header("LevelPerStaticNum")] 
    private static int LevelperDamage = 1;
    private static int LevelperSpeed = 10;
    private static int LevelPerNeedGold = 100;
    
    
    [Header("selected Buttons info")] 
    [SerializeField]private int SelectedTurretType = 0;
    [SerializeField]private List<int> turretPosTypes;

    [Header("Gold")] 
    [SerializeField] private int GoldCount;
    [SerializeField] private Text GoldCountText;
    [SerializeField] private GameObject UpgradeErrorPopup;
    
    [Header("Store")] 
    [SerializeField] private GameObject StorePopup;

    [SerializeField] private Button BuyGoldButton;
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
        UpgradeButton.onClick.AddListener(delegate { UpGradeButtonsOnClick(); });

        GameStartButton = GameObject.Find("GameStartButton").GetComponent<Button>();

        BackButton = GameObject.Find("BackButton").GetComponent<Button>();
        BuyGoldButton = GameObject.Find("GoldBuyButton").GetComponent<Button>();
        BuyGoldButton.onClick.AddListener(delegate { BuyGoldButtonOnClick(); });
        TurretTypeButtons = new List<Button>();
        PlayerStatus._turretinfos = new List<Turretinfo>();
        
        XMLReader.LoadXMLTurretTable(Application.dataPath + "/DATA.xml", out PlayerStatus._turretinfos);
        //터렛 정보 가져오기
        for (int i = 0; i < PlayerStatus._turretinfos.Count; ++i)
        {
            var turretinfo = PlayerStatus._turretinfos[i];
            print(turretinfo._sprite == null);
            turretinfo._sprite = TurretSpriteImage[i];

            if (PlayerPrefs.HasKey(i + "TurretLevel") == false)
            {
                turretinfo._level = 1;
                SaveTurretInfoLevel(i, turretinfo);
            }
            else
            {
                turretinfo._level = PlayerPrefs.GetInt(i + "TurretLevel");
            }

            turretinfo._damage = SetTurretDamage(turretinfo);
            turretinfo._speed = SetTurretSpeed(turretinfo);


           //print("level" + turretinfo._level  + " damge "+ turretinfo._damage +"speed"+ turretinfo._speed);
           PlayerStatus._turretinfos[i] = turretinfo;

        }
        
        for (int i = 0; i < GameObject.Find("TypesContent").transform.childCount; ++i)
        {
            TurretTypeButtons.Add(GameObject.Find("TypesContent").transform.GetChild(i).GetComponent<Button>());

            int temp = i;
            TurretTypeButtons[i].onClick.AddListener(delegate { TurretTypeButtonsOnClick(temp); });
            if(i < PlayerStatus._turretinfos.Count)
            {
                
                TurretTypeButtons[i].transform.GetChild(1).GetComponent<Text>().text = "Lv "+ PlayerStatus._turretinfos[i]._level ;
                
            }
        }

        //getINFO


        TurretPosButtons = new List<Button>();
        TurretToButtonLines = new List<LineRenderer>();

        
        for (int i = 0; i <GameObject.Find("SelectPositionButtons").transform.childCount; i++)
        {
            int num = i;
            TurretPosButtons.Add(GameObject.Find("SelectPositionButtons").transform.GetChild(i).GetComponent<Button>());
            TurretToButtonLines.Add(GameObject.Find("SelectPositionButtons").transform.GetChild(i).GetComponent<LineRenderer>());
            TurretPosButtons[i].onClick.AddListener(delegate { TurretPosButtonOnClick(num); });
        }
        
        for (int i = 0; i < TurretPosButtons.Count; ++i)
        {
            Vector3 btnPos = TurretPosButtons[i].transform.GetChild(1).transform.position;

            TurretToButtonLines[i].positionCount = 2;
            TurretToButtonLines[i].SetPosition(0 ,btnPos);
            TurretToButtonLines[i].SetPosition(1 , GameObject.Find("TurretsPosition").transform.GetChild(i).transform.position);
        }    
        
        turretPosTypes = new List<int>();
        for (int i = 0; i < TurretPosButtons.Count; ++i)
        {
            if (PlayerPrefs.HasKey("TurretPos" + i))
            {
                turretPosTypes.Add(PlayerPrefs.GetInt("TurretPos" + i ));
                SetTurretPosSprite(i);
            }
            else
            {
                turretPosTypes.Add(0);
                saveTurretPosType(i);
                SetTurretPosSprite(i);
            }

  
        }
        
        HaveToPayGoldText = GameObject.Find("HaveToPayGoldText").GetComponent<Text>();
        TurretNameText = GameObject.Find("TurretNameText").GetComponent<Text>();
        TurretInfoText = GameObject.Find("TurretInfoText").GetComponent<Text>();
        TurretDamegeBeforeText = GameObject.Find("TurretDamegeBeforeText").GetComponent<Text>();
        TurretDamegeAfterText = GameObject.Find("TurretDamegeAfterText").GetComponent<Text>();
        TurretShootBeforeSpeedText = GameObject.Find("TurretShootBeforeSpeedText").GetComponent<Text>();
        TurretShootAfterSpeedText = GameObject.Find("TurretShootAfterSpeedText").GetComponent<Text>();
        TurretLevelText = GameObject.Find("NowSelectTurretLevelText").GetComponent<Text>();
            
            
        GoldCountText = GameObject.Find("GoldCountText").GetComponent<Text>();
        UpgradeErrorPopup = GameObject.Find("UpgradeErrorPopup");
        UpgradeErrorPopup.SetActive(false);

        StorePopup = GameObject.Find("StorePopup");
        StorePopup.SetActive(false);
        
        if (PlayerPrefs.HasKey("GoldCount"))
        {
            GoldCount = PlayerPrefs.GetInt("GoldCount");
            GoldCountText.text = GoldCount.ToString();
        }
        else
        {
            GoldCountText.text = 0.ToString();
        }
        
        
        CraftZoneAnimator = CraftZonePannel.GetComponent<Animator>();
        StartSceneAnimator = StartPannel.GetComponent<Animator>();
        ShipModelAnimator = ShipObject.GetComponent<Animator>();
        
        if (StartButton != null)
        {
            StartButton.onClick.AddListener(delegate { StartButtonOnClick(); });
        }      
        if (BackButton != null)
        {
            BackButton.onClick.AddListener(delegate { BackButtonOnClick(); });
        }
        
        CraftZonePannel.SetActive(false);
        ShipModelAnimator.SetBool("isRotateRight" , false);
        ShipModelAnimator.SetBool("isRotateLeft", true);
    }

    private void SaveTurretInfoLevel(int level, Turretinfo turretinfo)
    {
        PlayerPrefs.SetInt(level + "TurretLevel", turretinfo._level);
    }

    private void SetTurretPosSprite(int TypeNum)
    {
        TurretPosButtons[TypeNum].transform.GetChild(0).GetComponent<Image>().sprite =
            PlayerStatus._turretinfos[turretPosTypes[TypeNum]]._sprite;
    }

    private void saveTurretPosType(int i)
    {
        PlayerPrefs.SetInt("TurretPos" + i, turretPosTypes[i]);
    }

    private int SetTurretDamage(Turretinfo turretinfo)
    {
        return (int) (turretinfo._attackPattern + 1) * turretinfo._level * LevelperDamage;
    }
    private int SetTurretDamage(Turretinfo turretinfo, int Level)
    {
        return (int) (turretinfo._attackPattern + 1) * Level * LevelperDamage;
    }
    private int SetTurretSpeed(Turretinfo turretinfo)
    {
        return turretinfo._level * LevelperSpeed / ((int) (turretinfo._attackPattern) +1);
    }
    private int SetTurretSpeed(Turretinfo turretinfo, int Level)
    {
        return Level * LevelperSpeed / ((int) (turretinfo._attackPattern) +1);
    }
    private void TurretPosButtonOnClick(int num)
    {
        // num 에 터렛 바꿔주면 됨
        TurretPosButtons[num].transform.GetChild(0).GetComponent<Image>().sprite = PlayerStatus._turretinfos[SelectedTurretType]._sprite;
        turretPosTypes[num] = SelectedTurretType;
        PlayerPrefs.SetInt("TurretPos" + num , SelectedTurretType );
    }

    private void StartButtonOnClick()
    {              
        CraftZonePannel.SetActive(false); 
        StartSceneAnimator.SetBool("isEnter", false);
        StartSceneAnimator.SetBool("isOut", true);
        
        
        CraftZonePannel.SetActive(true);  
        CraftZoneAnimator.SetBool("isOut", false);
        CraftZoneAnimator.SetBool("isEnter", true);
        
        ShipModelAnimator.SetBool("isRotateRight" , true);
        ShipModelAnimator.SetBool("isRotateLeft", false);
        for (int i = 0; i < TurretPosButtons.Count; ++i)
        {
            TurretToButtonLines[i].startWidth = 0.1f;
            TurretToButtonLines[i].endWidth = 0.1f;
        }

    }
    private void BackButtonOnClick()
    {              
        StartSceneAnimator.SetBool("isEnter", true);
        StartSceneAnimator.SetBool("isOut", false);

        CraftZoneAnimator.SetBool("isOut", true);
        CraftZoneAnimator.SetBool("isEnter", false);
        
        ShipModelAnimator.SetBool("isRotateRight" , false);
        ShipModelAnimator.SetBool("isRotateLeft", true);
        
        for (int i = 0; i < TurretPosButtons.Count; ++i)
        {
            TurretToButtonLines[i].startWidth = 0;
            TurretToButtonLines[i].endWidth = 0;
        }
    }

    private void TurretTypeButtonsOnClick(int num)
    {

        SelectedTurretType = num;
        UpdateInfoUI();
    }


    private int GetLevelPerNeedGold(int num)
    {
        return PlayerStatus._turretinfos[num]._level * LevelPerNeedGold;
    }

    private void UpGradeButtonsOnClick()
    {
        if (GoldCount >= GetLevelPerNeedGold(SelectedTurretType))
        {
            GoldCount -= GetLevelPerNeedGold(SelectedTurretType);
            GoldCountText.text = GoldCount.ToString();
            
            var turretinfo = PlayerStatus._turretinfos[SelectedTurretType];
            turretinfo._level += 1;
            SaveTurretInfoLevel(turretinfo._level, turretinfo);
            turretinfo._speed = SetTurretSpeed(turretinfo);
            turretinfo._damage = SetTurretDamage(turretinfo);
            PlayerStatus._turretinfos[SelectedTurretType]  = turretinfo;
            UpdateInfoUI();
        }
        else
        {
            UpgradeErrorPopup.SetActive(true);
        }
    }

    private void BuyGoldButtonOnClick()
    {
        GoldCount += 100;
        PlayerPrefs.SetInt("GoldCount", GoldCount);
        GoldCountText.text = GoldCount.ToString();
    }

    private void UpdateInfoUI()
    {
        TurretNameText.text = PlayerStatus._turretinfos[SelectedTurretType]._name;
        TurretInfoText.text = PlayerStatus._turretinfos[SelectedTurretType]._info;
        
        TurretDamegeBeforeText.text = PlayerStatus._turretinfos[SelectedTurretType]._damage.ToString();
        TurretDamegeAfterText.text = SetTurretDamage(PlayerStatus._turretinfos[SelectedTurretType], PlayerStatus._turretinfos[SelectedTurretType]._level + 1).ToString();
        TurretDamegeAfterText.color = Color.blue;
        
        TurretShootBeforeSpeedText.text  = PlayerStatus._turretinfos[SelectedTurretType]._speed.ToString();
        TurretShootAfterSpeedText.text = SetTurretSpeed(PlayerStatus._turretinfos[SelectedTurretType], PlayerStatus._turretinfos[SelectedTurretType]._level + 1).ToString();
        TurretShootAfterSpeedText.color = Color.blue;
        
        TurretLevelText.text = PlayerStatus._turretinfos[SelectedTurretType]._level.ToString();
        if (GetLevelPerNeedGold(SelectedTurretType) > GoldCount)
        {
            HaveToPayGoldText.color = Color.red;
        }
        else
        {
            HaveToPayGoldText.color = Color.black;
        }

        TurretTypeButtons[SelectedTurretType].transform.GetChild(1).GetComponent<Text>().text = "Lv "+
            PlayerStatus._turretinfos[SelectedTurretType]._level ;
        HaveToPayGoldText.text = GetLevelPerNeedGold(SelectedTurretType).ToString();
        
        
    }
}
