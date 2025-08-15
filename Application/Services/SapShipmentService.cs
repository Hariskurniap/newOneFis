using Application.DTOs;
using Application.Interfaces;
using Application.Settings;
using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SapShipmentService : ISapShipmentService
    {
        private readonly IShipmentService _shipmentService;
        private readonly HttpClient _httpClient;
        private readonly SapSynchSettings _settings;

        public SapShipmentService(
            IShipmentService shipmentService,
            HttpClient httpClient,
            IOptions<SapSynchSettings> options
        )
        {
            _shipmentService = shipmentService;
            _httpClient = httpClient;
            _settings = options.Value;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _settings.TokenUrl);

            var body = new
            {
                grant_type = _settings.GrantType,
                client_id = _settings.ClientId,
                client_secret = _settings.ClientSecret
            };

            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var accessToken = doc.RootElement.GetProperty("access_token").GetString();

            return accessToken!;
        }

        public async Task<string> PostShipmentSapAsync(string onefisShipmentCode, DateTime updatedAt, string updatedBy)
        {
            var shipment = await _shipmentService.GetShipmentByCodeAsync(onefisShipmentCode);
            if (shipment == null)
                throw new Exception("Shipment tidak ditemukan");

            var externalPayload = new
            {
                TPP = shipment.Plant,
                BULK_SHIPMENT_TYPE = "ZTVF",
                SHIPMENT_DATE = shipment.ShipmentDate,
                Vehicle_Number = shipment.VehicleNumber,
                Driver_Number = shipment.DriverAllocation?.FirstOrDefault(d => d.DriverRole == "amt1")?.DriverName,
                Carrier = "",
                Route = "",
                Driver_Code = shipment.DriverAllocation?.FirstOrDefault(d => d.DriverRole == "amt1")?.DriverCode,
                Compartment_Allocation = shipment.CompartmentAllocation
                    ?.Take(1)
                    .Select(d => new { Delivery_Number = d.DeliveryNumber })
                    .ToList(),
                Seal_Structure = shipment.SealStructure
                    ?.Take(1)
                    .Select(s => new { Seal_Number = s.SealNumber })
                    .ToList()
            };

            var accessToken = await GetAccessTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Post, _settings.ShipmentUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Content = new StringContent(JsonSerializer.Serialize(externalPayload), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            var now = DateTime.UtcNow;
            var modifyRequest = new ModifyShipmentRequest
            {
                OnefisShipmentCode = onefisShipmentCode,
                OnefisUpdatedAt = now.ToString("yyyy-MM-dd"),
                OnefisUpdatedBy = updatedBy,
                Datas = new Shipment
                {
                    PushSapShipment = true
                }
            };

            await _shipmentService.UpdateShipmentAsync(modifyRequest);


            return responseContent;
        }

        public async Task<string> PostShipmentGiSapAsync(string onefisShipmentCode, DateTime updatedAt, string updatedBy)
        {
            var shipment = await _shipmentService.GetShipmentByCodeAsync(onefisShipmentCode);
            if (shipment == null)
                throw new Exception("Shipment tidak ditemukan");

            var compartment = shipment.CompartmentAllocation?.FirstOrDefault();
            if (compartment == null)
                throw new Exception("Compartment allocation tidak ditemukan");

            var externalPayload = new
            {
                Shipment_Number = shipment.ShipmentNumber,
                Loading_date = shipment.ShipmentDate,
                Compartment_allocation = new[]
                {
                new
                {
                    Storage_location = "LSTK",
                    Material_number = compartment.MaterialCode,
                    Batch = "",
                    Quantity = compartment.Quantity,
                    Qty_UOM = "KL"
                }
            }
            };

            var accessToken = await GetAccessTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Post, _settings.GiShipmentUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Content = new StringContent(JsonSerializer.Serialize(externalPayload), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }

}
