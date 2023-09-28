# Rate Games

Find useful information about your favorite games, rate them and explore new ones.

![Recent games](https://github.com/t-eldar/rate-games/assets/74774738/a844bf6b-0a55-456c-b05c-f323f39f275d)

## IGDB.com

Uses the biggest games database with over 250 000 unique games.

## Rate your favorite games
![Rate game](https://github.com/t-eldar/rate-games/assets/74774738/038a2973-5e13-4eec-94ab-a57dcb32c0bb)

## Apicalypse queries

Uses Apicalypse query builder that allowed easily write queries to IGDB.

Apicalypse query
```
fields name,release_dates,genres.name,rating;
where id = 1942; 
sort rating;
```

Same query with builder.
```cs
var query = _queryBuilderCreator.CreateFor<Game>()
  .Select(g => new
  {
    g.Name,
    g.FirstReleaseDate,
    GenreName = g.Genres!.IncludeProperty(gn => gn.Value!.Name),
    g.Rating,
  })
  .Where(g => g.Id == 1942)
  .OrderBy(g => g.Rating)
  .Build();
```
