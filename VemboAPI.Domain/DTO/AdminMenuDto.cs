using System;
using System.Collections.Generic;

namespace VemboAPI.Domain.DTOs
{
    public class AdminMenuDto
    {
        public IReadOnlyList<AdminMenuSectionDto> Sections { get; set; } = Array.Empty<AdminMenuSectionDto>();
    }

    public class AdminMenuSectionDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public IReadOnlyList<AdminMenuItemDto> Items { get; set; } = Array.Empty<AdminMenuItemDto>();
    }

    public class AdminMenuItemDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public IReadOnlyList<string> RelatedEntities { get; set; } = Array.Empty<string>();
        public IReadOnlyList<string> AllowedRoles { get; set; } = Array.Empty<string>();
    }
}
