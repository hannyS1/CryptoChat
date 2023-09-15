using CryptoChat.AppServices.Contracts.Services;
using CryptoChat.Common.Contracts.Exceptions;
using CryptoChat.Database;
using CryptoChat.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoChat.AppServices.Services;

internal class RoomService : IRoomService
{
    private readonly ApplicationContext _dbContext;
    
    public RoomService(ApplicationContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public async Task CreateAsync(int firstUserId, int secondUserId)
    {
        if(firstUserId == secondUserId)
            throw new ChatException("you cant create room with yourself");
        
        if (await RoomAlreadyExist(firstUserId, secondUserId))
            throw new ChatException("room with this user already exist");
        
        var roomEntry = await _dbContext.AddAsync(new Room());
        await _dbContext.SaveChangesAsync();
        var room = roomEntry.Entity;

        _dbContext.UsersRooms.Add(new UserRoom { RoomId = room.Id, UserId = firstUserId });
        _dbContext.UsersRooms.Add(new UserRoom { RoomId = room.Id, UserId = secondUserId });
        await _dbContext.SaveChangesAsync();
    }

    private async Task<bool> RoomAlreadyExist(int firstUserId, int secondUserId)
    {
        var firstUserRoomsIds = await _dbContext.UsersRooms
            .Where(ur => ur.UserId == firstUserId)
            .Select(ur => ur.RoomId)
            .ToListAsync();

        return await _dbContext.UsersRooms
            .AnyAsync(ur => firstUserRoomsIds.Contains(ur.RoomId) && ur.UserId == secondUserId);
    }
}