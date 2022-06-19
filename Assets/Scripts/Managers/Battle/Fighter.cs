using UnityEngine;

public record Fighter : MonoBehaviour
{
    public string name { get; init; }
    public int health { get; set; }
    public int maxHealth { get; init; }
}
