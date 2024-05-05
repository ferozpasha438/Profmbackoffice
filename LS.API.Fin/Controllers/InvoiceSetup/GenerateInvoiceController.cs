using CIN.Application;
using CIN.Application.Common;
using CIN.Application.InvoiceDtos;
using CIN.Application.InvoiceQuery;
using CIN.Application.OperationsMgtQuery;
using CIN.Application.SystemQuery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LS.API.Fin.Controllers
{
    public class GenerateInvoiceController : FileBaseController
    {
        public GenerateInvoiceController(IOptions<AppSettingsJson> appSettings, IWebHostEnvironment env) : base(appSettings, env)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilterDto filter)
        {

            //await Task.Delay(3000);
            var list = await Mediator.Send(new GetArSalesInvoiceList()
            {
                Input = filter.Values(),
                InvoiceStatusId = filter.StatusId ?? (int)InvoiceStatusIdType.Invoice,
                User = UserInfo()
            });
            return Ok(list);
        }

        [HttpGet("getCreditList")]
        public async Task<IActionResult> GetCreditList([FromQuery] PaginationFilterDto filter)
        {
            //await Task.Delay(3000);
            var list = await Mediator.Send(new GetArSalesInvoiceList() { Input = filter.Values(), InvoiceStatusId = (int)InvoiceStatusIdType.Credit, User = UserInfo() });
            return Ok(list);
        }

        [HttpGet("getSingleSaleInvoiceById/{id}")]
        public async Task<IActionResult> GetSingleInvoiceById([FromRoute] int id)
        {
            //await Task.Delay(3000);
            var obj = await Mediator.Send(new GetSingleCreditInvoiceById() { Id = id, InvoiceStatusId = (int)InvoiceStatusIdType.Invoice, User = UserInfo() });
            return Ok(obj);
        }

        [HttpGet("getSingleCreditInvoiceById/{id}")]
        public async Task<IActionResult> GetSingleCreditInvoiceById([FromRoute] int id)
        {
            //await Task.Delay(3000);
            var obj = await Mediator.Send(new GetSingleCreditInvoiceById() { Id = id, InvoiceStatusId = (int)InvoiceStatusIdType.Credit, User = UserInfo() });
            return Ok(obj);
        }

        [HttpGet("getSalesInvoicePrintingList")]
        public async Task<ActionResult> GetSalesInvoicePrintingList([FromQuery] string branchCode, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int? id)
        {
            var result = await Mediator.Send(new GetSalesInvoicePrintingList() { DateFrom = from, DateTo = to, BranchCode = branchCode, Id = id, User = UserInfo() });
            return Ok(result);
        }

        [HttpGet("getSchoolSalesInvoicePrintingList/{id}")]
        public async Task<IActionResult> GetSchoolSalesInvoicePrintingList([FromRoute] int id)
        {
            //await Task.Delay(3000);
            var invoice = await Mediator.Send(new GetSchoolSalesInvoicePrintingList() { Id = id, User = UserInfo() });

            var Company = await Mediator.Send(new GetCompany() { Id = (int)invoice.CompanyId, User = UserInfo() });
            //var serviceDate1 = Convert.ToDateTime(invoice.ServiceDate1, CultureInfo.InvariantCulture);

            // invoice.ServiceDate1 = serviceDate1.ToString("MMM", CultureInfo.InvariantCulture) + " " + serviceDate1.Year.ToString();
            string createdDate = invoice.CreatedOn.Value.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);

            invoice.QRCode = GenerateQRCode(Company.CompanyName.Trim(), Company?.VATNumber.Trim(), invoice.TaxAmount.ToCommaInvarient().Replace(",", "").Trim(), invoice.TotalAmount.ToCommaInvarient().Replace(",", "").Trim(), createdDate.Trim());

            ToWord toWord = new ToWord(invoice.TotalAmount is null ? 0 : (decimal)invoice.TotalAmount, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            invoice.TotalAmountEn = toWord.ConvertToEnglish();
            invoice.TotalAmountAr = toWord.ConvertToArabic();

            return Ok(invoice);
        }

        [HttpGet("getCustomerInvoiceNumberList/{id}")]
        public async Task<IActionResult> GetCustomerInvoiceNumberList([FromRoute] int id, [FromQuery] string search)
        {
            //await Task.Delay(3000);
            var obj = await Mediator.Send(new GetCustomerInvoiceNumberList() { Id = id, Search = search, User = UserInfo() });
            return Ok(obj);
        }

        [HttpGet("getCustomerInvoiceItemsByIdList/{id}")]
        public async Task<IActionResult> GetCustomerInvoiceItemsByIdList([FromRoute] int id)
        {
            //await Task.Delay(3000);
            var obj = await Mediator.Send(new GetCustomerInvoiceItemsByIdList() { Id = id, User = UserInfo() });
            return Ok(obj);
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TblTranInvoiceDto input)
        {
            //input.InvoiceStatusId = (int)InvoiceStatusIdType.Invoice;
            if (input.InvoiceStatusId == 2)
                input.IsCreditConverted = true;

            input.InvoiceModule = "Finance";
            return await CreateInvoice(input);
        }

        [HttpPost("createCredit")]
        public async Task<ActionResult> CreateCredit([FromBody] TblTranInvoiceDto input)
        {
            input.InvoiceStatusId = (int)InvoiceStatusIdType.Credit;
            input.InvoiceModule = "Finance";

            if (input.ItemList is not null)
            {
                input.ItemList.ForEach(item =>
                {
                    item.Quantity = -item.Quantity;
                    item.TaxAmount = -item.TaxAmount;
                    item.TotalAmount = -item.TotalAmount;
                });
            }

            input.SubTotal = -input.SubTotal;
            input.TaxAmount = -input.TaxAmount;
            input.TotalAmount = -input.TotalAmount;

            return await CreateInvoice(input);
        }

        private async Task<ActionResult> CreateInvoice(TblTranInvoiceDto input)
        {
            var invoice = await Mediator.Send(new CreateInvoice() { Input = input, User = UserInfo() });
            if (invoice.Id > 0)
            {
                if (input.Id > 0)
                    return NoContent();
                else
                    return Created($"get/{invoice.Id}", input);
            }
            return BadRequest(new ApiMessageDto { Message = invoice.Message });

        }



        [HttpGet("generateInvoiceReportById/{id}")]
        public async Task<ActionResult> GenerateInvoiceReportById([FromRoute] int id)
        {

            //var Language = System.Globalization.CultureInfo.CurrentCulture.Name;
            // Language = Language == "en-US" ? "en" : Language;

            var invoice = await Mediator.Send(new GetSingleCreditInvoiceById() { Id = id, InvoiceStatusId = (int)InvoiceStatusIdType.Invoice, User = UserInfo() });
            var Company = await Mediator.Send(new GetCompany() { Id = (int)invoice.CompanyId, User = UserInfo() });
            var Customer = await Mediator.Send(new GetCustomerItem() { Id = (int)invoice.CustomerId, SiteCode = invoice.SiteCode, User = UserInfo() });

            if (invoice.CustName.HasValue())
            {
                Customer.CustName = invoice.CustName;
                Customer.CustArbName = invoice.CustArbName;
            }

            var Branch = await Mediator.Send(new GetBranchInfoByBranchCode() { Input = invoice.BranchCode, User = UserInfo() });
            var invoiceItems = invoice.ItemList;


            //InvoiceDTO_AR invoice = (await instance.GetReportByIdAsync(invoiceId)).Data.Data;
            //invoice.Vat = invoice.TaxTariffPercentage;
            //if (Language == "ar")
            //{
            //    invoice.SubTotal_AR = invoice.SubTotal.ToCommaInvarient();
            //    invoice.TaxAmount_AR = invoice.TaxAmount.ToCommaInvarient();
            //    invoice.TotalAmount_AR = invoice.TotalAmount.ToCommaInvarient();
            //  //invoice.Vat_AR = invoice.VatPercentage.ToCommaInvarient();
            //}


            var serviceDate1 = Convert.ToDateTime(invoice.ServiceDate1, CultureInfo.InvariantCulture);

            invoice.ServiceDate1 = serviceDate1.ToString("MMM", CultureInfo.InvariantCulture) + " " + serviceDate1.Year.ToString();
            string createdDate = invoice.CreatedOn.Value.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);

            //string createdDate = invoice.CreatedOn.Value.ToString("MM/dd/yyyy hh:mm", CultureInfo.InvariantCulture);


            invoice.QRCode = GenerateQRCode(Company.CompanyName.Trim(), Company?.VATNumber.Trim(), invoice.TaxAmount.ToCommaInvarient().Replace(",", "").Trim(), invoice.TotalAmount.ToCommaInvarient().Replace(",", "").Trim(), createdDate.Trim());// createdDate.Split(" ")[0].Trim()
            //foreach (var item in invoiceItems)
            //{
            //    item.Quantity_AR = item.Quantity.ToInvarient();
            //    item.UnitPrice_AR = item.UnitPrice.ToInvarient();
            //    item.DiscountAmount_AR = item.DiscountAmount.ToInvarient();
            //    item.TaxTariffPercentage_AR = item.TaxTariffPercentage.ToInvarient();
            //    item.AmountBeforeTax_AR = item.AmountBeforeTax.ToInvarient();
            //    item.TaxAmount_AR = item.UnitPrice.ToInvarient();
            //    item.SubTotal_AR = item.SubTotal.ToInvarient();
            //    item.TotalAmount_AR = item.TotalAmount.ToCommaInvarient();
            //    item.NameEN = item.NameEN;
            //    item.NameAR = item.NameAR;
            //}

            //var Language = System.Globalization.CultureInfo.CurrentCulture.Name;
            //Language = Language == "en-US" ? "en" : Language;
            //if (Language != "en-US")
            //{
            //    foreach (var item in invoiceItems)
            //    {
            //        item
            //    }
            //}
            PrintInvoiceDto printInvoice = new()
            {
                Company = Company,
                Customer = Customer,
                Invoice = invoice,
                InvoiceItems = invoiceItems,
                BankDetails = Branch
            };
            try
            {

                ToWord toWord = new ToWord(invoice.TotalAmount is null ? 0 : (decimal)invoice.TotalAmount, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                printInvoice.TotalAmountEn = toWord.ConvertToEnglish();
                printInvoice.TotalAmountAr = toWord.ConvertToArabic();
                return Ok(printInvoice);

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpPost("createInvoiceApproval")]
        public async Task<ActionResult> CreateInvoiceApproval([FromBody] TblTranInvoiceApprovalDto input)
        {
            var result = await Mediator.Send(new CreateInvoiceApproval() { Input = input, User = UserInfo() });
            return result ? Ok(result) : BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }

        [HttpPost("createSettlePayment")]
        public async Task<ActionResult> CreateSettlePayment([FromBody] TblTranInvoiceSettlementDto input)
        {
            var result = await Mediator.Send(new CreateInvoiceSettlement() { Input = input, User = UserInfo() });
            return result ? Ok(result) : BadRequest(new ApiMessageDto { Message = ApiMessageInfo.Failed });
        }

    }
}
