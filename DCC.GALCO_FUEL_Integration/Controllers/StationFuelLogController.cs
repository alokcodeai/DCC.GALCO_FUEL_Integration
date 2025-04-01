using DCC.GALCO_FUEL_Integration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using SAPbobsCOM;
using System;
using System.Data;
using System.Net.Sockets;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace DCC.GALCO_FUEL_Integration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationFuelLogController : ControllerBase
    {
        private readonly Common xcommon;
        private readonly IConfiguration _configuration;
        SAPbobsCOM.Documents oGoodsIssue = null;
        string _connectionString = "";
        /*DocumentCreation _obj;*/
        public StationFuelLogController(IConfiguration configuration)
        {
            _configuration = configuration;
            xcommon = new Common(configuration);
            _connectionString = _configuration.GetConnectionString("DBConnection");
           /* _obj = new DocumentCreation(configuration);*/
        }

        [HttpGet("GetData")]
        public string GetData()
        {
            return "Success";
        }


        [HttpPost("SaveFuelLog")]
        public ApiResponse<GlcTblIntegration> SaveFuelLog(GlcTblIntegration obj)
        {
            ApiResponse<GlcTblIntegration> result = new ApiResponse<GlcTblIntegration>();

             /*result = _obj.CreateTargetDocument();*/

            if (obj == null)
            {
                result.status = false; result.message = "Object can not be empty."; result.status_code = 500; return result;
            }
            if (obj.ShipmentNumber == "" || obj.LineId == "" || obj.VehicleId == "")
            {
                result.status = false; result.message = "Object can not be empty."; result.status_code = 500; return result;
            }

            try
            {
                string Payload = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                string query = "INSERT INTO GLC_tbl_Integration (BatchDate, Vehicle_Id, Shipment_Number, Line_Id, Total_Fuel_Litres, Per_Litre_Amount, Currency, Service_Station_Code, CreatedOn, IsProcessed,Status,Payload) VALUES ('" + obj.BatchDate + "','" + obj.VehicleId + "','" + obj.ShipmentNumber + "', '" + obj.LineId + "', '" + obj.TotalFuelLitres + "','" + obj.PerLitreAmount + "','" + obj.Currency + "', '" + obj.ServiceStationCode + "', '" + DateTime.Now + "', 'N','0','" + Payload + "') ; SELECT SCOPE_IDENTITY(); ";
                var integration_id = xcommon.ExecuteScalar(query, _connectionString);
                if (integration_id != null)
                {
                    result.status = true; result.message = "success"; result.DocKey = integration_id.ToString(); result.status_code = 200;
                }
            }
            catch (Exception ex)
            {
                result.status = false; result.message = ex.Message; result.status_code = 500;
            }

            return result;

        }

    }
}
