using Project_Shoe_Stock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Shoe_Stock.ViewModels
{
    public class GroupData
    {
        public string Key { get; set; }
        public IEnumerable<Stock> Data { get; set; } = new List<Stock>();
    }
}