using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIN.Application
{
    public static class EnumData
    {
        public static List<CustomSelectListItem> GetPayCodeTypeEnum()
        {
            return Enum.GetValues(typeof(PayCodeTypeEnum))
                 .Cast<PayCodeTypeEnum>()
                 .Select(v => new CustomSelectListItem { Text = v.ToString(), Value = ((int)v).ToString() })
                 .ToList();
        }

        #region JobPlan Child Frequency

        public static List<LanCustomSelectListItem> GetJobPlanFrequecies()
        {
            return new(){
                new(){ Text = "Annual",TextAr="Annual", Value = "Annual" },
                new(){ Text = "Semi Annual",TextAr="Semi Annual", Value = "SemiAnnual" },
                new(){ Text = "Quarterly",TextAr="Quarterly", Value = "Quarterly" },
                new(){ Text = "Monthly",TextAr="Monthly", Value = "Monthly" },
                new(){ Text = "Weekly",TextAr="Weekly", Value = "Weekly" },
                //new(){ Text = "Day",TextAr="Day", Value = "Day" },
                };
        }

        #endregion
    }
}
