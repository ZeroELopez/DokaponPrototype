using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface Actions
{
    Vector2Int loc { get; set; }
    PlayerScript player { get; set; }
    void Play();
    void Reverse();
    void Undo();
}