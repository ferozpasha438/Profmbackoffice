﻿using CIN.Application;
using CIN.Application.Common;
using CIN.Application.FomMgtDtos;
using CIN.Application.FomMgtQuery.ProfmQuery;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.API.FomMobB2C.Controllers
{
    public class AssetMaintenanceController : BaseController
    {
        private readonly IWebHostEnvironment _env;
        public AssetMaintenanceController(IOptions<AppSettingsJson> appSettings, IWebHostEnvironment env) : base(appSettings)
        {
            _env = env;
        }


        #region Asset Mangement



        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
        {
            var list = await Mediator.Send(new GetFomAssetMasterList() { Input = filter.Values(), User = UserInfo() });
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var astMaster = await Mediator.Send(new GetFomAssetMasterById() { Id = id, User = UserInfo() });
            return Ok(astMaster);
        }
        [HttpGet("getFomAssetMasterTaskByAsset")]
        public async Task<IActionResult> GetFomAssetMasterTaskByAsset([FromQuery] string assetCode)
        {
            var astMaster = await Mediator.Send(new GetFomAssetMasterTaskByAsset() { AssetCode = assetCode, User = UserInfo() });
            return Ok(astMaster);
        }

        [HttpGet("GetFomAssetMasterByAssetCode")]
        public async Task<IActionResult> GetFomAssetMasterByAssetCode([FromQuery] string assetCode)
        {
            var astMster = await Mediator.Send(new GetFomAssetMasterByAssetCode() { AssetCode = assetCode, User = UserInfo() });
            return Ok(astMster);
        }

        [HttpGet("getFomAssetMasterChildsByAssetCode")]
        public async Task<IActionResult> GetFomAssetMasterChildsByAssetCode([FromQuery] string assetCode, [FromQuery] int id)
        {
            var astMster = await Mediator.Send(new GetFomAssetMasterChildsByAssetCode() { AssetCode = assetCode, Id = id, User = UserInfo() });
            return Ok(astMster);
        }

        [HttpGet("getFomAssetMasterSelectList")]
        public async Task<IActionResult> GetFomAssetMasterSelectList()
        {
            var astMster = await Mediator.Send(new GetFomAssetMasterSelectList() { User = UserInfo() });
            return Ok(astMster);
        }

        [HttpPost("createUpdateFomAssetMaster")]
        public async Task<ActionResult> CreateUpdateFomAssetMaster([FromBody] TblErpFomAssetMasterDto input)
        {
            var res = await Mediator.Send(new CreateUpdateFomAssetMaster() { Input = input, User = UserInfo() });
            var msg = new ApiMessageDto { Message = res.Message };

            if (res.Id > 0)
                return input.Id > 0 ? NoContent() : Created($"get/{input.Id}", input);
            else
                return BadRequest(msg);
        }

        [HttpPost("importExcelFomAssetMaster")]
        public async Task<ActionResult> ImportExcelFomAssetMaster(IFormFile file)
        {
            AppCtrollerDto result = new();

            if (file == null || file.Length == 0)
                return BadRequest(result.Message = "FileNotSelected");

            string fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
                return BadRequest(new ApiMessageDto
                {
                    Message = "Invalid File"
                });

            var guid = Guid.NewGuid().ToString();
            guid = $"{guid}_{fileExtension}";
            var webRoot = $"{_env.ContentRootPath}/files/uploadedastmaster";

            //var webRoot = $"{_env.ContentRootPath}/files/uploadedastmaster";
            //var fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.Now.ToString("ddMMyyyyHHmmssfff", CultureInfo.InvariantCulture)}{Path.GetExtension(file.FileName)}";

            //Create directory if not exists.
            if (!Directory.Exists(webRoot))
                Directory.CreateDirectory(webRoot);

            var filePath = Path.Combine(webRoot, guid);
            //var filePath = Path.Combine(webRoot, fileName);
            var fileLocation = new FileInfo(filePath);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            try
            {
                if (fileExtension.ToUpper() == ".XLSX")
                {
                    using (ExcelPackage package = new ExcelPackage(fileLocation))
                    {
                        //ExcelWorksheet workSheet = package.Workbook.Worksheets["Table1"];
                        var workSheet = package.Workbook.Worksheets.First();
                        int totalRows = workSheet.Dimension.Rows;

                        var assetMasterList = new List<TblErpFomAssetMasterDto>();
                        var assetMasterChilds = new List<TblErpFomAssetMasterChildDto>();

                        for (int i = 2; i <= totalRows; i++)
                        {
                            if (workSheet.Cells[i, 1].Value != null || workSheet.Cells[i, 2].Value != null)
                            {
                                string assetCode = Convert.ToString(workSheet.Cells[i, 1].Value);

                                if (assetCode.HasValue())
                                {
                                    var asstMasterDto = new TblErpFomAssetMasterDto();
                                    asstMasterDto.AssetCode = assetCode;
                                    asstMasterDto.Name = Convert.ToString(Convert.ToString(workSheet.Cells[i, 2].Value));
                                    asstMasterDto.NameAr = Convert.ToString(Convert.ToString(workSheet.Cells[i, 3].Value));


                                    asstMasterDto.Classification = Convert.ToString(workSheet.Cells[i, 6].Value);

                                    StringBuilder dataDes = new();
                                    //dataDes.AppendLine($"Manufacturer : {Convert.ToString(workSheet.Cells[i, 4].Value)}");
                                    dataDes.AppendLine($"Manufacturer : {Convert.ToString(workSheet.Cells[i, 7].Value)}");
                                    dataDes.AppendLine($"Country of Origin : {Convert.ToString(workSheet.Cells[i, 8].Value)}");
                                    dataDes.AppendLine($"Model Number : {Convert.ToString(workSheet.Cells[i, 9].Value)}");
                                    dataDes.AppendLine($"Serial Number : {Convert.ToString(workSheet.Cells[i, 10].Value)}");
                                    asstMasterDto.Description = dataDes.ToString();

                                    asstMasterDto.RouteGroup = Convert.ToString(workSheet.Cells[i, 11].Value);
                                    asstMasterDto.JobPlan = string.Empty;
                                    asstMasterDto.SectionCode = Convert.ToString(workSheet.Cells[i, 14].Value);
                                    asstMasterDto.DeptCode = Convert.ToString(workSheet.Cells[i, 15].Value);
                                    asstMasterDto.ContractCode = Convert.ToString(workSheet.Cells[i, 17].Value);
                                    asstMasterDto.Location = Convert.ToString(workSheet.Cells[i, 18].Value);
                                    asstMasterDto.IsActive = Convert.ToString(workSheet.Cells[i, 19].Value).ToLower() == "active" ? true : false;
                                    asstMasterDto.IsWrittenOff = false;
                                    asstMasterDto.AssetScale = 0;
                                    assetMasterList.Add(asstMasterDto);
                                }
                                else
                                {
                                    var asstMasterChildDto = new TblErpFomAssetMasterChildDto
                                    {
                                        ChildCode = Convert.ToString(Convert.ToString(workSheet.Cells[i, 2].Value)),
                                        Name = Convert.ToString(Convert.ToString(workSheet.Cells[i, 3].Value)),
                                        AssetCode = Convert.ToString(Convert.ToString(workSheet.Cells[i, 4].Value)),
                                    };

                                    assetMasterChilds.Add(asstMasterChildDto);
                                }
                            }
                        }

                        if (assetMasterChilds.Count > 0)
                        {
                            foreach (var astMaster in assetMasterList)
                            {
                                astMaster.AssetChilds = assetMasterChilds.Where(e => e.AssetCode == astMaster.AssetCode).ToList();
                                astMaster.HasChild = astMaster.AssetChilds.Any();
                                astMaster.JobPlan = string.Empty;
                                astMaster.IsWrittenOff = false;
                                astMaster.AssetScale = 0;

                            }
                        }

                        if (assetMasterList.Count() > 0)
                            result = await Mediator.Send(new ImportExcelFomAssetMaster() { Input = assetMasterList, User = UserInfo() });
                        //result = await Mediator.Send(new BulkImportExcelFomAssetMaster() { Input = assetMasterList, User = UserInfo() });

                        FileInfo fi = new FileInfo(filePath);
                        fi.Delete();

                        if (result.Id == assetMasterList.Count())
                        {
                            result.Message = "Successfully imported data";
                            return Ok(result);
                        }
                        else if (result.Id > 0 && result.Id < assetMasterList.Count())
                        {
                            result.Message = "Partially Successful";
                            return Ok(result);
                        }
                        else
                            return BadRequest(new ApiMessageDto
                            {
                                Message = result.Message
                            });
                    }

                }
                else if (fileExtension.ToUpper() == ".XLS")
                {
                    static List<T> DataTableToList<T>(DataTable table) where T : new()
                    {
                        List<T> list = new List<T>();

                        foreach (DataRow row in table.Rows)
                        {
                            T obj = new T();

                            foreach (var prop in typeof(T).GetProperties())
                            {
                                if (table.Columns.Contains(prop.Name) && row[prop.Name] != DBNull.Value)
                                {
                                    prop.SetValue(obj, Convert.ChangeType(row[prop.Name], prop.PropertyType));
                                }
                            }

                            list.Add(obj);
                        }

                        return list;
                    }

                    List<TblErpFomAssetMasterDto> assetMasterList = null;
                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        using (var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration()
                        {
                            FallbackEncoding = System.Text.Encoding.GetEncoding(1252)
                        }))
                        {
                            var conf = new ExcelDataSetConfiguration
                            {
                                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                                {
                                    UseHeaderRow = true
                                }
                            };

                            var dataSet = reader.AsDataSet(conf);
                            var dataTable = dataSet.Tables[0];
                            assetMasterList = DataTableToList<TblErpFomAssetMasterDto>(dataTable);

                        }
                    }

                    if (assetMasterList is not null && assetMasterList.Count() > 0)
                    {
                        StringBuilder dataDes = new();
                        assetMasterList.ForEach(item =>
                        {
                            dataDes.AppendLine($"Manufacturer : {item.Manufacturer}");
                            dataDes.AppendLine($"Country of Origin : {item.CountryofOrigin}");
                            dataDes.AppendLine($"Model Number : {item.ModelNumber}");
                            dataDes.AppendLine($"Serial Number : {item.SerialNumber}");
                            item.Description = dataDes.ToString();
                            item.Location = item.ProjectLocation;
                            item.ContractCode = item.Project;
                            item.IsActive = item.Status.ToLower() == "active" ? true : false;

                            dataDes.Clear();
                        });

                        var assetMasterChilds = assetMasterList.Where(e => !e.AssetCode.HasValue()).Select(e => new TblErpFomAssetMasterChildDto
                        {
                            ChildCode = e.Name,
                            Name = e.NameAr,
                            AssetCode = e.Description
                        }).ToList();

                        assetMasterList = assetMasterList.Where(e => e.AssetCode.HasValue()).ToList();

                        if (assetMasterChilds.Count > 0)
                        {
                            foreach (var astMaster in assetMasterList)
                            {
                                astMaster.AssetChilds = assetMasterChilds.Where(e => e.AssetCode == astMaster.AssetCode).ToList();
                                astMaster.HasChild = astMaster.AssetChilds.Any();
                                astMaster.JobPlan = string.Empty;
                                astMaster.IsWrittenOff = false;
                                astMaster.AssetScale = 0;
                            }
                        }

                        if (assetMasterList.Count() > 0)
                            result = await Mediator.Send(new ImportExcelFomAssetMaster() { Input = assetMasterList, User = UserInfo() });
                        //result = await Mediator.Send(new BulkImportExcelFomAssetMaster() { Input = assetMasterList, User = UserInfo() });

                        FileInfo fi = new FileInfo(filePath);
                        fi.Delete();

                        if (result.Id == assetMasterList.Count())
                        {
                            result.Message = "Successfully imported data";
                            return Ok(result);
                        }
                        else if (result.Id > 0 && result.Id < assetMasterList.Count())
                        {
                            result.Message = "Partially Successful";
                            return Ok(result);
                        }
                        else
                            return BadRequest(new ApiMessageDto
                            {
                                Message = result.Message
                            });
                    }

                }


                return BadRequest(new ApiMessageDto
                {
                    Message = "Invalid File"
                });

                ////static string ReadExcel()
                ////{
                ////    DataTable dtTable = new DataTable();
                ////    List<string> rowList = new List<string>();
                ////    ISheet sheet;
                ////    using (var stream = new FileStream("TestData.xlsx", FileMode.Open))
                ////    {
                ////        stream.Position = 0;
                ////        XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                ////        sheet = xssWorkbook.GetSheetAt(0);
                ////        IRow headerRow = sheet.GetRow(0);
                ////        int cellCount = headerRow.LastCellNum;
                ////        for (int j = 0; j < cellCount; j++)
                ////        {
                ////            ICell cell = headerRow.GetCell(j);
                ////            if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                ////            {
                ////                dtTable.Columns.Add(cell.ToString());
                ////            }
                ////        }
                ////        for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                ////        {
                ////            IRow row = sheet.GetRow(i);
                ////            if (row == null) continue;
                ////            if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                ////            for (int j = row.FirstCellNum; j < cellCount; j++)
                ////            {
                ////                if (row.GetCell(j) != null)
                ////                {
                ////                    if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) && !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
                ////                    {
                ////                        rowList.Add(row.GetCell(j).ToString());
                ////                    }
                ////                }
                ////            }
                ////            if (rowList.Count > 0)
                ////                dtTable.Rows.Add(rowList.ToArray());
                ////            rowList.Clear();
                ////        }
                ////    }
                ////    return JsonConvert.SerializeObject(dtTable);
                ////}


            }
            catch (Exception ex)
            {

                return BadRequest(new ApiMessageDto
                {
                    Message = "Can not Process File"
                });
            }

        }

        [HttpDelete("deleteAssetMaster/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var BranchId = await Mediator.Send(new DeleteAssetMaster() { Id = id, User = UserInfo() });
            if (BranchId > 0)
                return NoContent();
            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }

        #endregion



        #region JobPlanMaster Or PPM

        [HttpGet("getFomJobPlanMasterList")]
        public async Task<IActionResult> GetFomJobPlanMasterList([FromQuery] PaginationFilterDto filter)
        {
            var list = await Mediator.Send(new GetFomJobPlanMasterList() { Input = filter.Values(), User = UserInfo() });
            return Ok(list);
        }
        [HttpGet("getFomAssetJobPlanOrdersList")]
        public async Task<IActionResult> GetFomAssetJobPlanOrdersList([FromQuery] PaginationFilterDto filter)
        {
            var list = await Mediator.Send(new GetFomAssetJobPlanOrdersList() { Input = filter.Values(), User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("getJobPlanFrequecies")]
        public IActionResult GetJobPlanFrequecies()
        {
            var list = EnumData.GetJobPlanFrequecies();
            return Ok(list);
        }

        [HttpGet("allJobPlanFomCalenderScheduleList")]
        public async Task<IActionResult> AllJobPlanFomCalenderScheduleList([FromQuery] string jobPlanCode)
        {
            var list = await Mediator.Send(new AllJobPlanFomCalenderScheduleList() { JobPlanCode = jobPlanCode, User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("getFomJobPlanMasterById/{id}")]
        public async Task<IActionResult> GetFomJobPlanMasterById([FromRoute] int id)
        {
            var list = await Mediator.Send(new GetFomJobPlanMasterById() { Id = id, User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("getAssetjoborderchilditemsByJob")]
        public async Task<IActionResult> GetAssetjoborderchilditemsByJob([FromQuery] string jobPlanCode, [FromQuery] string status)
        {
            var list = await Mediator.Send(new GetAssetjoborderchilditemsByJob() { JobPlanCode = jobPlanCode, Status = status, User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("getJobPlanNotesByJobCode")]
        public async Task<IActionResult> GetJobPlanNotesByJobCode([FromQuery] string jobPlanCode)
        {
            var list = await Mediator.Send(new GetJobPlanNotesByJobCode() { JobPlanCode = jobPlanCode, User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("getFomJobPlanChildSchedulePrintByJobCode")]
        public async Task<IActionResult> GetFomJobPlanChildSchedulePrintByJobCode([FromQuery] string jobPlanCode)
        {
            var list = await Mediator.Send(new GetFomJobPlanChildSchedulePrintByJobCode() { JobPlanCode = jobPlanCode, User = UserInfo() });
            return Ok(list);
        }

        [HttpPost("createUpdateFomJobPlanMaster")] // For canGenChildSch = true
        public async Task<ActionResult> CreateUpdateFomJobPlanMaster([FromBody] TblErpFomJobPlanMasterDto input)
        {
            var res = await Mediator.Send(new CreateUpdateFomJobPlanMaster() { Input = input, User = UserInfo() });
            var msg = new ApiMessageDto { Message = res.Message };

            if (res.Id > 0)
                return input.Id > 0 ? NoContent() : Created($"get/{input.Id}", input);
            else
                return BadRequest(msg);
        }

        [HttpPost("createUpdateFomJobPlanMasterNoSchedules")] // For canGenChildSch = false
        public async Task<ActionResult> CreateUpdateFomJobPlanMasterNoSchedules([FromBody] TblErpFomJobPlanMasterDto input)
        {
            var schJobPlanDates = GetListOfJobPlanDates(new() { Frequency = input.Frequency, PlanStartDate = input.PlanStartDate });
            List<TblErpFomJobPlanMasterDateScheduleDto> JobPlanSchedules = new();
            foreach (var item in schJobPlanDates)
            {
                JobPlanSchedules.Add(new()
                {
                    Frequency = input.Frequency,
                    AssetCode = input.AssetCode,
                    ChildCode = string.Empty,
                    Date = item.PlanStartDate,
                    Remarks = string.Empty,
                });
            }
            input.JobPlanSchedules = JobPlanSchedules;
            var res = await Mediator.Send(new CreateUpdateFomJobPlanMasterNoSchedules() { Input = input, User = UserInfo() });
            var msg = new ApiMessageDto { Message = res.Message };

            if (res.Id > 0)
                return input.Id > 0 ? NoContent() : Created($"get/{input.Id}", input);
            else
                return BadRequest(msg);
        }

        [HttpPost("approveJobMaster")]
        public async Task<ActionResult> ApproveJobMaster([FromBody] TblErpFomJobPlanMasterApprovalDto input)
        {
            var res = await Mediator.Send(new ApproveJobMaster() { Input = input, User = UserInfo() });
            var msg = new ApiMessageDto { Message = res.Message };

            if (res.Id > 0)
                return Ok(msg);
            else
                return BadRequest(msg);
        }

        [HttpPost("addJobPlanNotes")]
        public async Task<ActionResult> AddJobPlanNotes([FromBody] TblErpFomJobPlanMessageLogDto input)
        {
            var res = await Mediator.Send(new AddJobPlanNotes() { Input = input, User = UserInfo() });
            var msg = new ApiMessageDto { Message = res.Message };

            if (res.Id > 0)
                return Ok(msg);
            else
                return BadRequest(msg);
        }

        [HttpPost("createAssetClosingInfo")]
        public async Task<ActionResult> CreateAssetClosingInfo()
        {
            List<TblErpSysFileUploadDto> fileUploads = new();
            var file = HttpContext.Request.Form.Files[0];
            var guid = Guid.NewGuid().ToString();
            string fileName = file.FileName;
            string name = file.Name;
            string description = Convert.ToString(HttpContext.Request.Form[name]);
            description = string.IsNullOrEmpty(description) ? file.FileName : description;
            guid = $"{guid}_{Path.GetExtension(file.FileName)}";
            var webRoot = $"{_env.ContentRootPath}/files/uploadedastmaster";
            var filePath = Path.Combine(webRoot, guid);
            fileUploads.Add(new() { Description = description, FileName = guid, Name = fileName, UploadedBy = String.Empty });
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var module = Convert.ToString(HttpContext.Request.Form["input"]);
            TblErpFomJobPlanScheduleClosureDto input = JsonConvert.DeserializeObject<TblErpFomJobPlanScheduleClosureDto>(module);
            fileUploads.ForEach(file =>
            {
                file.SourceId = input.ChildScheduleId.ToString();
                file.Type = "JOBPPMCLS"; // JOBPPMCLS = Asset_Order_JobPlan
            });

            var fileUpload = await Mediator.Send(new UploadingFile() { Input = fileUploads });

            var res = await Mediator.Send(new CreateAssetClosingInfo() { Input = input, User = UserInfo() });
            var msg = new ApiMessageDto { Message = res.Message };

            if (res.Id > 0)
                return Ok(msg);
            else
                return BadRequest(msg);
        }


        [HttpPost("calculateDatesForFrequencySelected")]
        public async Task<List<CalculateDatesForFrequencySelectedDto>> calculateDatesForFrequencySelected([FromBody] CalculateDatesForFrequencySelectedDto input)
        {
            if (input.Id > 0)
            {
                var schItems = await Mediator.Send(new GetFomJobPlanChildScheduleByJobCode() { Id = input.Id, ChildCode = input.ChildCode });
                if (schItems.Count > 0)
                    return schItems;
                return GetListOfJobPlanDates(input);
            }

            return GetListOfJobPlanDates(input);
        }



        [HttpDelete("deleteJobMaster/{id}")]
        public async Task<ActionResult> DeleteJobMaster([FromRoute] int id)
        {
            var BranchId = await Mediator.Send(new DeleteJobMaster() { Id = id, User = UserInfo() });
            if (BranchId > 0)
                return NoContent();
            return BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }



        #region All Private Methods

        private List<CalculateDatesForFrequencySelectedDto> GetListOfJobPlanDates(CalculateDatesForFrequencySelectedDto input)
        {
            var selectedDate = input.PlanStartDate;
            var selectedDay = input.PlanStartDate.Day;
            var frequency = input.Frequency;

            switch (frequency)
            {
                case "Annual":
                    return CheckDates(new() { new() { PlanStartDate = selectedDate, } }, selectedDate);
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
                else if (selectedDay == 29)
                {
                    bool isLeapYear = DateTime.IsLeapYear(selectedDate.Year);
                    if (isLeapYear && planStartDate.Year == selectedDate.Year)
                        item.PlanStartDate = new DateTime(planStartDate.Year, planStartDate.Month, 29);
                    else
                    if (planStartDate.Month == 2)
                        item.PlanStartDate = new DateTime(planStartDate.Year, planStartDate.Month, 28);
                    else
                        item.PlanStartDate = new DateTime(planStartDate.Year, planStartDate.Month, 29);
                }
                //else if (selectedDay <= 28)
                //{
                //    if (planStartDate.Month == 2)
                //        item.PlanStartDate = new DateTime(planStartDate.Year, planStartDate.Month, 28);
                //}

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
                DateTime month = startDate.AddMonths(i);
                //DateTime endOfMonth = new DateTime(today.Year, today.Month,
                //                                   DateTime.DaysInMonth(today.Year, today.Month));
                dates.Add(new() { PlanStartDate = month });
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

        #endregion



        #endregion



        #region Asset Management Reports




        #endregion

    }
}
