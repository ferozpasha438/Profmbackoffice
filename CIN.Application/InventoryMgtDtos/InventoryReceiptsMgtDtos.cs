﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CIN.Domain;
using CIN.Domain.InventoryMgt;
using CIN.Domain.PurchaseMgt;

namespace CIN.Application.InventoryMgtDtos
{
    [AutoMap(typeof(TblIMReceiptsTransactionHeader))]
    public class TblIMReceiptsTransactionHeaderDto : AutoActiveGenerateIdAuditableKey<int>
    {
        [StringLength(20)]
        [Key]
        public string TranNumber { get; set; }


        public DateTime TranDate { get; set; }

        [StringLength(50)]
        public string TranUser { get; set; }

        [StringLength(10)]
        public string TranLocation { get; set; }

        [StringLength(10)]
        public string TranToLocation { get; set; }


        [StringLength(50)]
        public string TranDocNumber { get; set; }

        [StringLength(50)]
        public string TranReference { get; set; }

        [StringLength(10)]
        public string TranType { get; set; }


        public decimal TranTotalCost { get; set; }


        public int TranTotItems { get; set; }


        public DateTime TranCreateDate { get; set; }


        public string TranCreateUser { get; set; }



        public sbyte TranLockStat { get; set; }


        public string TranLockUser { get; set; }

        public sbyte TranPostStatus { get; set; }


        public DateTime TranPostDate { get; set; }


        public string TranpostUser { get; set; }

        public sbyte TranVoidStatus { get; set; }


        public string TranVoidUser { get; set; }



        public DateTime TranvoidDate { get; set; }

        [StringLength(50)]
        public string TranRemarks { get; set; }

        [StringLength(50)]
        public string TranInvAccount { get; set; }

        [StringLength(50)]
        public string TranInvAdjAccount { get; set; }

        [StringLength(15)]
        public string JVNum { get; set; }
        [StringLength(10)]
        public string TranBranch { get; set; }
        public bool IsPaid { get; set; }
        public Boolean IsApproved { get; set; }
    }

    public class ReceiptsProductUnitPriceDTO
    {

        public string tranItemCode { get; set; }
        public string tranItemName { get; set; }
        public string tranItemUnitCode { get; set; }
        public string tranItemCost { get; set; }
        public decimal tranItemUomFactor { get; set; }
        public decimal ItemAvgcost { get; set; }


    }

    public class TblReceiptsInventoryReturntDto : TblIMReceiptsTransactionHeaderDto
    {

        public List<TblIMReceiptsTransactionDetailsDto> itemList { get; set; }
    }
    [AutoMap(typeof(TblIMReceiptsTransactionDetails))]
    public class TblIMReceiptsTransactionDetailsDto : AutoActiveGenerateIdKeyDto<int>
    {

        [StringLength(20)]
        [Key]
        public string TranNumber { get; set; }
        public int SNo { get; set; }

        public DateTime TranDate { get; set; }

        [StringLength(10)]
        public string TranLocation { get; set; }

        [StringLength(10)]
        public string TranToLocation { get; set; }

        [StringLength(10)]
        public string TranType { get; set; }

        [StringLength(20)]
        public string TranItemCode { get; set; }

        [StringLength(25)]
        public string TranBarcode { get; set; }


        [StringLength(100)]
        public string TranItemName { get; set; }

        [StringLength(100)]
        public string TranItemName2 { get; set; }


        public decimal TranItemQty { get; set; }

        [StringLength(10)]
        public string TranItemUnit { get; set; }


        public decimal TranUOMFactor { get; set; }


        public decimal TranItemCost { get; set; }


        public decimal TranTotCost { get; set; }

        [StringLength(25)]
        public string ItemAttribute1 { get; set; }

        [StringLength(25)]
        public string ItemAttribute2 { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }

        [StringLength(50)]
        public string INVAcc { get; set; }

        [StringLength(50)]
        public string INVADJAcc { get; set; }




    }
    public class ReceiptsPaginationDto : TblIMReceiptsTransactionHeaderDto
    {
        public bool CanEditSurveyForm { get; set; }
        public TblPurAuthorities Authorities { get; set; }
        public bool ApprovedUser { get; set; }
        public bool HasAuthority { get; set; }
        public bool IsSettled { get; set; }
        public bool CanSettle { get; set; }

    }
}
