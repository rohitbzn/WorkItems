using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WorkItems.Service.Data;
using WorkItems.Shared;

namespace WorkItems.Service.Service
{
    [RoutePrefix("api/v1/workitems")]
    public class WorkItemsController : ApiController
    {
        private readonly WorkItemsDbContext _db = new WorkItemsDbContext();

        // GET /api/v1/workitems?status={StatusEnum}
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get([FromUri] WorkItemStatus? status = null)
        {
            var query = _db.WorkItems.AsQueryable();
            if (status.HasValue)
                query = query.Where(w => w.Status == status.Value);

            var items = await query
                .OrderByDescending(w => w.UpdatedAt)
                .ToListAsync();

            var dtos = items.Select(w => new WorkItemDto
            {
                Id = w.Id,
                Title = w.Title,
                Status = w.Status,
                Priority = w.Priority,
                UpdatedAt = w.UpdatedAt
            }).ToList();

            return Ok(dtos);
        }

        // POST /api/v1/workitems
        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post([FromBody] WorkItemDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("Title is required.");

            var entity = new WorkItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Status = dto.Status,
                Priority = dto.Priority,
                UpdatedAt = DateTime.UtcNow
            };

            _db.WorkItems.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            dto.UpdatedAt = entity.UpdatedAt;
            return CreatedAtRoute("", new { id = entity.Id }, dto);
        }

        // PATCH /api/v1/workitems/{id}/status
        [HttpPatch, Route("{id:guid}/status")]
        public async Task<IHttpActionResult> PatchStatus(Guid id, [FromBody] WorkItemStatus status)
        {
            var entity = await _db.WorkItems.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.Status = status;
            entity.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            return Ok(new { id = entity.Id, status = entity.Status, updatedAt = entity.UpdatedAt });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}