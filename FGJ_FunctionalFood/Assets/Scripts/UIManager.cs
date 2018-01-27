using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MadLagBots
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private InputStreamVisualizer _visualizerPrefab;
		private Canvas _canvas;
		public List<Vector2> VisualizerPositions;
		public List<Vector2> VisualizerScales;
		public List<Vector2> VisualizerMins;
		public List<Vector2> VisualizerMaxs;

		private float BeginWidth = 384;

		public void AddVisualizer(RoboModule player, Color color)
		{
			_canvas = FindObjectOfType<Canvas>();
			var playerNumber = player.InputModule.Player;
			var visualization = Instantiate(_visualizerPrefab);
			var rect = visualization.GetComponent<RectTransform>();
			rect.SetParent(_canvas.transform);
			rect.sizeDelta = new Vector2(BeginWidth, 32f);
			rect.localScale = VisualizerScales[playerNumber - 1];
			rect.anchorMin = VisualizerMins[playerNumber - 1];
			rect.anchorMax = VisualizerMaxs[playerNumber - 1];
			rect.anchoredPosition = VisualizerPositions[playerNumber - 1];
			rect.gameObject.GetComponent<Image>().color = color * 0.75f;
			player.InputModule.visualizer = visualization;
		}
    }
}