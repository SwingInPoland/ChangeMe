using ChangeMe.Shared.Extensions;

namespace ChangeMe.Shared.Application.Queries;

public static class PagedQueryHelper
{
    public const string Offset = "Offset";

    public const string Next = "Next";

    public static PageData GetPageData(IPagedQuery query)
    {
        // ignore warnings about nullable types as HasNoValue() extension method is used
        var offset = query.Page.HasNoValue() || query.Size.HasNoValue() ? 0 : (query.Page.Value - 1) * query.Size.Value;

        var next = query.Size ?? int.MaxValue;

        return new PageData(offset, next);
    }

    public static string AppendPageStatement(string sql) =>
        $"{sql} " +
        $"OFFSET @{Offset} ROWS FETCH NEXT @{Next} ROWS ONLY; ";
}