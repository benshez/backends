using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Environment.Shell;
using Shezzy.Firebase.Services.Form;
using Shezzy.Firebase.Services.Tenant;
using Shezzy.Shared.Abstractions;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Shezzy.Firebase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ISLogger _logger;
        private readonly ShellSettings _shellHost;
        private readonly IFirestoreQueryService _queryService;
        private readonly CancellationToken _cancellationToken;

        public UserController(
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
        [Authorize(Roles = "User.Write")]
        public async Task<User> AddOrUpdate([FromBody] User user)
        {
            string tenantId = _shellHost.TenantId;

            if (string.IsNullOrEmpty(tenantId))
            {
                _logger.LogAndThrowArgumentNullException($"Tenant Id is required by method {nameof(GetAll)} in {nameof(UserController)}.");
            }

            user.TenantId = tenantId;

            await _queryService.AddOrUpdate(user, _cancellationToken);

            return await Get(user.Id);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "User.Read")]
        public async Task<User> Get([FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogAndThrowArgumentNullException($"User Id is required by method {nameof(Get)} in {nameof(UserController)}.");
            }

            return await _queryService.Get<User>(userId, _cancellationToken);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "User.Read")]
        public async Task<IReadOnlyCollection<User>> GetAll()
        {
            string tenantId = _shellHost.TenantId;

            if (string.IsNullOrEmpty(tenantId))
            {
                _logger.LogAndThrowArgumentNullException($"Tenant Id is required by method {nameof(GetAll)} in {nameof(UserController)}.");
            }

            return await _queryService.GetAll<User>(tenantId, _cancellationToken);
        }
    }
}
