using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interface
{
    public interface IuserRepository
    {
        void update(AppUsers user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUsers>> GetUsersAsync();
        Task<AppUsers> GetUserByIdAsync(int Id);
        Task<AppUsers> GetUserByUsernameAsync(string username);

         Task<MemberDto> GetMemberByUsernameAsync(string username);

          Task<MemberDto> GetMemberAsync(string username);


    }
}