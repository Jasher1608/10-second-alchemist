using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    
    public static State state;
    public GameObject nextLevel;

    public List<Sprite> ingredients = new List<Sprite>();
    public List<GameObject> slots = new List<GameObject>();
    private List<Sprite> targetIngredient = new List<Sprite>();
    private List<bool> correctIngredient = new List<bool>();

    [SerializeField] private float previewDuration;

    private void Start()
    {
        state = State.Intro;
        for (int i = 0; i < slots.Count; i++)
        {
            targetIngredient.Add(ingredients[Random.Range(0, ingredients.Count)]); // Sets the desired ingredient for each slot
            correctIngredient.Add(false);
        }

        PreviewIngredients();
    }

    private void Update()
    {
        if (state == State.Gameplay)
        {
            CheckSlotMatch();
            if (TimerController.timeRemaining <= 0)
            {
                state = State.OutroLose;
                StartOutroLose();
            }
        }
    }

    private void PreviewIngredients()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Image tempSlotImage;
            tempSlotImage = slots[i].GetComponent<Image>();
            tempSlotImage.sprite = targetIngredient[i];
            tempSlotImage.color = new Color(tempSlotImage.color.r, tempSlotImage.color.g, tempSlotImage.color.b, 0.5f);
        }

        StartCoroutine("StartGameplay");
    }
    
    private void CheckSlotMatch()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Image tempSlotImage;
            tempSlotImage = slots[i].GetComponent<Image>();
            if (tempSlotImage.sprite == targetIngredient[i])
            {
                correctIngredient[i] = true;
            }
            else if (tempSlotImage.sprite != targetIngredient[i])
            {
                tempSlotImage.sprite = null;
                tempSlotImage.color = new Color(tempSlotImage.color.r, tempSlotImage.color.g, tempSlotImage.color.b, 0f);
            }
        }

        if (correctIngredient.TrueForAll(AllTrue))
        {
            state = State.OutroWin;
            StartOutroWin();
        }
    }

    private bool AllTrue(bool b)
    {
        return b;
    }

    private IEnumerator StartGameplay()
    {
        yield return new WaitForSeconds(previewDuration);

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<Image>().sprite = null;
            Image tempSlotImage;
            tempSlotImage = slots[i].GetComponent<Image>();
            tempSlotImage.color = new Color(tempSlotImage.color.r, tempSlotImage.color.g, tempSlotImage.color.b, 0f);
        }
        state = State.Gameplay;
        TimerController.timeRemaining = 10f;
    }

    private void StartOutroWin()
    {
        if (nextLevel != null)
        {
            nextLevel.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else if (nextLevel == null)
        {
            Debug.Log("You Win!");
        }
    }

    private void StartOutroLose()
    {
        Debug.Log("You lose!");
    }
}

public enum State
{
    Intro, // Start of level, where ingredients get flashed for a short amount of time
    Gameplay, // Main part of level, where players must match ingredients
    OutroWin, // End of level, where level transitions to the next level
    OutroLose // End of level, where player failed
}
