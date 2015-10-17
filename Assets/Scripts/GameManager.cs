using UnityEngine;
using System.Collections;
using InputHandler;
using MenusHandler;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int PlayerCount = 4;
    private static GameManager _instance;
    public MomBehavior mom;
    public Image Icon1;
    public Image Icon2;
    public Image Icon3;
    public Image IconGo;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;

        /*
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }*/
    }

    void Start()
    {
        for (int i = 0; i < PlayerCount; i++)
        {
            InputManager.Instance.AddCallback(i, HandleMenuInput);
        }


        // play gameplay music
        MusicManager.Instance.PlayGameplayMusic();

        Icon1.gameObject.SetActive(false);
        Icon2.gameObject.SetActive(false);
        Icon3.gameObject.SetActive(false);
        IconGo.gameObject.SetActive(false);

        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(FadeOutNumber(Icon3));
        yield return StartCoroutine(FadeOutNumber(Icon2));
        yield return StartCoroutine(FadeOutNumber(Icon1));

        mom.StartGame = true;

        yield return StartCoroutine(FadeOutNumber(IconGo));
    }

    private IEnumerator FadeOutNumber(Image number)
    {
        number.gameObject.SetActive(true);

        number.rectTransform.offsetMax = Vector2.zero;
        number.rectTransform.offsetMin = Vector2.zero;

        Vector2 initialAnchorMin = new Vector2(0.4f, 0.3f);
        Vector2 initialAnchorMax = new Vector2(0.6f, 0.7f);

        Vector2 finalAnchor = new Vector2(0.5f, 0.5f);

        float ratio = 0f;

        Color initialColor = number.color;
        Color finalColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / 1f;

            number.rectTransform.anchorMin = Vector2.Lerp(initialAnchorMin, finalAnchor, ratio);
            number.rectTransform.anchorMax = Vector2.Lerp(initialAnchorMax, finalAnchor, ratio);

            number.color = Color.Lerp(initialColor, finalColor, ratio);

            yield return null;
        }

        number.gameObject.SetActive(false);
    }

    public void PushMenuContext()
    {
        for (int i = 0; i < PlayerCount; i++)
        {
            InputManager.Instance.PushActiveContext("Menu", i);
        }
    }

    public void PopMenuContext()
    {
        for (int i = 0; i < PlayerCount; i++)
        {
            InputManager.Instance.PopActiveContext(i);
        }
    }

    private void HandleMenuInput(MappedInput input)
    {
        float yAxis = 0f;

        if (input.Ranges.ContainsKey("SelectOptionUp"))
        {
            yAxis = input.Ranges["SelectOptionUp"];
        }
        else if (input.Ranges.ContainsKey("SelectOptionDown"))
        {
            yAxis = -input.Ranges["SelectOptionDown"];
        }

        bool accept = input.Actions.Contains("Accept");

        MenusManager.Instance.SetInputValues(accept, false, 0f, yAxis);
    }

    public MomBehavior.State GetMomState() {
        return mom.GetState();
    }
}
