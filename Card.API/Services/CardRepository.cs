using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace CityInfo.API.Services
{
    public class CardRepository : ICardRepository
    {
        private readonly CardContext _context;

        public CardRepository(CardContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<bool> CardExistsAsync(int cardId)
        {
          return await _context.Cards.AnyAsync(c => c.Id == cardId);
        }

        public async Task<bool> UserIsAdminAsync(int userId)
        {
          return await _context.Users.AnyAsync(c => c.Id == userId && c.Role =="Admin");
        }

        public async Task<bool> UserHasAccessToCardAsync(int cardId, int userId)
        {
          var isadmin =  await _context.Cards.AnyAsync(c => c.Id == cardId && c.UserId == userId);
          var hasaccess = await _context.Cards.AnyAsync(c => c.Id == cardId && c.UserId == userId);
          return  (isadmin || hasaccess);
        }

    public async Task<(IEnumerable<Card>, PaginationMetadata)> GetCardsAsync(
            int userId, string? name, string color, string status ,string? searchQuery, 
            string orderbyQuery, int pageNumber, int pageSize)
      {
      // collection to start from
      var collection = _context.Cards.Where(x=>x.UserId == userId) as IQueryable<Card>;

      if (!string.IsNullOrWhiteSpace(name))
        {
        name = name.Trim();
        collection = collection.Where(c => c.Name == name);
        }

      if (!string.IsNullOrWhiteSpace(color))
        {
        color = color.Trim();
        collection = collection.Where(c => c.Color == color);
        }

      if (!string.IsNullOrWhiteSpace(status))
        {
        status = status.Trim();
        collection = collection.Where(c => c.Status == status);
        }

      if (!string.IsNullOrWhiteSpace(searchQuery))
        {
        searchQuery = searchQuery.Trim();
        collection = collection.Where(a => a.Name.Contains(searchQuery)
            || (a.Description != null && a.Description.Contains(searchQuery)));
        }

      if (!string.IsNullOrWhiteSpace(orderbyQuery))
        {
         ApplySort(ref collection, orderbyQuery);
        }

      var totalItemCount = await collection.CountAsync();

      var paginationMetadata = new PaginationMetadata(
          totalItemCount, pageSize, pageNumber);

      var collectionToReturn = await collection
          .Skip(pageSize * (pageNumber - 1))
          .Take(pageSize)
          .ToListAsync();

      return (collectionToReturn, paginationMetadata);
      }

    public async Task<Card?> GetCardAsync(int cardId)
     {
           return await _context.Cards
            .Where(c => c.Id == cardId).FirstOrDefaultAsync();
     }
    public void AddCard(Card pointOfInterest)
      {
        _context.Cards.Add(pointOfInterest);
      }

    public void DeleteCard(Card card)
     {
       _context.Cards.Remove(card);
     }

    public async Task<int> GetMaxCardIdAsync()
      {
      var maxCard = await _context.Cards.ToListAsync();
      return maxCard.Max(x=>x.Id);
      }

    public async Task<User> GetCurrentUserAsync(string email, string password) 
      {
      return await _context.Users.FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
      }

    public void ApplySort(ref IQueryable<Card> cards, string orderByQueryString)
      {
      if (!cards.Any())
        return;

      if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
        cards = cards.OrderBy(x => x.Id);
        return;
        }
      var orderParams = orderByQueryString.Trim().Split(',');
      var propertyInfos = typeof(Card).GetProperties(BindingFlags.Public | BindingFlags.Instance);
      var orderQueryBuilder = new StringBuilder();

      foreach (var param in orderParams)
        {
        if (string.IsNullOrWhiteSpace(param))
          continue;
        var propertyFromQueryName = param.Split(" ")[0];
        var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
        if (objectProperty == null)
          continue;
        var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
        orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
        }
      var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

      if (string.IsNullOrWhiteSpace(orderQuery))
        {
        cards = cards.OrderBy(x => x.Name);
        return;
        }
      cards = cards.OrderBy(orderQuery);
      }
    }
}
