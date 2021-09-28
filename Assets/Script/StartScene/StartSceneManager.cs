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
    public struct Turretinfo
    {
        //xml 로부터 읽어오는 db
        public AttackPattern _attackPattern;
        public string _name;
        public string _info;

        //유저가 가지고있는 정보
        public int _level;
        public int _speed;
        public int _damage;
        //이름에 따라 가져오는 부분
        public Sprite _sprite;
    }

    [SerializeField] private List<Turretinfo> _turretinfos;
    [SerializeField] private List<AttackPattern> _playerTurretAttackPatterns;
    [Header("int")] 
    private int LevelperDamage = 1;

    private int LevelperSpeed = 10;
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

        BackButton = GameObject.Find("BackButton").GetComponent<Button>();

        TurretTypeButtons = new List<Button>();
        _turretinfos = new List<Turretinfo>();
        
        XMLReader.LoadXML(Application.dataPath + "/DATA.xml", out _turretinfos);
        //터렛 정보 저장
        for (int i = 0; i < _turretinfos.Count; ++i)
        {
            var turretinfo = _turretinfos[i];
            print(turretinfo._sprite == null);
            turretinfo._sprite = TurretSpriteImage[i];

            if (PlayerPrefs.HasKey(i + "TurretLevel") == false)
            {
                turretinfo._level = 1;
                PlayerPrefs.SetInt(i + "TurretLevel"  , turretinfo._level);
            }
            else
            {
                turretinfo._level = PlayerPrefs.GetInt(i + "TurretLevel");
            }

            turretinfo._damage = SetTurretDamage(turretinfo);
            turretinfo._speed = SetTurretSpeed(turretinfo);


           //print("level" + turretinfo._level  + " damge "+ turretinfo._damage +"speed"+ turretinfo._speed);
            _turretinfos[i] = turretinfo;
        }
        
        for (int i = 0; i < GameObject.Find("TypesContent").transform.childCount; ++i)
        {
            TurretTypeButtons.Add(GameObject.Find("TypesContent").transform.GetChild(i).GetComponent<Button>());
            int temp = i;
            TurretTypeButtons[i].onClick.AddListener(delegate { TurretTypeButtonsOnClick(temp); });
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
        print(num);
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

        TurretNameText.text = _turretinfos[num]._name;
        TurretInfoText.text = _turretinfos[num]._info;
        
        TurretDamegeBeforeText.text = _turretinfos[num]._damage.ToString();
        TurretDamegeAfterText.text = SetTurretDamage(_turretinfos[num], _turretinfos[num]._level + 1).ToString();
        TurretDamegeAfterText.color = Color.blue;
        
        TurretShootBeforeSpeedText.text  = _turretinfos[num]._speed.ToString();
        TurretShootAfterSpeedText.text = SetTurretSpeed(_turretinfos[num], _turretinfos[num]._level + 1).ToString();
        TurretShootAfterSpeedText.color = Color.blue;
        
        TurretLevelText.text = _turretinfos[num]._level.ToString();
        HaveToPayGoldText.text = (_turretinfos[num]._level * 100).ToString();
    }
}
