using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIMS_CustomWorkflow.Helper
{
    class Helper
    {
        public string SuccessfulTraceMsg(string name)
        {
            string result = "";
            result = "record name = " + name + " was succesfully created " + DateTime.Now;

            return result;
        }
        public string UnsuccessfulTraceMsg(string name)
        {
            string result = "";
            result = "record name = " + name + " was unsuccesfully created " + DateTime.Now;

            return result;
        }


    }
}
