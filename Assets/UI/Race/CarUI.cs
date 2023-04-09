using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CarUI : MonoBehaviour
{
    private Label _speedometerValue;
    private ProgressBar _powerValue;
    private Label _gearValue;
    
    private int _counterToUpdate = 0;
    private const int UpdateEvery = 10;

    public CarUI(GameObject gameObject, float gearSpeedPeriod)
    {
        UIDocument uiDocument = gameObject.transform.Find("UIDocument").GetComponent<UIDocument>();
        
        _speedometerValue = uiDocument.rootVisualElement.Q<Label>("speedometer__value");
        
        _powerValue = uiDocument.rootVisualElement.Q<ProgressBar>("speedometer__power");
        _powerValue.highValue = gearSpeedPeriod;
        // _powerValue.style.color = new StyleColor(Color.red);
        _powerValue.Children().First().Children().First().Children().First().style.backgroundColor =
            new StyleColor(Color.black);
        _gearValue = uiDocument.rootVisualElement.Q<Label>("speedometer__gear");
    }

    private void UpdateSpeedValue(float speedValue)
    {
        _speedometerValue.text = Convert.ToUInt16(speedValue).ToString();
    }
    
    private void UpdatePowerValue(float powerValue)
    {
        _powerValue.value = powerValue;
    }

    private void UpdateCurrentGear(int currentGear)
    {
        _gearValue.text = currentGear.ToString();
    }

    public void ResetPowerAfterAccelerate()
    {
        _powerValue.value = _powerValue.value > 0 ? _powerValue.value - (_powerValue.value / _powerValue.highValue) * 100 : 0;
    }

    public void UpdateSpeedometer(float speedValue, float powerValue, int currentGear)
    {
        _counterToUpdate += 1;
        
        if (_counterToUpdate % UpdateEvery == 0)
        {
            UpdateSpeedValue(speedValue);
        }
        UpdatePowerValue(powerValue);
        
        UpdateCurrentGear(currentGear);
    }
}
