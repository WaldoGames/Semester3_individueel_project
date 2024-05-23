using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_DAL.Classes;
using Backend_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core_unit_tests.IntegrationTests
{
    public class ArtistIntergrationTests : IntergrationTestsBase
    {
        IArtistRepository a;
        public ArtistIntergrationTests() : base()
        {
            a = new ArtistRepository(context);
        }

        [Fact]
        public void GetArtistsForSearch_Happyflow_findTwoArtists()
        {
            Result<ArtistsDto> result = a.GetArtistsForSearch("jeff");

            Assert.True(result.Data.Artists.Count == 2);
            Assert.Contains("jeff",result.Data.Artists.Select(a => a.name).ToList());
            Assert.Contains("jeffry", result.Data.Artists.Select(a => a.name).ToList());

            Assert.False(result.IsFailed);
        }
        [Fact]
        public void GetArtistsForSearch_Happyflow_findnoArtists()
        {
            Result<ArtistsDto> result = a.GetArtistsForSearch("hiugiugig");

            Assert.Empty(result.Data.Artists);

            Assert.False(result.IsFailed);
        }
        [Fact]
        public void GetArtistsForSearch_InvalidData_ReturnsWarningError()
        {
            Result<ArtistsDto> result = a.GetArtistsForSearch(null);

            Assert.True(result.IsFailedError);

        }
        [Fact]
        public void AddNewArtist_Happyflow_AddsNewArtist()
        {
            Assert.Equal(4, context.Artists.Count());

            SimpleResult result = a.AddNewArtist(new NewArtistDto { ArtistName = "name" });
            context.SaveChanges();
            Assert.False(result.IsFailed);
            Assert.Equal(5, context.Artists.Count());
            Assert.Contains("name",context.Artists.Select(a => a.name).ToArray());
        }
        [Fact]
        public void AddNewArtist_InvalidData_IsfailedError()
        {
            Assert.Equal(4, context.Artists.Count());

            SimpleResult result= a.AddNewArtist(new NewArtistDto {});

            Assert.True(result.IsFailedError);
        }

        [Fact]
        public void RemoveArtist_Happyflow_removesArtist()
        {
            Assert.Equal(4, context.Artists.Count());

            SimpleResult result = a.RemoveArtist(1);
            context.SaveChanges();
            Assert.False(result.IsFailed);
            Assert.Equal(3, context.Artists.Count());
            Assert.DoesNotContain("jeff", context.Artists.Select(a => a.name).ToArray());
        }
        [Fact]
        public void RemoveArtist_InvalidData_IsfailedError()
        {
            Assert.Equal(4, context.Artists.Count());

            SimpleResult result = a.RemoveArtist(98077);

            Assert.True(result.IsFailedWarning);
        }
        [Fact]
        public void DoesArtistExist_Happyflow_Yes()
        {

            Result<bool> result = a.DoesArtistExist(1);
            Assert.False(result.IsFailed);

            Assert.True(result.Data);
           
        }
        [Fact]
        public void DoesArtistExist_Happyflow_No()
        {
            Result<bool> result = a.DoesArtistExist(189898);
            Assert.False(result.IsFailed);

            Assert.False(result.Data);
        }
        [Fact]
        public void GetArtistsFromSong_Happyflow_GetJeff()
        {

            Result<ArtistsDto> result = a.GetArtistsFromSong(1);
            Assert.False(result.IsFailed);

            Assert.Single(result.Data.Artists);
            Assert.Equal("jeff", result.Data.Artists[0].name);


        }
        [Fact]
        public void GetArtistsFromSong_InvailidId_WarningMessage()
        {

            Result<ArtistsDto> result = a.GetArtistsFromSong(177);
            Assert.True(result.IsFailed);

            Assert.True(result.IsFailedWarning);
            Assert.False(result.IsFailedError);

        }
        /*
        [Fact]
        public void GetArtistById_happyflow_GetArtistJeff()
        {
            NullableResult<ArtistDto> result = a.GetArtistById(1);

            Assert.False(result.IsFailed);
            Assert.Equal("jeff", result.Data.name);
        }
        [Fact]
        public void GetArtistById_happyflow_ReturnsEmptyResult()
        {
            NullableResult<ArtistDto> result = a.GetArtistById(19879879);

            Assert.False(result.IsFailed);
            Assert.Equal("jeff", result.Data.name);
        }*/

        
    }
}
