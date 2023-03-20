using API.DTOs;
using API.Entities;
using API.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IuserRepository
    { 
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
            
        }
        public async Task<AppUsers> GetUserByIdAsync(int Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public async Task<AppUsers> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(x=>x.UserName==username);
        }

        public async Task<IEnumerable<AppUsers>> GetUsersAsync()
        {
            return await _context.Users.Include(p=>p.Photos).ToListAsync();
        }

        public async Task<MemberDto> GetMemberByUsernameAsync(string username)
        {
            return await _context.Users.Where(x=>x.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
            
        }

        public async Task<IEnumerable<MemberDto>> GetMemberAsync()
        {
            return await _context.Users.Include(p=>p.Photos).ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }

        public void update(AppUsers user)
        {
            _context.Entry(user).State=EntityState.Modified;
        }

        public Task<MemberDto> GetMemberAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}