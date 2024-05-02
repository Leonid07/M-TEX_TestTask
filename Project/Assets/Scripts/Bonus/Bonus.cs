using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using static Bonus;

public class Bonus : MonoBehaviour
{
    [SerializeField] float speed = 10f; // Скорость падения бонуса
    [SerializeField] public TypeBonus typeBonus; // тип бонуса
    [SerializeField] float rotationSpeed = 50f; // скорость вращения
    [SerializeField] float timeToDestroy = 4f; //Время до уничтожения
    // показывать в инспекторе только когда TypeBonus =PowerUp
    public int levelUpForWeapons;

    // показывать в инспекторе только когда TypeBonus = ScoreBoost
    public int NumberOfPoints;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
        // Двигаем бонус вниз по оси Y
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            })
            .AddTo(this);
    }
    [Serializable]
    public enum TypeBonus
    {
        None = 0,
        PowerUp = 1,
        ScoreBoost = 2
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Bonus))]
public class PlayerMovingEditor : Editor
{
    private Bonus script;

    private SerializedProperty typeBonus;
    private SerializedProperty speed;
    private SerializedProperty timeToDestroy;

    private SerializedProperty levelUpForWeapons;

    private SerializedProperty NumberOfPoints;

    private void OnEnable()
    {
        script = (Bonus)target;

        typeBonus = serializedObject.FindProperty("typeBonus");
        speed = serializedObject.FindProperty("speed");
        timeToDestroy = serializedObject.FindProperty("timeToDestroy");

        levelUpForWeapons = serializedObject.FindProperty("levelUpForWeapons");

        NumberOfPoints = serializedObject.FindProperty("NumberOfPoints");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (script == null)
        {
            EditorGUILayout.LabelField("Item is null");
            return;
        }

        EditorGUILayout.PropertyField(typeBonus);
        EditorGUILayout.PropertyField(speed);
        EditorGUILayout.PropertyField(timeToDestroy);

        TypeBonus selectedType = script.typeBonus;
        switch (selectedType)
        {
            case Bonus.TypeBonus.PowerUp:
                EditorGUILayout.PropertyField(levelUpForWeapons);
                break;
            case Bonus.TypeBonus.ScoreBoost:
                EditorGUILayout.PropertyField(NumberOfPoints);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif