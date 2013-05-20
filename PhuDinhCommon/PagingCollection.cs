using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhuDinhCommon
{
    public class PagingCollection<T>
    {
        private List<T> _list;

        private int _pageCount;
        public int PageCount
        {
            get { return _pageCount; }
            private set { _pageCount = value; }
        }

        private int _currentPage;
        public int CurrentPage
        {
            get { return this._currentPage; }
            set
            {
                if (value <= 0)
                {
                    this._currentPage = 1;
                }
                else if (value > this._pageCount)
                {
                    this._currentPage = this._pageCount;
                }
                else
                {
                    this._currentPage = value;
                }

                this.UpdateCurrentPageItems();
            }
        }

        private int _itemsPerPage;
        public int ItemsPerPage
        {
            get { return _itemsPerPage; }
            set
            {
                if (value <= 0)
                {
                    this._itemsPerPage = 1;
                }
                else if (value > this._list.Count)
                {
                    this._itemsPerPage = this._list.Count;
                }
                else
                {
                    this._itemsPerPage = value;
                }

                this.UpdatePageCount();
                this.UpdateCurrentPageItems();
            }
        }

        private List<T> _currentPageItems = new List<T>();
        public List<T> CurrentPageItems
        {
            get { return _currentPageItems; }
        }

        private int EndIndex
        {
            get
            {
                if (this._itemsPerPage <= 0)
                    return this._list.Count;

                var end = this._currentPage * this._itemsPerPage;
                return (end > this._list.Count) ? this._list.Count : end;
            }
        }

        private int StartIndex
        {
            get
            {
                if (this._itemsPerPage <= 0)
                    return 0;

                return (this._currentPage - 1) * this._itemsPerPage;
            }
        }

        public PagingCollection(List<T> list)
        {
            this._list = list;
            this._currentPage = 1;
            this.ItemsPerPage = this._list.Count;
        }

        public List<T> MoveToNextPage()
        {
            if (this._currentPage == this._pageCount)
                return this.CurrentPageItems;

            this._currentPage++;
            this.UpdateCurrentPageItems();

            return this.CurrentPageItems;
        }

        public List<T> MoveToPreviousPage()
        {
            if (this._currentPage == 1)
                return this.CurrentPageItems;

            this._currentPage--;
            this.UpdateCurrentPageItems();

            return this.CurrentPageItems;
        }

        public List<T> MoveToLastPage()
        {
            this._currentPage = this._pageCount;
            this.UpdateCurrentPageItems();

            return this.CurrentPageItems;
        }

        public List<T> MoveToFirstPage()
        {
            this._currentPage = 1;
            this.UpdateCurrentPageItems();

            return this.CurrentPageItems;
        }

        private void UpdateCurrentPageItems()
        {
            this._currentPageItems.Clear();
            int startIndex = this.StartIndex;
            int endIndex = this.EndIndex;
            for (int i = startIndex; i < endIndex; i++)
            {
                this._currentPageItems.Add(this._list[i]);
            }
        }

        private void UpdatePageCount()
        {
            this._pageCount = (this._list.Count + this._itemsPerPage - 1) / this._itemsPerPage;
        }
    }
}
