using AutoMapper;
using CIN.Application.Common;
using CIN.Application.GeneralLedgerDtos;
using CIN.Application.InvoiceDtos;
using CIN.DB;
using CIN.Domain.GeneralLedger.CashVoucher;
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

namespace CIN.Application.GeneralLedgerQuery
{
    #region TrialBalanceReportList

    public class TrialBalanceReportList : IRequest<TrialBalanceReportListDto>
    {
        public UserIdentityDto User { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    public class TrialBalanceReportListHandler : IRequestHandler<TrialBalanceReportList, TrialBalanceReportListDto>
    {
        private readonly CINDBOneContext _context;
        private readonly IMapper _mapper;
        public TrialBalanceReportListHandler(CINDBOneContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TrialBalanceReportListDto> Handle(TrialBalanceReportList request, CancellationToken cancellationToken)
        {
            var ledgerList = _context.AccountsLedgers
                .OrderBy(e => e.AcCode)
                .AsNoTracking();

            if (request.DateFrom is not null && request.DateTo is not null)
            {
                //Opening Balance calculating
                ledgerList = ledgerList.Where(e => EF.Functions.DateFromParts(e.PostDate.Value.Year, e.PostDate.Value.Month, e.PostDate.Value.Day) >= request.DateFrom
                                               && EF.Functions.DateFromParts(e.PostDate.Value.Year, e.PostDate.Value.Month, e.PostDate.Value.Day) <= request.DateTo);
            }

            var list = await ledgerList.GroupBy(e => e.AcCode)
                 .Select(e =>
             new TrialBalanceReportDto
             {
                 FinAcCode = e.Key,
                 DrAmount = e.Sum(d => d.DrAmount),
                 CrAmount = e.Sum(d => d.CrAmount),
                 Balance = e.Sum(d => d.DrAmount) - e.Sum(d => d.CrAmount),
                 CrBalance = 0,
                 DrBalance = 0
             }).ToListAsync();


            foreach (var item in list)
            {
                if (item.Balance < 0)
                    item.CrBalance = item.Balance * -1;

                else if (item.Balance > 0)
                    item.DrBalance = item.Balance;

                var mainAC = await _context.FinMainAccounts.FirstOrDefaultAsync(e => e.FinAcCode == item.FinAcCode);
                item.Description = mainAC.FinAcDesc;
            }

            var branch = await _context.CompanyBranches.Include(e => e.SysCompany)
                .FirstOrDefaultAsync(e => e.Id == request.User.BranchId);

            var trialBalance = new TrialBalanceReportListDto
            {
                List = list,
                TotalDrBalance = list.Sum(e => e.DrBalance),
                TotalCrBalance = list.Sum(e => e.CrBalance),
                CompanyName = branch?.SysCompany.CompanyName ?? string.Empty
            };

            var company = branch?.SysCompany;

            if (company is not null)
            {
                trialBalance.Company = new()
                {
                    CompanyName = company.CompanyName,
                    CompanyAddress = company.CompanyAddress,
                    Phone = company.Phone,
                    LogoURL = company.LogoURL,
                    BranchName = branch.BranchName,
                    //ledger.Fax = company.;
                    //ledger.PoBox = company.;
                };
            }

            return trialBalance;

        }
    }

    #endregion
}
