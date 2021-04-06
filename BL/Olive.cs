using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
namespace BL
{
    public class Olive
    {
        private int oliveID;
        private string oliveName;
        private string description;
        private string image;
        /// <summary>
        /// Constructor for Olive. Recives its parameters from datarow.
        /// </summary>
        /// <param name="dr"></param>
        public Olive (DataRow dr)
        {
            oliveID = (int)dr["OliveID"];
            oliveName = dr["OliveName"].ToString();
            description = dr["Description"].ToString();
            image = dr["Image"].ToString();
        }
        public int OliveID
        {
            get
            {
                return oliveID;
            }
        }
        public string OliveName
        {
            get
            {
                return oliveName;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
        }
        public string Image
        {
            get
            {
                return image;
            }
        }
    }
}
