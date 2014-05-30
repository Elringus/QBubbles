using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(LevelsManager))]
public class LevelsManagerInspector : Editor
{
	private SerializedObject targetObject;

	public void OnEnable ()
	{
		targetObject = new SerializedObject(target);
	}

	public override void OnInspectorGUI ()
	{
		targetObject.Update();

		int sizeTimes = targetObject.FindProperty("LevelStartTimes.Array.size").intValue;
		int sizeMinIntervals = targetObject.FindProperty("MinSpawnIntervals.Array.size").intValue;
		int sizeMaxIntervals = targetObject.FindProperty("MaxSpawnIntervals.Array.size").intValue;
		int sizeMinSizes = targetObject.FindProperty("MinBubbleSizes.Array.size").intValue;
		int sizeMaxSizes = targetObject.FindProperty("MaxBubbleSizes.Array.size").intValue;
		int sizeSpeedFactors = targetObject.FindProperty("BubbleSpeedFactors.Array.size").intValue;

		EditorGUILayout.BeginHorizontal();
		int curSize = EditorGUILayout.IntField("Количество уровней:", sizeTimes);
		if (GUILayout.Button("+")) curSize++;
		if (GUILayout.Button("-")) curSize--;
		EditorGUILayout.EndHorizontal();

		if (curSize != sizeTimes) targetObject.FindProperty("LevelStartTimes.Array.size").intValue = curSize;
		if (curSize != sizeMinIntervals) targetObject.FindProperty("MinSpawnIntervals.Array.size").intValue = curSize;
		if (curSize != sizeMaxIntervals) targetObject.FindProperty("MaxSpawnIntervals.Array.size").intValue = curSize;
		if (curSize != sizeMinSizes) targetObject.FindProperty("MinBubbleSizes.Array.size").intValue = curSize;
		if (curSize != sizeMaxSizes) targetObject.FindProperty("MaxBubbleSizes.Array.size").intValue = curSize;
		if (curSize != sizeSpeedFactors) targetObject.FindProperty("BubbleSpeedFactors.Array.size").intValue = curSize;

		GUIStyle style = new GUIStyle(GUI.skin.label);
		style.margin = new RectOffset(0, 0, 10, 20);
		style.padding = new RectOffset(0, 0, 0, 0);
		style.fontSize = 13;
		style.fontStyle = FontStyle.Bold;
		style.alignment = TextAnchor.MiddleCenter;

		for (int i = 0; i < curSize; i++)
		{
			EditorGUILayout.Separator();

			EditorGUILayout.LabelField("Уровень сложности №" + (i + 1), style);

			EditorGUILayout.Space();

			EditorGUILayout.PropertyField(targetObject.FindProperty(string.Format("LevelStartTimes.Array.data[{0}]", i)), new GUIContent("Время начала, сек"));

			EditorGUILayout.Space();

			float minSpawn = targetObject.FindProperty(string.Format("MinSpawnIntervals.Array.data[{0}]", i)).floatValue;
			float maxSpawn = targetObject.FindProperty(string.Format("MaxSpawnIntervals.Array.data[{0}]", i)).floatValue;
			EditorGUILayout.LabelField(string.Format("Интервал спавна: от {0} сек. до {1} сек.", minSpawn.ToString("F2"), maxSpawn.ToString("F2")));
			EditorGUILayout.MinMaxSlider(ref minSpawn, ref maxSpawn, .1f, 10);
			targetObject.FindProperty(string.Format("MinSpawnIntervals.Array.data[{0}]", i)).floatValue = minSpawn;
			targetObject.FindProperty(string.Format("MaxSpawnIntervals.Array.data[{0}]", i)).floatValue = maxSpawn;

			EditorGUILayout.Space();

			float minSize = targetObject.FindProperty(string.Format("MinBubbleSizes.Array.data[{0}]", i)).floatValue;
			float maxSize = targetObject.FindProperty(string.Format("MaxBubbleSizes.Array.data[{0}]", i)).floatValue;
			EditorGUILayout.LabelField(string.Format("Интервал размера: от {0:P0} до {1:P0}", minSize, maxSize));
			EditorGUILayout.MinMaxSlider(ref minSize, ref maxSize, .01f, 1);
			targetObject.FindProperty(string.Format("MinBubbleSizes.Array.data[{0}]", i)).floatValue = minSize;
			targetObject.FindProperty(string.Format("MaxBubbleSizes.Array.data[{0}]", i)).floatValue = maxSize;

			EditorGUILayout.Space();

			EditorGUILayout.LabelField(string.Format("Модификатор скорости: {0:P0}", targetObject.FindProperty(string.Format("BubbleSpeedFactors.Array.data[{0}]", i)).floatValue));
			targetObject.FindProperty(string.Format("BubbleSpeedFactors.Array.data[{0}]", i)).floatValue = 
				EditorGUILayout.Slider(targetObject.FindProperty(string.Format("BubbleSpeedFactors.Array.data[{0}]", i)).floatValue, 0.01f, 1);

			EditorGUILayout.Space();
		}

		targetObject.ApplyModifiedProperties();
	}
}