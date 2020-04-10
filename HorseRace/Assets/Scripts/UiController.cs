using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField]
    private GameObject Ui;
    [SerializeField]
    private Sprite[] suitsSprites;
    [SerializeField]
    private GameObject restartBtn;

    private Text winTxt;
    private Image winSprite;

    private void Start()
    {
        winTxt = Ui.GetComponentInChildren<Text>();
        winSprite = Ui.GetComponentInChildren<Image>();
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
        
        winTxt.enabled = true;
        winSprite.enabled = true;
        StartCoroutine(ActivateRestartBtn());
    }

    IEnumerator ActivateRestartBtn()
    {
        yield return new WaitForSeconds(2.5f);
        restartBtn.SetActive(true);    
    }

    /// <summary>disable txt, sprite and btn</summary>
    public void RestartUi()
    {
        winTxt.enabled = false;
        winSprite.enabled = false;
        restartBtn.SetActive(false);
    }

}
