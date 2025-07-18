using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/cards")]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        const int maxCardsPageSize = 20;

        public CardsController(ICardRepository cityInfoRepository,
            IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? 
                throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardDto>>> GetCards(
            string? name, string? color, string? status, string? searchQuery, string? orderbyQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxCardsPageSize)
            {
                pageSize = maxCardsPageSize;
            }

            var userId = GetUserId();
            var (cityEntities, paginationMetadata) = await _cityInfoRepository
                .GetCardsAsync(userId, name, color, status , searchQuery, orderbyQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<CardDto>>(cityEntities));       
        }

        [HttpGet("{id}", Name = "GetCard")]
        public async Task<IActionResult> GetCard(int id)
        {
            var userId = GetUserId();

            var card = await _cityInfoRepository.GetCardAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            var isgranted = await _cityInfoRepository.UserHasAccessToCardAsync(id, userId);
            if (!isgranted)
            {
                return Forbid();
            }

            return Ok(_mapper.Map<CardDto>(card));
        }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCard(int id, CardForUpdateDto card)
      {
      var userId = GetUserId();

      var cardEntity = await _cityInfoRepository
          .GetCardAsync(id);

      if (cardEntity == null)
        {
        return NotFound();
        }

      var isgranted = await _cityInfoRepository.UserHasAccessToCardAsync(id, userId);
      if (!isgranted)
        {
        return Forbid();
        }

      _mapper.Map(card, cardEntity);

      await _cityInfoRepository.SaveChangesAsync();

      return NoContent();
      }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCard(int id)
      {
      var userId = GetUserId();

      if (!await _cityInfoRepository.CardExistsAsync(id))
        {
        return NotFound();
        }

      var isgranted = await _cityInfoRepository.UserHasAccessToCardAsync(id, userId);
      if (!isgranted)
        {
        return Forbid();
        }

      var pointOfInterestEntity = await _cityInfoRepository
          .GetCardAsync(id);

      if (pointOfInterestEntity == null)
        {
        return NotFound();
        }

      _cityInfoRepository.DeleteCard(pointOfInterestEntity);
      await _cityInfoRepository.SaveChangesAsync();

      return NoContent();
      }

    [HttpPost]
    public async Task<ActionResult<CardForCreationDto>> CreateCard(
      CardForCreationDto pointOfInterest)
      {
      var finalPointOfInterest = _mapper.Map<Entities.Card>(pointOfInterest);
      finalPointOfInterest.UserId = GetUserId();

       _cityInfoRepository.AddCard(finalPointOfInterest);

      await _cityInfoRepository.SaveChangesAsync();

      var cardId = await _cityInfoRepository.GetMaxCardIdAsync();

      var createdPointOfInterestToReturn =
          _mapper.Map<Models.CardDto>(finalPointOfInterest);

      return CreatedAtRoute("GetCard", new { id = cardId }, createdPointOfInterestToReturn);
      }

    private int GetUserId()
      {
      return Convert.ToInt32(User.Claims.ToList().FirstOrDefault().Value);
      }

    }
}
