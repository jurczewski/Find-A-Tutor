using Find_A_Tutor.Core.Domain;
using Find_A_Tutor.Core.Exceptions;
using Find_A_Tutor.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Find_A_Tutor.Core.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid id)
        {
            var user = await repository.GetAsync(id);
            if (user == null)
            {
                throw new RepositoryException($"User with id: '{id}' does not exist.");
            }

            return user;
        }
    }
}
