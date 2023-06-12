using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorWin
{
    public enum Shift
    {
        None = 0,
        Morning = 1,
        Afternoon = 2,
        Night = 3,
    }

    internal class clsShift
    {
        public static Shift GetCurrentShift()
        {
            DateTime dt = DateTime.Now;
            //DateTime dt = new DateTime(2023, 6, 11, 4, 11, 11);
            Shift shift = Shift.None;

            DateTime morningStart = new DateTime(dt.Year, dt.Month, dt.Day, 7, 0, 0);
            DateTime morningEnd = morningStart.AddHours(8);
            DateTime afternoonEnd = morningEnd.AddHours(8);

            if (dt.CompareTo(morningStart) >= 0 && dt.CompareTo(morningEnd) < 0)
            {
                shift = Shift.Morning;
            }
            else if (dt.CompareTo(morningEnd) >= 0 && dt.CompareTo(afternoonEnd) < 0)
            {
                shift = Shift.Afternoon;
            }
            else
            {
                shift = Shift.Night;
            }

            return shift;
        }

        public static (DateTime, DateTime) GetShiftTime(Shift shift)
        {
            DateTime dt = DateTime.Now;
            //DateTime dt = new DateTime(2023, 6, 11, 12, 11, 11);
            DateTime morningStart = new DateTime(dt.Year, dt.Month, dt.Day, 7, 0, 0);

            //Shift shift = GetCurrentShift();
            if (shift == Shift.Morning)
            {
                return (morningStart, morningStart.AddHours(8));
            }
            else if (shift == Shift.Afternoon)
            {
                return (morningStart.AddHours(8), morningStart.AddHours(16));
            }
            else
            {
                DateTime midNight = new DateTime(dt.Year, dt.Month, dt.Day, 23, 0, 0);
                if (dt.CompareTo(midNight) >= 0)
                {
                    return (midNight, midNight.AddHours(8));
                }
                else
                {
                    return (midNight.AddHours(-24), midNight.AddHours(-16));
                }
            }
        }

        public static (Shift, DateTime, DateTime) GetCurrentShiftCombo()
        {
            Shift shift = GetCurrentShift();
            (DateTime, DateTime) shiftTime = GetShiftTime(shift);
            return (shift, shiftTime.Item1, shiftTime.Item2);
        }

        public static (Shift, DateTime, DateTime) GetLastShiftCombo()
        {
            Shift shift = GetCurrentShift();

            (DateTime, DateTime) currentShiftRange = GetShiftTime(shift);

            Shift LastShift = shift == Shift.Morning ? Shift.Night :
                (shift == Shift.Afternoon ? Shift.Morning : Shift.Afternoon);

            return (LastShift, currentShiftRange.Item1.AddHours(-8), currentShiftRange.Item2.AddHours(-8));
        }

        public static (Shift, DateTime, DateTime) GetSecondLastShiftCombo()
        {
            Shift shift = GetCurrentShift();
            (DateTime, DateTime) currentShiftRange = GetShiftTime(shift);

            Shift SecondLast = shift == Shift.Morning ? Shift.Afternoon :
                (shift == Shift.Afternoon ? Shift.Night : Shift.Morning);

            return (SecondLast, currentShiftRange.Item1.AddHours(-16),  currentShiftRange.Item2.AddHours(-16));
        }

        public static string FormatDateTime(DateTime dt )
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }
}
