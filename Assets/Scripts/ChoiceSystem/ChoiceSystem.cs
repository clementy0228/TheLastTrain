﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceSystem : MonoSingleton<ChoiceSystem>
{
    [SerializeField] [Range(0f, 1f)]
    private float TrainCardProbability;

    [SerializeField][Space]
    private Vector2[] CadrPositions;

    [SerializeField]
    private ChoiceCard[] TrainCards;

    [SerializeField]
    private ChoiceCard[] PolicyCards;

    private WeekTable mStartByWeekTable;

    private void Awake()
    {
        GameEvent.Instance.DescribeMonthEvent(ShowUpChoiceCards);

        mStartByWeekTable = GameEvent.Instance.GetWeek.GetWeekTable;
    }
    private void ShowUpChoiceCards()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Random.value <= TrainCardProbability)
            {
                EnableCard(i, TrainCards);
            }
            else
            {
                EnableCard(i, PolicyCards);
            }
        }
    }
    private void EnableCard(int index, ChoiceCard[] cards)
    {
        int selectIndex = 0;

        float probability = Random.value;

        float closestValue = float.MaxValue;

        int dayCondition = 0;

        if (GameEvent.Instance.GetWeek.GetWeekTable.years - mStartByWeekTable.years >= 0)
        { dayCondition = 2; }
        else 
        if (GameEvent.Instance.GetWeek.GetWeekTable.month - mStartByWeekTable.month >= 6)
        { dayCondition = 1; }

        for (int i = 0; i < cards.Length; i++)
        {
            float close = probability - cards[i].GetProbabilities[dayCondition];

            if (close < closestValue) {
                selectIndex = i;

                closestValue = close;
            }
        }
        cards[selectIndex].transform.localPosition = CadrPositions[index];

        cards[selectIndex].gameObject.SetActive(true);
    }
}
