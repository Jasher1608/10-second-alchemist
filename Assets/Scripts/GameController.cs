using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public State state;
    public GameObject nextLevel;

    public List<Sprite> ingredients = new List<Sprite>();
    public List<GameObject> slots = new List<GameObject>();
    private List<Sprite> targetIngredient = new List<Sprite>();

    private void Start()
    {
        state = State.Intro;
        for (int i = 0; i < slots.Count; i++)
        {
            targetIngredient.Add(ingredients[Random.Range(0, ingredients.Count)]); // Sets the desired ingredient for each slot
        }

        for (int i = 0; i < slots.Count; i++)
        {
            Image tempSlotImage;
            tempSlotImage = slots[i].GetComponent<Image>();
            tempSlotImage.sprite = targetIngredient[i];
            tempSlotImage.color = new Color(tempSlotImage.color.r, tempSlotImage.color.g, tempSlotImage.color.b, 0.5f);
        }
    }

    private void Update()
    {
        
    }

    private void PreviewIngredients()
    {

    }
    
    private void CheckSlotMatch()
    {
        
    }
}

public enum State
{
    Intro, // Start of level, where ingredients get flashed for a short amount of time
    Gameplay, // Main part of level, where players must match ingredients
    Outro // End of level, where level transitions to the next level
}
