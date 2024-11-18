using System;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;
using Newtonsoft.Json;

namespace Services
{
    public class MusicServiceWapi : IMusicService
    {
        private readonly ILogger<MusicServiceWapi> _logger = null;
        private readonly HttpClient _httpClient = null;

        //To ensure Json deserializern is using the class implementations instead of the Interfaces 
        readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            Converters = {
                new AbstractConverter<MusicGroup, IMusicGroup>(),
                new AbstractConverter<Album, IAlbum>(),
                new AbstractConverter<Artist, IArtist>()
            },
        };

        #region constructors
        public MusicServiceWapi(IHttpClientFactory httpClientFactory, ILogger<MusicServiceWapi> logger)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient(name: "MusicWebApi");
        }
        #endregion

        #region Admin Services
        public Task<GstUsrInfoAllDto> InfoAsync => throw new NotImplementedException();

        public Task<GstUsrInfoAllDto> SeedAsync(int nrOfItems) 
        {
            throw new NotImplementedException();
        }
        public Task<GstUsrInfoAllDto> RemoveSeedAsync(bool seeded)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region MusicGroup CRUD
        public Task<ResponsePageDto<IMusicGroup>> ReadMusicGroupsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) 
        {
            throw new NotImplementedException();
        }
        public Task<IMusicGroup> ReadMusicGroupAsync(Guid id, bool flat)
        {
            throw new NotImplementedException();
        }
        public Task<IMusicGroup> DeleteMusicGroupAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<IMusicGroup> UpdateMusicGroupAsync(MusicGroupCUdto item)
        {
            throw new NotImplementedException();
        }
        public Task<IMusicGroup> CreateMusicGroupAsync(MusicGroupCUdto item)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Album CRUD      
        public async Task<ResponsePageDto<IAlbum>> ReadAlbumsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
        {
            string uri = $"Album/Read?seeded=true&flat=false&pageNr=0&pageSize=10";

            //Send the HTTP Message and await the repsonse
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            //Throw an exception if the response is not successful
            response.EnsureSuccessStatusCode();

            //Get the resonse data
            string s = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponsePageDto<IAlbum>>(s, _jsonSettings);
            return resp;
        }
        public Task<IAlbum> ReadAlbumAsync(Guid id, bool flat)
        {
            throw new NotImplementedException();
        }
        public Task<IAlbum> DeleteAlbumAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<IAlbum> UpdateAlbumAsync(csAlbumCUdto item)
        {
            throw new NotImplementedException();
        }
        public Task<IAlbum> CreateAlbumAsync(csAlbumCUdto item)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Artist CRUD 
        public Task<ResponsePageDto<IArtist>> ReadArtistsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
        public Task<IArtist> ReadArtistAsync(Guid id, bool flat)
        {
            throw new NotImplementedException();
        }
        public Task<IArtist> DeleteArtistAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<IArtist> UpdateArtistAsync(csArtistCUdto item)
        {
            throw new NotImplementedException();
        }
        public Task<IArtist> CreateArtistAsync(csArtistCUdto item)
        {
            throw new NotImplementedException();
        }
        public Task<IArtist> UpsertArtistAsync(csArtistCUdto item)
        {
            throw new NotImplementedException();
        }
        #endregion
      
    }
public class AbstractConverter<TReal, TAbstract> 
    : JsonConverter where TReal : TAbstract
{
    public override Boolean CanConvert(Type objectType)
        => objectType == typeof(TAbstract);

    public override Object ReadJson(JsonReader reader, Type type, Object value, JsonSerializer jser)
        => jser.Deserialize<TReal>(reader);

    public override void WriteJson(JsonWriter writer, Object value, JsonSerializer jser)
        => jser.Serialize(writer, value);
}
}
