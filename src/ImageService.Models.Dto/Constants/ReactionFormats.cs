using System.Collections.Immutable;

namespace LT.DigitalOffice.ImageService.Models.Dto.Constants;

public static class ReactionFormats
{
  public const string jpg = ".jpg";
  public const string jpeg = ".jpeg";
  public const string png = ".png";
  public const string svg = ".svg";
  public const string webp = ".webp";
  public const string gif = ".gif";

  public static ImmutableList<string> formats = ImmutableList
  .Create(jpg, jpeg, png, svg, webp, gif);
}
