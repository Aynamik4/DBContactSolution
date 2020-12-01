using System;
using System.Collections.Generic;
using System.Text;

namespace DBContactLibrary.Models
{
    public class ContactInfo
    {
        public int ID { get; set; }
        public string Info { get; set; }
        public int? ContactID { get; set; } // Note: Nullable int.

        public override string ToString()
        {
            return $"ID: {ID}, Info: {Info}, ContactID: {ContactID}";
        }
    }
}
