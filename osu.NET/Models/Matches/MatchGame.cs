﻿using Newtonsoft.Json;
using osu.NET.Enums;
using osu.NET.Models.Beatmaps;

namespace osu.NET.Models.Matches;

/// <summary>
/// Represents the singular game played in a match.
/// <br/><br/>
/// API docs: <a href="https://osu.ppy.sh/docs/index.html#matchgame"/><br/>
/// Source: <a href="https://github.com/ppy/osu-web/blob/master/app/Models/LegacyMatch/Game.php"/>
/// </summary>
public class MatchGame
{
  /// <summary>
  /// The ID of this match game.
  /// </summary>
  [JsonProperty("id")]
  public int Id { get; private set; }

  /// <summary>
  /// The beatmap played in this match game. This includes <see cref="Beatmaps.Beatmap.Set"/>.
  /// </summary>
  [JsonProperty("beatmap")]
  public Beatmap Beatmap { get; private set; } = default!;

  /// <summary>
  /// The ID of the beatmap played in this match game.
  /// </summary>
  [JsonProperty("beatmap_id")]
  public int BeatmapId { get; private set; }

  /// <summary>
  /// The datetime at which this match game was started.
  /// </summary>
  [JsonProperty("start_time")]
  public DateTimeOffset StartedAt { get; private set; }

  /// <summary>
  /// The datetime at which this match game was ended. This will be null if the match game is still ongoing.
  /// </summary>
  [JsonProperty("end_time")]
  public DateTimeOffset? EndedAt { get; private set; }

  /// <summary>
  /// The ruleset played in this match game.
  /// </summary>
  [JsonProperty("mode")]
  public Ruleset Ruleset { get; private set; }

  /// <summary>
  /// The mods used in this match game.
  /// </summary>
  [JsonProperty("mods")]
  public string[] Mods { get; private set; } = default!;

  /// <summary>
  /// The scores set by each player in this match game.
  /// </summary>
  [JsonProperty("scores")]
  public MatchScore[] Scores { get; private set; } = default!;

  /// <summary>
  /// The scoring type used in this match game.
  /// </summary>
  [JsonProperty("scoring_type")]
  public MatchScoringType ScoringType { get; private set; }

  /// <summary>
  /// The team type used in this match game.
  /// </summary>
  [JsonProperty("team_type")]
  public MatchTeamType TeamType { get; private set; }
}
