using ChangeMe.Shared.Application.Queries;
using Xunit;

namespace ChangeMe.Shared.Application.UnitTests.Queries;

public class PagedQueryHelperTests
{
    [Theory]
    [InlineData(1, 5, 0, 5)]
    [InlineData(3, 10, 20, 10)]
    [InlineData(null, 20, 0, 20)]
    [InlineData(5, null, 0, int.MaxValue)]
    [InlineData(null, null, 0, int.MaxValue)]
    public void PagedQueryHelper_GetPageData_Test(int? page, int? size, int offset, int next)
    {
        IPagedQuery query = new TestQuery(page, size);
        var pageData = PagedQueryHelper.GetPageData(query);

        Assert.Equal(pageData, new PageData(offset, next));
    }

    private class TestQuery : IPagedQuery
    {
        public TestQuery(int? page, int? size)
        {
            Page = page;
            Size = size;
        }

        public int? Page { get; }

        public int? Size { get; }
    }
}