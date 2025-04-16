using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField]
    Image topCurtain, bottomCurtain, blackCurtain, anchorPointTop, anchorPointBottom, anchorPointCenter;
    [SerializeField]
    Text stageNumberText, gameOverText;


    public void ShowStage(int stageNumber)
    {
        stageNumberText.enabled = true;
        stageNumberText.text = $"Stage    {stageNumber}";
        topCurtain.rectTransform.position = new Vector3(topCurtain.rectTransform.position.x, anchorPointCenter.rectTransform.position.y + (anchorPointCenter.rectTransform.position.y/2));
        bottomCurtain.rectTransform.position = new Vector3(bottomCurtain.rectTransform.position.x, anchorPointCenter.rectTransform.position.y - (anchorPointCenter.rectTransform.position.y / 2));
        blackCurtain.rectTransform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(StartStage());
    }

    public void JustShowStage()
    {
        topCurtain.enabled = bottomCurtain.enabled = blackCurtain.enabled = false;
        stageNumberText.enabled = gameOverText.enabled = false;
    }

    public void ShowGameOver()
    {
        gameOverText.enabled = true;
        StartCoroutine(GameOver());
    }

    IEnumerator StartStage()
    {
        StartCoroutine(RevealStageNumber());
        yield return new WaitForSeconds(5);
        StartCoroutine(RevealTopStage());
        StartCoroutine(RevealBottomStage());
    }
    IEnumerator RevealStageNumber()
    {
        while (blackCurtain.rectTransform.localScale.y > 0)
        {
            blackCurtain.rectTransform.localScale = new Vector3(1, Mathf.Clamp(blackCurtain.rectTransform.localScale.y - Time.deltaTime, 0, 1), 1);
            yield return null;
        }
    }
    IEnumerator RevealTopStage()
    {
        stageNumberText.enabled = false;
        while (topCurtain.rectTransform.position.y < anchorPointTop.rectTransform.position.y)
        {
            topCurtain.rectTransform.Translate(new Vector3(0, 500 * Time.deltaTime, 0));
            yield return null;
        }
    }
    IEnumerator RevealBottomStage()
    {
        while (bottomCurtain.rectTransform.position.y > anchorPointBottom.rectTransform.position.y)
        {
            bottomCurtain.rectTransform.Translate(new Vector3(0, -500 * Time.deltaTime, 0));
            yield return null;
        }
    }

    public IEnumerator GameOver()
    {
        while (gameOverText.rectTransform.localPosition.y < 0)
        {
            gameOverText.rectTransform.localPosition = new Vector3(gameOverText.rectTransform.localPosition.x, gameOverText.rectTransform.localPosition.y + 120f * Time.deltaTime, gameOverText.rectTransform.localPosition.z);
            yield return null;
        }
    }
}
