using AutoMapper;
using CRM.Application.Abstraction;
using CRM.Application.Services;
using CRM.Infrastructure.Dictionaries;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRM.WebAPI
{
    [Authorize]
    public class ReportsController : AppController
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("getNewClientsCount")]
        public async Task<ActionResult> GetNewClientsCount()
        {
            var data = await _reportService.GetNewClientsCount();

            return Ok(data);
        }

        [HttpGet("getNewTasksCount")]
        public async Task<ActionResult> GetNewTasksCount()
        {
            var data = await _reportService.GetNewTasksCount();

            return Ok(data);
        }
    }
}
