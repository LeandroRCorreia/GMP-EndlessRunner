using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownOverlay : MonoBehaviour
{

    [Header("CountDown")]
    [SerializeField] private UiAudioController uiAudioController;
    [SerializeField] private TextMeshProUGUI countDown;
    [SerializeField] private int initialFontSize = 100;
    [SerializeField] private int endFontSize = 300;
    [SerializeField] private float speedScaleFontSize = 1.0f;

    
    public IEnumerator PerformCountDown(int startGameDelay)
    {
        countDown.text = "";
        float timeRemaining = startGameDelay;
        
        while(timeRemaining > 0)
        {
            int lastTime = Mathf.CeilToInt(timeRemaining);
            countDown.text =  $"{lastTime}";
            countDown.fontSize = initialFontSize;
            uiAudioController.PlayCountDownSound();
            while(lastTime == Mathf.CeilToInt(timeRemaining))
            {
                timeRemaining -= Time.deltaTime;
                
                countDown.fontSize = Mathf.Lerp(countDown.fontSize, endFontSize, speedScaleFontSize * Time.deltaTime);
              
                yield return null;

            }
            yield return null;
            countDown.text = "";
            uiAudioController.PlayCountDownEnd();
        }

    }




}
