using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Project_Recipient_Application.Utility
{
    public class DataConvert
    {
        public string ConverToShamsi()
        {
            PersianCalendar ps = new PersianCalendar();
            return ps.GetYear(DateTime.Now) + "/" + ps.GetMonth(DateTime.Now) + "/" + ps.GetDayOfMonth(DateTime.Now);
        }

        public string ConverToShamsi(DateTime dt)
        {
            PersianCalendar ps = new PersianCalendar();
            return ps.GetYear(DateTime.Now) + "/" + ps.GetMonth(DateTime.Now) + "/" + ps.GetDayOfMonth(DateTime.Now);
        }

        public int GetYearShamsi()
        {
            PersianCalendar ps = new PersianCalendar();
            return ps.GetYear(DateTime.Now);
        }
    }
}