using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro; // Add this namespace for TextMeshPro

public class CarSwapper : MonoBehaviour
{
    // Start is called before the first frame update
    // Array of car prefabs
    public GameObject[] carPrefabs;

    // Current car index
    private int currentCarIndex = 0;

    // UI Button for next car
    public Button nextButton;
    public Button previousButton;

    // UI Text for car name
    public TextMeshProUGUI carNameText; // Add a reference to your TextMeshProUGUI component

    void Start()
    {
        // Initialize the first car
        ShowCar(currentCarIndex);

        // Add a listener to the next button
        nextButton.onClick.AddListener(NextCar);
        previousButton.onClick.AddListener(PreviousCar);

        // Add a listener to the EventSystem for mobile touch events
    }

    // Show the car at the specified index
    void ShowCar(int index)
    {
        // Disable all cars
        foreach (GameObject car in carPrefabs)
        {
            car.SetActive(false);
        }

        // Enable the car at the specified index
        carPrefabs[index].SetActive(true);

        // Update the car name text
        carNameText.text = carPrefabs[index].name; // Display the name of the GameObject

    }

    // Called when the next button is clicked
    void NextCar()
    {
        // Increment the current car index
        currentCarIndex = (currentCarIndex + 1) % carPrefabs.Length;

        // Show the next car
        ShowCar(currentCarIndex);
    }

    void PreviousCar()
    {
        // Decrement the current car index
        currentCarIndex = (currentCarIndex - 1 + carPrefabs.Length) % carPrefabs.Length;

        // Show the previous car
        ShowCar(currentCarIndex);
    }
}

public class Car : MonoBehaviour
{
    public string carName; // Add a carName property to your Car script
}