using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    public enum ItemType
    {
        None,
        Water,
        Thermo,
    }

    [CreateAssetMenu(menuName = "HERA Touch/Item")]
    public class Item : ScriptableObject
    {
        public ItemType type;

        [MyBox.ConditionalField(nameof(type), true, ItemType.None)]
        public Site collectSite;

        [MyBox.ConditionalField(nameof(type), true, ItemType.None)]
        public Site returnSite;

        [MyBox.ConditionalField(nameof(type), true, ItemType.None)]
        public bool shouldBeReturned;
    }

    // public class Item
    // {
    //     public ItemType type;
    // 
    //     // public Vector3 collectLocation;
    //     // public Vector3 returnLocation;
    // 
    //     // public bool shouldBeReturned;
    //     public Item()
    //     {
    //         type = ItemType.None;
    //     }
    // }
}
