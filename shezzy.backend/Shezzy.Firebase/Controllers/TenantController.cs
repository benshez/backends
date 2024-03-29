﻿using Microsoft.AspNetCore.Mvc;
using OrchardCore.Environment.Shell;
using Shezzy.Firebase.Services.Form;
using Shezzy.Firebase.Services.Tenant;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Shezzy.Shared.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace Shezzy.Firebase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [IgnoreAntiforgeryToken]
    [Authorize]
    public class TenantController : ControllerBase
    {
        private readonly ISLogger _logger;
        private readonly ShellSettings _shellHost;
        private readonly IFirestoreQueryService _queryService;
        private readonly CancellationToken _cancellationToken;

        public TenantController(
            ShellSettings shellHost,
            IFirestoreQueryService queryService,
            ISLogger logger)
        {
            _shellHost = shellHost;
            _queryService = queryService;
            _cancellationToken = new CancellationTokenSource().Token;
            _logger = logger;
        }


        [HttpPost]
        [Route("[action]")]
        [IgnoreAntiforgeryToken]
        [Authorize(Roles = "Tenant.Write")]
        public async Task<Tenant> AddOrUpdate([FromBody] Tenant tenant)
        {
            await _queryService.AddOrUpdate(tenant, _cancellationToken);

            return await Get();
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Tenant.Read")]
        public async Task<Tenant> Get()
        {
            string tenantId = _shellHost.TenantId;

            if (string.IsNullOrEmpty(tenantId))
            {
                _logger.LogAndThrowArgumentNullException($"Tenant Id is required by method {nameof(Get)} in {nameof(TenantController)}.");
            }

            return await _queryService.Get<Tenant>(tenantId, _cancellationToken);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Tenant.Read")]
        public async Task<IReadOnlyCollection<Tenant>> GetAll()
        {
            string tenantId = _shellHost.TenantId;

            if (string.IsNullOrEmpty(tenantId))
            {
                _logger.LogAndThrowArgumentNullException($"Tenant Id is required by method {nameof(Get)} in {nameof(TenantController)}.");
            }

            return await _queryService.GetAll<Tenant>(tenantId, _cancellationToken);
        }
    }
}
