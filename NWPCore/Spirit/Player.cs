using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWPCore
{
    public class Player : Spirit
    {
        public Player(GameManager gm)
        {
            GM = gm;
            Items = new Dictionary<string, int>();
        }

        GameManager GM;

        public Dictionary<string, int> Items;

        public WorkCompleted GetItem(string itemID)
        {
            WorkCompleted result = new WorkCompleted();

            if (Items.ContainsKey(itemID))
            {
                Items[itemID] += 1;
            }
            else
            {
                Items.Add(itemID, 1);
            }

            result.result = true;
            result.log = "你获得了" + GM.ItemDictionary[itemID].Name;

            return result;
        }
    }
}
