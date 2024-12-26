namespace AdventureWorks.Identity.Application.DomainEvents.User;

/// <summary>
/// Represents the event of a user's access failed count being changed.
/// </summary>
/// <remarks>
/// This class provides information about the old and new access failed counts.
/// </remarks>
/// <param name="oldCount">The original access failed count.</param>
/// <param name="newCount">The new access failed count.</param>
public class AccessFailedCountChanged(int oldCount, int newCount)
{
    /// <summary>
    /// Gets or sets the original access failed count.
    /// </summary>
    public int OldCount { get; set; } = oldCount;

    /// <summary>
    /// Gets or sets the new access failed count.
    /// </summary>
    public int NewCount { get; set; } = newCount;
}