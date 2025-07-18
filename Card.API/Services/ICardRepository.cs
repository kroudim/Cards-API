using CityInfo.API.Entities;

namespace CityInfo.API.Services
  {
  public interface ICardRepository
    {
    Task<(IEnumerable<Card>, PaginationMetadata)> GetCardsAsync(
    int userId, string? name, string? color, string? status, string? searchQuery, 
    string? orderQuery, int pageNumber, int pageSize);
    Task<bool> CardExistsAsync(int cardId);
    Task<bool> UserHasAccessToCardAsync(int cardId, int userId);
    Task<Card?> GetCardAsync(int cardId);
    Task<int> GetMaxCardIdAsync();
    void DeleteCard(Card card);

    void AddCard(Card pointOfInterest);
    Task<bool> SaveChangesAsync();

    Task<User> GetCurrentUserAsync(string email, string password);
    }
  }
