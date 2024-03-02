using OkVip.Gamble.Games;
using System;
using System.Collections.Generic;

namespace OkVip.Gamble.Categories
{
    public class Category : BaseEntity<Guid>
    {
        private ICollection<Game> _games;

        public string Name { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<Game> Games
        {
            get => _games ??= new List<Game>();
            set => _games = value;
        }
    }
}