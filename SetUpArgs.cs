using Miracle.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateEmployeeScheduleReport
{
    [ArgumentDescription("Set up arguments through the command line arguments")]
    public class SetUpArgs
    {
        [ArgumentName("Name", "N")]
        [ArgumentRequired]
        [ArgumentDescription("Employee name!")]
        public string EmployeeName { get; set; }

        [ArgumentName("startDate", "SD")]
        [ArgumentRequired]
        [ArgumentDescription("Start date where it starts with day shift!")]
        public string StartDate { get; set; }


        [ArgumentName("checkDate", "CD")]
        [ArgumentRequired]
        [ArgumentDescription("Check date, which is our interest to know if we are in day shift, night shift or it's off!")]
        public string CheckDate { get; set; }

        [ArgumentName("Language","Lang", "LN")]
        [ArgumentDescription("The Culture of the program!")]
        [ArgumentRequired]
        public string Language { get; set; }
    }
}
