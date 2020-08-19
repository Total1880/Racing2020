﻿using Racing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Racing.Services.Interfaces
{
    public interface IRaceService
    {
        Task<IList<Race>> GetRaces();
    }
}
