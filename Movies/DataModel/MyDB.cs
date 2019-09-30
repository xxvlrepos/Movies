using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DataModel
{
    public partial class MyDB : UserDB
    {
        public MyDB()
            : base("name=AdminDB")
        {

        }


    }
}
