using Auth.Dtos;
using Auth.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Auth.Services
{
    public class UserService
    {
        private readonly IMongoCollection<ApplicationUser> _users;
        private readonly BookmarkService _bookmarkService;

        public UserService(IMongoDatabase database, BookmarkService bookmarkService)
        {
            _users = database.GetCollection<ApplicationUser>("users");
            _bookmarkService = bookmarkService;
        }

        public async Task<(ApplicationUser User, Bookmark Bookmark)> GetUserWithBookmarkByIdAsync(string userId)
        {
            var user = await _users.Find(u => u.Id.ToString() == userId).FirstOrDefaultAsync();
            var bookmarks = await _bookmarkService.GetByUserIdAsync(userId);
            Console.WriteLine("hello world2");
            return (user, bookmarks);
        }
        public async Task<bool> EditUserProfileAsync(string userId, UserEditRequest userEditRequest)
        {
            var filter = Builders<ApplicationUser>.Filter.Eq(u => u.Id, new Guid(userId));

            var updateDefinition = Builders<ApplicationUser>.Update
                .Set(u => u.FullName, userEditRequest.FullName)
                .Set(u => u.UserName, userEditRequest.UserName)
                .Set(u => u.Email, userEditRequest.Email);
            var result = await _users.UpdateOneAsync(filter, updateDefinition);

            return result.ModifiedCount > 0;
        }

    }
}