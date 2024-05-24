using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Classes
{
    public class ArtistRepository : IArtistRepository
    {
        readonly MusicAppContext context;

        public ArtistRepository()
        {
            context = new MusicAppContext();
        }
        public ArtistRepository(MusicAppContext context)
        {
            this.context = context;
        }

        public SimpleResult AddNewArtist(NewArtistDto newArtist)
        {
            try
            {
                Artist newArtistObject = new Artist()
                {
                    name = newArtist.ArtistName,
                };

                context.Artists.Add(newArtistObject);
                context.SaveChanges();

                return new SimpleResult();
            }
            catch (Exception e)
            {
                return new SimpleResult() { ErrorMessage = "Dal->ArtistRepository->AddNewArtist " + e.Message };
            }
        }

        public Result<bool> DoesArtistExist(int artistId)
        {
            try
            {
                bool exists = context.Artists.Where(s => s.Id == artistId).Any();

                return new Result<bool>() { Data = exists };
            }
            catch (Exception e)
            {
                return new Result<bool>() { ErrorMessage = "Dal->ArtistRepository->DoesArtistExist error: " + e.Message };
            }
        }

        public NullableResult<ArtistDto> GetArtistById(int artistId)
        {
            throw new NotImplementedException();
        }

        public Result<ArtistsDto> GetArtistsForSearch(string name)
        {
            try
            {
                List<Artist> artists = context.Artists.Where(a => a.name.Contains(name)).Take(3).ToList();

                if (!artists.Any())
                {
                    return new Result<ArtistsDto> { Data = new ArtistsDto()};
                }

                ArtistsDto artistsDto = new ArtistsDto();

                artists.ForEach(delegate (Artist artist)
                {
                    artistsDto.Artists.Add(new ArtistDto() { Id = artist.Id, name = artist.name });
                });

                return new Result<ArtistsDto> { Data = artistsDto };

            }
            catch (Exception e)
            {
                return new Result<ArtistsDto> { ErrorMessage = "Dal->ArtistRepository->GetArtistsForSearch: " + e.Message };
            }
        }

        public Result<ArtistsDto> GetArtistsFromSong(int songId)
        {
            try
            {
                List<Artist> artists = context.Artists.Where(a=>a.Songs.Select(s=>s.Id).Contains(songId)).ToList();

                if(!artists.Any()) {
                    return new Result<ArtistsDto> { WarningMessage = "Dal->ArtistRepository->GetArtistFromSong->[ songId: " + songId + "]:" + "no artist found for songId :" + songId };
                }

                ArtistsDto artistsDto = new ArtistsDto();

                artists.ForEach(delegate(Artist artist)
                {
                    artistsDto.Artists.Add(new ArtistDto() { Id = artist.Id, name = artist.name});
                });

                return new Result<ArtistsDto> { Data = artistsDto };

            }
            catch (Exception e)
            {
                return new Result<ArtistsDto> { ErrorMessage = "Dal->ArtistRepository->GetArtistFromSong->[ songId: " + songId + "]:" + e.Message };
            }
        }

        public Result<ArtistsDto> GetArtistsListFromSongList(List<int> songIds, int max, int Offset = 0)
        {
            try
            {
                ArtistsDto finalArtistsList = new ArtistsDto();

                foreach (int songId in songIds)
                {
                    Result<ArtistsDto> songsArtists = GetArtistsFromSong(songId);

                    if (songsArtists.IsFailedError|| songsArtists.Data==null || !songsArtists.Data.Artists.Any())
                    {
                        continue;
                    }
                    foreach (ArtistDto item in songsArtists.Data.Artists)
                    {
                        if (!finalArtistsList.Artists.Select(a=>a.Id).Contains(item.Id))
                        {
                            finalArtistsList.Artists.Add(item);
                        }
                    }
                   
                }

                return new Result<ArtistsDto> { Data = finalArtistsList };
            }
            catch (Exception e)
            {
                return new Result<ArtistsDto> { ErrorMessage = "Dal->ArtistRepository->GetArtistsListFromSongList " + e.Message };
            }
        }
        public SimpleResult RemoveArtist(int artistId)
        {
            Result<bool> result = DoesArtistExist(artistId);

            if (result.IsFailed) return result;
            if (result.Data == false)
            {
                return new SimpleResult { WarningMessage = "ArtistRepository->RemoveArtist Artist doesn't exist" };
            }

            try
            {
                var entityToDelete = context.Artists.Find(artistId); // Replace id with the ID of the entity you want to delete

                if (entityToDelete != null)
                {
                    context.Artists.Remove(entityToDelete);
                    context.SaveChanges();
                }
                return new SimpleResult { };
            }
            catch (Exception e)
            {

                return new SimpleResult { ErrorMessage = "ArtistRepository->RemoveArtist Error: "+ e.Message };
            }
        }

        public SimpleResult UpdateArtist(int artistId, UpdateArtistDto updateArtist)
        {
            throw new NotImplementedException();
        }
    }
}
