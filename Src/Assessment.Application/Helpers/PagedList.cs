using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Application.Helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }

        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => (CurrentPage > 1);
        public bool HasNext => (CurrentPage < TotalPages);
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountPrevious { get; set; }
        public List<T> PagedItems { get; set; } = new();
        public PagedList()
        {

        }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize, decimal totalAmount, decimal totalAmountPrevious)
        {
            TotalAmount = totalAmount;
            TotalAmountPrevious = totalAmountPrevious;
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
            PagedItems.AddRange(items);
        }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            TotalCount = count;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize, decimal totalAmount, decimal totalAmountPrevious)
        {
            var count = source.Count();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize, totalAmount, totalAmountPrevious);
        }

        public static PagedList<T> Create(List<T> source, int pageNumber, int pageSize, decimal totalAmount, decimal totalAmountPrevious)
        {
            var count = source.Count;
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize, totalAmount, totalAmountPrevious);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = await source.ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize, string sort)
        {
            var count = source.Count();
            if (pageSize == 0)
            {
                pageSize = count;
            }

            if (!string.IsNullOrEmpty(sort))
            {
                var sortedData = SortHelper<T>.OrderByDynamic(source.AsQueryable(), sort);

                var items = await Task.Run(() => sortedData.Skip(((pageNumber - 1) * pageSize)).Take(pageSize).ToList());
                return new PagedList<T>(items, count, pageNumber, pageSize);
            }
            else
            {
                var items = await Task.Run(() => source.Skip(((pageNumber - 1) * pageSize)).Take(pageSize).ToList());
                return new PagedList<T>(items, count, pageNumber, pageSize);
            }
        }
    }
}
