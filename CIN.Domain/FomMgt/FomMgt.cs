using CIN.Domain.SystemSetup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIN.Domain;
using CIN.Domain.OpeartionsMgt;

namespace CIN.Domain.FomMgt
{
    [Table("tblErpFomClientMaster")]
    public class TblErpFomClientMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [StringLength(20)]
        public string ClientCode { get; set; }
        [Required]
        public string ClientName { get; set; }
        public string ClientName_Ar { get; set; }
        public string ClientShortName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string PrimaryAddress { get; set; }
        public string SecondaryAddress { get; set; }
        public string RegisteredMobile { get; set; }
        public string RegisteredEmail { get; set; }
        public string AlternameMobile { get; set; }
        public string AlternateEmail { get; set; }
        public string LandLine1 { get; set; }
        public string LandLine2 { get; set; }
        public string ContactPerson1 { get; set; }
        public string Designation1 { get; set; }
        public string ContactPerson2 { get; set; }
        public string Designation2 { get; set; }
        public string VATNum { get; set; }
        public string CRNum { get; set; }
        public string TypeOfBusiness { get; set; }
        public string NumOfEmp { get; set; }
        public string LogoPath { get; set; }
        public float GeoLocLat { get; set; }
        public float GeoLocLan { get; set; }
        public float GeoLocGain { get; set; }
        public DateTime InActiveDate { get; set; }
        public string LoginPassword { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    //[Table("tblErpFomSysCompany")]
    //public class TblErpFomSysCompany
    //{
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }

    //    [Key]
    //    [StringLength(20)]
    //    public string CompanyCode { get; set; }
    //    [StringLength(100)]
    //    public string CompanyName { get; set; }
    //    [StringLength(100)]
    //    public string CompanyNameAr { get; set; }
    //    [Required]
    //    [StringLength(500)]
    //    public string CompanyAddress { get; set; }
    //    [StringLength(500)]
    //    public string CompanyAddressAr { get; set; }
    //    [StringLength(20)]
    //    public string Phone { get; set; }
    //    [Required]
    //    [StringLength(100)]
    //    public string Email { get; set; }
    //    [StringLength(50)]
    //    public string VATNumber { get; set; }
    //    [StringLength(12)]
    //    public string DateFormat { get; set; }
    //    [StringLength(15)]
    //    public string GeoLocLatitude { get; set; }
    //    [StringLength(15)]
    //    public string GeoLocLongitude { get; set; }
    //    [StringLength(100)]
    //    public string LogoURL { get; set; }
    //    [StringLength(1)]
    //    public char PriceDecimal { get; set; }
    //    [StringLength(1)]
    //    public char QuantityDecimal { get; set; }
    //    [StringLength(20)]
    //    public string City { get; set; }
    //    [StringLength(20)]
    //    public string State { get; set; }
    //    [StringLength(50)]
    //    public string Country { get; set; }
    //    [StringLength(20)]
    //    public string Mobile { get; set; }
    //    [StringLength(100)]
    //    public string Website { get; set; }
    //    [StringLength(200)]
    //    public string LogoImagePath { get; set; }
    //    [StringLength(50)]
    //    public string CrNumber { get; set; }
    //    [StringLength(80)]
    //    public string CcNumber { get; set; }
    //    public bool IsActive { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public string CreatedBy { get; set; }


    //}

