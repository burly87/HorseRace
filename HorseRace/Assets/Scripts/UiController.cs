using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UiController : MonoBehaviour
{

    private DataManager dataManager;
    private GameController gameController;

    [SerializeField]
    private GameObject Ui;

    [Header("Win Related")]
    [SerializeField]
    private Sprite[] suitsSprites;
    [SerializeField]
    private GameObject restartBtn;
    [SerializeField]
    private TextMeshProUGUI winnerList;
    [SerializeField]
    private GameObject winScreen;

    [SerializeField]
    private Text winTxt;
    [SerializeField]
    private Image winSprite;

    [Header("Menu related")]
    public GameObject menu;
    public GameObject betMenu;
    [HideInInspector]
    public bool betsPlaced = false;

    public GameObject uiHints;

    [Header("Others")]
    [SerializeField]
    private Button btn_Autoplay;

    private void Start()
    {
        winTxt = Ui.GetComponentInChildren<Text>();
        winSprite = Ui.GetComponentInChildren<Image>();
        dataManager = FindObjectOfType<DataManager>();
        gameController = FindObjectOfType<GameController>();

        winScreen.SetActive(false);
        betMenu.SetActive(true);

        uiHints.SetActive(true);
        btn_Autoplay.interactable = false;
    }

    /// <summary> Show up Winning Text. and Sprite </summary>
    /// <param name="winner"></param>
    public void ShowWinningText(char winner)
    {

        switch (winner)
        {
            case 'C':
                winTxt.text = "CLUBS WINS!";
                winSprite.sprite = suitsSprites[0];
                break;
            case 'D':
                winTxt.text = "DIAMANDS WINS!";
                winSprite.sprite = suitsSprites[1];
                break;
            case 'H':
                winTxt.text = "HEART WINS!";
                winSprite.sprite = suitsSprites[2];
                break;
            case 'S':
                winTxt.text = "SPADES WINS!";
                winSprite.sprite = suitsSprites[3];
                break;
        }

        // Winscreen with names and sips
        winScreen.SetActive(true);
        winnerList.text = "";

        foreach (Player p in dataManager.players)
        {
            winnerList.text += p.name +": " + p.sipstoGive + " sips to give\n";
        }
        
        winTxt.enabled = true;
        winSprite.enabled = true;
        StartCoroutine(ActivateRestartBtn());
    }

    IEnumerator ActivateRestartBtn()
    {
        yield return new WaitForSeconds(2.5f);
        restartBtn.SetActive(true);    
    }

    public void DisableButtons()
    {
        btn_Autoplay.interactable = false;
    }

    public void Autoplay(bool isAutoplay)
    {
        if(isAutoplay)
        {
            var tmpColors = btn_Autoplay.colors;
            tmpColors.normalColor = Color.green;
            btn_Autoplay.colors = tmpColors;
        }
        else
        {
            var tmpColors = btn_Autoplay.colors;
            tmpColors.normalColor = Color.white;
            btn_Autoplay.colors = tmpColors;
        }
    }

    public void OpenCloseMainMenu(bool menuisOpen)
    {
        menu.SetActive(menuisOpen);
        uiHints.SetActive(menuisOpen);

        if (betsPlaced == false)
        {
            betMenu.SetActive(!menuisOpen);
        }
    }

    public void CloseBetMenu()
    {
        betMenu.SetActive(false);
        uiHints.SetActive(false);
        betsPlaced = true;
        btn_Autoplay.interactable = true;
    }

    public void BacktoCharacterMenu()
    {
        dataManager.ClearData();
        gameController.ObservReset();
        SceneManager.LoadScene(0);
    }

    /// <summary>disable txt, sprite and btn</summary>
    public void RestartUi()
    {
        winTxt.enabled = false;
        winSprite.enabled = false;
        winScreen.SetActive(false);

        restartBtn.SetActive(false);
        menu.SetActive(false);
        uiHints.SetActive(false);

        betMenu.SetActive(true);

        betsPlaced = false;
        btn_Autoplay.interactable = false;
    }
}
