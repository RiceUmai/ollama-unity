using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingAnim : MonoBehaviour
{

    [SerializeField]
    private Image[] circles;
    private const float DURATION = 1f;

    void Start()
    {
        for (var i = 0; i < circles.Length; i++)
        {
            var angle = -2 * Mathf.PI * i / circles.Length;
            circles[i].rectTransform.anchoredPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 150f;
            circles[i].DOFade(0f, DURATION).SetLoops(-1, LoopType.Yoyo).SetDelay(DURATION * i / circles.Length);
        }
    }
}
