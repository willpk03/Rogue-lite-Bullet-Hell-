using UnityEngine;

[CreateAssetMenu(fileName = "myData", menuName = "ScriptableObjects/myData")]
public class myData : ScriptableObject
{
    public int health; //Player Health
    public float speed; //Player Movement Speed
    public int HScredits; //Players highest reached Score
    public double reloadCD; //Players Bullet CD
    public int pierce; //Bullet will do deal 1 Damage X amount of times
}
