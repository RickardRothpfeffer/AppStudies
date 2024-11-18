using System;
using Models;
using Models.DTO;

namespace Services
{
    public interface IMusicService
	{
        //Full set of async methods
        public Task<GstUsrInfoAllDto> InfoAsync { get; }
        public Task<GstUsrInfoAllDto> SeedAsync(int nrOfItems);
        public Task<GstUsrInfoAllDto> RemoveSeedAsync(bool seeded);

        public Task<ResponsePageDto<IMusicGroup>> ReadMusicGroupsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
        public Task<IMusicGroup> ReadMusicGroupAsync(Guid id, bool flat);
        public Task<IMusicGroup> DeleteMusicGroupAsync(Guid id);
        public Task<IMusicGroup> UpdateMusicGroupAsync(MusicGroupCUdto item);
        public Task<IMusicGroup> CreateMusicGroupAsync(MusicGroupCUdto item);

        public Task<ResponsePageDto<IAlbum>> ReadAlbumsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
        public Task<IAlbum> ReadAlbumAsync(Guid id, bool flat);
        public Task<IAlbum> DeleteAlbumAsync(Guid id);
        public Task<IAlbum> UpdateAlbumAsync(csAlbumCUdto item);
        public Task<IAlbum> CreateAlbumAsync(csAlbumCUdto item);

        public Task<ResponsePageDto<IArtist>> ReadArtistsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
        public Task<IArtist> ReadArtistAsync(Guid id, bool flat);
        public Task<IArtist> DeleteArtistAsync(Guid id);
        public Task<IArtist> UpdateArtistAsync(csArtistCUdto item);
        public Task<IArtist> CreateArtistAsync(csArtistCUdto item);
        public Task<IArtist> UpsertArtistAsync(csArtistCUdto item);
    }
}

