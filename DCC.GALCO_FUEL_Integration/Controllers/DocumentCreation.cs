using DCC.GALCO_FUEL_Integration.Models;
using SAPbobsCOM;

namespace DCC.GALCO_FUEL_Integration.Controllers
{
    public class DocumentCreation
    {
        private readonly Common xcommon;
        private readonly IConfiguration _configuration;
        SAPbobsCOM.Documents oGoodsIssue = null;
        string _connectionString = "";
        string IsDebug = string.Empty;

        public DocumentCreation(IConfiguration configuration)
        {
            _configuration = configuration;
            xcommon = new Common(configuration);
            _connectionString = configuration.GetConnectionString("DBConnection");
            IsDebug = configuration["AppConfig:IsDebug"];
        }

        public ApiResponse<GlcTblIntegration> CreateTargetDocument()
        {
            if (IsDebug == "Y")
                xcommon.ErrorLog("CreateTargetDocument Function called : " + DateTime.Now);

            ApiResponse<GlcTblIntegration> result = new ApiResponse<GlcTblIntegration>();
            int Received_LineId = 0;
            int integration_id = 0;
            double Received_Quantity = 0;
            string Received_shipno = string.Empty;
            string Vehicle_id = string.Empty;
            double U_DocEntry = 0;
            int U_BLineId = 0;

            /*debug windowservice*/
            if (IsDebug == "Y")
                xcommon.ErrorLog("CreateTargetDocument step 1 ");

            string query = "SELECT Id, BatchDate, Vehicle_Id, Shipment_Number, Line_Id, Total_Fuel_Litres, Per_Litre_Amount, Currency, Service_Station_Code, CreatedOn FROM  GLC_tbl_Integration WHERE  (IsProcessed = 'N') AND (Status = 0)";
            var datatable = xcommon.ExecuteNonQueryWithDataTable(query, _connectionString);
            if (datatable != null)
            {
                for (int i = 0; i < datatable.Rows.Count; i++)
                {
                    /*debug windowservice*/
                    if (IsDebug == "Y")
                        xcommon.ErrorLog("CreateTargetDocument step 2 ");

                    Received_LineId = Convert.ToInt32(datatable.Rows[i]["Line_Id"]);
                    integration_id = Convert.ToInt32(datatable.Rows[i]["Id"]);
                    Received_Quantity = Convert.ToDouble(datatable.Rows[i]["Total_Fuel_Litres"]);
                    Received_shipno = datatable.Rows[i]["Shipment_Number"]?.ToString() ?? string.Empty;
                    Vehicle_id = datatable.Rows[i]["Vehicle_Id"]?.ToString() ?? string.Empty;

                    xcommon.ValidateCompanyConnection();
                    SAPbobsCOM.Recordset recordset = (SAPbobsCOM.Recordset)xcommon.SboCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                    /*debug windowservice*/
                    if (IsDebug == "Y")
                        xcommon.ErrorLog("CreateTargetDocument step 3 ValidateCompanyConnection() ");


                    recordset.DoQuery($"EXEC DCC_SP_GLC_INT_GetShipmentData '{Received_shipno}', '{Received_LineId}','{Vehicle_id}' ");
                    if (recordset.RecordCount > 0)
                    {
                        /*debug windowservice*/
                        if (IsDebug == "Y")
                            xcommon.ErrorLog("CreateTargetDocument step 4 DCC_SP_GLC_INT_GetShipmentData called ");

                        if (recordset.Fields.Item("U_BLineId").Value != null && !string.IsNullOrEmpty(recordset.Fields.Item("U_BLineId").Value.ToString()))
                        {
                            U_BLineId = Convert.ToInt32(recordset.Fields.Item("U_BLineId").Value);
                        }

                        if (recordset.Fields.Item("U_DocEntry").Value != null && !string.IsNullOrEmpty(recordset.Fields.Item("U_DocEntry").Value.ToString()))
                        {
                            U_DocEntry = Convert.ToDouble(recordset.Fields.Item("U_DocEntry").Value);
                        }


                        int DocType = recordset.Fields.Item("U_DocType").Value != null ? Convert.ToInt32(recordset.Fields.Item("U_DocType").Value) : 0;
                        if (DocType == 60 && U_DocEntry == 0 && U_BLineId == 0)
                        {
                            /*debug windowservice*/
                            if (IsDebug == "Y")
                                xcommon.ErrorLog("CreateTargetDocument step 5 Goods Issue called ");

                            result = CreateGoodsIssue(recordset, Received_Quantity, Received_LineId, integration_id, Received_shipno);
                        }

                        if (DocType == 22 && U_DocEntry == 0 && U_BLineId == 0)
                        {
                            /*debug windowservice*/
                            if (IsDebug == "Y")
                                xcommon.ErrorLog("CreateTargetDocument step 6 purchase order called ");

                            result = PurchaseOrder(recordset, Received_Quantity, Received_LineId, integration_id, Received_shipno);
                        }

                        if (DocType == 60 && U_DocEntry > 0)
                        {
                            /*debug windowservice*/
                            if (IsDebug == "Y")
                                xcommon.ErrorLog("CreateTargetDocument step 7 Goods Issue for Closed Line called ");

                            SAPbobsCOM.Recordset xrecordset = (SAPbobsCOM.Recordset)xcommon.SboCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            xrecordset.DoQuery($"EXEC DCC_SP_GLC_INT_GetShipmentData_ForClosedLine '{Received_shipno}', '{Received_LineId}','{Vehicle_id}' ");
                            result = CreateGoodsIssue(xrecordset, Received_Quantity, Received_LineId, integration_id, Received_shipno);
                        }
                        if (DocType == 22 && U_DocEntry > 0)
                        {
                            /*debug windowservice*/
                            if (IsDebug == "Y")
                                xcommon.ErrorLog("CreateTargetDocument step 8 purchase order for Closed Line called ");

                            SAPbobsCOM.Recordset xrecordset = (SAPbobsCOM.Recordset)xcommon.SboCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            xrecordset.DoQuery($"EXEC DCC_SP_GLC_INT_GetShipmentData_ForClosedLine '{Received_shipno}', '{Received_LineId}','{Vehicle_id}' ");
                            result = PurchaseOrder(xrecordset, Received_Quantity, Received_LineId, integration_id, Received_shipno);
                        }

                    }
                }
            }

            return result;
        }

