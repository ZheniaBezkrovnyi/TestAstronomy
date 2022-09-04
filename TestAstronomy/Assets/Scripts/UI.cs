using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UI : MonoBehaviour
{
    [SerializeField] private Button updateCreate,backPanel,butPanelUpdateCreate;
    [SerializeField] private Game game;
    [SerializeField] private RectTransform panelMass;
    [SerializeField] private Text textMass;
    public SLider sliderMove;
    public SLider sliderRadius;
    public SLider sliderDistance;
    private void Start()
    {
        UpdateCreate();
        sliderMove.Begin();
        sliderRadius.Begin();
        sliderDistance.Begin();
        ActiveUI(updateCreate,panelMass,true);
        ActiveUI(backPanel, panelMass, false);
    }

    private void ActiveUI(Button button,RectTransform ui,bool Bool)
    {
        button.onClick.AddListener(() => ui.gameObject.SetActive(Bool));
    }

    private void UpdateCreate()
    {
        butPanelUpdateCreate.onClick.AddListener(() => game.CreateAll());
    }
    public int GetMassText()
    {
        return CheckText(textMass.text);
    }
    private int CheckText(string text)
    {
        if (textMass.text == null)
        {
            return 0;
        }
        int number = 0;
        int.TryParse(textMass.text, out number);
        return number;
    }
}

[Serializable]
public class SLider
{
    [SerializeField] private Slider slider;
    [SerializeField] private float min;
    [SerializeField] private float max;
    [SerializeField] private Text minText;
    [SerializeField] private Text maxText;
    public void Begin()
    {
        minText.text = min.ToString();
        maxText.text = max.ToString();
    }
    public float GetValue()
    {
        float different = max - min;
        return min + different * slider.value;
    }

}
