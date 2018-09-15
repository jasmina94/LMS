using System;

namespace LMS.DomainModel.ConversionHelper
{
    public static class ConversionHelper
    {
        private static readonly DateTime NullTime = new DateTime(1800, 1, 1);
        private static readonly TimeSpan NullTimeSpan = new TimeSpan(0, 0, 0);


        public static DateTime ToDateTimeFromDB(this object value)
        {
            DateTime dateValue;

            if (value.GetHashCode() != 0 && value != DBNull.Value)
            {
                dateValue = Convert.ToDateTime(value);
            }
            else
            {
                dateValue = NullTime;
            }

            return dateValue;
        }

        public static int ToIntFromDB(this object value)
        {
            int intValue;

            if (value != null && value != DBNull.Value)
            {
                intValue = Convert.ToInt32(value);
            }
            else
            {
                intValue = 0;
            }

            return intValue;
        }

        public static string ToStringFromDB(this object value)
        {
            string stringValue;

            if (value != null && value != DBNull.Value)
            {
                stringValue = Convert.ToString(value);
            }
            else
            {
                stringValue = String.Empty;
            }

            return stringValue;
        }

        public static bool ToBoolFromDB(this object value)
        {
            bool boolVal;

            if (value != null && value != DBNull.Value)
            {
                int test = Convert.ToInt32(value);
                if (test == -1)
                {
                    boolVal = true;
                }
                else
                {
                    boolVal = false;
                }
            }
            else
            {
                boolVal = false;
            }

            return boolVal;
        }

        public static int ToDBFromBool(this bool value)
        {
            int intVal;

            if (value == false)
            {
                intVal = 0;
            }
            else
            {
                intVal = -1;
            }

            return intVal;
        }

        public static string ToDBFromDateTime(this DateTime value)
        {
            string stringValue;

            if (value.GetHashCode() != 0)
            {
                stringValue = value.ToString("yyyy-MM-dd HH:mm:ss");

            }
            else
            {
                stringValue = NullTime.ToString("yyyy-MM-dd HH:mm:ss");
            }

            return stringValue;
        }

        public static long ToDBFromTimeSpan(this TimeSpan value)
        {
            long longValue;

            if (value != null)
            {
                longValue = value.Ticks;
            }
            else
            {
                longValue = 0;
            }

            return longValue;
        }

        public static TimeSpan ToTimeSpanFromBigint(this object value)
        {
            TimeSpan timeSpanValue;

            if (value != null)
            {
                timeSpanValue = TimeSpan.FromTicks(Convert.ToInt64(value));
            }
            else
            {
                timeSpanValue = NullTimeSpan;
            }

            return timeSpanValue;
        }
    }
}