        private ApiResponse<GlcTblIntegration> PurchaseOrder(SAPbobsCOM.Recordset recordset, double Received_Quantity, int Received_LineId, int integration_id, string Received_Shipno)
        {
            double csordr5_quantity = 0;
            int U_DocEntry = 0;
            int U_BaseLine = 0;
            string Payload = string.Empty;
            ApiResponse<GlcTblIntegration> res = new ApiResponse<GlcTblIntegration>();
            SAPbobsCOM.Recordset recordset_1 = (SAPbobsCOM.Recordset)xcommon.SboCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            int xresult = 0;

            try
            {
                xcommon.ValidateCompanyConnection();
                SAPbobsCOM.Documents oPurchaseOrder = (SAPbobsCOM.Documents)xcommon.SboCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseOrders);

                /*debug windowservice*/
                if (IsDebug == "Y")
                    xcommon.ErrorLog(" step 9 purchase order ValidateCompanyConnection ");

                oPurchaseOrder.DocDate = DateTime.Now;
                oPurchaseOrder.TaxDate = DateTime.Now;
                oPurchaseOrder.DocDueDate = DateTime.Now;

                oPurchaseOrder.CardCode = recordset.Fields.Item("U_CardCode").Value?.ToString() ?? "";
                oPurchaseOrder.Series = Convert.ToInt32(_configuration["AppConfig:PurchaseOrder_Series"]);
                oPurchaseOrder.DocCurrency = recordset.Fields.Item("U_CurCode").Value?.ToString() ?? "";

                if (recordset.Fields.Item("U_CurRate").Value != null)
                    oPurchaseOrder.DocRate = Convert.ToDouble(recordset.Fields.Item("U_CurRate").Value);

                oPurchaseOrder.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;

                oPurchaseOrder.UserFields.Fields.Item("U_DCCSOID").Value = recordset.Fields.Item("DocEntry").Value?.ToString() ?? "";
                oPurchaseOrder.UserFields.Fields.Item("U_DCC_CNT").Value = recordset.Fields.Item("U_ContNo").Value?.ToString() ?? "";
                oPurchaseOrder.UserFields.Fields.Item("U_DCCFile").Value = recordset.Fields.Item("U_FileNo").Value?.ToString() ?? "";
                oPurchaseOrder.UserFields.Fields.Item("U_DCCSFile").Value = recordset.Fields.Item("U_SubFile").Value?.ToString() ?? "";

                /*debug windowservice*/
                if (IsDebug == "Y")
                    xcommon.ErrorLog(" step 10 purchase order U_FileNo " + recordset.Fields.Item("U_FileNo").Value?.ToString() ?? "");

                string ExpCode = recordset.Fields.Item("U_ExpCode").Value?.ToString() ?? "";

                

                recordset_1.DoQuery("Select U_ItemCode,U_ExpAcct From [@DCC_EXPNS] Where Code = '" + ExpCode + "' ");
                if (recordset_1.RecordCount > 0)
                {
                    /*debug windowservice*/
                    if (IsDebug == "Y")
                        xcommon.ErrorLog(" step 11 purchase order ExpCode " + ExpCode);
                    oPurchaseOrder.Lines.ItemCode = recordset_1.Fields.Item("U_ItemCode").Value?.ToString() ?? "";
                    oPurchaseOrder.Lines.AccountCode = recordset_1.Fields.Item("U_ExpAcct").Value?.ToString() ?? "";
                }
                else
                {
                    res.status = false; res.status_code = 500; res.message = "ItemCode is missing";
                    /*debug windowservice*/
                    if (IsDebug == "Y")
                        xcommon.ErrorLog(" step 11 purchase order " + res.message);
                    return res;
                }

                oPurchaseOrder.Lines.Quantity = Received_Quantity;
                oPurchaseOrder.Lines.Currency = recordset.Fields.Item("U_CurCode").Value?.ToString() ?? "";
                oPurchaseOrder.Lines.ShipDate = Convert.ToDateTime(recordset.Fields.Item("U_ExpDate").Value ?? DateTime.Now);
                oPurchaseOrder.Lines.WarehouseCode = _configuration["AppConfig:PurchaseOrder_wherehouse"];

                if (recordset.Fields.Item("U_Price").Value != null)
                    oPurchaseOrder.Lines.UnitPrice = Convert.ToDouble(recordset.Fields.Item("U_Price").Value);

                oPurchaseOrder.Lines.UserFields.Fields.Item("U_DCCFlQty").Value = Received_Quantity;
                oPurchaseOrder.Lines.UserFields.Fields.Item("U_DCCSOID").Value = recordset.Fields.Item("DocEntry").Value?.ToString() ?? "";
                oPurchaseOrder.Lines.UserFields.Fields.Item("U_DCCVehId").Value = recordset.Fields.Item("truck").Value?.ToString() ?? "";
                oPurchaseOrder.Lines.UserFields.Fields.Item("U_DCC_CNT").Value = recordset.Fields.Item("U_ContNo").Value?.ToString() ?? "";
                oPurchaseOrder.Lines.UserFields.Fields.Item("U_DCCFile").Value = recordset.Fields.Item("U_FileNo").Value?.ToString() ?? "";
                oPurchaseOrder.Lines.UserFields.Fields.Item("U_DCCSFile").Value = recordset.Fields.Item("U_SubFile").Value?.ToString() ?? "";
                oPurchaseOrder.Lines.UserFields.Fields.Item("U_ExpCode").Value = ExpCode;
                oPurchaseOrder.Lines.UserFields.Fields.Item("U_DCCLine").Value = Convert.ToInt32(recordset.Fields.Item("LineId").Value);

                /*debug windowservice*/
                if (IsDebug == "Y")
                    xcommon.ErrorLog(" step 12 purchase order U_VehId" + recordset.Fields.Item("truck").Value?.ToString() ?? "");

                if (recordset.Fields.Item("U_DIM1").Value != null)
                    oPurchaseOrder.Lines.CostingCode = recordset.Fields.Item("U_DIM1").Value?.ToString() ?? "";

                if (recordset.Fields.Item("U_DIM2").Value != null)
                    oPurchaseOrder.Lines.CostingCode2 = recordset.Fields.Item("U_DIM2").Value?.ToString() ?? "";

                if (recordset.Fields.Item("U_DIM3").Value != null)
                    oPurchaseOrder.Lines.CostingCode3 = recordset.Fields.Item("U_DIM3").Value?.ToString() ?? "";

                if (recordset.Fields.Item("U_DIM4").Value != null)
                    oPurchaseOrder.Lines.CostingCode4 = recordset.Fields.Item("U_DIM4").Value?.ToString() ?? "";

                if (recordset.Fields.Item("U_DIM5").Value != null)
                    oPurchaseOrder.Lines.CostingCode5 = recordset.Fields.Item("U_DIM5").Value?.ToString() ?? "";

                if (recordset.Fields.Item("trip_no").Value != null)
                    oPurchaseOrder.Lines.ProjectCode = recordset.Fields.Item("trip_no").Value?.ToString() ?? "";

                /*debug windowservice*/
                if (IsDebug == "Y")
                    xcommon.ErrorLog(" step 12 purchase order trip_no" + recordset.Fields.Item("trip_no").Value?.ToString() ?? "");

                oPurchaseOrder.Lines.ActualDeliveryDate = DateTime.Now;

                if (xcommon.SboCompany.InTransaction == false)
                    xcommon.SboCompany.StartTransaction();

                if (oPurchaseOrder.GetByKey(oPurchaseOrder.DocEntry))
                {
                    xresult = oPurchaseOrder.Update();
                }
                else
                {
                    xresult = oPurchaseOrder.Add();
                    /*debug windowservice*/
                    if (IsDebug == "Y")
                        xcommon.ErrorLog(" step 13 purchase order xresult " + xresult + " " + xcommon.SboCompany.GetLastErrorCode().ToString() + " " + xcommon.SboCompany.GetLastErrorDescription());
                }

                if (xresult != 0)
                {
                    if (xcommon.SboCompany.InTransaction == true)
                    {
                        xcommon.SboCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                        res.status = false; res.status_code = 500; res.message = xcommon.SboCompany.GetLastErrorCode().ToString() + " " + xcommon.SboCompany.GetLastErrorDescription();
                        Payload = Newtonsoft.Json.JsonConvert.SerializeObject(res);
                        xcommon.ExecuteNonQuery("UPDATE GLC_tbl_Integration SET IsProcessed = 'Y' , Status = 0 , Response = '" + Payload + "'  WHERE Shipment_Number = '" + Received_Shipno + "' AND Id = '" + integration_id + "'  ", _connectionString);
                    }
                }
                else
                {
                    String Dockey = xcommon.SboCompany.GetNewObjectKey();

                    if (recordset.Fields.Item("U_Qty").Value != null)
                        csordr5_quantity = Convert.ToDouble(recordset.Fields.Item("U_Qty").Value);

                    if (recordset.Fields.Item("U_DocEntry").Value != null && !string.IsNullOrWhiteSpace(recordset.Fields.Item("U_DocEntry").Value.ToString()))
                        U_DocEntry = Convert.ToInt32(recordset.Fields.Item("U_DocEntry").Value);

                    

                    /* CASE A */
                    if (Received_Quantity >= csordr5_quantity && U_DocEntry == 0)
                    {
                       
                        res.status = true; res.message = "Po Created Successfully"; res.status_code = 200; res.DocKey = Dockey;
                        Payload = Newtonsoft.Json.JsonConvert.SerializeObject(res);
                        xcommon.ExecuteNonQuery("UPDATE [@DCC_CSORDR5] SET U_IssQty = '" + Received_Quantity + "' , U_DocEntry = '" + Dockey + "'  WHERE DocEntry = '" + Received_Shipno + "' AND LineId = '" + (Convert.ToInt32(recordset.Fields.Item("LineId").Value)) + "' ", _connectionString);
                        xcommon.ExecuteNonQuery("UPDATE GLC_tbl_Integration SET IsProcessed = 'Y' , Status = 1 , Response = '" + Payload + "' , DocumentType = '22' , TargetDocument = '" + Dockey + "' WHERE Shipment_Number = '" + Received_Shipno + "' AND Id = '" + integration_id + "'  ", _connectionString);

                        /*debug windowservice*/
                        if (IsDebug == "Y")
                            xcommon.ErrorLog("step 14 purchase order Case A" + Received_Quantity + " " + csordr5_quantity + " " + res.message + " " + Dockey);
                    }

                    /* CASE B */

                    if (Received_Quantity < csordr5_quantity && U_DocEntry == 0)
                    {
                        /*debug windowservice*/
                        if (IsDebug == "Y")
                            xcommon.ErrorLog("Case B Called step 14 purchase order " + Received_Quantity + " " + csordr5_quantity +" " + Dockey);

                        double RemainingQty = csordr5_quantity - Received_Quantity;
                        xcommon.ExecuteNonQuery("UPDATE [@DCC_CSORDR5] SET U_IssQty = '" + Received_Quantity + "' , U_DocEntry = '" + Dockey + "'  WHERE DocEntry = '" + Received_Shipno + "' AND LineId = '" + Convert.ToInt32(recordset.Fields.Item("LineId").Value) + "' ", _connectionString);

                        var x = AddShipmentOrderExpenses(recordset, RemainingQty, Received_LineId, Received_Shipno);
                        if (x == "success")
                        {
                            res.status = true; res.message = "Po Created Successfully"; res.status_code = 200; res.DocKey = Dockey;
                            Payload = Newtonsoft.Json.JsonConvert.SerializeObject(res);

                            xcommon.ExecuteNonQuery("UPDATE GLC_tbl_Integration SET IsProcessed = 'Y' , Status = 1 , Response = '" + Payload + "' , DocumentType = '22' , TargetDocument = '" + Dockey + "' WHERE Shipment_Number = '" + Received_Shipno + "' AND Id = '" + integration_id + "'  ", _connectionString);

                            /*debug windowservice*/
                            if (IsDebug == "Y")
                                xcommon.ErrorLog("step 14 purchase order case B " + x + " " + res.message);
                        }
                        /*debug windowservice*/
                        if (IsDebug == "Y")
                            xcommon.ErrorLog("step 15 purchase order case B  for Addshipment " + x );
                    }
                    if (xcommon.SboCompany.InTransaction == true)
                    {
                        xcommon.SboCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                        /*debug windowservice*/
                        if (IsDebug == "Y")
                            xcommon.ErrorLog(" step 16 purchase order for commit  ");
                    }
                }
            }
            catch (Exception ex)
            {
                res.status = false; res.status_code = 500; res.message = ex.Message;
                /*debug windowservice*/
                if (IsDebug == "Y")
                    xcommon.ErrorLog("step 16 purchase order Exception" + ex.Message +" "+ res.message);
            }

