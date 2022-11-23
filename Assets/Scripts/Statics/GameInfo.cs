using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class GameInfo 
{
    public enum Roles{
        Attacker,
        Defender
    }
    public enum TeamType{
        Player,
        Enemy
    }
    private static Roles playerRole;
    private static int rounds;
    private static int playerScore = 0;
    private static int enemyScore = 0;

    public static int Rounds{
        get{ return rounds; }
        set { rounds = value; }
    }
    public static int PlayerScore{
        get { return playerScore; }
    }
    public static int EnemyScore{
        get { return enemyScore; }
    }

    public static void ResetScore(){
        playerScore = 0;
        enemyScore = 0;
    }
    public static Roles PlayerRole{
        get{ return playerRole; }
    }
    public static void RoleSwitch(){
        switch (playerRole)
        {
            case Roles.Attacker:
                playerRole = Roles.Defender;
                break;
            case Roles.Defender:
                playerRole = Roles.Attacker;
                break;
        }
    }
    
    public static void IncreaseScore(int val, TeamType team){

        switch (team)
        {
            case TeamType.Player:
            playerScore += val;
            break;
            case TeamType.Enemy:
            enemyScore += val;
            break;
        }

    }

}
