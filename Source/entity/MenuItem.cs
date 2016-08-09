using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaloBoSach.entity
{
    public class DynamicMenuItem
    {
        public DynamicMenuItem(int id,string name,int parent,int order,string url, string route)
        {
            ItemId = id;
            ItemName = name;
            ParentItemId = parent;
            SortOrder = order;
            DirectUrl = url;
            RoutePath = route;
        }
        private int itemId;
        private string itemName;
        private int parentItemId;
        private List<DynamicMenuItem> subItem;
        private int sortOrder;
        public string DirectUrl { get; set; }
        public string RoutePath { get; set; }
        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public List<DynamicMenuItem> SubItem
        {
            get { return subItem; }
            set { subItem = value; }
        }

        public int ParentItemId
        {
            get { return parentItemId; }
            set { parentItemId = value; }
        }

        public int SortOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }
    }
}