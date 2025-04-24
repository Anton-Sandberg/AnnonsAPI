using AnnonsAPI.DTOs;
using AnnonsAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnnonsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AdsController(ApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AdReadDto>>> GetAds()
        {
            var ads = await _dbContext.Ads.ToListAsync();

            return Ok(_mapper.Map<List<AdReadDto>>(ads));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AdReadDto>> GetAd(int id)
        {
            var adDto = await _dbContext.Ads.FindAsync(id);

            if (adDto == null)
            {
                return NotFound($"Ad with id {id} not found.");
            }

            return Ok(_mapper.Map<AdReadDto>(adDto));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutAd(int id, AdUpdateDto adUpdateDto)
        {
            var adToUpdate = await _dbContext.Ads.FindAsync(id);

            if (adToUpdate == null)
            {
                return NotFound($"Ad with id {id} not found.");
            }

            _mapper.Map(adUpdateDto, adToUpdate);

            await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<AdReadDto>(adToUpdate));
        }

        [HttpPost]
        public async Task<ActionResult<AdReadDto>> PostAd(AdCreateDto adCreateDto)
        {
            var ad = _mapper.Map<Ad>(adCreateDto);
            _dbContext.Ads.Add(ad);
            await _dbContext.SaveChangesAsync();

            var readDto = _mapper.Map<AdReadDto>(ad);

            return CreatedAtAction(nameof(GetAd), new { id = readDto.Id }, readDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAd(int id)
        {
            var ad = await _dbContext.Ads.FindAsync(id);
            if (ad == null)
            {
                return NotFound($"Ad with id {id} not found.");
            }

            _dbContext.Ads.Remove(ad);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<AdReadDto>> PatchAd(JsonPatchDocument<AdUpdateDto> adPatchDoc, int id)
        {
            var ad = await _dbContext.Ads.FindAsync(id);
            if (ad == null)
            {
                return NotFound($"Ad with id {id} not found.");
            }

            var adToPatch = _mapper.Map<AdUpdateDto>(ad);

            adPatchDoc.ApplyTo(adToPatch);

            _mapper.Map(adToPatch, ad);

            await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<AdReadDto>(ad));
        }
    }
}
