﻿using Backend_core.Classes;
using Backend_core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.Interfaces
{
    public interface IShowRepository
    {
        public NullableResult<ShowDto> GetShowById(int showId);

        public Result<bool> DoesShowExist(int showId);

        public Result<ShowsDto> GetAllShowsWithConnectionToUser(string authSub);

        public SimpleResult CreateNewShow(NewShowDto newShow);

        public NullableResult<string> GetShowDiscriptionOfSong(int songId, int showId);
        public SimpleResult DeleteShow(int showId);

    }
}