    //[Table("tblErpFomSysCompanyBranch")]
    //public class TblErpFomSysCompanyBranch
    //{
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }

    //    [Key]
    //    [StringLength(20)]
    //    public string BranchCode { get; set; }
    //    public int CompanyId { get; set; }
    //    public int? ZoneId { get; set; }

    //    [StringLength(150)]
    //    public string BankName { get; set; }
    //    [StringLength(150)]
    //    public string BankNameAr { get; set; }
    //    [StringLength(100)]
    //    public string BranchName { get; set; }
    //    [StringLength(500)]
    //    public string BranchAddress { get; set; }
    //    [StringLength(500)]
    //    public string BranchAddressAr { get; set; }
    //    [StringLength(80)]
    //    public string AccountNumber { get; set; }
    //    [StringLength(20)]
    //    public string Phone { get; set; }
    //    [StringLength(20)]
    //    public string Mobile { get; set; }
    //    public string City { get; set; }
    //    [StringLength(20)]
    //    public string State { get; set; } // ref state
    //    [StringLength(100)]
    //    public string AuthorityName { get; set; }
    //    [StringLength(15)]
    //    public string GeoLocLatitude { get; set; }
    //    [StringLength(15)]
    //    public string GeoLocLongitude { get; set; }
    //    [StringLength(512)]
    //    public string Remarks { get; set; }
    //    [StringLength(80)]
    //    public string Iban { get; set; }
    //    public bool IsActive { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public string CreatedBy { get; set; }
    //}

    //[Table("tblErpFomSysUser")]
    //public class TblErpFomSysUser
    //{
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    [Key]
    //    [StringLength(20)]
    //    public string UserCode { get; set; }
    //    [StringLength(100)]
    //    public string UserName { get; set; }
    //    [StringLength(128)]
    //    public string Password { get; set; }
    //    [StringLength(15)]
    //    public string UserType { get; set; } //Admin, Manager, etc
    //    [StringLength(256)]
    //    public string UserEmail { get; set; }
    //    [StringLength(256)]
    //    public string SwpireCardId { get; set; }
    //    public string PrimaryBranch { get; set; } // Ref branchCode
    //    [StringLength(128)]
    //    public string ImagePath { get; set; }
    //    public bool IsActive { get; set; }
    //    public DateTime ModifiedOn { get; set; }
    //    public string ModifiedBy { get; set; }
    //    public bool IsLoginAllow { get; set; }
    //    public bool IsSigned { get; set; }
    //    [StringLength(50)]
    //    public string SiteCode { get; set; }
    //    public string LoginType { get; set; } //Employee,Teacher,Driver

    //}

    //[Table("tblErpFomServiceCategory")]
    //public class TblErpFomServiceCategory
    //{
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    [Key]
    //    [StringLength(20)]
    //    public string ItemCatCode { get; set; }
    //    [StringLength(50)]
    //    public string ItemCatName { get; set; }
    //    [StringLength(50)]
    //    public string ItemCatNameAr { get; set; }
    //    public bool IsActive { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public string CreatedBy { get; set; }


    //}

    [Table("tblErpFomUserClientLoginMapping")]
    public class TblErpFomUserClientLoginMapping
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [StringLength(20)]
        public string UserClientLoginCode { get; set; }
        [StringLength(256)]
        public string RegEmail { get; set; }
        [StringLength(256)]
        public string RegMobile { get; set; }

        [StringLength(128)]
        public string Password { get; set; }
        [StringLength(15)]
        public string LoginType { get; set; } //user,client
        public string LastLoginDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

    }
    [Table("tblFomJobTicketLogNote")]
    public class TblFomJobTicketLogNote
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(TblFomJobTicket))]
        public string TicketNumber { get; set; }
        [StringLength(1)]
        public string Type { get; set; } = "N";  //N->Note,F->Feedback,U->status update
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }
        public string Note { get; set; }
        public bool ShowToCustomer { get; set; } = false;
        public bool? IsB2c { get; set; } = false;

    }
    [Table("tblFomJobTicket")]
    public class TblFomJobTicket : ProFmAutoGeneratedIdAuditableEntity<int>
    {
        // [StringLength(20)]
        [Key]
        public string TicketNumber { get; set; }
        public string CustomerCode { get; set; }

        [ForeignKey(nameof(CustomerCode))]
        public TblSndDefCustomerMaster SysCustomer { get; set; }
        [Required]
        public string CustRegEmail { get; set; }
        //[StringLength(20)]
        public string SiteCode { get; set; }
        [ForeignKey(nameof(SiteCode))]
        public TblSndDefSiteMaster SysSite { get; set; }
        public DateTime JODate { get; set; }
        [StringLength(100)]
        public string JODocNum { get; set; }
        [StringLength(200)]
        public string JOSubject { get; set; }
        public short JOStatus { get; set; } = 0;          //metadata
        public string JODescription { get; set; }
        [StringLength(20)]
        public string JODeptCode { get; set; }
        [ForeignKey(nameof(JODeptCode))]
        public TblErpFomDepartment SysDepartment { get; set; }
        public string JOBookedBy { get; set; }
        public bool IsApproved { get; set; } = false;
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }         //from UI
        public DateTime? WorkStartDate { get; set; }
        public DateTime? ExpWorkEndDate { get; set; }
        public DateTime? ActWorkEndDate { get; set; }
        public string ClosingRemarks { get; set; }
        public string ClosedBy { get; set; }
        public string JOImg1 { get; set; }
        public string JOImg2 { get; set; }
        public string JOImg3 { get; set; }


        public string JobMaintenanceType { get; set; } = "Corrective"; //Corrective / Preventive (Metadata)
        public string JobType { get; set; }	//	meta data--> Normal,Urgent,Emergency
        public string JOSupervisor { get; set; }
        public string WorkOrders { get; set; }
        public bool IsInScope { get; set; } = false;
        public bool IsCreatedByCustomer { get; set; } = false;


        public bool IsOpen { get; set; } = true;
        public bool IsRead { get; set; } = false;
        public bool IsLateResponse { get; set; } = false;

        public bool IsVoid { get; set; } = false;
        public bool IsSurvey { get; set; } = false;
        public bool IsWorkInProgress { get; set; } = false;
        public bool IsForeClosed { get; set; } = false;
        public bool IsClosed { get; set; } = false;
        public bool IsReconcile { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
        public bool IsTransit { get; set; } = false;
        public bool IsConvertedToWorkOrder { get; set; } = false;

        public string ForecloseReasonCode { get; set; }
        public DateTime? ForecloseDate { get; set; }
        public string ForecloseBy { get; set; }
        public string CancelReasonCode { get; set; }
        public DateTime? CancelDate { get; set; }
        public string CancelBy { get; set; }

        public string QuotationNumber { get; set; }  //future purpose
        public DateTime? QuotationDate { get; set; }  //future purpose


        public bool IsQuotationSubmitted { get; set; }
        public bool IsPoRecieved { get; set; }
        public bool IsHold { get; set; } = false;
    }

    [Table("tblFomJobWorkOrder")]
    public class TblFomJobWorkOrder : ProFmAutoGeneratedIdAuditableEntity<int>
    {
        // [StringLength(20)]
        //[Key]

        [ForeignKey(nameof(TicketNumber))]
        public TblFomJobTicket SysTicket { get; set; }
        public string TicketNumber { get; set; }
        public string CustomerCode { get; set; }

        [ForeignKey(nameof(CustomerCode))]
        public TblSndDefCustomerMaster SysCustomer { get; set; }
        [Required]
        public string CustRegEmail { get; set; }
        //[StringLength(20)]
        public string SiteCode { get; set; }
        [ForeignKey(nameof(SiteCode))]
        public TblSndDefSiteMaster SysSite { get; set; }
        public DateTime JODate { get; set; }
        [StringLength(100)]
        public string JODocNum { get; set; }
        [StringLength(200)]
        public string JOSubject { get; set; }
        public short JOStatus { get; set; } = 0;          //metadata
        public string JODescription { get; set; }
        [StringLength(20)]
        public string JODeptCode { get; set; }
        [ForeignKey(nameof(JODeptCode))]
        public TblErpFomDepartment SysDepartment { get; set; }
        public string JOBookedBy { get; set; }
        public bool IsApproved { get; set; } = false;
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }         //from UI
        public DateTime? WorkStartDate { get; set; }
        public DateTime? ExpWorkEndDate { get; set; }
        public DateTime? ActWorkEndDate { get; set; }
        public string ClosingRemarks { get; set; }
        public string ClosedBy { get; set; }
        public string JOImg1 { get; set; }
        public string JOImg2 { get; set; }
        public string JOImg3 { get; set; }


        public string JobMaintenanceType { get; set; } = "Corrective"; //Corrective / Preventive (Metadata)
        public string JobType { get; set; }	//	meta data--> Normal,Urgent,Emergency
        public string JOSupervisor { get; set; }
        public string WorkOrders { get; set; }
        public bool IsInScope { get; set; } = false;
        public bool IsCreatedByCustomer { get; set; } = false;


        public bool IsOpen { get; set; } = true;
        public bool IsRead { get; set; } = false;
        public bool IsLateResponse { get; set; } = false;

        public bool IsVoid { get; set; } = false;
        public bool IsSurvey { get; set; } = false;
        public bool IsWorkInProgress { get; set; } = false;
        public bool IsForeClosed { get; set; } = false;
        public bool IsClosed { get; set; } = false;
        public bool IsReconcile { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
        public bool IsTransit { get; set; } = false;
        public bool IsConvertedToWorkOrder { get; set; } = false;

        public string ForecloseReasonCode { get; set; }
        public DateTime? ForecloseDate { get; set; }
        public string ForecloseBy { get; set; }
        public string CancelReasonCode { get; set; }
        public DateTime? CancelDate { get; set; }
        public string CancelBy { get; set; }

        public string QuotationNumber { get; set; }
        public DateTime? QuotationDate { get; set; }
        [StringLength(500)]
        public string WoLocation { get; set; }
        [StringLength(500)]
        public string WoNote { get; set; }
    }

    //[Table("tblErpFomServiceSubCategory")]
    //public class TblErpFomServiceSubCategory
    //{
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    [Key]
    //    [StringLength(20)]
    //    public string ItemSubCatCode { get; set; }
    //    [StringLength(20)]
    //    public string ItemCatCode { get; set; }
    //    [StringLength(50)]
    //    public string ItemSubCatName { get; set; }
    //    [StringLength(50)]
    //    public string ItemSubCatNameAr { get; set; }
    //    [StringLength(50)]
    //    public string ItemCatDesc { get; set; }
    //    [StringLength(5)]
    //    public string ItemCatPrefix { get; set; }
    //    public bool IsActive { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public string CreatedBy { get; set; }


    //}


    //[Table("tblErpFomServiceItem")]
    //public class TblErpFomServiceItem
    //{
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    [Key]
    //    [StringLength(20)]
    //    public string ItemCode { get; set; }
    //    [StringLength(20)]
    //    public string ItemSubCatCode { get; set; }
    //    [StringLength(20)]
    //    public string ItemCatCode { get; set; }
    //    [StringLength(50)]
    //    public string ItemName { get; set; }
    //    [StringLength(50)]
    //    public string ItemNameAr { get; set; }
    //    [StringLength(100)]
    //    public string ItemDetails { get; set; }
    //    [StringLength(50)]
    //    public string ItemType { get; set; }
    //    public int ItemQuantity { get; set; }
    //    public string DeptCodes { get; set; }
    //    public DateTime LastSyncDate { get; set; }
    //    public bool IsActive { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public string CreatedBy { get; set; }


    //}



    //[Table("tblErpInvItemMaster")]
    //public class TblErpInvItemMaster
    //{

    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    [StringLength(20)]
    //    [Key]
    //    public string ItemCode { get; set; }

    //    [StringLength(20)]
    //    public string HSNCode { get; set; }

    //    [StringLength(100)]
    //    public string ItemDescription { get; set; }

    //    [StringLength(100)]
    //    public string ItemDescriptionAr { get; set; }

    //    [StringLength(250)]
    //    public string ShortName { get; set; }
    //    [StringLength(250)]
    //    public string ShortNameAr { get; set; }

    //    [ForeignKey(nameof(ItemCat))]
    //    public TblInvDefCategory InvCategory { get; set; }
    //    [StringLength(20)]
    //    public string ItemCat { get; set; }

    //    [ForeignKey(nameof(ItemSubCat))]
    //    public TblInvDefSubCategory InvSubCategories { get; set; }
    //    [StringLength(20)]
    //    public string ItemSubCat { get; set; }

    //    [StringLength(20)]
    //    public string ItemClass { get; set; }

    //    [StringLength(20)]
    //    public string ItemSubClass { get; set; }

    //    [StringLength(10)]
    //    public string ItemBaseUnit { get; set; }

    //    [StringLength(10)]
    //    public string ItemAvgCost { get; set; }

    //    [StringLength(10)]
    //    public string ItemStandardCost { get; set; }

    //    [StringLength(20)]
    //    public string ItemPrimeVendor { get; set; }

    //    [Column(TypeName = "decimal(12,5)")]
    //    public decimal ItemStandardPrice1 { get; set; }

    //    [Column(TypeName = "decimal(12,5)")]
    //    public decimal ItemStandardPrice2 { get; set; }

    //    [Column(TypeName = "decimal(12,5)")]
    //    public decimal ItemStandardPrice3 { get; set; }

    //    [StringLength(20)]
    //    public string ItemType { get; set; }

    //    public decimal ItemQty { get; set; }

    //    [StringLength(20)]
    //    public string DeptCodes { get; set; }

    //    [StringLength(20)]
    //    public string ItemTracking { get; set; }
    //    [StringLength(20)]
    //    public string ItemWeight { get; set; }

    //    [StringLength(20)]
    //    public string ItemTaxCode { get; set; }
    //    public bool AllowPriceOverride { get; set; }
    //    public bool AllowDiscounts { get; set; }
    //    public bool AllowQuantityOverride { get; set; }


    //}


    //[Table("tblInvDefSubCategory")]
    //public class TblInvDefSubCategory
    //{
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }

    //    [Key]
    //    [StringLength(20)]
    //    public string ItemSubCatCode { get; set; }
    //    [StringLength(41)]
    //    public string SubCatKey { get; set; }
    //    [ForeignKey(nameof(ItemCatCode))]
    //    public TblInvDefCategory InvCategory { get; set; }
    //    [StringLength(20)]
    //    public string ItemCatCode { get; set; }
    //    [StringLength(50)]
    //    [Required]
    //    public string ItemSubCatName { get; set; }
    //    [StringLength(50)]
    //    [Required]
    //    public string ItemSubCatNameAr { get; set; }
    //    [StringLength(50)]
    //    public string ItemSubCatDesc { get; set; }

    //    public bool IsActive { get; set; }

    //    public DateTime CreatedOn { get; set; }
    //    public string CreatedBy { get; set; }

    //}


    //[Table("tblInvDefCategory")]
    //public class TblInvDefCategory
    //{
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    [Key]
    //    [StringLength(20)]
    //    public string ItemCatCode { get; set; }
    //    [StringLength(50)]
    //    [Required]
    //    public string ItemCatName { get; set; }

    //    [StringLength(50)]
    //    [Required]
    //    public string ItemCatName_Ar { get; set; }

    //    [StringLength(50)]
    //    public string ItemCatDesc { get; set; }
    //    [StringLength(5)]
    //    public string ItemCatPrefix { get; set; }

    //    public bool IsActive { get; set; }

    //    public DateTime CreatedOn { get; set; }
    //    public string CreatedBy { get; set; }



    //}


    //[Table("tblSndDefCustomerMaster")]
    //public class TblSndDefCustomerMaster
    //{
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }

    //    [Key]
    //    [StringLength(20)]
    //    public string CustCode { get; set; }
    //    [StringLength(200)]
    //    [Required]
    //    public string CustName { get; set; }
    //    [StringLength(200)]
    //    public string CustArbName { get; set; }
    //    [StringLength(50)]
    //    public string CustAlias { get; set; }
    //    public short CustType { get; set; }

    //    [StringLength(50)]
    //    public string VATNumber { get; set; }

    //    [Required]
    //    public string CustCatCode { get; set; }

    //    public short CustRating { get; set; }

    //    [Required]
    //    public string SalesTermsCode { get; set; }
    //    [Column(TypeName = "decimal(17,3)")]
    //    public decimal CustDiscount { get; set; }
    //    [Column(TypeName = "decimal(17,3)")]
    //    public decimal CustCrLimit { get; set; }
    //    [Column(TypeName = "decimal(17,3)")]
    //    public decimal? CustOutStandBal { get; set; }

    //    [Column(TypeName = "decimal(17,3)")]
    //    public decimal? CustAvailCrLimit { get; set; }

    //    [StringLength(20)]
    //    public string CustSalesRep { get; set; }
    //    [StringLength(100)]
    //    public string CustSalesArea { get; set; }
    //    public string CustARAc { get; set; }
    //    [Column(TypeName = "date")]
    //    public DateTime CustLastPaidDate { get; set; }
    //    [Column(TypeName = "date")]
    //    public DateTime CustLastSalesDate { get; set; }
    //    [Column(TypeName = "decimal(17,3)")]
    //    public decimal CustLastPayAmt { get; set; }
    //    [StringLength(500)]
    //    public string CustAddress1 { get; set; }
    //    [StringLength(20)]
    //    [Required]
    //    public string CustCityCode1 { get; set; }
    //    [StringLength(50)]
    //    public string CustMobile1 { get; set; }
    //    [StringLength(50)]
    //    public string CustPhone1 { get; set; }
    //    [StringLength(500)]
    //    public string CustEmail1 { get; set; }
    //    [StringLength(200)]
    //    public string CustContact1 { get; set; }
    //    [StringLength(500)]
    //    public string CustAddress2 { get; set; }
    //    public string CustCityCode2 { get; set; }

    //    [StringLength(50)]
    //    public string CustMobile2 { get; set; }
    //    [StringLength(50)]
    //    public string CustPhone2 { get; set; }
    //    [StringLength(500)]
    //    public string CustEmail2 { get; set; }
    //    [StringLength(200)]
    //    public string CustContact2 { get; set; }
    //    [StringLength(200)]
    //    public string CustUDF1 { get; set; }
    //    [StringLength(200)]
    //    public string CustUDF2 { get; set; }
    //    [StringLength(200)]
    //    public string CustUDF3 { get; set; }
    //    public bool CustAllowCrsale { get; set; }
    //    public bool CustAlloCrOverride { get; set; }
    //    public bool CustOnHold { get; set; }
    //    public bool CustAlloChkPay { get; set; }
    //    public bool CustSetPriceLevel { get; set; }
    //    public short CustPriceLevel { get; set; }
    //    public bool CustIsVendor { get; set; }
    //    public bool CustArAcBranch { get; set; }
    //    public string CustArAcCode { get; set; }
    //    [Required]
    //    public string CustDefExpAcCode { get; set; }
    //    [Required]
    //    public string CustARAdjAcCode { get; set; }
    //    [Required]
    //    public string CustARDiscAcCode { get; set; }
    //    [StringLength(50)]
    //    public string CrNumber { get; set; }
    //    [StringLength(200)]
    //    public string CustNameAliasEn { get; set; }
    //    [StringLength(200)]
    //    public string CustNameAliasAr { get; set; }

    //}



    //[Table("tblSndDefCustomerCategory")]
    //public class TblSndDefCustomerCategory
    //{
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    [StringLength(20)]
    //    [Key]
    //    public string CustCatCode { get; set; }
    //    [StringLength(50)]
    //    public string CustCatName { get; set; }
    //    [StringLength(50)]
    //    public string CustCatDesc { get; set; }
    //    [StringLength(3)]
    //    public string CatPrefix { get; set; }
    //    public int LastSeq { get; set; }

    //}


    //[Table("tblSndDefSiteMaster")]
    //public class TblSndDefSiteMaster
    //{
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    [Key]
    //    [StringLength(50)]
    //    public string SiteCode { get; set; }
    //    [Required]
    //    [StringLength(200)]
    //    public string SiteName { get; set; }
    //    [StringLength(200)]
    //    public string SiteArbName { get; set; }
    //    [ForeignKey(nameof(CustomerCode))]
    //    public TblSndDefCustomerMaster SysCustomerCode { get; set; }
    //    [Required]
    //    public string CustomerCode { get; set; }
    //    [StringLength(500)]
    //    [Required]
    //    public string SiteAddress { get; set; }
    //    [StringLength(20)]
    //    [Required]
    //    [ForeignKey(nameof(SiteCityCode))]
    //    public TblErpSysCityCode SysSiteCityCode { get; set; }
    //    public string SiteCityCode { get; set; }
    //    [Column(TypeName = "decimal(18, 3)")]
    //    [Required]
    //    public decimal SiteGeoLatitude { get; set; }
    //    [Required]
    //    [Column(TypeName = "decimal(18, 3)")]
    //    public decimal SiteGeoLongitude { get; set; }
    //    [Column(TypeName = "decimal(18, 3)")]
    //    [Required]
    //    public decimal SiteGeoGain { get; set; }
    //    [Column(TypeName = "decimal(18, 3)")]
    //    public decimal SiteGeoLatitudeMeter { get; set; }
    //    [Column(TypeName = "decimal(18, 3)")]
    //    public decimal SiteGeoLongitudeMeter { get; set; }

    //    public bool IsChildCustomer { get; set; }
    //    [StringLength(50)]
    //    public string VATNumber { get; set; }
    //}

    [Table("tblErpFomSubContractor")]
    public class TblErpFomSubContractor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        [StringLength(20)]
        public string SubContCode { get; set; }
        public string DeptCodes { get; set; }
        public string NameEng { get; set; }
        public string NameArabic { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string ContactPerson1 { get; set; }
        public string DesgContactPerson1 { get; set; }
        public string ContactPerson1Phone { get; set; }
        public string ContactPerson2 { get; set; }
        public string DesgContactPerson2 { get; set; }
        public string ContactPerson2Phone { get; set; }
        public string Website { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }


    }


    [Table("tblErpFomDepartment")]
    public class TblErpFomDepartment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        [StringLength(20)]
        public string DeptCode { get; set; }
        public string NameEng { get; set; }
        public string NameArabic { get; set; }
        public string DeptServType { get; set; }
        public string ServiceTimePeriods { get; set; }
        public string FullImagePath { get; set; }
        public string ThumbNailImage { get; set; }
        public bool IsSheduleRequired1 { get; set; }
        public bool IsSheduleRequired2 { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }


    }

    [Table("tblErpFomDepartmentTypes")]
    public class TblErpFomDepartmentTypes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        [StringLength(20)]
        public string ServiceTypeCode { get; set; }
        public string ServiceTypeName { get; set; }
        public string ServiceTypeName_Ar { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }


    [Table("tblErpFomClientCategory")]
    public class TblErpFomClientCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ClientCatCode { get; set; }
        public string NameEng { get; set; }
        public string NameArabic { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }


    }



    [Table("tblErpFomCustomerContract")]
    public class TblErpFomCustomerContract
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        [StringLength(20)]
        public string ContractCode { get; set; }

        // [Required]
        public string CustCode { get; set; }

        // [Required]
        public string CustSiteCode { get; set; }
        public string CustContNumber { get; set; }
        public DateTime ContStartDate { get; set; }
        public DateTime ContEndDate { get; set; }
        public string ContDeptCode { get; set; }
        public string ContProjManager { get; set; }
        public string ContProjSupervisor { get; set; }
        public string ContApprAuthorities { get; set; }
        public string Remarks { get; set; }
        public bool IsAppreoved { get; set; }
        public bool IsSheduleRequired { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string File1 { get; set; }
        public string File2 { get; set; }
        public string File3 { get; set; }


    }



    [Table("tblErpFomResourceType")]
    public class TblErpFomResourceType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public string ResTypeCode { get; set; }
        public string ResTypeName { get; set; }
        public string ResTypeNameAr { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

    }


    [Table("tblErpFomResources")]
    public class TblErpFomResources
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [StringLength(20)]
        public string ResCode { get; set; }

        [ForeignKey(nameof(ResTypeCode))]
        public TblErpFomResourceType SysResType { get; set; }
        public string ResTypeCode { get; set; }
        public string DeptCodes { get; set; }           //multiple Codes separated by ,(comma)
        public string NameEng { get; set; }
        public string NameAr { get; set; }

        [StringLength(100)]
        public string LoginUser { get; set; }
        public bool ApprovalAuth { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }


    }


    [Table("tblErpFomActivities")]
    public class TblErpFomActivities
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        [StringLength(20)]
        public string ActCode { get; set; }
        [ForeignKey(nameof(DeptCode))]
        public TblErpFomDepartment ErpDeptCode { get; set; }
        public string DeptCode { get; set; }
        public string ActName { get; set; }
        public string ActNameAr { get; set; }
        public string FullImagePath { get; set; }
        public string ThumbNailImage { get; set; }
        public bool IsActive { get; set; }
        public bool IsB2B { get; set; }
        public bool IsB2C { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }


    }

    [Table("tblErpFomSysLoginAuthority")]
    public class TblErpFomSysLoginAuthority
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int LoginID { get; set; }
        public bool RaiseTicket { get; set; }
        public bool VoidTicket { get; set; }
        public bool ForeCloseWO { get; set; }
        public bool ApproveTicket { get; set; }
        public bool CloseWO { get; set; }
        public bool ManageWO { get; set; }
        public bool ModifyTicket { get; set; }
        public bool ModifyWO { get; set; }
        public bool VoidAfterApproval { get; set; }

    }

    [Table("tblErpFomServiceItems")]
    public class TblErpFomServiceItems
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [StringLength(20)]
        public string ServiceCode { get; set; }

        [ForeignKey(nameof(DeptCode))]
        public TblErpFomDepartment ErpDeptCode { get; set; }
        public string DeptCode { get; set; }

        [ForeignKey(nameof(ActivityCode))]
        public TblErpFomActivities ActCode { get; set; }
        public string ActivityCode { get; set; }
        public string ServiceShortDesc { get; set; }
        public string ServiceShortDescAr { get; set; }
        public string ServiceDetails { get; set; }
        public string ServiceDetailsAr { get; set; }
        public TimeSpan TimeUnitPrimary { get; set; }
        public int ResourceUnitPrimary { get; set; }
        public int MinReqResource { get; set; }
        public TimeSpan MinRequiredHrs { get; set; }
        public Decimal PotentialCost { get; set; }
        public Decimal PrimaryUnitPrice { get; set; }
        public Decimal ApplicableDiscount { get; set; }
        public bool IsOnOffer { get; set; }
        public Decimal OfferPrice { get; set; }
        public DateTime OfferStartDate { get; set; }
        public DateTime OfferEndDate { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public string FullImagePath { get; set; }
        public string ThumbNailImagePath { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime CreatedBy { get; set; }
        [StringLength(126)]
        public string Serviceitems { get; set; }
    }

    [Table("tblErpFomServiceUnitItems")]
    public class TblErpFomServiceUnitItems
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(ServiceCode))]
        public TblErpFomServiceItems ErpServiceCode { get; set; }
        public string ServiceCode { get; set; }
        public int UnitFlag { get; set; }
        public TimeSpan TimeUnitPrimary { get; set; }
        public int ResourceUnitPrimary { get; set; }
        public Decimal UnitPrice { get; set; }
        public Decimal OfferPrice { get; set; }
        public Decimal PotentialCostFactor { get; set; }
        public Decimal PotentialUnitCost { get; set; }
        public bool IsActive { get; set; }


    }

    [Table("tblErpFomServiceItemsDetails")]
    public class TblErpFomServiceItemsDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(ServiceCode))]
        public TblErpFomServiceItems ErpServiceCode { get; set; }
        public string ServiceCode { get; set; }
        public int ImagePath { get; set; }
        public string Desc1 { get; set; }
        public int Desc1Ar { get; set; }
        public string Desc2 { get; set; }
        public int Desc2Ar { get; set; }
        public string ServiceDetails { get; set; }
        public bool IsActive { get; set; }


    }

    [Table("tblErpFomPeriod")]
    public class TblErpFomPeriod
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public string Descriptions { get; set; }
        public string Descriptions_Ar { get; set; }
        public bool IsActive { get; set; }
    }

    [Table("tblErpFomScheduleSummary")]
    public class TblErpFomScheduleSummary
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int ContractId { get; set; }
        public string DeptCode { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ApproveDate { get; set; }
        public string ApprovedBy { get; set; }
        public bool IsSchGenerated { get; set; }


    }


    [Table("tblErpFomScheduleWeekdays")]
    public class TblErpFomScheduleWeekdays
    {
        public int Id { get; set; }

        [ForeignKey(nameof(SchId))]
        public TblErpFomScheduleSummary ErpSchSumId { get; set; }
        public int SchId { get; set; }
        public int ContractId { get; set; }
        public string WeekDay { get; set; }
        public TimeSpan Time { get; set; }
        public bool AllDayLong { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
    }


    [Table("tblErpFomScheduleDetails")]
    public class TblErpFomScheduleDetails
    {
        public int Id { get; set; }

        [ForeignKey(nameof(SchId))]
        public TblErpFomScheduleSummary ErpSchSumId { get; set; }
        public int SchId { get; set; }
        public int ContractId { get; set; }
        public DateTime SchDate { get; set; }
        public string Department { get; set; }
        public string SerType { get; set; }
        public string Frequency { get; set; }
        public string TranNumber { get; set; }
        public string ServiceItem { get; set; }
        public string Remarks { get; set; }
        public TimeSpan Time { get; set; }
        public bool IsReschedule { get; set; }
        public bool IsActive { get; set; }
        public bool? IsB2c { get; set; }
    }



    [Table("tblFomB2CUserClientLoginMapping")]
    public class TblFomB2CUserClientLoginMapping
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [StringLength(20)]
        public string UserClientLoginCode { get; set; }
        [StringLength(256)]
        public string RegEmail { get; set; }
        [StringLength(256)]
        public string RegMobile { get; set; }

        [StringLength(128)]
        public string Password { get; set; }
        [StringLength(15)]
        public string LoginType { get; set; } //user,client
        public string LastLoginDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

    }



    public abstract class ProFmAutoGeneratedIdAuditableEntity<T>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
    [Table("tblFomB2CJobTicket")]
    public class TblFomB2CJobTicket : ProFmAutoGeneratedIdAuditableEntity<int>
    {
        public string TicketNumber { get; set; }
        [StringLength(100)]
        public string SpTicketNumber { get; set; }
        public string CustomerCode { get; set; }

        [ForeignKey(nameof(CustomerCode))]
        public TblSndDefCustomerMaster SysCustomer { get; set; }
        [Required]
        public string CustRegEmail { get; set; }
        public DateTime JODate { get; set; }
        [StringLength(100)]
        public string JODocNum { get; set; }
        [StringLength(200)]
        public string JOSubject { get; set; }
        public short JOStatus { get; set; } = 0;          //metadata
        public string JODescription { get; set; }
        [StringLength(20)]
        public string JODeptCode { get; set; }
        [ForeignKey(nameof(JODeptCode))]
        public TblErpFomDepartment SysDepartment { get; set; }
        public string JOBookedBy { get; set; }
        public bool IsApproved { get; set; } = false;
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }         //from UI
        public DateTime? WorkStartDate { get; set; }
        public DateTime? ExpWorkEndDate { get; set; }
        public DateTime? ActWorkEndDate { get; set; }
        public string ClosingRemarks { get; set; }
        public string Feedback { get; set; }
        public string ClosedBy { get; set; }
        public string JOImg1 { get; set; }
        public string JOImg2 { get; set; }
        public string JOImg3 { get; set; }
        public decimal GeoLatitude { get; set; }
        public decimal GeoLongitude { get; set; }
        public TimeSpan OnlyTime { get; set; }

        public string JobMaintenanceType { get; set; } = "Corrective"; //Corrective / Preventive (Metadata)
        public string JobType { get; set; }	//	meta data--> Normal,Urgent,Emergency
        public string JOSupervisor { get; set; }
        public string WorkOrders { get; set; }
        public bool IsInScope { get; set; } = false;
        public bool IsCreatedByCustomer { get; set; } = false;


        public bool IsOpen { get; set; } = true;
        public bool IsRead { get; set; } = false;
        public bool IsLateResponse { get; set; } = false;

        public bool IsVoid { get; set; } = false;
        public bool IsSurvey { get; set; } = false;
        public bool IsWorkInProgress { get; set; } = false;
        public bool IsForeClosed { get; set; } = false;
        public bool IsClosed { get; set; } = false;
        public bool IsReconcile { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
        public bool IsTransit { get; set; } = false;
        public bool IsConvertedToWorkOrder { get; set; } = false;

        public string ForecloseReasonCode { get; set; }
        public DateTime? ForecloseDate { get; set; }
        public string ForecloseBy { get; set; }
        public string CancelReasonCode { get; set; }
        public DateTime? CancelDate { get; set; }
        public string CancelBy { get; set; }

        public string QuotationNumber { get; set; }  //future purpose
        public DateTime? QuotationDate { get; set; }  //future purpose


        public bool IsQuotationSubmitted { get; set; }
        public bool IsPoRecieved { get; set; }
        public bool IsHold { get; set; } = false;

        public bool IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public int? SchDetailId { get; set; }
        [StringLength(4)]
        public string TicketType { get; set; }
        [StringLength(4)]
        public string SchType { get; set; }
        [StringLength(20)]
        public string ResCode { get; set; }
        
        ////// [StringLength(20)]
        ////[Key]
        ////public string TicketNumber { get; set; }
        ////public string CustomerCode { get; set; }

        ////[ForeignKey(nameof(CustomerCode))]
        ////public TblSndDefCustomerMaster SysCustomer { get; set; }
        ////[Required]
        ////public string CustRegEmail { get; set; }
        ////public DateTime JODate { get; set; }
        ////[StringLength(100)]
        ////public string JODocNum { get; set; }
        ////[StringLength(200)]
        ////public string JOSubject { get; set; }
        ////public short JOStatus { get; set; } = 0;          //metadata
        ////public string JODescription { get; set; }
        ////[StringLength(20)]
        ////public string JODeptCode { get; set; }
        ////[ForeignKey(nameof(JODeptCode))]
        ////public TblErpFomDepartment SysDepartment { get; set; }
        ////public string JOBookedBy { get; set; }
        ////public bool IsApproved { get; set; } = false;
        ////public string ApprovedBy { get; set; }
        ////public DateTime? ApprovedDate { get; set; }         //from UI
        ////public DateTime? WorkStartDate { get; set; }
        ////public DateTime? ExpWorkEndDate { get; set; }
        ////public DateTime? ActWorkEndDate { get; set; }
        ////public string ClosingRemarks { get; set; }
        ////public string ClosedBy { get; set; }
        ////public string JOImg1 { get; set; }
        ////public string JOImg2 { get; set; }
        ////public string JOImg3 { get; set; }
        ////public decimal GeoLatitude { get; set; }
        ////public decimal GeoLongitude { get; set; }
        ////public TimeSpan OnlyTime { get; set; }

        ////public string JobMaintenanceType { get; set; } = "Corrective"; //Corrective / Preventive (Metadata)
        ////public string JobType { get; set; } //	meta data--> Normal,Urgent,Emergency
        ////public string JOSupervisor { get; set; }
        ////public string WorkOrders { get; set; }
        ////public bool IsInScope { get; set; } = false;
        ////public bool IsCreatedByCustomer { get; set; } = false;


        ////public bool IsOpen { get; set; } = true;
        ////public bool IsRead { get; set; } = false;
        ////public bool IsLateResponse { get; set; } = false;

        ////public bool IsVoid { get; set; } = false;
        ////public bool IsSurvey { get; set; } = false;
        ////public bool IsWorkInProgress { get; set; } = false;
        ////public bool IsForeClosed { get; set; } = false;
        ////public bool IsClosed { get; set; } = false;
        ////public bool IsReconcile { get; set; } = false;
        ////public bool IsCompleted { get; set; } = false;
        ////public bool IsTransit { get; set; } = false;
        ////public bool IsConvertedToWorkOrder { get; set; } = false;

        ////public string ForecloseReasonCode { get; set; }
        ////public DateTime? ForecloseDate { get; set; }
        ////public string ForecloseBy { get; set; }
        ////public string CancelReasonCode { get; set; }
        ////public DateTime? CancelDate { get; set; }
        ////public string CancelBy { get; set; }

        ////public string QuotationNumber { get; set; }  //future purpose
        ////public DateTime? QuotationDate { get; set; }  //future purpose


        ////public bool IsQuotationSubmitted { get; set; }
        ////public bool IsPoRecieved { get; set; }
        ////public bool IsHold { get; set; } = false;
    }




    [Table("tblFomJobTicketFeedBack")]
    public class TblFomJobTicketFeedBack
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(TblFomB2CJobTicket))]
        public string TicketNumber { get; set; }
        public int TeamRating { get; set; }
        public int CompanyRating { get; set; }
        [StringLength(750)]
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Date { get; set; }

    }



    [Table("tblFomJobTicketPayment")]
    public class TblFomJobTicketPayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string TicketNumber { get; set; }
        [StringLength(250)]
        public string TokenNumber { get; set; }
        public string Response { get; set; }
        public bool IsDayService { get; set; }
        public DateTime Date { get; set; }
    }


    [Table("tblFomB2CDefaultPaymentPrice")]
    public class TblFomB2CDefaultPaymentPrice : PrimaryKey<int> //ProFmAutoGeneratedIdAuditableEntity<int>
    {
        [StringLength(10)]
        public string PayType { get; set; }
        [StringLength(20)]
        public string ServiceCode { get; set; }
        public decimal Price { get; set; }
        public decimal ApDiscount { get; set; }
        public decimal OfferPrice { get; set; }
        public bool Applicable { get; set; }
    }

}
