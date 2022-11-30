﻿namespace Aboriginal_Art_Gallery_of_Australia.Models.DTOs
{
    public class ExhibitionInputDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? BackdropImageURL { get; set; }

        public ExhibitionInputDto(string name, string description, string backdropImageURL)
        {
            Name = name;
            Description = description;
            BackdropImageURL = backdropImageURL;
        }
    }

    public class ExhibitionOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? BackdropImageURL { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public ExhibitionOutputDto(int id, string name, string description, string? backdropImageURL, DateTime modifiedAt, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            BackdropImageURL = backdropImageURL;
            ModifiedAt = modifiedAt;
            CreatedAt = createdAt;
        }
    }

    public class ExhibitionArtworkOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? BackdropImageURL { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ArtworkOutputDto> ExhibitionArtworks { get; set; }

        public ExhibitionArtworkOutputDto(int id, string name, string description, string? backdropImageURL, DateTime modifiedAt, DateTime createdAt, List<ArtworkOutputDto> exhibitionArtworks)
        {
            Id = id;
            Name = name;
            Description = description;
            BackdropImageURL = backdropImageURL;
            ModifiedAt = modifiedAt;
            CreatedAt = createdAt;
            ExhibitionArtworks = exhibitionArtworks;
        }
    }
}