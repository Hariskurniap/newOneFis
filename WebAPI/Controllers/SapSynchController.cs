using Application.Interfaces;
using Common.Responses;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("/LoadingOrder/api/[controller]")]
[Authorize]
//[ApiExplorerSettings(GroupName = "LoadingOrder")]
public class SapSynchController : ControllerBase
{
    private readonly ISapSynchService _sapSynchService;
    private readonly IMongoRepository _mongoRepository;

    public SapSynchController(
        ISapSynchService sapSynchService,
        IMongoRepository mongoRepository)
    {
        _sapSynchService = sapSynchService;
        _mongoRepository = mongoRepository;
    }

    [HttpPost("GetDOWithDetail")]
    public async Task<IActionResult> GetDOWithDetail([FromBody] MinimalListDORequest request)
    {
        
        var sapToken = await _sapSynchService.GetAccessTokenAsync();

        var fullRequest = new ListDORequest
        {
            Depot = "",
            Shipping_point = request.Shipping_point,
            Delivery_date_From = "01012025",  // bisa diganti dinamis sesuai kebutuhan
            Delivery_date_To = "26062025",
            Ship_to = "",
            Delivery_time_From = "000000",
            Delivery_time_to = "235959",
            DO_Type = "",
            Delivery_Number = request.Delivery_Number,
            GI_Status = request.GI_Status
        };

        var listDoJson = await _sapSynchService.GetListDOFromSapAsync(fullRequest, sapToken);
        var listDoResponse = JsonSerializer.Deserialize<SapListDOResponse>(listDoJson);

        if (listDoResponse?.mt_getListDOResponse?.Details == null || !listDoResponse.mt_getListDOResponse.Details.Any())
            return NotFound(ApiResponse<object>.NotFound("Data kosong"));

        var firstDO = listDoResponse.mt_getListDOResponse.Details[0];

        var detailDoParams = new
        {
            Plant = fullRequest.Shipping_point,
            Delivery_Number = firstDO.Delivery_Number
        };

        var detailDoJson = await _sapSynchService.GetDetailDOFromSapAsync(detailDoParams, sapToken);
        var detailDoResponse = JsonSerializer.Deserialize<SapDetailDO>(detailDoJson);

        var customerParams = new
        {
            Ship_to_Code = firstDO.Ship_to?.Trim(),
            Sales_Organization = firstDO.Sales_org,
            Distribution_Channel = firstDO.Distribution_Channel
        };

        var customerJson = await _sapSynchService.GetCustomerFromSapAsync(customerParams, sapToken);
        var customerResponse = JsonSerializer.Deserialize<SapCustomerResponse>(customerJson);

        var result = new
        {
            ListDO = listDoResponse,
            DetailDO = detailDoResponse,
            Customer = customerResponse
        };

        return Ok(ApiResponse<object>.Success(result));
    }

