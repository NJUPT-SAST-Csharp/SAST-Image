global using ImageId = long;

namespace Domain.Album;

internal sealed class ImageState : IEquatable<ImageState>
{
    public ImageId Id { get; init; }
    public UserId[] Likes { get; set; } = [];

    public bool Equals(ImageState? other) => Id == other?.Id;

    public override bool Equals(object? obj) => Equals(obj as ImageState);

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(ImageState? left, ImageState? right) => Equals(left, right);

    public static bool operator !=(ImageState? left, ImageState? right) => !Equals(left, right);
}
