using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    public static State state;
    public static int level = 1;

    [SerializeField] private int difficulty = 1;

    public List<Sprite> ingredients = new List<Sprite>();
    public List<GameObject> slots = new List<GameObject>();
    private List<Sprite> targetIngredient = new List<Sprite>();
    private List<bool> correctIngredient = new List<bool>();

    private float previewDuration;

    [SerializeField] private ParticleSystem explosionParticleEffect = default;

    [SerializeField] private Canvas canvas;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private TextMeshProUGUI lossText;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level01":
                level = 1;
                break;
            case "Level02":
                level = 2;
                break;
        }

        TimerController.timeRemaining = 10;
        timerText.text = "10";
        correctIngredient.Clear();
        targetIngredient.Clear();
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
        else if (state == State.OutroWin)
        {
            SpinBoard();
        }
        else if (state == State.OutroLose)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                lossText.gameObject.SetActive(false);
                difficulty = 1;
                Start();
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
            StartCoroutine(StartOutroWin(3));
        }
    }

    private bool AllTrue(bool b)
    {
        return b;
    }

    private IEnumerator StartGameplay()
    {
        if (level == 1)
        {
            switch (difficulty)
            {
                case 1:
                    previewDuration = 3;
                    break;
                case 2:
                    previewDuration = 2;
                    break;
                case 3:
                    previewDuration = 1;
                    break;
            }
        }
        else if (level == 2)
        {
            switch (difficulty)
            {
                case 1:
                    previewDuration = 3;
                    break;
                case 2:
                    previewDuration = 2.33f;
                    break;
                case 3:
                    previewDuration = 1.5f;
                    break;
            }
        }
        
        yield return new WaitForSeconds(previewDuration);

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<Image>().sprite = null;
            Image tempSlotImage;
            tempSlotImage = slots[i].GetComponent<Image>();
            tempSlotImage.color = new Color(tempSlotImage.color.r, tempSlotImage.color.g, tempSlotImage.color.b, 0f);
        }
        state = State.Gameplay;
    }

    private IEnumerator StartOutroWin(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
        explosionParticleEffect.Play();
        transform.rotation = Quaternion.Euler(12.5f, 0, 0);
        difficulty += 1;

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<Image>().sprite = null;
            Image tempSlotImage;
            tempSlotImage = slots[i].GetComponent<Image>();
            tempSlotImage.color = new Color(tempSlotImage.color.r, tempSlotImage.color.g, tempSlotImage.color.b, 0f);
        }

        if (difficulty <= 3)
        {
            Start();
        }
        else if (difficulty > 3 && level == 1)
        {
            level += 1;
            difficulty = 1;
            SceneManager.LoadScene("Cutscene01", LoadSceneMode.Single);
        }
        else if (difficulty > 3 && level == 2)
        {
            level += 1;
            difficulty = 1;
            // Load cutscene02
        }
    }

    private void StartOutroLose()
    {
        lossText.gameObject.SetActive(true);
    }

    private void SpinBoard()
    {
        this.gameObject.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}

public enum State
{
    Intro, // Start of level, where ingredients get flashed for a short amount of time
    Gameplay, // Main part of level, where players must match ingredients
    OutroWin, // End of level, where level transitions to the next level
    OutroLose // End of level, where player failed
}