    [HttpPost("ModifyDOWithDetail")]
    public async Task<IActionResult> ModifyDOWithDetail([FromBody] MinimalListDORequest request)
    {
        var sapToken = await _sapSynchService.GetAccessTokenAsync();
        DateTime now = DateTime.UtcNow;

        var fullRequest = new ListDORequest
        {
            Depot = "",
            Shipping_point = request.Shipping_point,
            Delivery_date_From = now.AddDays(-2).ToString("ddMMyyyy"),
            Delivery_date_To = now.AddDays(2).ToString("ddMMyyyy"),
            Ship_to = "",
            Delivery_time_From = "000000",
            Delivery_time_to = "235959",
            DO_Type = "",
            Delivery_Number = request.Delivery_Number,
            GI_Status = request.GI_Status
        };

        var listDoJson = await _sapSynchService.GetListDOFromSapAsync(fullRequest, sapToken);
        var listDoResponse = JsonSerializer.Deserialize<SapListDOResponse>(listDoJson);

        if (listDoResponse?.mt_getListDOResponse?.Details == null || !listDoResponse.mt_getListDOResponse.Details.Any())
            return NotFound(ApiResponse<object>.NotFound("No DO found in SAP"));

        var listDO = listDoResponse.mt_getListDOResponse.Details[0];

        var detailDoParams = new
        {
            Plant = fullRequest.Shipping_point,
            Delivery_Number = listDO.Delivery_Number
        };

        var detailJson = await _sapSynchService.GetDetailDOFromSapAsync(detailDoParams, sapToken);
        var detailResponse = JsonSerializer.Deserialize<SapDetailDO>(detailJson);

        if (detailResponse?.mt_getDetailDOResponse?.DeliveryDetails == null)
            return NotFound(ApiResponse<object>.NotFound("No DO Detail found in SAP"));

        var detail = detailResponse.mt_getDetailDOResponse.DeliveryDetails;

        var customerParams = new
        {
            Ship_to_Code = listDO.Ship_to?.Trim(),
            Sales_Organization = listDO.Sales_org,
            Distribution_Channel = listDO.Distribution_Channel
        };

        var customerJson = await _sapSynchService.GetCustomerFromSapAsync(customerParams, sapToken);
        var customerResponse = JsonSerializer.Deserialize<SapCustomerResponse>(customerJson);

        var customer = customerResponse?.mt_getCustomerResponse?.Details?.FirstOrDefault();
        if (customer == null)
            return NotFound(ApiResponse<object>.NotFound("No Customer found in SAP"));

        var exists = await _mongoRepository.CheckDOExistAsync(listDO.Delivery_Number, detail.Plant);
        if (exists)
            return Conflict(ApiResponse<object>.Conflict("Data with the specified delivery number and plant already exists."));

        var userId = "system"; // bisa disesuaikan jika ada user context

        var mappedData = new ListDO
        {
            DeliveryNumber = listDO.Delivery_Number,
            Plant = detail.Plant,
            ActualGiDate = detail.Actual_GI_Date,
            ActualGiTime = detail.Actual_GI_TIme,
            ConditionDelivery = detail.Condition_Delivery,
            ConditionShipfrom = detail.Condition_Ship_from,
            ConditionShipping = detail.Condition_Shipping,
            CustomerPoDate = detail.Customer_PO_Date,
            CustomerPoNumber = detail.Customer_PO_Number,
            DeliveryDate = detail.Delivery_Date,
            DeliveryQty = listDO.Delivery_Qty?.Trim(),
            DistributionChannel = listDO.Distribution_Channel,
            MaterialCode = listDO.Material_code,
            MaterialName = listDO.Material_name,
            DriverNumber = new List<DriverInfo>
            {
                new DriverInfo { DriverId = "", DriverName = "" }
            },
            GiStatus = listDO.GI_Status,
            GiStatusDesc = listDO.GI_Status_Desc,
            Items = detail.Items?.Select(i => new Item
            {
                Density = "",
                DensityUom = "",
                ItemNumber = i.Item_number,
                Material = i.Material,
                MaterialName = i.Material_Name,
                Qty = i.Qty,
                QtyUom = i.QTY_UOM,
                Temp = "",
                TempUom = "",
                Weight = i.Weight,
                WeightUom = i.Weight_UOM
            }).ToList() ?? new List<Item>(),
            Netweight = detail.Net_weight,
            OnefisCreatedAt = now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            OnefisCreatedBy = userId,
            OnefisDoStatus = "0",
            OnefisDoStatusDesc = "Open",
            OnefisEvidenceDeletedDos = new List<string>(),
            OnefisGetDetailCreatedBy = userId,
            OnefisGetDetailUpdatedBy = "",
            OnefisRemarkDeletedDo = " ",
            OnefisUpdatedAt = null,
            OnefisUpdatedBy = "",
            OnfisGetDetailCreatedAt = now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            OnfisGetDetailUpdatedAt = null,
            PlannedGiDate = listDO.Planned_GI_Date,
            SalesOrder = detail.Order_Number,
            SalesOrderDate = detail.Order_Date,
            SalesOrg = listDO.Sales_org,
            ShipmentNumber = listDO.Shipment_Number,
            ShippingPoint = listDO.Shipping_point,
            ShipToAddress = detail.Ship_to_address,
            ShipToCity = detail.Ship_to_City,
            ShipToCode = listDO.Ship_to?.Trim(),
            ShipToName = listDO.Ship_name,
            ShipToPostalCode = detail.Ship_to_postalcode,
            TotalVolume = detail.Total_Volume,
            TotalWeight = detail.Total_Weight,
            Transporter = listDO.Transporter,
            VehicleNumber = listDO.Vehicle_Number,
            VolumeUom = detail.Volume_UOM,
            WeightUom = detail.Weight_UOM,
            AdditionalProp1 = new List<string>(),
            SoldToCode = customer.Sold_to_code?.Trim(),
            SoldToDescription = customer.Sold_to_description,
            DepotCode = customer.Depot,
            DepotDesc = customer.Desc_Depot,
            CustomerGroup = customer.Customer_Group,
            SpbuCode = customer.Name2 != null
                ? customer.Name2.Replace("SPBU", "").Replace(".", "").Replace(" ", "")
                : ""
        };

        await _mongoRepository.InsertListDOAsync(mappedData);

        var result = new
        {
            Message = "Insert successful",
            DeliveryNumber = listDO.Delivery_Number
        };

        return Ok(ApiResponse<object>.Success(result, "Insert successful"));
    }

