using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoPlayerStats : MonoBehaviour
{
    //Score
    int score;

    //Combo
    int combo;
    int comboMax;

    //Stats du joueur
    int nbPièces;
    int nbFan;

    public void AddComboAndScore()
    {
        combo += 1;
        score += 1 * combo;

        //Si le combo est plus grand que le comboMax, alors go le mettre à jour.
        if (combo >= comboMax)
        {
            comboMax = combo;
        }
    }

    public void ResetAddComboAndScore()
    {
        combo = 0;
    }

    void UpdateScoreandCombo()
    {
        //Besoin d'un écran de fin (GameOver ou fin de partie)
    }
}
