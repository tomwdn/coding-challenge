using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<ColorCount> _colorCounts;
        private readonly List<Shirt> _shirts;
        private readonly List<SizeCount> _sizeCounts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.

            _sizeCounts = Size.All.Select(x => new SizeCount {Size = x}).ToList();
            _colorCounts = Color.All.Select(x => new ColorCount {Color = x}).ToList();
        }

        public SearchResults Search(SearchOptions options)
        {
            // TODO: search logic goes here.

            var colorIds = options.Colors.Select(c => c.Id).ToArray();
            var sizeIds = options.Sizes.Select(s => s.Id).ToArray();

            var shirts = _shirts.Where(x =>
                    (!colorIds.Any() || colorIds.Contains(x.Color.Id)) &&
                    (!sizeIds.Any() || sizeIds.Contains(x.Size.Id)))
                .ToList();

            foreach (var colorId in shirts.Select(x => x.Color.Id).Distinct())
                _colorCounts.Single(x => x.Color.Id == colorId).Count = shirts.Count(x => x.Color.Id == colorId);

            foreach (var sizeId in shirts.Select(x => x.Size.Id).Distinct())
                _sizeCounts.Single(x => x.Size.Id == sizeId).Count = shirts.Count(x => x.Size.Id == sizeId);

            return new SearchResults
            {
                Shirts = shirts,
                SizeCounts = _sizeCounts,
                ColorCounts = _colorCounts
            };
        }
    }
}