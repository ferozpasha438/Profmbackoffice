using CIN.Application.FomMgtDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LS.API.FomMobB2C.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpPost("calculateDatesForFrequencySelected")]
        public List<CalculateDatesForFrequencySelectedDto> calculateDatesForFrequencySelected([FromBody] CalculateDatesForFrequencySelectedDto input)
        {
            var selectedDate = input.PlanStartDate;
            var selectedDay = input.PlanStartDate.Day;
            var frequency = input.Frequency;

            switch (frequency)
            {
                case "Annual":
                    return CheckDates(new() { new() { PlanStartDate = selectedDate } }, selectedDate);
                case "SemiAnnual":
                    return CheckDates(new() { new() { PlanStartDate = selectedDate }, new() { PlanStartDate = selectedDate.AddMonths(6) } }, selectedDate);
                case "Quarterly":

                    DateTime f1 = selectedDate, f2 = f1.AddMonths(3), f3 = f2.AddMonths(3), f4 = f3.AddMonths(3);
                    return CheckDates(new() {
                        new() { PlanStartDate = f1 }, new() { PlanStartDate = f2 },
                        new() { PlanStartDate = f3 }, new() { PlanStartDate = f4 }
                    }, selectedDate);

                case "Monthly":
                    return CheckDates(GenerateMonthlyDatesForOneYear(selectedDate), selectedDate);
                case "Weekly":
                    return GenerateWeeklyDatesForOneYear(selectedDate);
                //case 'Day':
                //    return new Array(365);
                //    break;
                default:
                    return null;
            }

        }
        private List<CalculateDatesForFrequencySelectedDto> CheckDates(List<CalculateDatesForFrequencySelectedDto> input, DateTime selectedDate)
        {
            var selectedDay = selectedDate.Day;
            foreach (var item in input)
            {
                var planStartDate = item.PlanStartDate;
                if (selectedDay == 31)
                {
                    item.PlanStartDate = new DateTime(planStartDate.Year, planStartDate.Month,
                                                      DateTime.DaysInMonth(planStartDate.Year, planStartDate.Month));
                }
                else if (selectedDay == 30)
                {
                    if (planStartDate.Month == 2)
                        item.PlanStartDate = new DateTime(planStartDate.Year, planStartDate.Month, 28);
                    else
                        item.PlanStartDate = new DateTime(planStartDate.Year, planStartDate.Month, 30);
                }

            }
            return input;
        }

        // Method to generate weekly dates starting from 2024-10-22 for one year
        static List<CalculateDatesForFrequencySelectedDto> GenerateMonthlyDatesForOneYear(DateTime planDate)
        {
            DateTime startDate = planDate;
            DateTime endDate = startDate.AddYears(1); // End one year later
            List<CalculateDatesForFrequencySelectedDto> dates = new();
            for (int i = 0; i < 12; i++)
            {
                DateTime today = startDate.AddMonths(i);
                DateTime endOfMonth = new DateTime(today.Year, today.Month,
                                                   DateTime.DaysInMonth(today.Year, today.Month));
                dates.Add(new() { PlanStartDate = endOfMonth });
            }
            return dates;

            //while (startDate < endDate)
            //{
            //    yield return startDate;

            //    DateTime today = startDate.AddMonths(1);
            //    DateTime endOfMonth = new DateTime(today.Year,
            //                                       today.Month,
            //                                       DateTime.DaysInMonth(today.Year,
            //                                                            today.Month));

            //    //var totalDays = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            //    // Console.WriteLine("endOfMonth  " + endOfMonth.ToString("yyyy-MM-dd"));
            //    startDate = startDate.AddDays(DateTime.DaysInMonth(endOfMonth.Year, endOfMonth.Month)); // Move to the next week
            //}


        }

        static List<CalculateDatesForFrequencySelectedDto> GenerateWeeklyDatesForOneYear(DateTime planDate)
        {
            DateTime startDate = planDate;
            DateTime endDate = startDate.AddYears(1); // End one year later
            List<CalculateDatesForFrequencySelectedDto> dates = new();
            int seq = 0;
            while (startDate < endDate)
            {
                seq = seq + 1;
                //yield return startDate;
                //DateTime endOfMonth = new DateTime(startDate.Year, startDate.Month,
                //                                  DateTime.DaysInMonth(startDate.Year, startDate.Month));
                dates.Add(new() { PlanStartDate = startDate, Seq = seq });
                startDate = startDate.AddDays(7); // Move to the next week
            }

            return dates;

            //while (startDate < endDate)
            //{
            //    yield return startDate;

            //    DateTime today = startDate.AddMonths(1);
            //    DateTime endOfMonth = new DateTime(today.Year,
            //                                       today.Month,
            //                                       DateTime.DaysInMonth(today.Year,
            //                                                            today.Month));

            //    //var totalDays = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            //    // Console.WriteLine("endOfMonth  " + endOfMonth.ToString("yyyy-MM-dd"));
            //    startDate = startDate.AddDays(DateTime.DaysInMonth(endOfMonth.Year, endOfMonth.Month)); // Move to the next week
            //}


        }
        ////private void TotalDays1()
        ////{
        ////    DateTime startDate = new DateTime(2024, 10, 31);
        ////    DateTime endDate = startDate.AddYears(1); // End one year later

        ////    while (startDate < endDate)
        ////    {
        ////        yield return startDate;
        ////        DateTime today = startDate.AddMonths(1);
        ////        DateTime endOfMonth = new DateTime(today.Year,
        ////                                           today.Month,
        ////                                           DateTime.DaysInMonth(today.Year,
        ////                                                                today.Month));

        ////        //var totalDays = DateTime.DaysInMonth(startDate.Year, startDate.Month);

        ////        startDate = startDate.AddDays(DateTime.DaysInMonth(endOfMonth.Year, endOfMonth.Month)); // Move to the next week
        ////    }
        ////}

        ////private void TotalDays()
        ////{

        ////    DateTime startDate = new DateTime(2024, 10, 31);
        ////    DateTime endDate = startDate.AddYears(1); // End one year later

        ////    while (startDate < endDate)
        ////    {
        ////        yield return startDate;
        ////        var totalDays = DateTime.DaysInMonth(startDate.Year, startDate.Month);
        ////        startDate = startDate.AddDays(totalDays); // Move to the next week
        ////    }


        ////    DateTime today1 = DateTime.Today;
        ////    DateTime today = today1.AddMonths(9);
        ////    DateTime endOfMonth = new DateTime(today.Year,
        ////                                       today.Month,
        ////                                       DateTime.DaysInMonth(today.Year,
        ////                                                            today.Month));
        ////    Console.WriteLine(endOfMonth.ToString("yyyy-MM-dd"));
        ////}
    }
}
