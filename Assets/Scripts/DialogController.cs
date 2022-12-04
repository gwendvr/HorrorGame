using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI DialogText;
    public string[] sentences;
    private int index = 0;
    public float dialogSpeed;
    public Animator dialogAnimator;
    private bool startDialog = true;
    private bool space = true;
    public float timer = 0.0f;
    float canSpace = 0.5f;

    void Update()
    {
        timer += Time.deltaTime;
        float seconds = timer % 60;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            timer = 0.0f;
            timer += Time.deltaTime;

            if (startDialog)
            {
                dialogAnimator.SetTrigger("Enter");
                startDialog = false;
            }
            else
            {
                if (seconds >= canSpace)
                {
                    nextSentence();
                }
            }
        }
    }

    void nextSentence()
    {
        if(index <= sentences.Length -1)
        {
            DialogText.text = "";
            StartCoroutine(writeSentence());
        }
        else
        {
            DialogText.text = "";
            dialogAnimator.SetTrigger("Exit");
            index = 0;
        }
    }

    IEnumerator writeSentence()
    {
        foreach(char character in sentences[index].ToCharArray())
        {
            DialogText.text += character;
            yield return new WaitForSeconds(dialogSpeed);
        }
        index++;
    }
}
