using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IItem
{
    string name { get; }

    int basePrice { get; }

    void Use(PlayerScript player);
    void Remove();
    void Pocket(PlayerScript player);
    void SteppedOn(PlayerScript player);
}