            return res;
        }

        private ApiResponse<GlcTblIntegration> CreateGoodsIssue(SAPbobsCOM.Recordset recordset, double Received_Quantity, int Received_LineId, int integration_id, string Received_Shipno)
        {
            double csordr5_quantity = 0;
            int U_DocEntry = 0;
            int U_BaseLine = 0;
            string Payload = string.Empty;
            ApiResponse<GlcTblIntegration> res = new ApiResponse<GlcTblIntegration>();
            SAPbobsCOM.Recordset recordset_1 = (SAPbobsCOM.Recordset)xcommon.SboCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            int xresult = 0;

            try
            {
                xcommon.ValidateCompanyConnection();
                oGoodsIssue = (SAPbobsCOM.Documents)xcommon.SboCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenExit);

                /*debug windowservice*/
                if (IsDebug == "Y")
                    xcommon.ErrorLog(" step 9 oGoodsIssue ValidateCompanyConnection ");

                oGoodsIssue.DocDate = DateTime.Now;
                oGoodsIssue.TaxDate = DateTime.Now;
                oGoodsIssue.Series = Convert.ToInt32(_configuration["AppConfig:GoodIssue_Series"]);

                string ExpCode = recordset.Fields.Item("U_ExpCode").Value?.ToString() ?? "";
                recordset_1.DoQuery("Select U_ItemCode,U_ExpAcct From [@DCC_EXPNS] Where Code = '" + ExpCode + "' ");
                if (recordset_1.RecordCount > 0)
                {
              
                    oGoodsIssue.Lines.ItemCode = recordset_1.Fields.Item("U_ItemCode").Value?.ToString() ?? "";
                    oGoodsIssue.Lines.AccountCode = recordset_1.Fields.Item("U_ExpAcct").Value?.ToString() ?? "";

                    /*debug windowservice*/
                    if (IsDebug == "Y")
                        xcommon.ErrorLog(" step 10 oGoodsIssue ExpCode " + ExpCode);
                }
                else
                {
                    res.status = false; res.status_code = 500; res.message = "ItemCode is missing";
                    /*debug windowservice*/
                    if (IsDebug == "Y")
                        xcommon.ErrorLog(" step 10 oGoodsIssue  " + res.message);
                    return res;
                }

                oGoodsIssue.Lines.Quantity = Received_Quantity;

                if (recordset.Fields.Item("U_Price").Value != null)
                    oGoodsIssue.Lines.UnitPrice = Convert.ToDouble(recordset.Fields.Item("U_Price").Value);

                oGoodsIssue.Lines.WarehouseCode = _configuration["AppConfig:GoodIssue_wharehouse"];

                if (recordset.Fields.Item("U_DIM1").Value != null)
                    oGoodsIssue.Lines.CostingCode = recordset.Fields.Item("U_DIM1").Value?.ToString() ?? "";

                if (recordset.Fields.Item("U_DIM2").Value != null)
                    oGoodsIssue.Lines.CostingCode2 = recordset.Fields.Item("U_DIM2").Value?.ToString() ?? "";

                if (recordset.Fields.Item("U_DIM3").Value != null)
                    oGoodsIssue.Lines.CostingCode3 = recordset.Fields.Item("U_DIM3").Value?.ToString() ?? "";

                if (recordset.Fields.Item("U_DIM4").Value != null)
                    oGoodsIssue.Lines.CostingCode4 = recordset.Fields.Item("U_DIM4").Value?.ToString() ?? "";

                if (recordset.Fields.Item("U_DIM5").Value != null)
                    oGoodsIssue.Lines.CostingCode5 = recordset.Fields.Item("U_DIM5").Value?.ToString() ?? "";

                if (recordset.Fields.Item("trip_no").Value != null)
                    oGoodsIssue.Lines.ProjectCode = recordset.Fields.Item("trip_no").Value?.ToString() ?? "";


                oGoodsIssue.Lines.ActualDeliveryDate = DateTime.Now;

                oGoodsIssue.Lines.UserFields.Fields.Item("U_DCCVehId").Value = recordset.Fields.Item("truck").Value?.ToString() ?? "";

                string vehID= recordset.Fields.Item("truck").Value?.ToString() ?? "";

                /*debug windowservice*/
                if (IsDebug == "Y")
                    xcommon.ErrorLog(" step 11 oGoodsIssue U_VehId " + vehID + " tripno " + recordset.Fields.Item("trip_no").Value?.ToString() ?? "");

                oGoodsIssue.Lines.UserFields.Fields.Item("U_DCCSOID").Value = recordset.Fields.Item("DocEntry").Value?.ToString() ?? "";

                oGoodsIssue.Lines.UserFields.Fields.Item("U_ExpCode").Value = ExpCode;
                oGoodsIssue.Lines.UserFields.Fields.Item("U_DCCLine").Value = Convert.ToInt32(recordset.Fields.Item("LineId").Value);

                if (xcommon.SboCompany.InTransaction == false)
                    xcommon.SboCompany.StartTransaction();

                if (oGoodsIssue.GetByKey(oGoodsIssue.DocEntry))
                {
                    xresult = oGoodsIssue.Update();
                }
                else
                {
                    xresult = oGoodsIssue.Add();
                    /*debug windowservice*/
                    if (IsDebug == "Y")
                        xcommon.ErrorLog(" step 12 oGoodsIssueAdd  " + xresult +" "+ xcommon.SboCompany.GetLastErrorCode().ToString() + " " + xcommon.SboCompany.GetLastErrorDescription());
                }

                if (xresult != 0)
                {
                    if (xcommon.SboCompany.InTransaction == true)
                    {
                        xcommon.SboCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                        res.status = false; res.status_code = 500; res.message = xcommon.SboCompany.GetLastErrorCode().ToString() + " " + xcommon.SboCompany.GetLastErrorDescription();
                        Payload = Newtonsoft.Json.JsonConvert.SerializeObject(res);
                        xcommon.ExecuteNonQuery("UPDATE GLC_tbl_Integration SET IsProcessed = 'Y' , Status = 0 , Response = '" + Payload + "'  WHERE Shipment_Number = '" + Received_Shipno + "' AND Id = '" + integration_id + "'  ", _connectionString);
                    }
                }
                else
                {
                    String Dockey = xcommon.SboCompany.GetNewObjectKey();

                    if (recordset.Fields.Item("U_Qty").Value != null)
                        csordr5_quantity = Convert.ToDouble(recordset.Fields.Item("U_Qty").Value);

                    if (recordset.Fields.Item("U_DocEntry").Value != null && !string.IsNullOrWhiteSpace(recordset.Fields.Item("U_DocEntry").Value.ToString()))
                        U_DocEntry = Convert.ToInt32(recordset.Fields.Item("U_DocEntry").Value);


                    /* CASE A */
                    if (Received_Quantity >= csordr5_quantity && U_DocEntry == 0)
                    {
                        res.status = true; res.message = "Good Issue Created Successfully"; res.status_code = 200; res.DocKey = Dockey;
                        Payload = Newtonsoft.Json.JsonConvert.SerializeObject(res);

                        xcommon.ExecuteNonQuery("UPDATE [@DCC_CSORDR5] SET U_IssQty = '" + Received_Quantity + "' , U_DocEntry = '" + Dockey + "'  WHERE DocEntry = '" + Received_Shipno + "' AND LineId = '" + (Convert.ToInt32(recordset.Fields.Item("LineId").Value)) + "' ", _connectionString);
                        xcommon.ExecuteNonQuery("UPDATE GLC_tbl_Integration SET IsProcessed = 'Y' , Status = 1 , Response = '" + Payload + "' , DocumentType = '60' , TargetDocument = '" + Dockey + "' WHERE Shipment_Number = '" + Received_Shipno + "' AND Id = '" + integration_id + "'  ", _connectionString);

                        /*debug windowservice*/
                        if (IsDebug == "Y")
                            xcommon.ErrorLog(" step 13 oGoodsIssueAdd  " + res.message + " " + Received_Quantity + " " + csordr5_quantity);
                    }

                    /* CASE B */

                    if (Received_Quantity < csordr5_quantity && U_DocEntry == 0)
                    {
                        double RemainingQty = csordr5_quantity - Received_Quantity;
                        xcommon.ExecuteNonQuery("UPDATE [@DCC_CSORDR5] SET U_IssQty = '" + Received_Quantity + "' , U_DocEntry = '" + Dockey + "'  WHERE DocEntry = '" + Received_Shipno + "' AND LineId = '" + (Convert.ToInt32(recordset.Fields.Item("LineId").Value)) + "' ", _connectionString);


                        var x = AddShipmentOrderExpenses(recordset, RemainingQty, Received_LineId, Received_Shipno);
                        if (x == "success")
                        {
                            res.status = true; res.message = "Good Issue Created Successfully"; res.status_code = 200; res.DocKey = Dockey;
                            Payload = Newtonsoft.Json.JsonConvert.SerializeObject(res);

                            /*debug windowservice*/
                            if (IsDebug == "Y")
                                xcommon.ErrorLog(" step 13 oGoodsIssueAdd  " + res.message + " " + Received_Quantity + " " + csordr5_quantity + ""+ x);

                            xcommon.ExecuteNonQuery("UPDATE GLC_tbl_Integration SET IsProcessed = 'Y' , Status = 1 , Response = '" + Payload + "' , DocumentType = '60' , TargetDocument = '" + Dockey + "' WHERE Shipment_Number = '" + Received_Shipno + "' AND Id = '" + integration_id + "'  ", _connectionString);
                            /*debug windowservice*/
                            if (IsDebug == "Y")
                                xcommon.ErrorLog(" step 14 oGoodsIssue Update GLC");
                        }
                    }

                    if (xcommon.SboCompany.InTransaction == true)
                    {
                        xcommon.SboCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                        /*debug windowservice*/
                        if (IsDebug == "Y")
                            xcommon.ErrorLog(" step 15 oGoodsIssue Commit");
                    }
                }
            }
            catch (Exception ex)
            {
                res.status = false; res.status_code = 500; res.message = ex.Message;

                /*debug windowservice*/
                if (IsDebug == "Y")
                    xcommon.ErrorLog("step 15 Good Issue Exception" + ex.Message + " "+res.message);
            }
            return res;
        }

        private string AddShipmentOrderExpenses(SAPbobsCOM.Recordset recordset, double RemainingQty, int Received_LineId, string Received_ShipNo)
        {
            string result = "success";
            try
            {
                CompanyService companyService = xcommon.SboCompany.GetCompanyService();
                GeneralService generalService = (GeneralService)companyService.GetGeneralService("DCCSORDR");
                GeneralDataParams dataParams = (GeneralDataParams)generalService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);

                dataParams.SetProperty("DocEntry", Received_ShipNo);
                GeneralData generalData = generalService.GetByParams(dataParams);
                GeneralDataCollection children = generalData.Child("DCC_CSORDR5");
                GeneralData childRow = children.Add();

                /*debug windowservice*/
                if (IsDebug == "Y")
                    xcommon.ErrorLog("  AddShipmentOrderExpenses called 1");

                childRow.SetProperty("U_ExpCode", recordset.Fields.Item("U_ExpCode").Value?.ToString() ?? "");
                childRow.SetProperty("U_ExpName", recordset.Fields.Item("U_ExpName").Value?.ToString() ?? "");
                childRow.SetProperty("U_FundAct", recordset.Fields.Item("U_FundAct").Value?.ToString() ?? "");

                if (recordset.Fields.Item("U_CurRate").Value != null)
                    childRow.SetProperty("U_CurRate", Convert.ToDouble(recordset.Fields.Item("U_CurRate").Value));

                if (recordset.Fields.Item("U_OrgQty").Value != null)
                    childRow.SetProperty("U_OrgQty", Convert.ToDouble(recordset.Fields.Item("U_OrgQty").Value));

                /*debug windowservice*/
                if (IsDebug == "Y")
                    xcommon.ErrorLog("  AddShipmentOrderExpenses 2 U_CurRate " + Convert.ToDouble(recordset.Fields.Item("U_CurRate").Value)+" "+ Convert.ToDouble(recordset.Fields.Item("U_OrgQty").Value));

                childRow.SetProperty("U_ContNo", recordset.Fields.Item("U_ContNo").Value?.ToString() ?? "");
                childRow.SetProperty("U_Qty", RemainingQty);
                childRow.SetProperty("U_CurCode", recordset.Fields.Item("U_CurCode").Value?.ToString() ?? "");

                if (recordset.Fields.Item("U_Price").Value != null)
                    childRow.SetProperty("U_Price", Convert.ToDouble(recordset.Fields.Item("U_Price").Value));

                /*debug windowservice*/
                if (IsDebug == "Y")
                    xcommon.ErrorLog("  AddShipmentOrderExpenses 3 U_Price " + Convert.ToDouble(recordset.Fields.Item("U_Price").Value));

                Double _totalAmnt = (Convert.ToDouble(recordset.Fields.Item("U_Qty").Value) - Convert.ToDouble(recordset.Fields.Item("U_IssQty").Value)) * Convert.ToDouble(recordset.Fields.Item("U_Price").Value);
                childRow.SetProperty("U_TotAmnt", Convert.ToDouble(_totalAmnt));
                childRow.SetProperty("U_DueAmnt", "0");

                childRow.SetProperty("U_CardCode", recordset.Fields.Item("U_CardCode").Value?.ToString() ?? "");
                childRow.SetProperty("U_DocType", recordset.Fields.Item("U_DocType").Value?.ToString() ?? "");
                childRow.SetProperty("U_BLineId", Received_LineId);

                /*debug windowservice*/
                if (IsDebug == "Y")
                    xcommon.ErrorLog(" AddShipmentOrderExpenses 4 Received_LineId = " + Received_LineId);

                generalService.Update(generalData);

                /*debug windowservice*/
                if (IsDebug == "Y")
                    xcommon.ErrorLog("  AddShipmentOrderExpensesUpdate 5 "+ result);

                return result;
            }
            catch (Exception ex)
            {
                /*debug windowservice*/
                if (IsDebug == "Y")
                    xcommon.ErrorLog("  AddShipmentOrderExpenses Exception " + ex.Message);
                return result = ex.Message;
            }
        }
    }
}
