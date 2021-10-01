using UnityEngine;
using iii_UMVR06_TPSDefenseGame_Subroutines_2.InventorySystem;

[CreateAssetMenu(fileName = "New Trap Item", menuName = "Inventory Items/Trap Item")]
public class TrapObject : InventoryItem {
   
    public new string Name => name;

    public override string GetInfoDisplayText() {
        return "";
        //StringBuilder builder = new StringBuilder();

        //builder.Append(Name).AppendLine();
        //builder.Append("Max Stack: ").Append(MaxStack).AppendLine();

        //return builder.ToString();
    }

}
