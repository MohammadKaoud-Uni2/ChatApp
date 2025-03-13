using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ChatApp.Api.Data.Helper
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }

        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        
        public PagedList(IEnumerable<T> items, int Count, int PageSize, int PageNumber)
        {
            CurrentPage = PageNumber;
            TotalPages = (int)(Count / (double)PageSize);
            this.PageSize = PageSize;
            AddRange(items);
            TotalCount = Count;
        }
        public static PagedList<T> ToPagedList(IQueryable<T> source, int PageNumber, int PageSize)
        {
            int count=  source.Count ();
            IEnumerable<T>items=  source.Skip((PageNumber-1)*PageSize).Take(PageSize).ToList();
            return new PagedList<T>(items, count, PageSize, PageNumber);

        }
    }
}
