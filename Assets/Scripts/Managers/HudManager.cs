using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudManager : UnitySingleton<HudManager> {

	public TextMeshProUGUI scoreText;
	public Slider healthBarSlider;
	public Slider energyBarSlider;

}
