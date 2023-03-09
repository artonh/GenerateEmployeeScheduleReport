using Miracle.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using WorkingHoursWShifts12Day24Off12Night48Off;

namespace GenerateEmployeeScheduleReport
{
    class Program
    {
        static ResourceManager rm;
        static void Main(string[] args)
        {
            try
            {
                if (args != null && args.Any())
                {
                    rm = new ResourceManager("GenerateEmployeeScheduleReport.Resources.Resources", typeof(Program).Assembly);

                    var arg = args.ParseCommandLine<SetUpArgs>();

                    var startDate = DateTime.Parse(arg.StartDate);
                    var checkDate = DateTime.Parse(arg.CheckDate);

                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(arg.Language);
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(arg.Language);

                    StringBuilder strOutput = new StringBuilder();
                    WorkingHours wschedule;

                    int lastCheckedMonth = startDate.Month;

                    strOutput.AppendLine(string.Format(GetFromResource("Title"), arg.EmployeeName, DateTime.Now.Year));

                    strOutput.AppendLine(startDate.ToString("MMMM").ToUpper());


                    foreach (var day in EachDay(startDate, checkDate))
                    {
                        wschedule = new WorkingHours(startDate);
                        var result = wschedule.GetWorkingType(day);

                        if (result != WorkingType.Off)
                        {
                            if (lastCheckedMonth != day.Month)
                            {
                                strOutput.AppendLine(Environment.NewLine);
                                strOutput.AppendLine(day.ToString("MMMM").ToUpper());
                                lastCheckedMonth = day.Month;
                            }
                            strOutput.Append(string.Format("{0}:{1}! -------", day.Day, (result == WorkingType.Day ? GetFromResource("DayShift") : GetFromResource("NightShift"))));
                        }

                    }

                    new PDFGenerator().GeneratePDF($"{arg.EmployeeName} {GetFromResource("ReportName")} {DateTime.Now.Year}.pdf", strOutput);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        static string GetFromResource(string resID)
        {
            return rm.GetString(resID);
        }
    }




}
