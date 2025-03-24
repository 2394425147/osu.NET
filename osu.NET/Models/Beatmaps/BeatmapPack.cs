﻿using Newtonsoft.Json;
using osu.NET.Enums;

namespace osu.NET.Models.Beatmaps;

/// <summary>
/// Represents a beatmap pack, which is an officially composed bundle of beatmapsets.
/// <br/><br/>
/// API docs: <a href="https://osu.ppy.sh/docs/index.html#beatmappack"/><br/>
/// Source: <a href="https://github.com/ppy/osu-web/blob/master/app/Models/BeatmapPack.php"/>
/// </summary>
public class BeatmapPack
{
  /// <summary>
  /// The author of the beatmap pack.
  /// </summary>
  [JsonProperty("author")]
  public string Author { get; private set; } = default!;

  /// <summary>
  /// The creation date of the beatmap pack.
  /// </summary>
  [JsonProperty("date")]
  public DateTimeOffset CreatedAt { get; private set; } = default;

  /// <summary>
  /// The name of the beatmap pack.
  /// </summary>
  [JsonProperty("name")]
  public string Name { get; private set; } = default!;

  /// <summary>
  /// Bool whether difficulty reduction mods may be used to clear the beatmap pack.
  /// </summary>
  [JsonProperty("no_diff_reduction")]
  public bool NoDifficultyReduction { get; private set; } = default;

  /// <summary>
  /// The ruleset this beatmap pack is made for. This will be null if the beatmap pack is made for all rulesets.
  /// </summary>
  [JsonProperty("ruleset_id")]
  public Ruleset? Ruleset { get; private set; }

  /// <summary>
  /// The tag of the beatmap pack. Starts with the character representing the <see cref="BeatmapPackType"/>, followed by the ID.
  /// </summary>
  [JsonProperty("tag")]
  public string Tag { get; private set; } = default!;

  /// <summary>
  /// The URL for downloading the beatmap pack.
  /// </summary>
  [JsonProperty("url")]
  public string Url { get; private set; } = default!;

  /// <summary>
  /// The beatmap sets included in the beatmap pack. This property will be null if multiple beatmap packs are requested.
  /// </summary>
  [JsonProperty("beatmapsets")]
  public BeatmapSetExtended[]? BeatmapSets { get; private set; }
}