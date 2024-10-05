using ApiDevBP.Entities;
using ApiDevBP.Models;
using ApiDevBP.Options;
using AutoMapper;
using Microsoft.Extensions.Options;
using SQLite;

namespace ApiDevBP.Services
{
    public class UserService : IUserService
    {
        private readonly SQLiteConnection _db;
        private readonly IMapper _mapper;

        public UserService(IOptions<DatabaseOptions> options, IMapper mapper)
        {
            _db = new SQLiteConnection(options.Value.ConnectionString);
            _db.CreateTable<UserEntity>();
            _mapper = mapper;
        }

        public async Task<bool> SaveUserAsync(UserModel user)
        {
            var entity = _mapper.Map<UserEntity>(user);

            try
            {
                var result = _db.Insert(entity);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            try
            {
                var users = _db.Table<UserEntity>().ToList();
                return _mapper.Map<List<UserModel>>(users);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<bool> UpdateUserAsync(int id, UserModel user)
        {
            var entity = _db.Find<UserEntity>(id);
            if (entity == null) 
                return false;

            _mapper.Map(user, entity);
            try
            {
                var result = _db.Update(entity);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var entity = _db.Find<UserEntity>(id);
            if (entity == null) 
                return false;

            try
            {
                var result = _db.Delete(entity);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
