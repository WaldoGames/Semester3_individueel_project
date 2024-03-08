// See https://aka.ms/new-console-template for more information
using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_DAL.Classes;

ArtistService artistService = new ArtistService(new ShowRepository(), new SongRepository(), new ArtistRepository());

Result<ArtistsDto> artists= artistService.getArtistsUsedInShow(1);

int i = 1;