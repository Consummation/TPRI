using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevel
{
    void Accept();
    void Decline();
    void GenerateNewCharacter();
    void CalculateCharacter();
}
