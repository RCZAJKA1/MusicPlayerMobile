namespace MusicPlayerMobile.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicPlayerMobile.Models;

    /// <inheritdoc cref="IDataStore{T}"/>
    public class MockDataStore : IDataStore<Song>
    {
        /// <summary>
        ///     The songs.
        /// </summary>
        private readonly IEnumerable<Song> _songs;

        /// <summary>
        ///     Creates a new instance of the <see cref="MockDataStore"/> class.
        /// </summary>
        public MockDataStore()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //using (System.IO.StreamReader streamReader = new System.IO.StreamReader("filename.txt"))
            //{
            //    string content = streamReader.ReadToEnd();
            //    System.Diagnostics.Debug.WriteLine(content);
            //}
        }

        /// <inheritdoc/>
        public async Task<Song> GetSongAsync(int id)
        {
            return await Task.FromResult(this._songs.FirstOrDefault(s => s.Id == id));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Song>> GetAllSongsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(this._songs);
        }
    }
}