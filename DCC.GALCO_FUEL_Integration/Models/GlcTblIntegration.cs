using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DCC.GALCO_FUEL_Integration.Models;

public partial class GlcTblIntegration
{
    public int Id { get; set; }

    public string? BatchDate { get; set; }

    public string? VehicleId { get; set; }

    public string? ShipmentNumber { get; set; }

    public string? LineId { get; set; }

    public decimal? TotalFuelLitres { get; set; }

    public decimal? PerLitreAmount { get; set; }

    public string? Currency { get; set; }

    public string? ServiceStationCode { get; set; }


}
