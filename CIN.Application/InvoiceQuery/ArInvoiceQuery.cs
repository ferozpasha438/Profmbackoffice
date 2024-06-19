﻿using AutoMapper;
using CIN.Application.Common;
using CIN.Application.GeneralLedgerDtos;
using CIN.Application.InvoiceDtos;
using CIN.DB;
using CIN.Domain.GeneralLedger;
using CIN.Domain.GeneralLedger.Distribution;
using CIN.Domain.GeneralLedger.Ledger;
using CIN.Domain.InvoiceSetup;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace CIN.Application.InvoiceQuery
{
    #region GetPagedList

    public class GetArSalesInvoiceList : IRequest<PaginatedList<TblTranInvoiceListDto>>
    {
        public UserIdentityDto User { get; set; }
        public int InvoiceStatusId { get; set; }
        public PaginationFilterDto Input { get; set; }
    }

    public class GetArSalesInvoiceListHandler : IRequestHandler<GetArSalesInvoiceList, PaginatedList<TblTranInvoiceListDto>>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetArSalesInvoiceListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TblTranInvoiceListDto>> Handle(GetArSalesInvoiceList request, CancellationToken cancellationToken)
        {
            try
            {



                Log.Info("----Info GetArSalesInvoiceList method start----");
                var invoices = _context.TranInvoices.AsNoTracking();//.Where(e => e.BranchCode == request.User.BranchCode);
                var companies = _context.Companies.AsNoTracking();
                var customers = _context.OprCustomers.AsNoTracking();
                var custApproval = _context.TrnCustomerApprovals.AsNoTracking();
                var cInvoices = _context.TrnCustomerInvoices.AsNoTracking();
                var optSites = _context.OprSites.AsNoTracking();
                // var users = _context.SystemLogins.Select(e => new { e.Id, e.PrimaryBranch });
                var brnApproval = _context.FinBranchesAuthorities.AsNoTracking();
                bool isArab = request.User.Culture.IsArab();
                //var customers3 = customers.Where(c => EF.Functions.Like(c.NameAR, "%" + request.Input.Query + "%"));                    


                if (request.Input.Id > 0)
                {
                    var invoiceItems = _context.TranInvoiceItems.AsNoTracking();
                    var invoiceIds = invoiceItems.Where(e => e.ProductId == request.Input.Id).Select(e => e.InvoiceId);

                    invoices = invoices.Where(e => invoiceIds.Any(id => id == e.Id));
                }

                bool isDate = DateTime.TryParse(request.Input.Query, out DateTime invoiceDate);
                var reports = from IV in invoices
                              join ST in optSites
                              on IV.SiteCode equals ST.SiteCode
                              into IV_ST
                              from SITE in IV_ST.DefaultIfEmpty()

                              join CM in companies
                              on IV.CompanyId equals CM.Id
                              join CS in customers
                              on IV.CustomerId equals CS.Id


                              //join ST in optSites
                              //on IV.SiteCode equals ST.SiteCode

                              // into IV_LeftList
                              //from CS in IV_LeftList.DefaultIfEmpty()

                              //into AP_Left
                              //from AP in AP_Left.DefaultIfEmpty()
                              //join AP in approvals
                              //on IV.Id equals AP.InvoiceId
                              where //CM.Id == request.User.CompanyId &&
                              IV.InvoiceStatusId == request.InvoiceStatusId &&
                              (
                         CS.CustName.Contains(request.Input.Query) ||
                         SITE.SiteName.Contains(request.Input.Query) ||

                         EF.Functions.Like(CS.CustArbName, "%" + request.Input.Query + "%") ||
                         EF.Functions.Like(SITE.SiteArbName, "%" + request.Input.Query + "%") ||

                         IV.InvoiceNumber.Contains(request.Input.Query) ||
                         IV.SpInvoiceNumber.Contains(request.Input.Query) ||
                         IV.TotalAmount.ToString().Contains(request.Input.Query.Replace(",", "")) ||
                         (isDate && IV.InvoiceDate == invoiceDate)
                              )
                              select new TblTranInvoiceListDto
                              {
                                  Id = IV.Id,
                                  //SpInvoiceNumber = IV.SpInvoiceNumber,
                                  InvoiceNumber = IV.InvoiceNumber == string.Empty ? IV.SpInvoiceNumber : IV.InvoiceNumber,
                                  InvoiceDate = IV.InvoiceDate,
                                  CreatedOn = IV.CreatedOn,
                                  InvoiceDueDate = IV.InvoiceDueDate,
                                  CompanyId = (int)IV.CompanyId,
                                  CustomerId = (long)IV.CustomerId,
                                  SubTotal = (decimal)IV.SubTotal,
                                  DiscountAmount = (decimal)IV.DiscountAmount,
                                  AmountBeforeTax = (decimal)IV.AmountBeforeTax,
                                  ServiceDate1 = IV.ServiceDate1,
                                  TaxAmount = (decimal)IV.TaxAmount,
                                  TotalAmount = (decimal)IV.TotalAmount,
                                  TotalPayment = (decimal)IV.TotalPayment,
                                  AmountDue = (decimal)IV.AmountDue,
                                  VatPercentage = (decimal)IV.VatPercentage,
                                  CompanyName = CM.CompanyName,
                                  BranchName = IV.SysCompanyBranch.BranchName,
                                  CustomerName = isArab ? CS.CustArbName : CS.CustName,
                                  // SiteName = isArab ? optSites.Where(e => e.SiteCode == IV.SiteCode).Select(e => e.SiteArbName).FirstOrDefault() : optSites.Where(e => e.SiteCode == IV.SiteCode).Select(e => e.SiteName).FirstOrDefault(),
                                  SiteName = isArab ? SITE.SiteArbName : SITE.SiteName,
                                  InvoiceRefNumber = IV.InvoiceRefNumber,
                                  LpoContract = IV.LpoContract,
                                  PaymentTermId = IV.SndSalesTermsCode.SalesTermsName,
                                  TaxIdNumber = IV.TaxIdNumber,
                                  InvoiceStatus = IV.IsCreditConverted ? "Credit" : "Invoice",
                                  HasAuthority = brnApproval.Any(e => e.AppAuth == request.User.UserId.ToString() && e.FinBranchCode == IV.BranchCode && e.AppAuthAR),
                                  ApprovedUser = custApproval.Any(e => e.LoginId == request.User.UserId && e.InvoiceId == IV.Id && e.IsApproved),
                                  IsApproved = custApproval.Where(e => e.InvoiceId == IV.Id && e.IsApproved).Any(),
                                  CanSettle = brnApproval.Where(e => e.FinBranchCode == IV.BranchCode && e.AppAuthAR).Select(e => new { AppAuth = e.AppAuth }).GroupBy(e => e.AppAuth).Count() <= custApproval.Where(e => e.InvoiceId == IV.Id && e.IsApproved).Count(),
                                  IsSettled = cInvoices.Where(e => e.InvoiceId == IV.Id && e.IsPaid).Any()
                              };


                if (!string.IsNullOrEmpty(request.Input.Approval))
                {
                    string aprv = request.Input.Approval;

                    reports = aprv switch
                    {
                        "settled" => reports.Where(e => e.IsSettled),
                        "unsettled" => reports.Where(e => !e.IsSettled),
                        "approval" => reports.Where(e => e.IsApproved),
                        "unapproval" => reports.Where(e => !e.IsApproved),
                        _ => reports
                    };
                }

                //.OrderByDescending(t => t.CustomerId)
                // .ProjectTo<TblTranInvoiceDto>(_mapper.ConfigurationProvider)
                var nreports = await reports.OrderBy(request.Input.OrderBy).PaginationListAsync(request.Input.Page, request.Input.PageCount, cancellationToken);
                Log.Info("----Info GetArSalesInvoiceList method Exit----");
                return nreports;
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }

    #endregion


    #region GetSingleCreditInvoiceById

    public class GetSingleCreditInvoiceById : IRequest<TblTranInvoiceDto>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
        public int InvoiceStatusId { get; set; }
    }

    public class GetSingleCreditInvoiceByIdHandler : IRequestHandler<GetSingleCreditInvoiceById, TblTranInvoiceDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetSingleCreditInvoiceByIdHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TblTranInvoiceDto> Handle(GetSingleCreditInvoiceById request, CancellationToken cancellationToken)
        {

            Log.Info("----Info GetSingleCreditInvoiceById method start----");
            var invoice = await _context.TranInvoices.Include(e => e.SysCompanyBranch).ThenInclude(e => e.SysCompany)
                .FirstOrDefaultAsync(e => e.Id == request.Id);
            var custInvoice = await _context.TrnCustomerInvoices.AsNoTracking().FirstOrDefaultAsync(e => e.InvoiceId == invoice.Id);
            bool isArab = request.User.Culture.IsArab();
            var siteMaster = await GetSiteName(_context, invoice.SiteCode);

            var items = await _context.TranInvoiceItems
                .Where(e => e.InvoiceId == request.Id)
                //.Include(e => e.Product)
                //.ThenInclude(e => e.UnitType)
                .ToListAsync();



            var productUnitTypes = _context.TranProducts.Include(e => e.UnitType).AsNoTracking()
                .Select(e => new { e.Id, pNameEN = e.NameEN, uNameEN = e.UnitType.NameEN });


            var itemUnitCodes = _context.InvItemMaster.Include(e => e.InvUoms).AsNoTracking()
             .Select(e => new { e.Id, pNameEN = isArab ? e.ShortNameAr : e.ShortName, uNameEN = e.InvUoms.UOMName });

            //.Where(e=> items.Any(ivt=>ivt.ProductId == e.ProductTypeId)).ToListAsync();
            //var unitTypes = _context.TranUnitTypes.AsNoTracking();

            //var ivItems = await (from iv in items
            //                     join pd in products
            //                     on iv.ProductId equals pd.Id
            //                     into P_Left
            //                     from PL in P_Left.DefaultIfEmpty()
            //                     join ut in unitTypes
            //                     on PL.UnitTypeId equals (int)ut.Id
            //                     select new { iv, PL, ut }).ToListAsync();

            int invoiceStatus = 1;

            if (request.InvoiceStatusId == (int)InvoiceStatusIdType.Credit)
                invoiceStatus = -1;

            TblTranInvoiceDto invoiceDto = new()
            {
                CustomerId = invoice.CustomerId,
                InvoiceDate = invoice.InvoiceDate,
                InvoiceDueDate = invoice.InvoiceDueDate,
                CompanyId = invoice.CompanyId,
                BranchCode = invoice.BranchCode,
                InvoiceRefNumber = invoice.InvoiceRefNumber,
                LpoContract = invoice.LpoContract,
                PaymentTermId = invoice.PaymentTerms,
                InvoiceNumber = invoice.InvoiceNumber,
                ServiceDate1 = invoice.ServiceDate1,
                IsCreditConverted = invoice.IsCreditConverted,
                InvoiceStatusId = invoice.InvoiceStatusId,

                SubTotal = invoice.SubTotal + invoice.DiscountAmount,
                TaxAmount = invoice.TaxAmount,
                TotalAmount = invoice.TotalAmount,
                DiscountAmount = invoice.DiscountAmount,
                PaidAmount = custInvoice?.PaidAmount ?? 0,

                CreatedOn = invoice.CreatedOn,
                TaxIdNumber = invoice.TaxIdNumber,
                InvoiceNotes = invoice.InvoiceNotes,
                Remarks = invoice.Remarks,
                CustName = invoice.CustName,
                CustArbName = invoice.CustArbName,
                CustomerName = invoice.CustName,
                SiteCode = invoice.SiteCode,
                SiteName = siteMaster ?? new(),
                LogoImagePath = invoice.SysCompanyBranch.SysCompany.LogoURL,
            };

            List<TblTranInvoiceItemDto> itemList = new();


            foreach (var item in items)
            {
                try
                {
                    var siteMaster1 = await GetSiteName(_context, item.SiteCode);

                    string pNameEN = string.Empty, uNameEN = string.Empty;

                    if (item.InvoiceType is null)
                    {
                        var punitType = await productUnitTypes.FirstOrDefaultAsync(e => e.Id == item.ProductId);
                        if (punitType is not null)
                        {
                            pNameEN = punitType.pNameEN;
                            uNameEN = punitType.uNameEN;
                        }

                    }
                    else
                    {
                        var itemCode = await itemUnitCodes.FirstOrDefaultAsync(e => e.Id == item.ProductId);
                        if (itemCode is not null)
                        {
                            pNameEN = itemCode.pNameEN;
                            uNameEN = itemCode.uNameEN;
                        }
                    }


                    TblTranInvoiceItemDto itemDto = new()
                    {
                        ProductId = item.ProductId,
                        ProductName = pNameEN,
                        Description = item.Description,
                        Quantity = invoiceStatus * item.Quantity,
                        UnitType = uNameEN,
                        UnitPrice = item.UnitPrice,
                        Discount = item.Discount,
                        DiscountAmount = item.DiscountAmount,
                        SiteCode = item.SiteCode,
                        SiteName = (isArab ? siteMaster1?.TextTwo : siteMaster1?.Text) ?? String.Empty,
                        TaxTariffPercentage = item.TaxTariffPercentage,
                        TaxAmount = invoiceStatus * item.TaxAmount,
                        TotalAmount = invoiceStatus * item.TotalAmount
                    };

                    itemList.Add(itemDto);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            //items.ForEach(async item =>
            //{

            //});

            //items.ForEach(item =>
            //{
            //    TblTranInvoiceItemDto itemDto = new()
            //    {
            //        ProductId = item.iv.ProductId,
            //        ProductName = item.PL.NameEN,
            //        Description = item.iv.Description,
            //        Quantity = invoiceStatus * item.iv.Quantity,
            //        UnitType = item.ut.NameEN,
            //        UnitPrice = item.iv.UnitPrice,
            //        TaxTariffPercentage = item.iv.TaxTariffPercentage,
            //        TaxAmount = invoiceStatus * item.iv.TaxAmount,
            //        TotalAmount = invoiceStatus * item.iv.TotalAmount
            //    };

            //    itemList.Add(itemDto);
            //});


            invoiceDto.ItemList = itemList;

            Log.Info("----Info GetSingleCreditInvoiceById method Exit----");
            return invoiceDto;
        }

        async Task<CustomSelectListItem> GetSiteName(CINDBOneContext _context, string siteCode) =>
             await _context.OprSites.Where(e => e.SiteCode == siteCode)
             .Select(e => new CustomSelectListItem
             {
                 Text = e.SiteName,
                 TextTwo = e.SiteArbName
             }).FirstOrDefaultAsync();


    }

    #endregion


    #region CreateUpdateInvoice

    public class CreateInvoice : UserIdentityDto, IRequest<AppCtrollerDto>
    {
        public UserIdentityDto User { get; set; }
        public TblTranInvoiceDto Input { get; set; }
    }
    public class CreateInvoiceQueryHandler : IRequestHandler<CreateInvoice, AppCtrollerDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateInvoiceQueryHandler(IMapper mapper, CINDBOneContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<AppCtrollerDto> Handle(CreateInvoice request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Log.Info("----Info CreateUpdateInvoice method start----");

                    var obj = request.Input;
                    if (obj.TotalAmount <= 0)
                        return ApiMessageInfo.Status(" Total Amount is >= 0 ", 0);

                    string spInvoiceNumber = $"{(obj.IsCreditConverted ? "C" : "S") + new Random().Next(99, 9999999).ToString()}";

                    //invoiceNumber = await _context.TranInvoices.CountAsync();
                    //invoiceNumber += 1;
                    TblTranInvoice Invoice = new();

                    if (request.Input.Id > 0)
                    {
                        Invoice = await _context.TranInvoices.FirstOrDefaultAsync(e => e.Id == request.Input.Id);
                        spInvoiceNumber = Invoice.SpInvoiceNumber;

                        Invoice.CustomerId = obj.CustomerId;
                        Invoice.InvoiceDate = Convert.ToDateTime(obj.InvoiceDate);
                        Invoice.InvoiceDueDate = Convert.ToDateTime(obj.InvoiceDueDate);
                        Invoice.ServiceDate1 = obj.ServiceDate1;
                        Invoice.CompanyId = obj.CompanyId;
                        Invoice.BranchCode = obj.BranchCode;
                        Invoice.InvoiceRefNumber = obj.InvoiceRefNumber;

                        Invoice.LpoContract = obj.LpoContract;
                        Invoice.PaymentTerms = obj.PaymentTermId;
                        Invoice.TaxIdNumber = obj.TaxIdNumber;
                        Invoice.InvoiceNotes = obj.InvoiceNotes;
                        Invoice.Remarks = obj.Remarks;


                        Invoice.SubTotal = obj.SubTotal;
                        Invoice.DiscountAmount = obj.DiscountAmount ?? 0;
                        Invoice.AmountBeforeTax = obj.AmountBeforeTax ?? 0;
                        Invoice.TaxAmount = obj.TaxAmount;
                        Invoice.TotalAmount = obj.TotalAmount;
                        Invoice.TotalPayment = obj.TotalPayment ?? 0;
                        Invoice.AmountDue = obj.AmountDue ?? 0;
                        Invoice.SiteCode = obj.SiteCode;


                        if (obj.CustName.HasValue())
                        {
                            Invoice.CustName = obj.CustName;
                            Invoice.CustArbName = obj.CustArbName;
                        }

                        var items = _context.TranInvoiceItems.Where(e => e.InvoiceId == request.Input.Id);
                        _context.RemoveRange(items);
                        _context.TranInvoices.Update(Invoice);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        if (obj.TotalAmount is null)
                            return ApiMessageInfo.Status(0);

                        Invoice = new()
                        {
                            SpInvoiceNumber = spInvoiceNumber,
                            InvoiceNumber = string.Empty,
                            InvoiceDate = Convert.ToDateTime(obj.InvoiceDate),
                            InvoiceDueDate = Convert.ToDateTime(obj.InvoiceDueDate),
                            CompanyId = obj.CompanyId,


                            SubTotal = obj.SubTotal,
                            DiscountAmount = obj.DiscountAmount ?? 0,
                            AmountBeforeTax = obj.AmountBeforeTax ?? 0,
                            TaxAmount = obj.TaxAmount,
                            TotalAmount = obj.TotalAmount,
                            TotalPayment = obj.TotalPayment ?? 0,
                            AmountDue = obj.AmountDue ?? 0,


                            IsDefaultConfig = true,
                            CreatedOn = Convert.ToDateTime(obj.InvoiceDate),
                            CreatedBy = request.User.UserId,
                            CustomerId = obj.CustomerId,
                            InvoiceStatus = "Open",
                            TaxIdNumber = obj.TaxIdNumber,
                            InvoiceModule = obj.InvoiceModule,
                            InvoiceNotes = obj.InvoiceNotes,
                            Remarks = obj.Remarks,
                            InvoiceRefNumber = obj.InvoiceRefNumber,
                            LpoContract = obj.LpoContract,
                            VatPercentage = obj.VatPercentage ?? 0,
                            PaymentTerms = obj.PaymentTermId,
                            BranchCode = obj.BranchCode,
                            ServiceDate1 = obj.ServiceDate1,
                            IsCreditConverted = obj.IsCreditConverted,
                            InvoiceStatusId = obj.InvoiceStatusId,
                            CustName = obj.CustName.HasValue() ? obj.CustName : String.Empty,
                            CustArbName = obj.CustArbName.HasValue() ? obj.CustArbName : String.Empty,
                            SiteCode = obj.SiteCode
                        };


                        await _context.TranInvoices.AddAsync(Invoice);
                        await _context.SaveChangesAsync();
                    }
                    //var company = await _context.Companies.FirstOrDefaultAsync(e => e.Id == request.CompanyId);
                    //if (company is not null)
                    //{
                    //    company.InvoiceSeq++;
                    //    await _context.SaveChangesAsync();
                    //}

                    Log.Info("----Info CreateUpdateInvoice method Exit----");

                    var invoiceId = Invoice.Id;
                    var invoiceItems = request.Input.ItemList;
                    if (invoiceItems.Count > 0)
                    {
                        List<TblTranInvoiceItem> invoiceItemsList = new();

                        foreach (var obj1 in invoiceItems)
                        {
                            var InvoiceItem = new TblTranInvoiceItem
                            {
                                InvoiceId = invoiceId,
                                InvoiceNumber = spInvoiceNumber,
                                // InvoiceType = obj1.InvoiceType,
                                ProductId = obj1.ProductId,
                                Quantity = obj1.Quantity,
                                UnitPrice = obj1.UnitPrice,
                                SubTotal = obj1.SubTotal,
                                DiscountAmount = obj1.DiscountAmount,
                                AmountBeforeTax = obj1.AmountBeforeTax,
                                TaxAmount = obj1.TaxAmount,
                                TotalAmount = obj1.TotalAmount,
                                IsDefaultConfig = true,
                                CreatedOn = obj.InvoiceDate,
                                CreatedBy = (int)request.UserId,
                                Description = obj1.Description,
                                TaxTariffPercentage = obj1.TaxTariffPercentage,
                                Discount = obj1.Discount,
                                SiteCode = obj1.SiteCode

                            };
                            invoiceItemsList.Add(InvoiceItem);
                        }
                        if (invoiceItemsList.Count > 0)
                        {
                            await _context.TranInvoiceItems.AddRangeAsync(invoiceItemsList);
                            await _context.SaveChangesAsync();
                        }
                    }


                    //updating Customer
                    //var customer = await _context.OprCustomers.FirstOrDefaultAsync(e => e.Id == obj.CustomerId);
                    //if (customer is not null && customer.Id > 0 && obj.CustName.HasValue() && obj.CustArbName.HasValue())
                    //{
                    //    customer.CustName = obj.CustName;
                    //    customer.CustArbName = obj.CustArbName;
                    //    _context.OprCustomers.Update(customer);
                    //    await _context.SaveChangesAsync();
                    //}

                    await transaction.CommitAsync();

                    return ApiMessageInfo.Status(1, invoiceId);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error("Error in CreateUpdateInvoice Method");
                    Log.Error("Error occured time : " + DateTime.UtcNow);
                    Log.Error("Error message : " + ex.Message);
                    Log.Error("Error StackTrace : " + ex.StackTrace);
                    return ApiMessageInfo.Status(0);
                }
            }
        }
    }

    #endregion

    #region CreateInvoiceApproval
    public class CreateInvoiceApproval : UserIdentityDto, IRequest<bool>
    {
        public UserIdentityDto User { get; set; }
        public TblTranInvoiceApprovalDto Input { get; set; }
    }
    public class CreateInvoiceApprovalQueryHandler : IRequestHandler<CreateInvoiceApproval, bool>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateInvoiceApprovalQueryHandler(IMapper mapper, CINDBOneContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<bool> Handle(CreateInvoiceApproval request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Log.Info("----Info CreateInvoiceApproval method start----");

                    var obj = await _context.TranInvoices.FirstOrDefaultAsync(e => e.Id == request.Input.Id);
                    var customer = await _context.OprCustomers.FirstOrDefaultAsync(e => e.Id == obj.CustomerId);

                    if (await _context.TrnCustomerApprovals.AnyAsync(e => e.InvoiceId == request.Input.Id && e.LoginId == request.User.UserId && e.IsApproved))
                        return true;

                    TblFinTrnCustomerApproval approval = new()
                    {
                        CompanyId = (int)obj.CompanyId,
                        BranchCode = obj.BranchCode,
                        TranDate = DateTime.Now,
                        TranSource = request.Input.TranSource,
                        Trantype = request.Input.Trantype,
                        CustCode = customer.CustCode,
                        DocNum = "DocNum",
                        LoginId = request.User.UserId,
                        AppRemarks = request.Input.AppRemarks,
                        InvoiceId = request.Input.Id,
                        IsApproved = true,
                    };

                    await _context.TrnCustomerApprovals.AddAsync(approval);
                    await _context.SaveChangesAsync();


                    if (!obj.InvoiceNumber.HasValue())
                    {
                        int invoiceSeq = 0;
                        var invSeq = await _context.Sequences.FirstOrDefaultAsync();
                        if (invSeq is null)
                        {
                            invoiceSeq = 1;
                            TblSequenceNumberSetting setting = new();
                            if (obj.IsCreditConverted)
                                setting.InvCredSeq = invoiceSeq;
                            else
                                setting.InvoiceSeq = invoiceSeq;

                            await _context.Sequences.AddAsync(setting);
                        }
                        else
                        {
                            if (obj.IsCreditConverted)
                            {
                                invoiceSeq = invSeq.InvCredSeq + 1;
                                invSeq.InvCredSeq = invoiceSeq;
                            }
                            else
                            {
                                invoiceSeq = invSeq.InvoiceSeq + 1;
                                invSeq.InvoiceSeq = invoiceSeq;
                            }
                            _context.Sequences.Update(invSeq);
                        }
                        await _context.SaveChangesAsync();

                        obj.InvoiceNumber = obj.IsCreditConverted ? $"C{invoiceSeq}" : invoiceSeq.ToString();

                        obj.SpInvoiceNumber = string.Empty;
                        _context.TranInvoices.Update(obj);
                        await _context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error("Error in CreateInvoiceApproval Method");
                    Log.Error("Error occured time : " + DateTime.UtcNow);
                    Log.Error("Error message : " + ex.Message);
                    Log.Error("Error StackTrace : " + ex.StackTrace);
                    return false;
                }
            }
        }
    }
    #endregion


    #region CreateInvoiceSettlement
    public class CreateInvoiceSettlement : UserIdentityDto, IRequest<bool>
    {
        public UserIdentityDto User { get; set; }
        public TblTranInvoiceSettlementDto Input { get; set; }
    }
    public class CreateInvoiceSettlementQueryHandler : IRequestHandler<CreateInvoiceSettlement, bool>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public CreateInvoiceSettlementQueryHandler(IMapper mapper, CINDBOneContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> Handle(CreateInvoiceSettlement request, CancellationToken cancellationToken)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    Log.Info("----Info CreateInvoiceSettlement method start----");

                    var input = request.Input;
                    var postedOn = input.CreatedOn ?? DateTime.Now;
                    var createdOn = DateTime.Now;

                    var obj = await _context.TranInvoices.FirstOrDefaultAsync(e => e.Id == input.Id);
                    var customer = await _context.OprCustomers.FirstOrDefaultAsync(e => e.Id == obj.CustomerId);
                    var paymentTerms = await _context.SndSalesTermsCodes.FirstOrDefaultAsync(e => e.SalesTermsCode == obj.PaymentTerms);

                    if (await _context.TrnCustomerInvoices.AnyAsync(e => e.InvoiceId == input.Id && e.LoginId == request.User.UserId && e.IsPaid))
                        return true;


                    if (!obj.InvoiceNumber.HasValue())
                    {
                        int invoiceSeq = 0;
                        TblSequenceNumberSetting setting = await _context.Sequences.FirstOrDefaultAsync();
                        if (setting is null)
                        {
                            invoiceSeq = 1;
                            setting = new();
                            if (obj.IsCreditConverted)
                                setting.InvCredSeq = invoiceSeq;
                            else
                                setting.InvoiceSeq = invoiceSeq;

                            await _context.Sequences.AddAsync(setting);
                        }
                        else
                        {
                            if (obj.IsCreditConverted)
                            {
                                invoiceSeq = setting.InvCredSeq + 1;
                                setting.InvCredSeq = invoiceSeq;
                            }
                            else
                            {
                                invoiceSeq = setting.InvoiceSeq + 1;
                                setting.InvoiceSeq = invoiceSeq;

                            }
                            _context.Sequences.Update(setting);
                        }

                        obj.InvoiceNumber = obj.IsCreditConverted ? $"C{invoiceSeq}" : invoiceSeq.ToString();
                        obj.SpInvoiceNumber = string.Empty;

                    }

                    obj.InvoiceStatus = "Closed";
                    _context.TranInvoices.Update(obj);

                    await _context.SaveChangesAsync();

                    obj.TotalAmount = obj.TotalAmount ?? 0;

                    TblFinTrnCustomerInvoice cInvoice = new()
                    {
                        CompanyId = (int)obj.CompanyId,
                        BranchCode = obj.BranchCode,
                        InvoiceNumber = obj.InvoiceNumber,// invoiceSeq.ToString(),
                        InvoiceDate = obj.InvoiceDate,
                        CreditDays = paymentTerms.SalesTermsDueDays,
                        DueDate = obj.InvoiceDueDate,
                        TranSource = input.TranSource,
                        Trantype = obj.IsCreditConverted ? "Credit" : "Invoice",
                        CustCode = customer.CustCode,
                        DocNum = obj.InvoiceRefNumber,
                        LoginId = request.User.UserId,
                        ReferenceNumber = obj.InvoiceRefNumber,
                        InvoiceAmount = obj.TotalAmount,
                        DiscountAmount = obj.DiscountAmount ?? 0,
                        NetAmount = obj.TotalAmount,
                        PaidAmount = Utility.IsNotCreditPay(request.Input.PaymentType) ? obj.TotalAmount : 0,
                        AppliedAmount = 0,
                        Remarks1 = input.Remarks1,
                        Remarks2 = input.Remarks2,
                        InvoiceId = input.Id,
                        IsPaid = true,
                    };
                    cInvoice.BalanceAmount = cInvoice.NetAmount - cInvoice.PaidAmount;
                    await _context.TrnCustomerInvoices.AddAsync(cInvoice);

                    TblFinTrnCustomerStatement cStatement = new()
                    {
                        CompanyId = (int)obj.CompanyId,
                        BranchCode = obj.BranchCode,
                        TranDate = postedOn,
                        TranSource = input.TranSource,
                        Trantype = obj.IsCreditConverted ? "Credit" : "Invoice",
                        TranNumber = obj.InvoiceNumber,// invoiceSeq.ToString(),
                        CustCode = customer.CustCode,
                        DocNum = "DocNum",
                        ReferenceNumber = obj.InvoiceRefNumber,
                        PaymentType = input.PaymentType,
                        PamentCode = "paycode",
                        CheckNumber = input.CheckNumber,
                        Remarks1 = input.Remarks1,
                        Remarks2 = input.Remarks2,
                        LoginId = request.User.UserId,
                        DrAmount = !obj.IsCreditConverted ? obj.TotalAmount : 0,
                        CrAmount = obj.IsCreditConverted ? obj.TotalAmount : 0,
                        //DrAmount = obj.TotalAmount,
                        //CrAmount = 0,
                        InvoiceId = input.Id,
                        SiteCode = obj.SiteCode
                    };
                    await _context.TrnCustomerStatements.AddAsync(cStatement);


                    if (IsNotCreditPay(request.Input.PaymentType))
                    {
                        TblFinTrnCustomerStatement cPaymentStatement = new()
                        {
                            CompanyId = (int)obj.CompanyId,
                            BranchCode = obj.BranchCode,
                            TranDate = postedOn,
                            TranSource = input.TranSource,
                            Trantype = "Payment",
                            TranNumber = obj.InvoiceNumber,// invoiceSeq.ToString(),
                            CustCode = customer.CustCode,
                            DocNum = "DocNum",
                            ReferenceNumber = obj.InvoiceRefNumber,
                            PaymentType = input.PaymentType,
                            PamentCode = "Paycode",
                            CheckNumber = input.CheckNumber,
                            Remarks1 = input.Remarks1,
                            Remarks2 = input.Remarks2,
                            LoginId = request.User.UserId,
                            DrAmount = 0,
                            CrAmount = obj.TotalAmount,
                            InvoiceId = input.Id,
                            SiteCode = obj.SiteCode
                        };
                        await _context.TrnCustomerStatements.AddAsync(cPaymentStatement);
                    }

                    //obj.InvoiceNumber = invoiceSeq.ToString();
                    //obj.SpInvoiceNumber = string.Empty;
                    //_context.TranInvoices.Update(obj);

                    string accountCode = string.Empty;
                    if (IsNotCreditPay(request.Input.PaymentType))
                    {
                        var payCode = await _context.FinAccountlPaycodes.FirstOrDefaultAsync(e => e.FinPayCode == input.PayCode);
                        accountCode = payCode.FinPayAcIntgrAC;
                    }

                    var customerMst = await _context.OprCustomers.FirstOrDefaultAsync(e => e.Id == obj.CustomerId);

                    TblFinTrnDistribution distribution1 = new()
                    {
                        InvoiceId = input.Id,
                        FinAcCode = IsNotCreditPay(request.Input.PaymentType) ? accountCode : customerMst.CustArAcCode,
                        CrAmount = obj.IsCreditConverted ? obj.TotalAmount : 0,
                        DrAmount = !obj.IsCreditConverted ? obj.TotalAmount : 0,
                        Source = "AR",
                        Type = IsNotCreditPay(request.Input.PaymentType) ? "paycode" : "Customer",
                        Gl = string.Empty,
                        CreatedOn = postedOn
                    };

                    TblFinTrnDistribution distribution2 = new()
                    {
                        InvoiceId = input.Id,
                        FinAcCode = customerMst.CustDefExpAcCode,
                        CrAmount = !obj.IsCreditConverted ? (obj.TotalAmount - obj.TaxAmount) : 0,
                        DrAmount = obj.IsCreditConverted ? (obj.TotalAmount - obj.TaxAmount) : 0,
                        Source = "AR",
                        Gl = string.Empty,
                        Type = "Expense",
                        CreatedOn = postedOn
                    };


                    await _context.FinDistributions.AddAsync(distribution1);
                    await _context.FinDistributions.AddAsync(distribution2);

                    var invoiceItem = await _context.TranInvoiceItems.FirstOrDefaultAsync(e => e.InvoiceId == obj.Id);
                    var tax = await _context.SystemTaxes.FirstOrDefaultAsync(e => e.TaxName == Convert.ToInt32(invoiceItem.TaxTariffPercentage).ToString());
                    List<TblFinTrnDistribution> distributionsList = new() { distribution1, distribution2 };

                    if (tax is not null)
                    {
                        //throw new NullReferenceException("Tax is empty");

                        TblFinTrnDistribution distribution3 = new()
                        {
                            InvoiceId = input.Id,
                            FinAcCode = tax?.OutputAcCode01,
                            CrAmount = !obj.IsCreditConverted ? obj.TaxAmount : 0,
                            DrAmount = obj.IsCreditConverted ? obj.TaxAmount : 0,
                            Source = "AR",
                            Gl = string.Empty,
                            Type = "VAT",
                            CreatedOn = postedOn
                        };

                        await _context.FinDistributions.AddAsync(distribution3);
                        distributionsList.Add(distribution3);
                    }
                    await _context.SaveChangesAsync();


                    /* updateing out standing balance*/
                    var custAmt = _context.TrnCustomerStatements.Where(e => e.CustCode == customer.CustCode);
                    var custInvAmt = (await custAmt.SumAsync(e => e.DrAmount) - await custAmt.SumAsync(e => e.CrAmount));

                    customer.CustOutStandBal = custInvAmt;
                    _context.OprCustomers.Update(customer);
                    await _context.SaveChangesAsync();

                    // var custAmt = _context.TranInvoices.Where(e => e.CustomerId == customer.Id && e.InvoiceStatus == "Open");
                    //var custInvAmt = await custAmt.Where(e => !e.IsCreditConverted).ToListAsync();
                    //var custCreditAmt = await custAmt.Where(e => e.IsCreditConverted).ToListAsync();

                    //customer.CustOutStandBal = (custInvAmt.Sum(e => e.TotalAmount) - custCreditAmt.Sum(e => e.TotalAmount));
                    //_context.OprCustomers.Update(customer);
                    //await _context.SaveChangesAsync();




                    int jvSeq = 0;
                    var seqquence = await _context.Sequences.FirstOrDefaultAsync();
                    if (seqquence is null)
                    {
                        jvSeq = 1;
                        TblSequenceNumberSetting setting1 = new()
                        {
                            JvVoucherSeq = jvSeq
                        };
                        await _context.Sequences.AddAsync(setting1);
                    }
                    else
                    {
                        jvSeq = seqquence.JvVoucherSeq + 1;
                        seqquence.JvVoucherSeq = jvSeq;
                        _context.Sequences.Update(seqquence);
                    }
                    await _context.SaveChangesAsync();



                    TblFinTrnJournalVoucher JV = new()
                    {
                        SpVoucherNumber = string.Empty,
                        VoucherNumber = jvSeq.ToString(),
                        CompanyId = (int)obj.CompanyId,
                        BranchCode = obj.BranchCode,
                        Batch = string.Empty,
                        Source = "AR",
                        Remarks = obj.Remarks,
                        Narration = obj.InvoiceNotes ?? obj.Remarks,
                        JvDate = postedOn,
                        Amount = obj.TotalAmount ?? 0,
                        DocNum = obj.InvoiceNumber,
                        CDate = createdOn,
                        Approved = true,
                        ApprovedDate = DateTime.Now,
                        Posted = true,
                        Void = false,
                        PostedDate = postedOn,
                        SiteCode = obj.SiteCode

                    };

                    await _context.JournalVouchers.AddAsync(JV);
                    await _context.SaveChangesAsync();

                    var jvId = JV.Id;



                    var branchAuths = await _context.FinBranchesAuthorities.Select(e => new { e.FinBranchCode, e.AppAuth })
                        .Where(e => e.FinBranchCode == obj.BranchCode).ToListAsync();
                    if (branchAuths.Count() > 0)
                    {
                        List<TblFinTrnJournalVoucherApproval> jvApprovalList = new();
                        foreach (var item in branchAuths)
                        {
                            TblFinTrnJournalVoucherApproval approval = new()
                            {
                                CompanyId = (int)obj.CompanyId,
                                BranchCode = obj.BranchCode,
                                JvDate = postedOn,
                                TranSource = "AR",
                                Trantype = obj.IsCreditConverted ? "Credit" : "Invoice",
                                DocNum = obj.InvoiceRefNumber,
                                LoginId = Convert.ToInt32(item.AppAuth),
                                AppRemarks = obj.Remarks,
                                JournalVoucherId = jvId,
                                IsApproved = true,
                            };
                            jvApprovalList.Add(approval);
                        }

                        if (jvApprovalList.Count > 0)
                        {
                            await _context.JournalVoucherApprovals.AddRangeAsync(jvApprovalList);
                            await _context.SaveChangesAsync();
                        }
                    }


                    List<TblFinTrnJournalVoucherItem> JournalVoucherItemsList = new();
                    var costallocations = await _context.CostAllocationSetups.Select(e => new { e.Id, e.CostType }).FirstOrDefaultAsync(e => e.CostType == "Customer");

                    foreach (var obj1 in distributionsList)
                    {
                        var JournalVoucherItem = new TblFinTrnJournalVoucherItem
                        {
                            JournalVoucherId = jvId,
                            BranchCode = obj.BranchCode,
                            Batch = string.Empty,
                            Batch2 = string.Empty,
                            Remarks = obj.Remarks,
                            CrAmount = obj1.CrAmount ?? 0,
                            DrAmount = obj1.DrAmount ?? 0,
                            FinAcCode = obj1.FinAcCode,
                            Description = obj.InvoiceNotes,
                            CostAllocation = costallocations.Id,
                            CostSegCode = customer.CustCode,
                            SiteCode = obj.SiteCode

                        };
                        JournalVoucherItemsList.Add(JournalVoucherItem);
                    }

                    if (JournalVoucherItemsList.Count > 0)
                    {
                        await _context.JournalVoucherItems.AddRangeAsync(JournalVoucherItemsList);
                        await _context.SaveChangesAsync();
                    }


                    TblFinTrnJournalVoucherStatement jvStatement = new()
                    {

                        JvDate = postedOn,
                        TranNumber = jvSeq.ToString(),
                        Remarks1 = input.Remarks1,
                        Remarks2 = input.Remarks2,
                        LoginId = request.User.UserId,
                        JournalVoucherId = jvId,
                        IsPosted = true,
                        IsVoid = false
                    };
                    await _context.JournalVoucherStatements.AddAsync(jvStatement);
                    await _context.SaveChangesAsync();


                    List<TblFinTrnAccountsLedger> ledgerList = new();
                    foreach (var item in JournalVoucherItemsList)
                    {
                        TblFinTrnAccountsLedger ledger = new()
                        {
                            MainAcCode = item.FinAcCode,
                            AcCode = item.FinAcCode,
                            AcDesc = item.Description,
                            Batch = item.Batch,
                            BranchCode = item.BranchCode,
                            CrAmount = item.CrAmount ?? 0,
                            DrAmount = item.DrAmount ?? 0,
                            IsApproved = true,
                            TransDate = postedOn,
                            PostedFlag = true,
                            PostDate = postedOn,
                            Jvnum = item.JournalVoucherId.ToString(),
                            Narration = item.Description,
                            Remarks = item.Remarks,
                            Remarks2 = string.Empty,
                            ReverseFlag = false,
                            VoidFlag = false,
                            Source = "AR",
                            ExRate = 0,
                            SiteCode = obj.SiteCode,
                            CostAllocation = item.CostAllocation,
                            CostSegCode = item.CostSegCode
                        };
                        ledgerList.Add(ledger);
                    }
                    if (ledgerList.Count > 0)
                    {
                        await _context.AccountsLedgers.AddRangeAsync(ledgerList);
                        await _context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Log.Error("Error in CreateInvoiceSettlement Method");
                    Log.Error("Error occured time : " + DateTime.UtcNow);
                    Log.Error("Error message : " + ex.Message);
                    Log.Error("Error StackTrace : " + ex.StackTrace);
                    return false;
                }
            }
        }

        bool IsNotCreditPay(string PaymentType) => Utility.IsNotCreditPay(PaymentType);
    }
    #endregion


    #region SalesInvoiceList Pringting
    public class GetSalesInvoicePrintingList : IRequest<PurchaseItemsReportingPrintListDto>
    {
        public UserIdentityDto User { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string BranchCode { get; set; }
        public int? Id { get; set; }
    }
    public class GetSalesInvoicePrintingListHandler : IRequestHandler<GetSalesInvoicePrintingList, PurchaseItemsReportingPrintListDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetSalesInvoicePrintingListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PurchaseItemsReportingPrintListDto> Handle(GetSalesInvoicePrintingList request, CancellationToken cancellationToken)
        {

            bool isArab = request.User.Culture.IsArab();

            var venInvoices = _context.TranInvoices.AsNoTracking();

            var vendors = _context.OprCustomers.AsNoTracking().Select(e => new { e.CustCode, e.CustArbName, e.CustName, e.Id });

            string branchName = string.Empty;

            if (request.BranchCode.HasValue())
            {
                var cBranch = await _context.CompanyBranches.FirstOrDefaultAsync(e => e.BranchCode == request.BranchCode);
                branchName = cBranch.BranchName;

                venInvoices = venInvoices.Where(e => e.BranchCode == request.BranchCode);
            }

            if (request.Id is not null && request.Id > 0)
            {
                venInvoices = venInvoices.Where(e => e.CustomerId == request.Id);
            }

            if (request.DateFrom is not null && request.DateTo is not null)
            {
                venInvoices = venInvoices.Where(e => e.InvoiceDate >= request.DateFrom && e.InvoiceDate <= request.DateTo);
            }

            var list = await venInvoices.Select(e => new TaxReportingPrintDto
            {
                TranNumber = e.InvoiceNumber,
                TaxIdNumber = e.TaxIdNumber,
                Code = vendors.FirstOrDefault(c => c.Id == e.CustomerId).CustCode,
                IsCredit = e.IsCreditConverted,
                TaxCode = string.Empty,
                Date = e.InvoiceDate,
                Name = isArab ? vendors.FirstOrDefault(c => c.Id == e.CustomerId).CustArbName : vendors.FirstOrDefault(c => c.Id == e.CustomerId).CustName,
                Source = "AR",
                Type = "",
                InputTaxAmount = e.DiscountAmount,
                OutputTaxAmount = e.TaxAmount,
                Amount = e.SubTotal,
                TotalAmount = e.TotalAmount

            }).ToListAsync();

            var grpList = list.GroupBy(e => e.Code).OrderByDescending(e => e.Key).ToList();

            List<PurchaseReportingPrintListDto> pRPintingList = new();

            var branch = await _context.CompanyBranches.Include(e => e.SysCompany)
            .FirstOrDefaultAsync(e => e.Id == request.User.BranchId);
            var company = branch?.SysCompany;
            var companyDto = new CommonDataLedgerDto();

            if (company is not null)
            {
                companyDto = new()
                {
                    CompanyName = company.CompanyName,
                    CompanyAddress = company.CompanyAddress,
                    Phone = company.Phone,
                    LogoURL = company.LogoURL,
                    BranchName = branchName.HasValue() ? branchName : branch.BranchName,
                    //ledger.Fax = company.;
                    //ledger.PoBox = company.;
                };
            }


            foreach (var item in grpList)
            {
                var invoiceList = item.Where(e => e.IsCredit == false).ToList();
                var creditList = item.Where(e => e.IsCredit == true).ToList();

                List<PurchaseInvoiceReportingPrintListDto> tList = new();
                List<TaxPricingDto> taxPricings = new();

                PurchaseInvoiceReportingPrintListDto calItem = new()
                {
                    List = invoiceList,
                    CreditList = creditList,

                    InvoicePrice = new()
                    {
                        TotalInputTaxAomunt = invoiceList.Sum(e => e.InputTaxAmount),
                        TotalOutputTaxAomunt = invoiceList.Sum(e => e.OutputTaxAmount),
                        TotalPurchaseAmount = invoiceList.Sum(e => e.Amount),
                        TotalSaleAmount = invoiceList.Sum(e => e.TotalAmount),
                    },

                    CreditPrice = new()
                    {
                        TotalInputTaxAomunt = creditList.Sum(e => e.InputTaxAmount),
                        TotalOutputTaxAomunt = creditList.Sum(e => e.OutputTaxAmount),
                        TotalPurchaseAmount = creditList.Sum(e => e.Amount),
                        TotalSaleAmount = creditList.Sum(e => e.TotalAmount),
                    },
                };

                //  calItem.SummaryList = new() { calItem.InvoicePrice, calItem.CreditPrice };

                tList.Add(calItem);

                pRPintingList.Add(new()
                {
                    ItemList = tList
                });
            }

            return new() { ItemPrintingList = pRPintingList, Company = companyDto };
        }
    }

    #endregion



    #region SchoolSalesInvoiceList Pringting


    public class GetSchoolSalesInvoicePrintingList : IRequest<TblTranInvoiceDto>
    {
        public UserIdentityDto User { get; set; }
        public long? Id { get; set; }
    }
    public class GetSchoolSalesInvoicePrintingListHandler : IRequestHandler<GetSchoolSalesInvoicePrintingList, TblTranInvoiceDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public GetSchoolSalesInvoicePrintingListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TblTranInvoiceDto> Handle(GetSchoolSalesInvoicePrintingList request, CancellationToken cancellationToken)
        {

            bool isArab = request.User.Culture.IsArab();

            var custInvoices = _context.TranInvoices.AsNoTracking();

            var customers = _context.OprCustomers.AsNoTracking().Select(e => new { e.CustCode, e.CustArbName, e.CustName, e.Id });
            var paymentTerms = _context.SndSalesTermsCodes.AsNoTracking();
            var payCodes = _context.FinAccountlPaycodes.AsNoTracking();
            var custInvoice = _context.TrnCustomerInvoices.AsNoTracking();

            string branchName = string.Empty;

            var invoice = await custInvoices.Where(e => e.Id == request.Id).Select(e => new TblTranInvoiceDto
            {
                Id = e.Id,
                CompanyId = e.CompanyId,
                InvoiceNumber = e.InvoiceNumber,
                InvoiceDate = e.InvoiceDate,
                CreatedOn = e.CreatedOn,
                InvoiceDueDate = e.InvoiceDueDate,
                CustomerId = e.CustomerId,
                CustName = e.CustName,
                CustomerName = isArab ? e.CustArbName : e.CustName,
                PaymentTermId = paymentTerms.FirstOrDefault(pt => pt.SalesTermsCode == e.PaymentTerms).SalesTermsName,
                TaxAmount = e.TaxAmount,
                SubTotal = e.SubTotal,
                TotalAmount = e.TotalAmount,
                TotalPayment = custInvoice.FirstOrDefault(cui => cui.InvoiceId == e.Id).PaidAmount

            }).FirstOrDefaultAsync();


            if (!invoice.CustName.HasValue())
                invoice.CustomerName = isArab ? customers.FirstOrDefault(c => c.Id == invoice.CustomerId).CustArbName : customers.FirstOrDefault(c => c.Id == invoice.CustomerId).CustName;

            invoice.TotalPayment = invoice.TotalPayment ?? 0;
            var dist = await _context.FinDistributions.Where(e => e.InvoiceId == invoice.Id && (e.Type == "Customer" || e.Type == "paycode"))
                .Include(e => e.FinDefMainAccounts).FirstOrDefaultAsync();

            invoice.AccountNo = dist.FinAcCode;
            invoice.BankName = dist.FinDefMainAccounts.FinAcName;

            var payCode = await payCodes.FirstOrDefaultAsync(e => e.FinPayAcIntgrAC == dist.FinAcCode);
            if (payCode != null)
            {
                invoice.BankAccount = payCode.FinPayCode;
                invoice.Iban = payCode.FinPayName;
            }

            var customer = await _context.OprCustomers.Where(e => e.Id == invoice.CustomerId).FirstOrDefaultAsync();
            var custCat = await _context.SndCustomerCategories.Where(e => e.CustCatCode == customer.CustCatCode).FirstOrDefaultAsync();
            invoice.TaxIdNumber = customer.VATNumber;
            invoice.Category = custCat.CustCatName;


            //  var products = _context.TranProducts.AsNoTracking();
            var venInvoiceItems = await _context.TranInvoiceItems.Where(e => e.InvoiceId == invoice.Id).AsNoTracking()
                .Select(e => new TblTranInvoiceItemDto
                {
                    Id = e.Id,
                    Description = e.Description,
                    SubTotal = e.TotalAmount - e.TaxAmount,
                    TaxAmount = e.TaxAmount,
                    TotalAmount = e.TotalAmount,

                }).ToListAsync();

            invoice.ItemList = venInvoiceItems;

            //var branch = await _context.CompanyBranches.Include(e => e.SysCompany)
            //.FirstOrDefaultAsync(e => e.Id == request.User.BranchId);
            // var company = branch?.SysCompany;
            var company = await _context.Companies.AsNoTracking().SingleOrDefaultAsync(x => x.Id == invoice.CompanyId);
            var companyDto = new CommonDataLedgerDto();

            if (company is not null)
            {
                companyDto = new()
                {
                    CompanyName = company.CompanyName,
                    CompanyAddress = company.CompanyAddress,
                    Phone = company.Phone,
                    Mobile = company.Mobile,
                    LogoURL = company.LogoURL,
                    BranchName = branchName.HasValue() ? branchName : invoice?.BranchName,
                    //ledger.Fax = company.;
                    //ledger.PoBox = company.;
                };
                invoice.Company = companyDto;
            }

            return invoice;
        }
    }

    #endregion



    #region GetCustomerInvoiceNumberList

    public class GetCustomerInvoiceNumberList : IRequest<List<CustomSelectListItem>>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
        public string Search { get; set; }
    }

    public class GetCustomerInvoiceNumberListQueryHandler : IRequestHandler<GetCustomerInvoiceNumberList, List<CustomSelectListItem>>
    {
        //private readonly ICurrentUserService _currentUserService;
        //protected string UserId => _currentUserService.UserId;
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public GetCustomerInvoiceNumberListQueryHandler(IMapper mapper, CINDBOneContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<CustomSelectListItem>> Handle(GetCustomerInvoiceNumberList request, CancellationToken cancellationToken)
        {
            try
            {

                var invNumbers = _context.TranInvoices.AsNoTracking()
                         .Where(e => e.CustomerId == request.Id && !string.IsNullOrEmpty(e.InvoiceNumber));

                if (request.Search.HasValue())
                {
                    invNumbers = invNumbers.Where(e => e.InvoiceNumber.Contains(request.Search));
                }

                var list = await invNumbers.OrderByDescending(e => e.Id)
                .Select(e => new CustomSelectListItem { Text = e.InvoiceNumber, Value = e.Id.ToString() })
               .ToListAsync(cancellationToken);
                return list;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetCustomerInvoiceNumberList Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return null;
            }
        }
    }

    #endregion


    #region GetCustomerInvoiceItemsByIdList

    public class GetCustomerInvoiceItemsByIdList : IRequest<List<TblTranInvoiceItemDto>>
    {
        public UserIdentityDto User { get; set; }
        public int Id { get; set; }
    }

    public class GetCustomerInvoiceItemsByIdListQueryHandler : IRequestHandler<GetCustomerInvoiceItemsByIdList, List<TblTranInvoiceItemDto>>
    {
        //private readonly ICurrentUserService _currentUserService;
        //protected string UserId => _currentUserService.UserId;
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;

        public GetCustomerInvoiceItemsByIdListQueryHandler(IMapper mapper, CINDBOneContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<TblTranInvoiceItemDto>> Handle(GetCustomerInvoiceItemsByIdList request, CancellationToken cancellationToken)
        {
            try
            {

                var invoice = await _context.TranInvoices.FirstOrDefaultAsync(e => e.Id == request.Id);
                int invoiceStatus = invoice.IsCreditConverted ? -1 : 1;

                var items = await _context.TranInvoiceItems
                                  .Where(e => e.InvoiceId == request.Id)
                                  .ToListAsync();

                var productUnitTypes = _context.TranProducts.Include(e => e.UnitType).AsNoTracking()
                    .Select(e => new { e.Id, pNameEN = e.NameEN, uNameEN = e.UnitType.NameEN });


                List<TblTranInvoiceItemDto> itemList = new();

                foreach (var item in items)
                {
                    var punitType = await productUnitTypes.FirstOrDefaultAsync(e => e.Id == item.ProductId);
                    string pNameEN = string.Empty, uNameEN = string.Empty;
                    if (punitType is not null)
                    {
                        pNameEN = punitType.pNameEN;
                        uNameEN = punitType.uNameEN;
                    }

                    TblTranInvoiceItemDto itemDto = new()
                    {
                        ProductId = item.ProductId,
                        ProductName = pNameEN,
                        Description = item.Description,
                        Quantity = invoiceStatus * item.Quantity,
                        UnitType = uNameEN,
                        UnitPrice = item.UnitPrice,
                        Discount = item.Discount,
                        DiscountAmount = item.DiscountAmount,

                        TaxTariffPercentage = item.TaxTariffPercentage,
                        TaxAmount = invoiceStatus * item.TaxAmount,
                        TotalAmount = invoiceStatus * item.TotalAmount
                    };

                    itemList.Add(itemDto);
                }

                return itemList;
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetCustomerInvoiceItemsByIdList Method");
                Log.Error("Error occured time : " + DateTime.UtcNow);
                Log.Error("Error message : " + ex.Message);
                Log.Error("Error StackTrace : " + ex.StackTrace);
                return null;
            }
        }
    }

    #endregion


}