    [HttpPost("BulkSyncDO")]
    public async Task<IActionResult> BulkSyncDOByShippingPoint([FromBody] SynchListDORequest request)
    {
        var sapToken = await _sapSynchService.GetAccessTokenAsync();

        DateTime now = DateTime.UtcNow;

        var fullRequest = new ListDORequest
        {
            Depot = "",
            Shipping_point = request.Shipping_point,
            Delivery_date_From = now.AddDays(-2).ToString("ddMMyyyy"),
            Delivery_date_To = now.AddDays(2).ToString("ddMMyyyy"),
            Ship_to = "",
            Delivery_time_From = "000000",
            Delivery_time_to = "235959",
            DO_Type = "",
            Delivery_Number = "",
            GI_Status = "A"
        };

        var listDoJson = await _sapSynchService.GetListDOFromSapAsync(fullRequest, sapToken);
        var listDoResponse = JsonSerializer.Deserialize<SapListDOResponse>(listDoJson);

        var doList = listDoResponse?.mt_getListDOResponse?.Details;

        if (doList == null || !doList.Any())
            return NotFound(ApiResponse<object>.NotFound("No DO found in SAP"));

        var insertList = new List<ListDO>();
        var inserted = new List<string>();
        var skipped = new List<string>();

        foreach (var listDO in doList)
        {
            var detailParams = new
            {
                Plant = request.Shipping_point,
                Delivery_Number = listDO.Delivery_Number
            };

            var detailJson = await _sapSynchService.GetDetailDOFromSapAsync(detailParams, sapToken);
            var detailResponse = JsonSerializer.Deserialize<SapDetailDO>(detailJson);
            var detail = detailResponse?.mt_getDetailDOResponse?.DeliveryDetails;

            if (detail == null)
            {
                skipped.Add($"{listDO.Delivery_Number} - Detail DO kosong");
                continue;
            }

            var customerParams = new
            {
                Ship_to_Code = listDO.Ship_to?.Trim(),
                Sales_Organization = listDO.Sales_org,
                Distribution_Channel = listDO.Distribution_Channel
            };

            var customerJson = await _sapSynchService.GetCustomerFromSapAsync(customerParams, sapToken);
            var customerResponse = JsonSerializer.Deserialize<SapCustomerResponse>(customerJson);
            var customer = customerResponse?.mt_getCustomerResponse?.Details?.FirstOrDefault();

            if (customer == null)
            {
                skipped.Add($"{listDO.Delivery_Number} - Customer tidak ditemukan");
                continue;
            }

            var exists = await _mongoRepository.CheckDOExistAsync(listDO.Delivery_Number, detail.Plant);
            if (exists)
            {
                skipped.Add($"{listDO.Delivery_Number} - Sudah ada");
                continue;
            }

            var userId = "system"; // bisa diganti sesuai user context

            var mappedData = new ListDO
            {
                DeliveryNumber = listDO.Delivery_Number,
                Plant = detail.Plant,
                ActualGiDate = detail.Actual_GI_Date,
                ActualGiTime = detail.Actual_GI_TIme,
                ConditionDelivery = detail.Condition_Delivery,
                ConditionShipfrom = detail.Condition_Ship_from,
                ConditionShipping = detail.Condition_Shipping,
                CustomerPoDate = detail.Customer_PO_Date,
                CustomerPoNumber = detail.Customer_PO_Number,
                DeliveryDate = detail.Delivery_Date,
                DeliveryQty = listDO.Delivery_Qty?.Trim(),
                DistributionChannel = listDO.Distribution_Channel,
                MaterialCode = listDO.Material_code,
                MaterialName = listDO.Material_name,
                DriverNumber = new List<DriverInfo>
                {
                    new DriverInfo { DriverId = listDO.Driver_Number, DriverName = "" }
                },
                GiStatus = listDO.GI_Status,
                GiStatusDesc = listDO.GI_Status_Desc,
                Items = detail.Items?.Select(i => new Item
                {
                    Density = "",
                    DensityUom = "",
                    ItemNumber = i.Item_number,
                    Material = i.Material,
                    MaterialName = i.Material_Name,
                    Qty = i.Qty,
                    QtyUom = i.QTY_UOM,
                    Temp = "",
                    TempUom = "",
                    Weight = i.Weight,
                    WeightUom = i.Weight_UOM
                }).ToList() ?? new List<Item>(),
                Netweight = detail.Net_weight,
                OnefisCreatedAt = now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                OnefisCreatedBy = userId,
                OnefisDoStatus = "0",
                OnefisDoStatusDesc = "Open",
                OnefisEvidenceDeletedDos = new List<string>(),
                OnefisGetDetailCreatedBy = userId,
                OnefisGetDetailUpdatedBy = "",
                OnefisRemarkDeletedDo = " ",
                OnefisUpdatedAt = null,
                OnefisUpdatedBy = "",
                OnfisGetDetailCreatedAt = now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                OnfisGetDetailUpdatedAt = null,
                PlannedGiDate = listDO.Planned_GI_Date,
                SalesOrder = detail.Order_Number,
                SalesOrderDate = detail.Order_Date,
                SalesOrg = listDO.Sales_org,
                ShipmentNumber = listDO.Shipment_Number,
                ShippingPoint = listDO.Shipping_point,
                ShipToAddress = detail.Ship_to_address,
                ShipToCity = detail.Ship_to_City,
                ShipToCode = listDO.Ship_to?.Trim(),
                ShipToName = listDO.Ship_name,
                ShipToPostalCode = detail.Ship_to_postalcode,
                TotalVolume = detail.Total_Volume,
                TotalWeight = detail.Total_Weight,
                Transporter = listDO.Transporter,
                VehicleNumber = listDO.Vehicle_Number,
                VolumeUom = detail.Volume_UOM,
                WeightUom = detail.Weight_UOM,
                AdditionalProp1 = new List<string>(),
                SoldToCode = customer.Sold_to_code?.Trim(),
                SoldToDescription = customer.Sold_to_description,
                DepotCode = customer.Depot,
                DepotDesc = customer.Desc_Depot,
                CustomerGroup = customer.Customer_Group,
                SpbuCode = customer.Name2 != null
                    ? customer.Name2.Replace("SPBU", "").Replace(".", "").Replace(" ", "")
                    : ""
            };

            insertList.Add(mappedData);
            inserted.Add(listDO.Delivery_Number);
        }

        if (insertList.Any())
            await _mongoRepository.InsertManyAsync(insertList);

        var result = new
        {
            Inserted = inserted,
            Skipped = skipped
        };

        return Ok(ApiResponse<object>.Success(result));
    }
}