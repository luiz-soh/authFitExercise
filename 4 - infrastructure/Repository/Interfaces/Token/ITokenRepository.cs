using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using models.Dto.Token;

namespace infrastructure.Repository.Interfaces.Token
{
    public interface ITokenRepository
    {
        Task<bool> AddToken(CachedTokenDTO input);
    }
}